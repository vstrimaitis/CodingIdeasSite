using System;

namespace CodingIdeas.Core
{
    [Flags]
    public enum ProgrammingLanguageProperties
    {
        None = 0,
        Id = 1,
        Name = 2,
        All = 3
    }

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
