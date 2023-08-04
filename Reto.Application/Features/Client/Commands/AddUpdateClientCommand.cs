using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Commands
{
    public class AddUpdateClientCommand : IRequest<GenericResponse<AddUpdateClientCommandResponse>>
    {
        public int? ClientId { get; set; }
        public bool? Status { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Identification { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
