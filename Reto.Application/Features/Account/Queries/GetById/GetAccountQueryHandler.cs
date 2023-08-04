using MediatR;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Queries.GetById
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GenericResponse<GetAccountQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Account> _accountRepository;

        public GetAccountQueryHandler(IGenericRepository<Domain.Entities.Account> accountRepository)
        {
            _accountRepository = accountRepository;   
        }

        public async Task<GenericResponse<GetAccountQueryResponse>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Account account = await _accountRepository
                .FirstOrDefaultWithInclude(x => x.AccountId == request.AccountId && x.Status == true, x => x.Client, x => x.Client.Person);

            if (account is null)
                throw new BusinessException("Cuenta no encontrada");

            GetAccountQueryResponse getAccountQueryResponse = new()
            {
                AccountId = account.AccountId,
                Status = account.Status,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                ClientId = account.ClientId,
                ClientName = account.Client.Person.Name,
                InitialBalance = account.InitialBalance
            };

            return new GenericResponse<GetAccountQueryResponse>
            {
                Data = getAccountQueryResponse,
                Message = "Información obtenida correctamente",
                Status = "Success"
            };
        }
    }
}
