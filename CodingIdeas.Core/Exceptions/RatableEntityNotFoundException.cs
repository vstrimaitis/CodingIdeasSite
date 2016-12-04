using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core.Exceptions
{
    public class RatableEntityNotFoundException : Exception
    {
        public RatableEntityNotFoundException() : base("The specified entity does not exist.")
        { }

        public RatableEntityNotFoundException(string message) : base(message)
        { }

        public RatableEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
