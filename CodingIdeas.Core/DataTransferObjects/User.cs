using System;
using System.Collections.Generic;

namespace CodingIdeas.Core
{
    [Flags]
    public enum UserProperties
    {
        None = 0,
        Email = 1,
        Username = 2,
        FirstName = 4,
        LastName = 8,
        DateOfBirth = 16,
        PasswordHash = 32,
        All = 64
    }

    public struct User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public IList<UserSkill> Skills { get; set; }

        public User(string email, string username, string firstName, string lastName, DateTime? dob, string passwordHash, IList<UserSkill> skills)
        {
            Id = Guid.NewGuid();
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            PasswordHash = passwordHash;
            Skills = skills;
        }
    }
}
