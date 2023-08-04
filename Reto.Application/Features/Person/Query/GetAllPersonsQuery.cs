using MediatR;
using Reto.Application.Features.Person.Commands.AddUpdate;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Person.Query
{
    public class GetAllPersonsQuery : IRequest<GenericResponse<List<PersonCommandQueryResponse>>>
    {
    }
}
