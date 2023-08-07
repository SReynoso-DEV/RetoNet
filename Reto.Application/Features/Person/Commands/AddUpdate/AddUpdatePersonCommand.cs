using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Person.Commands.AddUpdate
{
    public class AddUpdatePersonCommand : IRequest<GenericResponse<PersonCommandQueryResponse>>
    {
        public int? PersonId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Identification { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
