using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void AddComment(Comment comment)
        {
            var commentModel = new DB.Comment()
            {
                Id = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId
            };

            var ratableEntityModel = new DB.RatableEntity()
            {
                Id = comment.Id,
                PublishDate = comment.PublishDate,
                UserId = comment.AuthorId
            };

            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatableEntities.Add(ratableEntityModel);
                ctx.Comments.Add(commentModel);
                ctx.SaveChanges();
            }
        }

        public void AddPost(Post post)
        {
            var postModel = new DB.Post()
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title
            };

            var ratableEntityModel = new DB.RatableEntity()
            {
                Id = post.Id,
                UserId = post.AuthorId,
                PublishDate = post.PublishDate
            };

            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatableEntities.Add(ratableEntityModel);
                ctx.Posts.Add(postModel);
                ctx.SaveChanges();
            }
        }

        public void AddProgrammingLanguage(ProgrammingLanguage language)
        {
            var langModel = new DB.ProgrammingLanguage()
            {
                Id = language.Id,
                Name = language.Name
            };
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.ProgrammingLanguages.Add(langModel);
                ctx.SaveChanges();
            }
        }

        public void AddRating(User user, IRatable entity, sbyte rating)
        {
            throw new NotImplementedException();
        }

        public void AddSkill(User user, ProgrammingLanguage language, byte proficiency)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
