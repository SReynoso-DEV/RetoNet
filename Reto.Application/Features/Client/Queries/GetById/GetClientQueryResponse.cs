using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Application.Features.Client.Queries.GetById
{
    public class GetClientQueryResponse
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
