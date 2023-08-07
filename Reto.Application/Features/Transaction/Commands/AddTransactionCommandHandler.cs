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
using System.Xml;

namespace Reto.Application.Features.Transaction.Commands
{
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, GenericResponse<AddTransactionCommandResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Transaction> _transactionRepository;
        private readonly IGenericRepository<Domain.Entities.Account> _accountRepository;
        public AddTransactionCommandHandler(IGenericRepository<Domain.Entities.Transaction> transactionRepository, 
            IGenericRepository<Domain.Entities.Account> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository; 
        }

        public async Task<GenericResponse<AddTransactionCommandResponse>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Account account = await _accountRepository
                .FirstOrDefaultWithInclude(x => x.AccountId == request.AccountId && x.Status == true, x => x.Transactions) ??
                throw new BusinessException("Cuenta inexistente");

            Domain.Entities.Transaction transaction = new()
            {
                Account = account,
                CreatedDate = request.CreatedDate ?? throw new BusinessException("La fecha de creación es obligatoria"),
                AccountId = account.AccountId
            };
            decimal initialBalance = account.InitialBalance;
            var accountTransactions = account.Transactions;

            decimal currentBalance = !accountTransactions.Any() ? initialBalance : accountTransactions.Sum(x => x.Value) + initialBalance;

            // CREDITO ES POSITIVO
            // DEBITO ES NEGATIVO

            if (request.TransactionType.Trim().ToUpper() == Constants.TransactionType.CREDITO.ToUpper()) 
            {
                if (request.Value <= 0)
                    throw new BusinessException("El valor debe ser mayor a 0.");

                transaction.Balance = currentBalance + request.Value;
                transaction.TransactionType = Constants.TransactionType.CREDITO;
                transaction.Value = request.Value;            
            }
            else if (request.TransactionType.Trim().ToUpper() == Constants.TransactionType.DEBITO.ToUpper())
            {
                if (request.Value <= 0)
                    throw new BusinessException("El valor debe ser mayor a 0.");

                if (currentBalance -  request.Value < 0)
                    throw new BusinessException("Saldo no disponible.");

                transaction.Balance = currentBalance - request.Value;
                transaction.TransactionType = Constants.TransactionType.DEBITO;
                transaction.Value = -request.Value;
            }
            else
            {
                throw new BusinessException($"Tipo de transaccion invalido. Los valores permitidos son: {Constants.TransactionType.DEBITO} o {Constants.TransactionType.CREDITO}");
            }

            await _transactionRepository.Add(transaction);

            return new GenericResponse<AddTransactionCommandResponse>
            {
                Data = new AddTransactionCommandResponse
                {
                    AccountId = transaction.AccountId,
                    TransactionType = transaction.TransactionType,
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType,
                    Balance = transaction.Balance,
                    CreatedDate = transaction.CreatedDate,
                    InitialBalance = account.InitialBalance,
                    Value = transaction.Value,
                },
                Message = "Operación realizada con exito",
                Status = "Success"
            };
        }
    }
}
