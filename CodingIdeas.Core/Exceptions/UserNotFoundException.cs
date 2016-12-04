using System;

namespace CodingIdeas.Core.Exceptions
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("The specified user does not exist.")
        { }

        public UserNotFoundException(string message) : base(message)
        { }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
