using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidPageNumberException : Exception
    {
        public InvalidPageNumberException() : base("The page number is invalid.")
        { }

        public InvalidPageNumberException(string message) : base(message)
        { }

        public InvalidPageNumberException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
