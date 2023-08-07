using MediatR;
using Reto.Application.Models;
using Reto.Application.Password;
using Reto.Domain.Entities;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using Entities = Reto.Domain.Entities;

namespace Reto.Application.Features.Client.Commands
{
    public class AddUpdateClientCommandHandler : IRequestHandler<AddUpdateClientCommand, GenericResponse<ClientCommandQueryResponse>>
    {
        private readonly IGenericRepository<Entities.Client> _clientRepository;
        private readonly IGenericRepository<Entities.Person> _personRepository;

        public AddUpdateClientCommandHandler(IGenericRepository<Entities.Client> clientRepository, IGenericRepository<Entities.Person> personRepository)
        {
            _clientRepository = clientRepository;
            _personRepository = personRepository;
        }

        public async Task<GenericResponse<ClientCommandQueryResponse>> Handle(AddUpdateClientCommand request, CancellationToken cancellationToken)
        {            
            Entities.Client client = new();
            Entities.Person person = new();

            if (request.ClientId.HasValue)
            {
                client = await _clientRepository.FirstOrDefaultWithInclude(x => x.ClientId == request.ClientId.Value && x.Status == true, x => x.Person) ?? 
                    throw new BusinessException("Cliente no encontrado.");
                person = client.Person ?? throw new BusinessException("Persona no existente");
            }
            else
            {
                if (request.PersonId.HasValue)
                {
                    person = await _personRepository.FirstOrDefault(x => x.PersonId == request.PersonId.Value) ?? throw new BusinessException("Persona no existente");
                }
            }           

            client.Password = request.Password is null ? client.Password : PasswordHasher.HashPassword(request.Password);
            client.PersonId = person.PersonId;
            client.Person = person;
            client.Status = request.Status ?? client.Status;

            if (request.ClientId.HasValue)
                await _clientRepository.Update(client);
            else
            {
                client.Status = true;
                await _clientRepository.Add(client);
            }

            return new GenericResponse<ClientCommandQueryResponse>
            {
                Data = new ClientCommandQueryResponse
                {
                    ClientId = client.ClientId,
                    Address = person.Address,
                    Age = person.Age,
                    Identification = person.Identification,
                    PhoneNumber = person.PhoneNumber,
                    Name = person.Name
                },
                Status = "Success",
                Message = "Información guardada satisfactoriamente"
            };
            
        }
    }
}
