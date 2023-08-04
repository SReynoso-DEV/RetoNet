using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Domain.Exceptions
{
    public class SystemException : Exception
    {
        public SystemException(string message) : base(message)
        {
        }
    }
}
