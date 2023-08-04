using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Commands
{
    public class AddUpdateClientCommand : IRequest<GenericResponse<ClientCommandQueryResponse>>
    {
        public int? ClientId { get; set; }
        public int? PersonId { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }
    }
}
