using System;

namespace CodingIdeas.Core
{
    public struct ProgrammingLanguage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ProgrammingLanguage(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
