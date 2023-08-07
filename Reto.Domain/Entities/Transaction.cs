using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
