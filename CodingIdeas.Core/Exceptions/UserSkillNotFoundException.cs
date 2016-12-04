using System;

namespace CodingIdeas.Core.Exceptions
{
    public class UserSkillNotFoundException : Exception
    {
        public UserSkillNotFoundException() : base("The specified user skill does not exist.")
        { }

        public UserSkillNotFoundException(string message) : base(message)
        { }

        public UserSkillNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
