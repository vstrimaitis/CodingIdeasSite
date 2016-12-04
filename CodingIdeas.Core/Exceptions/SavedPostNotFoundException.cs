using System;

namespace CodingIdeas.Core.Exceptions
{
    public class SavedPostNotFoundException : Exception
    {
        public SavedPostNotFoundException() : base("The post saved by the specified user does not exist.")
        { }

        public SavedPostNotFoundException(string message) : base(message)
        { }

        public SavedPostNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
