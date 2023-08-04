using MediatR;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Models;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Queries.GetAll
{
    public class GetAllClientsCommandHandler : IRequestHandler<GetAllClientsQuery, GenericResponse<List<GetClientQueryResponse>>>
    {
        private readonly IGenericRepository<Domain.Entities.Client> _clientRepository;

        public GetAllClientsCommandHandler(IGenericRepository<Domain.Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<List<GetClientQueryResponse>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.WhereWithInclude(x => x.Status == true, x => x.Person);
            return new GenericResponse<List<GetClientQueryResponse>>
            {
                Data = clients.Select(x => new GetClientQueryResponse
                {
                    ClientId = x.ClientId,
                    Address = x.Person.Address,
                    Age = x.Person.Age,
                    Identification = x.Person.Identification,
                    Name = x.Person.Name,
                    PhoneNumber = x.Person.PhoneNumber
                }).ToList(),
                Message = "Información obtenida correctamente",
                Status = "Success"
            };
        }
    }
}
