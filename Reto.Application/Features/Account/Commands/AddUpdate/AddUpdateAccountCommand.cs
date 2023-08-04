using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Commands
{
    public class AddUpdateAccountCommand : IRequest<GenericResponse<AddUpdateAccountCommandResponse>>
    {
        public int? AccountId { get; set; }
        public bool? Status { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public decimal? InitialBalance { get; set; }
        public int? ClientId { get; set; }
    }
}
