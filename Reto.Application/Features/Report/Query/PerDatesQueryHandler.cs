using MediatR;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using Reto.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Report.Query
{
    public class PerDatesQueryHandler : IRequestHandler<PerDatesQuery, GenericResponse<List<PerDatesQueryResponse>>>
    {

        private readonly IGenericRepository<Domain.Entities.Account> _accountRepository;
        private readonly IGenericRepository<Domain.Entities.Client> _clientRepository;

        public PerDatesQueryHandler(IGenericRepository<Domain.Entities.Client> clientRepository,
            IGenericRepository<Domain.Entities.Account> accountRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<List<PerDatesQueryResponse>>> Handle(PerDatesQuery request, CancellationToken cancellationToken)
        {
            if (request.StartDate is null || request.EndDate is null)
                throw new BusinessException("Las fechas de inicio y fin son obligatorias.");

            var client = await _clientRepository.FirstOrDefault(x => x.ClientId == request.ClientId && x.Status == true) ??
                throw new BusinessException("Cliente inexistente");

            var account = await _accountRepository.WhereWithInclude(x => x.ClientId == request.ClientId && x.Status == true, 
                x => x.Client,
                x => x.Client.Person,
                x => x.Transactions);

            account = account.Where(x => x.Client.Status == true);

            if (!account.Any())
                throw new BusinessException("El cliente no posee cuentas");

            var data = account.Select(x => new PerDatesQueryResponse
            {
                AccountId = x.AccountId,
                AccountNumber = x.AccountNumber,
                AccountType = x.AccountType,
                ClientId = x.ClientId,
                ClientName = x.Client.Person.Name,
                CurrentBalance = x.InitialBalance + x.Transactions
                .Where(x => x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate.Value.AddDays(1).AddSeconds(-1))
                .Sum(x => x.Value),
                InitialBalance = x.InitialBalance,
                Status = x.Status,

                TotalCredits = x.Transactions.Where(x => x.TransactionType == Constants.TransactionType.CREDITO &&
                x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate.Value.AddDays(1).AddSeconds(-1))
                .Sum(x => x.Value),

                TotalDebits = x.Transactions.Where(x => x.TransactionType == Constants.TransactionType.DEBITO &&
                x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate.Value.AddDays(1).AddSeconds(-1))
                .Sum(x => x.Value)
            }).ToList();

            return new GenericResponse<List<PerDatesQueryResponse>>
            {
                Data = data,
                Message = "Reporte generado correctamente",
                Status = "Success"
            };
        }
    }
}
