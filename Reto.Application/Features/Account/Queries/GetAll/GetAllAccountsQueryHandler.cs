using MediatR;
using Reto.Application.Features.Account.Queries.GetAll;
using Reto.Application.Features.Account.Queries.GetById;
using Reto.Application.Models;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Queries.GetAll
{
    public class GetAllAccountsCommandHandler : IRequestHandler<GetAllAccountsQuery, GenericResponse<List<GetAccountQueryResponse>>>
    {
        private readonly IGenericRepository<Domain.Entities.Account> _clientRepository;

        public GetAllAccountsCommandHandler(IGenericRepository<Domain.Entities.Account> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<List<GetAccountQueryResponse>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _clientRepository.WhereWithInclude(x => x.Status == true, x => x.Client, x => x.Client.Person);
            return new GenericResponse<List<GetAccountQueryResponse>>
            {
                Data = accounts.Select(x => new GetAccountQueryResponse
                {
                   AccountId = x.AccountId,
                   Status = x.Status,
                   AccountNumber = x.AccountNumber,
                   AccountType = x.AccountType,
                   ClientId = x.ClientId,
                   ClientName = x.Client.Person.Name,
                   InitialBalance = x.InitialBalance

                }).ToList(),
                Message = "Información obtenida correctamente",
                Status = "Success"
            };
        }
    }
}
