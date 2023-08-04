using MediatR;
using Reto.Application.Features.Client.Queries.GetById;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Queries.GetAll
{
    public class GetAllClientsQuery : IRequest<GenericResponse<List<GetClientQueryResponse>>>
    {
    }
}
