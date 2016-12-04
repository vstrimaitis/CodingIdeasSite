using System;

namespace CodingIdeas.Core.Exceptions
{
    public class InvalidProficiencyException : Exception
    {
        public InvalidProficiencyException() : base("The proficiency must be between 1 and 5, inclusive.")
        { }

        public InvalidProficiencyException(string message) : base(message)
        { }

        public InvalidProficiencyException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
