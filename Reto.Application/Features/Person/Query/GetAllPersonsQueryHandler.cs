using MediatR;
using Reto.Application.Features.Client.Queries.GetAll;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Features.Person.Commands.AddUpdate;
using Reto.Application.Models;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Person.Query
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, GenericResponse<List<PersonCommandQueryResponse>>>
    {
        private readonly IGenericRepository<Domain.Entities.Person> _personRepository;

        public GetAllPersonsQueryHandler(IGenericRepository<Domain.Entities.Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<GenericResponse<List<PersonCommandQueryResponse>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetAll();

            return new GenericResponse<List<PersonCommandQueryResponse>>
            {
                Data = person.Select(x => new PersonCommandQueryResponse
                {
                    Address = x.Address,
                    Age = x.Age,
                    Gender = x.Gender,
                    Identification = x.Identification,
                    Name = x.Name,
                    PersonId = x.PersonId,
                    PhoneNumber = x.PhoneNumber,
                }).ToList(),
                Message = "Información obtenida correctamente",
                Status = "Success"
            };
        }
    }
}
