using MediatR;
using Reto.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Transaction.Commands
{
    public class AddTransactionCommand : IRequest<GenericResponse<AddTransactionCommandResponse>>
    {
        public DateTime? CreatedDate { get; set; }

        public string TransactionType { get; set; }

        public decimal Value { get; set; }

        public decimal Balance { get; set; }

        public int AccountId { get; set; }
    }
}

