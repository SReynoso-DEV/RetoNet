using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Queries.GetById
{
    public class GetAccountQuery : IRequest<GenericResponse<GetAccountQueryResponse>>
    {
        public int AccountId { get; set; }
    }
}
