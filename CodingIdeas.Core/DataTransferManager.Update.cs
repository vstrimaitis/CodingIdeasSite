using System.Linq;
using System;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void UpdateComment(Guid oldCommentId, string newContent)
        {
            ValidateComment(new Comment() { Content = newContent }, CommentProperties.Content);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var c = ctx.Comments.Where(x => x.Id == oldCommentId).First();
                c.Content = newContent;
                ctx.SaveChanges();
            }
        }

        public void UpdatePost(Guid oldPostId, Post @new, PostProperties propertiesToChange)
        {
            ValidatePost(@new, propertiesToChange);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var p = ctx.Posts.Where(x => x.Id == oldPostId).First();
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
                var l = ctx.ProgrammingLanguages.Where(x => x.Id == oldLanguageId).First();
                l.Name = newName;
                ctx.SaveChanges();
            }
        }

        public void UpdateRating(Guid userId, Guid entityId, sbyte newRating)
        {
            ValidateRating(userId, entityId, newRating);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var r = ctx.RatedEntities.Where(x => x.UserId == userId && x.EntityId == entityId).First();
                r.Rating = newRating;
                ctx.SaveChanges();
            }
        }

        public void UpdateUser(Guid oldUserId, User @new, UserProperties propertiesToChange)
        {
            ValidateUser(@new, propertiesToChange);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var r = ctx.Users.Where(x => x.Id == oldUserId).First();
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
                var s = ctx.UserSkills.Where(x => x.UserId == userId && x.ProgrammingLanguageId == languageId).First();
                s.Proficiency = proficiency;
                ctx.SaveChanges();
            }
        }
    }
}
