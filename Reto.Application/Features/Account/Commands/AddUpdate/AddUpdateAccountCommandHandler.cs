using MediatR;
using Reto.Application.Features.Account.Commands;
using Reto.Application.Models;
using Reto.Application.Password;
using Reto.Domain.Entities;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using Reto.Resource;
using System;
using Entities = Reto.Domain.Entities;

namespace Reto.Application.Features.Client.Commands
{
    public class AddUpdateAccountCommandHandler : IRequestHandler<AddUpdateAccountCommand, GenericResponse<AddUpdateAccountCommandResponse>>
    {
        private readonly IGenericRepository<Entities.Account> _accountRepository;
        private readonly IGenericRepository<Entities.Client> _clientRepository;

        public AddUpdateAccountCommandHandler(IGenericRepository<Entities.Account> accountRepository,
            IGenericRepository<Entities.Client> clientRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GenericResponse<AddUpdateAccountCommandResponse>> Handle(AddUpdateAccountCommand request, CancellationToken cancellationToken)
        {
            Entities.Account account = new();
            Entities.Client client = new();

            if (!string.IsNullOrEmpty(request.AccountType))
            {
                if (request.AccountType.Trim().ToUpper() == Constants.AccountType.AHORRO.ToUpper())
                {
                    request.AccountType = Constants.AccountType.AHORRO;
                }
                else if (request.AccountType.Trim().ToUpper() == Constants.AccountType.CORRIENTE.ToUpper())
                {
                    request.AccountType = Constants.AccountType.CORRIENTE;
                }
                else
                {
                    throw new BusinessException($"Tipo de cuenta invalido. Los valores permitidos son: {Constants.AccountType.AHORRO} o {Constants.AccountType.CORRIENTE}");
                }
            }
            

            if (request.AccountId.HasValue)
            {
                if (request.ClientId.HasValue)
                {
                    throw new BusinessException("No se puede editar la relacion entre el cliente y la cuenta existente.");
                }

                account = await _accountRepository
                    .FirstOrDefaultWithInclude(x => x.AccountId == request.AccountId.Value && x.Status == true, x => x.Client, x => x.Client.Person) ?? 
                    throw new BusinessException("Cuenta no encontrada.");

                client = account.Client;
            }
            else
            {
                if (!request.ClientId.HasValue)
                    throw new BusinessException("Se debe ingresar un cliente");

                client = await _clientRepository.FirstOrDefaultWithInclude(x => x.ClientId == request.ClientId.Value && x.Status == true, x => x.Person) ??
                throw new BusinessException("Cliente inexistente");

                account.ClientId = client.ClientId;
                account.Status = request.Status ?? account.Status;
                account.Client = client;
            }

            
            account.AccountNumber = request.AccountNumber ?? account.AccountNumber;
            account.AccountType = request.AccountType ?? account.AccountType;
            account.InitialBalance = request.InitialBalance ?? account.InitialBalance;

            if (request.AccountId.HasValue)
                await _accountRepository.Update(account);
            else
            {
                account.Status = true;
                await _accountRepository.Add(account);
            }

            return new GenericResponse<AddUpdateAccountCommandResponse>
            {
                Data = new AddUpdateAccountCommandResponse
                {
                    ClientId = client.ClientId,
                    Status = account.Status,
                    AccountId = account.AccountId,
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType,
                    InitialBalance = account.InitialBalance,
                    ClientName = client.Person.Name
                },
                Status = "Success",
                Message = "Información guardada satisfactoriamente"
            };
        }
    }
}
