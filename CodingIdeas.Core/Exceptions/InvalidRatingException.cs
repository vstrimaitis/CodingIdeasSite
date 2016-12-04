using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidRatingException : Exception
    {
        public InvalidRatingException() : base("The rating value must be either +1 or -1.")
        { }

        public InvalidRatingException(string message) : base(message)
        { }

        public InvalidRatingException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
