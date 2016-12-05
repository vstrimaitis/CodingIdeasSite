using System.Linq;
using System;
using CodingIdeas.Core.Exceptions;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void UpdateComment(Guid oldCommentId, string newContent)
        {
            ValidateComment(new Comment() { Content = newContent }, CommentProperties.Content);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var c = ctx.Comments.Where(x => x.Id == oldCommentId).FirstOrDefault();
                if (c == null)
                    throw new CommentNotFoundException();
                c.Content = newContent;
                ctx.SaveChanges();
            }
        }

        public void UpdatePost(Guid oldPostId, Post @new, PostProperties propertiesToChange)
        {
            ValidatePost(@new, propertiesToChange);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var p = ctx.Posts.Where(x => x.Id == oldPostId).FirstOrDefault();
                if (p == null)
                    throw new PostNotFoundException();
                if(propertiesToChange.HasFlag(PostProperties.Content))
                    p.Content = @new.Content;
                if (propertiesToChange.HasFlag(PostProperties.Title))
                    p.Title = @new.Title;
                ctx.SaveChanges();
            }
        }

        public void UpdateProgrammingLanguage(Guid oldLanguageId, string newName)
        {
            ValidateProgrammingLanguage(new ProgrammingLanguage() { Name = newName }, ProgrammingLanguageProperties.All);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var l = ctx.ProgrammingLanguages.Where(x => x.Id == oldLanguageId).FirstOrDefault();
                if (l == null)
                    throw new ProgrammingLanguageNotFoundException();
                l.Name = newName;
                ctx.SaveChanges();
            }
        }

        public void UpdateRating(Guid userId, Guid entityId, sbyte newRating)
        {
            ValidateRating(userId, entityId, newRating);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var r = ctx.RatedEntities.Where(x => x.UserId == userId && x.EntityId == entityId).FirstOrDefault();
                if (r == null)
                    throw new RatingNotFoundException();
                r.Rating = newRating;
                ctx.SaveChanges();
            }
        }

        public void UpdateUser(Guid oldUserId, User @new, UserProperties propertiesToChange)
        {
            ValidateUser(@new, propertiesToChange);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Username == @new.Username && x.Password == @new.Password).Count() != 0)
                    throw new InvalidCredentialsException();
                var r = ctx.Users.Where(x => x.Id == oldUserId).FirstOrDefault();
                if (r == null)
                    throw new UserNotFoundException();
                if(propertiesToChange.HasFlag(UserProperties.DateOfBirth))
                    r.DOB = @new.DateOfBirth;
                if (propertiesToChange.HasFlag(UserProperties.Email))
                    r.Email = @new.Email;
                if (propertiesToChange.HasFlag(UserProperties.FirstName))
                    r.FirstName = @new.FirstName;
                if (propertiesToChange.HasFlag(UserProperties.LastName))
                    r.LastName = @new.LastName;
                if (propertiesToChange.HasFlag(UserProperties.Password))
                    r.Password = @new.Password;
                if (propertiesToChange.HasFlag(UserProperties.Username))
                    r.Username = @new.Username;
                ctx.SaveChanges();
            }
        }
        
        public void UpdateSkill(Guid userId, Guid languageId, byte proficiency)
        {
            ValidateSkill(userId, languageId, proficiency);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var s = ctx.UserSkills.Where(x => x.UserId == userId && x.ProgrammingLanguageId == languageId).FirstOrDefault();
                if (s == null)
                    throw new UserSkillNotFoundException();
                s.Proficiency = proficiency;
                ctx.SaveChanges();
            }
        }
    }
}
