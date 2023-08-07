using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Transaction.Commands
{
    public class AddTransactionCommandResponse
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }

    }
}
