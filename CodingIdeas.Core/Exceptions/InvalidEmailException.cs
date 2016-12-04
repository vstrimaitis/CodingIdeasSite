using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("The specified email is invalid.")
        { }

        public InvalidEmailException(string message) : base(message)
        { }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
