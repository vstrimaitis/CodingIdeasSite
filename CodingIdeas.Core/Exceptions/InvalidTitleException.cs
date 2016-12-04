using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidTitleException : Exception
    {
        public InvalidTitleException() : base("The title cannot be null or empty.")
        { }

        public InvalidTitleException(string message) : base(message)
        { }

        public InvalidTitleException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
