using System.Linq;
using System;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void UpdateComment(Comment old, Comment @new)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var c = ctx.Comments.Where(x => x.Id == old.Id).First();
                c.Content = @new.Content;
                ctx.SaveChanges();
            }
        }

        public void UpdatePost(Post old, Post @new)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var p = ctx.Posts.Where(x => x.Id == old.Id).First();
                p.Content = @new.Content;
                p.Title = @new.Title;
                ctx.SaveChanges();
            }
        }

        public void UpdateProgrammingLanguage(ProgrammingLanguage old, ProgrammingLanguage @new)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var l = ctx.ProgrammingLanguages.Where(x => x.Id == old.Id).First();
                l.Name = @new.Name;
                ctx.SaveChanges();
            }
        }

        public void UpdateRating(User user, IRatable entity, sbyte newRating)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var r = ctx.RatedEntities.Where(x => x.UserId == user.Id && x.EntityId == entity.Id).First();
                r.Rating = newRating;
                ctx.SaveChanges();
            }
        }

        public void UpdateUser(User old, User @new, UserProperties propertiesToChange)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var r = ctx.Users.Where(x => x.Id == old.Id).First();
                if(propertiesToChange.HasFlag(UserProperties.DateOfBirth))
                    r.DOB = @new.DateOfBirth;
                if (propertiesToChange.HasFlag(UserProperties.Email))
                    r.Email = @new.Email;
                if (propertiesToChange.HasFlag(UserProperties.FirstName))
                    r.FirstName = @new.FirstName;
                if (propertiesToChange.HasFlag(UserProperties.LastName))
                    r.LastName = @new.LastName;
                if (propertiesToChange.HasFlag(UserProperties.PasswordHash))
                    r.Password = @new.PasswordHash;
                if (propertiesToChange.HasFlag(UserProperties.Username))
                    r.Username = @new.Username;
                ctx.SaveChanges();
            }
        }
        
        public void UpdateSkill(User user, ProgrammingLanguage language, byte proficiency)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var s = ctx.UserSkills.Where(x => x.UserId == user.Id && x.ProgrammingLanguageId == language.Id).First();
                s.Proficiency = proficiency;
                ctx.SaveChanges();
            }
        }
    }
}
