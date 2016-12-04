using System;

namespace CodingIdeas.Core.Exceptions
{
    public class RatingNotFoundException : Exception
    {
        public RatingNotFoundException() : base("The rating on the specified entity by the specified user does not exist.")
        { }

        public RatingNotFoundException(string message) : base(message)
        { }

        public RatingNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
