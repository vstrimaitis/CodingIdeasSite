using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("The specified credentials cannot be used.")
        { }

        public InvalidCredentialsException(string message) : base(message)
        { }

        public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
