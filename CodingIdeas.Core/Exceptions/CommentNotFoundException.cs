using System;

namespace CodingIdeas.Core.Exceptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException() : base("The specified comment does not exist.")
        { }

        public CommentNotFoundException(string message) : base(message)
        { }

        public CommentNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
