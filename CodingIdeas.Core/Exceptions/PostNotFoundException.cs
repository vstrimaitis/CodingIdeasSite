using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException() : base("The specified post does not exist.")
        { }

        public PostNotFoundException(string message) : base(message)
        { }

        public PostNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
