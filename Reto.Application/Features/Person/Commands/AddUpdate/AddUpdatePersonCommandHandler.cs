using MediatR;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Person.Commands.AddUpdate
{
    public class AddUpdatePersonCommandHandler : IRequestHandler<AddUpdatePersonCommand, GenericResponse<PersonCommandQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Person> _personRepository;

        public AddUpdatePersonCommandHandler(IGenericRepository<Domain.Entities.Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<GenericResponse<PersonCommandQueryResponse>> Handle(AddUpdatePersonCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Person person = new();

            if (request.PersonId.HasValue)
            {
                if (!string.IsNullOrEmpty(request.Identification))
                {
                    throw new BusinessException("No se puede editar la identificación de la persona.");
                }

                person = await _personRepository.FirstOrDefault(x => x.PersonId == request.PersonId.Value) ?? 
                    throw new BusinessException("Persona no existe");
            }

            person.PhoneNumber = request.PhoneNumber ?? person.PhoneNumber;
            person.Address = request.Address ?? person.Address;
            person.Gender = request.Gender ?? person.Gender;
            person.Age = request.Age ?? person.Age;
            person.Name = request.Name ?? person.Name;
            person.Identification = request.Identification ?? person.Identification;

            if (request.PersonId.HasValue)
                await _personRepository.Update(person);
            else
                await _personRepository.Add(person);

            return new GenericResponse<PersonCommandQueryResponse>
            {
                Data = new PersonCommandQueryResponse
                {
                    Address = person.Address,
                    Age = person.Age,
                    Gender = person.Gender,
                    Name = person.Name,
                    Identification = person.Identification,
                    PersonId = person.PersonId,
                    PhoneNumber = person.PhoneNumber
                },
                Message = "Información guardada correctamente",
                Status = "Success"
            };
        }
    }
}
