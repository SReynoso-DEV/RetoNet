using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
