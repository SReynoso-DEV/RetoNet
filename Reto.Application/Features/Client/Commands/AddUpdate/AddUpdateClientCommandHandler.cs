using MediatR;
using Reto.Application.Models;
using Reto.Application.Password;
using Reto.Domain.Entities;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using Entities = Reto.Domain.Entities;

namespace Reto.Application.Features.Client.Commands
{
    public class AddUpdateClientCommandHandler : IRequestHandler<AddUpdateClientCommand, GenericResponse<AddUpdateClientCommandResponse>>
    {
        private readonly IGenericRepository<Entities.Client> _clientRepository;

        public AddUpdateClientCommandHandler(IGenericRepository<Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<AddUpdateClientCommandResponse>> Handle(AddUpdateClientCommand request, CancellationToken cancellationToken)
        {            
            Entities.Client client = new();
            Entities.Person person = new();

            if (request.ClientId.HasValue)
            {
                client = await _clientRepository.FirstOrDefaultWithInclude(x => x.ClientId == request.ClientId.Value && x.Status == true, x => x.Person) ?? throw new BusinessException("Cliente no encontrado.");
                person = client.Person ?? throw new BusinessException("Persona no existente");
            }

            person.Identification = request.Identification ?? person.Identification;
            person.PhoneNumber = request.PhoneNumber ?? person.PhoneNumber;
            person.Address = request.Address ?? person.Address;
            person.Age = request.Age ?? person.Age;
            person.Gender = request.Gender ?? person.Gender;
            person.Name = request.Name ?? person.Name;

            client.Status = request.Status ?? client.Status;
            client.Password = request.Password is null ? client.Password : PasswordHasher.HashPassword(request.Password);
            
            client.Person = person;

            if (request.ClientId.HasValue)
                await _clientRepository.Update(client);
            else
            {
                client.Status = true;
                await _clientRepository.Add(client);
            }

            return new GenericResponse<AddUpdateClientCommandResponse>
            {
                Data = new AddUpdateClientCommandResponse
                {
                    ClientId = client.ClientId,
                    Address = person.Address,
                    Age = person.Age,
                    Identification = person.Identification,
                    PhoneNumber = person.PhoneNumber,
                    Name = person.Name
                },
                Status = "success",
                Message = "Información guardada satisfactoriamente"
            };
            
        }
    }
}
