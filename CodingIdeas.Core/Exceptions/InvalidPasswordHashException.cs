using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidPasswordHashException : Exception
    {
        public InvalidPasswordHashException() : base("The specified password hash is invalid.")
        { }

        public InvalidPasswordHashException(string message) : base(message)
        { }

        public InvalidPasswordHashException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
