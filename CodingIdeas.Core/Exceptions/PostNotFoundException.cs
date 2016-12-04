using System;

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
