using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidDateOfBirthException : Exception
    {
        public InvalidDateOfBirthException() : base("The specified date of birth is invalid.")
        { }

        public InvalidDateOfBirthException(string message) : base(message)
        { }

        public InvalidDateOfBirthException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
