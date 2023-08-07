using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Account.Queries.GetById
{
    public class GetAccountQueryResponse
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
