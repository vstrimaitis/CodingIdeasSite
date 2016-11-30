using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void RemoveRating(User user, IRatable entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveSkill(User user, ProgrammingLanguage language)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(User user)
        {
            throw new NotImplementedException();
        }

        public void Save(User user, Post postToSave)
        {
            throw new NotImplementedException();
        }

        public void Unsave(User user, Post post)
        {
            throw new NotImplementedException();
        }

    }
}
