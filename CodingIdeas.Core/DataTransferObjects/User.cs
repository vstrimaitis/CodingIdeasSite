﻿using System;
using System.Collections.Generic;

namespace CodingIdeas.Core
{
    [Flags]
    public enum UserProperties
    {
        None = 0,
        Id = 1,
        Email = 2,
        Username = 4,
        FirstName = 8,
        LastName = 16,
        DateOfBirth = 32,
        Password = 64,
        Skills = 128,
        All = 255
    }

    public struct User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public IList<UserSkill> Skills { get; set; }

        public User(string email, string username, string firstName, string lastName, DateTime? dob, string passwordHash, IList<UserSkill> skills)
        {
            Id = Guid.NewGuid();
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Password = passwordHash;
            Skills = skills;
        }
    }
}
