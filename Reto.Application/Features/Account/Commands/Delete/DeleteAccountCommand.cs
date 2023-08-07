using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Commands.Delete
{
    public class DeleteAccountCommand : IRequest<GenericResponse<object>>
    {
        public int AccountId { get; set; }
    }
}
