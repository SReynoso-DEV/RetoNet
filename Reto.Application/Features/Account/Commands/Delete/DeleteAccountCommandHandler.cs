using MediatR;
using Reto.Application.Features.Account.Commands.Delete;
using Reto.Application.Models;
using Reto.Domain.Exceptions;
using Reto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Commands.Delete
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, GenericResponse<object>>
    {
        private readonly IGenericRepository<Domain.Entities.Account> _accountRepository;

        public DeleteAccountCommandHandler(IGenericRepository<Domain.Entities.Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<GenericResponse<object>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetById(request.AccountId) ?? throw new BusinessException("Cuenta no encontrada");
            await _accountRepository.Delete(account.AccountId);

            return new GenericResponse<object>
            {
                Message = "Cuenta eliminada exitosamente!",
                Status = "Success"
            };
        }
    }
}
