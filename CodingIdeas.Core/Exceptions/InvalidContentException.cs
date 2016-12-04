using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidContentException : Exception
    {
        public InvalidContentException() : base("The content cannot be null or empty.")
        { }

        public InvalidContentException(string message) : base(message)
        { }

        public InvalidContentException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
