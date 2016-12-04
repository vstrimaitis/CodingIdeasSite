using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidPageNumberException : Exception
    {
        public InvalidPageNumberException() : base("The specified page number is invalid.")
        { }

        public InvalidPageNumberException(string message) : base(message)
        { }

        public InvalidPageNumberException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
