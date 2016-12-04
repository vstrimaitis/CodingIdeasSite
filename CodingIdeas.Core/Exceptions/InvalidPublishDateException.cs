using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidPublishDateException : Exception
    {
        public InvalidPublishDateException() : base("The publish date is invalid.")
        { }

        public InvalidPublishDateException(string message) : base(message)
        { }

        public InvalidPublishDateException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
