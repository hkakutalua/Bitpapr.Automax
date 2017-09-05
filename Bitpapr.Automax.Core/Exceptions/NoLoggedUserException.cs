using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Core.Exceptions
{
    public class NoLoggedUserException : ServiceException
    {
        public NoLoggedUserException(string message)
            : base(message)
        {
        }

        public NoLoggedUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
