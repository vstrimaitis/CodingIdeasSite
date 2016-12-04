using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException() : base("The specified name is invalid.")
        { }

        public InvalidNameException(string message) : base(message)
        { }

        public InvalidNameException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
