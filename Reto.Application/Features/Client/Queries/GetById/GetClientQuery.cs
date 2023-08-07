using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Queries.GetById
{
    public class GetClientQuery : IRequest<GenericResponse<GetClientQueryResponse>>
    {
        public int Id { get; set; }
    }
}
