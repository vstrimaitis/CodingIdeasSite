using CodingIdeas.Core.Exceptions;
using System;
using System.Linq;
using System.Data.Entity;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {

        public void RemoveComment(Guid commentId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var entity = ctx.RatableEntities.Where(x => x.Id == commentId).FirstOrDefault();
                if (entity == null)
                    throw new CommentNotFoundException();
                ctx.RatableEntities.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public void RemovePost(Guid postId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var post = ctx.Posts.Include(x => x.Comments).Where(x => x.Id == postId).FirstOrDefault();
                if (post == null)
                    throw new PostNotFoundException();
                foreach (var c in post.Comments.ToList())
                    ctx.Comments.Remove(c);
                ctx.Posts.Remove(post);
                ctx.SaveChanges();
            }
        }

        public void RemoveProgrammingLanguage(Guid languageId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var lang = ctx.ProgrammingLanguages.Where(x => x.Id == languageId).FirstOrDefault();
                if (lang == null)
                    throw new ProgrammingLanguageNotFoundException();
                ctx.ProgrammingLanguages.Remove(lang);
                ctx.SaveChanges();
            }
        }

        public void RemoveRating(Guid userId, Guid entityId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Id == userId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
                if (ctx.RatableEntities.Where(x => x.Id == entityId).FirstOrDefault() == null)
                    throw new RatableEntityNotFoundException();
                var rating = ctx.RatedEntities.Where(x => x.EntityId == entityId && x.UserId == userId).FirstOrDefault();
                if (rating == null)
                    throw new RatingNotFoundException();
                ctx.RatedEntities.Remove(rating);
                ctx.SaveChanges();
            }
        }

        public void RemoveSkill(Guid userId, Guid languageId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Id == userId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
                if (ctx.ProgrammingLanguages.Where(x => x.Id == languageId).FirstOrDefault() == null)
                    throw new ProgrammingLanguageNotFoundException();
                var skill = ctx.UserSkills.Where(x => x.UserId == userId && x.ProgrammingLanguageId == languageId).FirstOrDefault();
                if (skill == null)
                    throw new UserSkillNotFoundException();
                ctx.UserSkills.Remove(skill);
                ctx.SaveChanges();
            }
        }

        public void RemoveUser(Guid userId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }

        public void Unsave(Guid userId, Guid postId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users.Include(x => x.SavedPosts).Where(x => x.Id == userId).FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();
                var savedPost = user.SavedPosts.Where(x => x.Id == postId).FirstOrDefault();
                if (savedPost == null)
                    throw new SavedPostNotFoundException();
                user.SavedPosts.Remove(savedPost);
                ctx.SaveChanges();
            }
        }
    }
}
