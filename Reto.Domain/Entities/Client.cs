using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
