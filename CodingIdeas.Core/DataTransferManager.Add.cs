﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.RatedEntities.Add(new DB.RatedEntity() { UserId = user.Id, EntityId = entity.Id, Rating = rating });
                ctx.SaveChanges();
            }
        }

        public void AddSkill(User user, ProgrammingLanguage language, byte proficiency)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                ctx.UserSkills.Add(new DB.UserSkill()
                {
                    UserId = user.Id,
                    ProgrammingLanguageId = language.Id,
                    Proficiency = proficiency
                });
                ctx.SaveChanges();
            }
        }

        public void AddUser(User user)
        {
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
        
        public void Save(User user, Post postToSave)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var post = ctx.Posts.Where(x => x.Id == postToSave.Id).First();
                ctx.Users.Where(x => x.Id == user.Id).First().SavedPosts.Add(post);
                ctx.SaveChanges();
            }
        }
    }
}