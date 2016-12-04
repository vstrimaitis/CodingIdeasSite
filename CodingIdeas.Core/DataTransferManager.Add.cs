using System;
using System.Linq;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public void AddComment(Comment comment)
        {
            ValidateComment(comment, CommentProperties.All);
            var commentModel = new DB.Comment()
            {
                Id = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId,
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
            ValidatePost(post, PostProperties.All);
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
        
        public void AddRating(Guid userId, Guid entityId, sbyte rating)
        {
            ValidateRating(userId, entityId, rating);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatedEntities.Add(new DB.RatedEntity() { UserId = userId, EntityId = entityId, Rating = rating });
                ctx.SaveChanges();
            }
        }

        public void AddSkill(Guid userId, Guid languageId, byte proficiency)
        {
            ValidateSkill(userId, languageId, proficiency);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.UserSkills.Add(new DB.UserSkill()
                {
                    UserId = userId,
                    ProgrammingLanguageId = languageId,
                    Proficiency = proficiency
                });
                ctx.SaveChanges();
            }
        }

        public void AddUser(User user)
        {
            ValidateUser(user, UserProperties.All);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.Users.Add(new DB.User()
                {
                    Id = user.Id,
                    DOB = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.PasswordHash,
                    Username = user.Username
                });
                ctx.SaveChanges();
            }
        }

        public void Save(Guid userId, Guid postToSaveId)
        {
            ValidateSavePost(userId, postToSaveId);
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var post = ctx.Posts.Where(x => x.Id == postToSaveId).First();
                ctx.Users.Where(x => x.Id == userId).First().SavedPosts.Add(post);
                ctx.SaveChanges();
            }
        }
    }
}
