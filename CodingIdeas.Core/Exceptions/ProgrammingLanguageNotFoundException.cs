using System;

namespace CodingIdeas.Core.Exceptions
{
    public class ProgrammingLanguageNotFoundException : Exception
    {
        public ProgrammingLanguageNotFoundException() : base("The specified programming language does not exist.")
        { }

        public ProgrammingLanguageNotFoundException(string message) : base(message)
        { }

        public ProgrammingLanguageNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
