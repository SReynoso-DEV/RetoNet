using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Commands.Delete
{
    public class DeleteClientCommand : IRequest<GenericResponse<object>>
    {
        public int ClientId { get; set; }
    }
}
