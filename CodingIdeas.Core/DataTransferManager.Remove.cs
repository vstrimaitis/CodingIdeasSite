using System;
using System.Linq;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        private void RemoveRatableEntity(Guid entityId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatableEntities.Remove(ctx.RatableEntities.Where(x => x.Id == entityId).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveComment(Guid commentId)
        {
            RemoveRatableEntity(commentId);
        }

        public void RemovePost(Guid postId)
        {
            RemoveRatableEntity(postId);
        }

        public void RemoveProgrammingLanguage(Guid languageId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.ProgrammingLanguages.Remove(ctx.ProgrammingLanguages.Where(x => x.Id == languageId).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveRating(Guid userId, Guid entityId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatedEntities.Remove(ctx.RatedEntities.Where(x => x.EntityId == entityId && x.UserId == userId).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveSkill(Guid userId, Guid languageId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.UserSkills.Remove(ctx.UserSkills.Where(x => x.UserId == userId && x.ProgrammingLanguageId == languageId).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveUser(Guid userId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.Users.Remove(ctx.Users.Where(x => x.Id == userId).First());
                ctx.SaveChanges();
            }
        }

        public void Unsave(Guid userId, Guid postId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var u = ctx.Users.Where(x => x.Id == userId).First();
                u.SavedPosts.Remove(u.SavedPosts.Where(x => x.Id == postId).First());
                ctx.SaveChanges();
            }
        }
    }
}
