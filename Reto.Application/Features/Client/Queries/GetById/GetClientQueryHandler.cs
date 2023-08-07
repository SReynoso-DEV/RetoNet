using MediatR;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Queries.GetById
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, GenericResponse<GetClientQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Client> _clientRepository;

        public GetClientQueryHandler(IGenericRepository<Domain.Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<GetClientQueryResponse>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {           
            Domain.Entities.Client client = await _clientRepository
                .FirstOrDefaultWithInclude(x => x.ClientId == request.Id && x.Status == true, x => x.Person);

            if (client is null)
                throw new BusinessException("Cliente no encontrado");

            GetClientQueryResponse getClientQueryResponse = new()
            {
                Address = client.Person.Address,
                ClientId = client.ClientId,
                Age = client.Person.Age,
                Identification = client.Person.Identification,
                Name = client.Person.Name,
                PhoneNumber = client.Person.PhoneNumber
            };

            return new GenericResponse<GetClientQueryResponse>
            {
                Data = getClientQueryResponse,
                Message = "Información obtenida correctamente",
                Status = "Success"
            };
        }
    }
}
