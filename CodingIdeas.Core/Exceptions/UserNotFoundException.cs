using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core.Exceptions
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("The user with the specified credentials does not exist.")
        { }

        public UserNotFoundException(string message) : base(message)
        { }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
