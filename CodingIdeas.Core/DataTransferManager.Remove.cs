using System.Linq;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void RemoveComment(Comment comment)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.Comments.Remove(ctx.Comments.Where(x => x.Id == comment.Id).FirstOrDefault());
                ctx.RatableEntities.Remove(ctx.RatableEntities.Where(x => x.Id == comment.Id).FirstOrDefault());
                ctx.SaveChanges();
            }
        }

        public void RemovePost(Post post)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.Posts.Remove(ctx.Posts.Where(x => x.Id == post.Id).FirstOrDefault());
                ctx.RatableEntities.Remove(ctx.RatableEntities.Where(x => x.Id == post.Id).FirstOrDefault());
                ctx.SaveChanges();
            }
        }

        public void RemoveProgrammingLanguage(ProgrammingLanguage language)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.ProgrammingLanguages.Remove(ctx.ProgrammingLanguages.Where(x => x.Id == language.Id).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveRating(User user, IRatable entity)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatedEntities.Remove(ctx.RatedEntities.Where(x => x.EntityId == entity.Id && x.UserId == user.Id).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveSkill(User user, ProgrammingLanguage language)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.UserSkills.Remove(ctx.UserSkills.Where(x => x.UserId == user.Id && x.ProgrammingLanguageId == language.Id).First());
                ctx.SaveChanges();
            }
        }

        public void RemoveUser(User user)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.Users.Remove(ctx.Users.Where(x => x.Id == user.Id).First());
                ctx.SaveChanges();
            }
        }

        public void Unsave(User user, Post post)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var u = ctx.Users.Where(x => x.Id == user.Id).First();
                u.SavedPosts.Remove(u.SavedPosts.Where(x => x.Id == post.Id).First());
                ctx.SaveChanges();
            }
        }
    }
}
