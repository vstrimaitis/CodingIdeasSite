﻿using CodingIdeas.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public IEnumerable<Comment> GetComments(Guid postId, int pageNumber)
        {
            if (pageNumber <= 0)
                throw new InvalidPageNumberException();
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Posts.Where(x => x.Id == postId).FirstOrDefault() == null)
                    throw new PostNotFoundException();
                using (var conn = new SqlConnection(ctx.Database.Connection.ConnectionString))
                {
                    string selectSql = $@"SELECT C.[{Constants.Comment_Id}], C.[{Constants.Comment_Content}], R.[{Constants.RatableEntity_UserId}], U.[{Constants.User_Username}], R.[{Constants.RatableEntity_PublishDate}]
                                          FROM [{Constants.Schema}].[{Constants.Comment}] as C, [{Constants.Schema}].[{Constants.RatableEntity}] as R, [{Constants.Schema}].[{Constants.User}] as U
                                          WHERE R.[{Constants.RatableEntity_Id}] = C.[{Constants.Comment_Id}] AND C.[{Constants.Comment_PostId}] = '{postId}' AND R.[{Constants.RatableEntity_UserId}] = U.[{Constants.User_Id}]
                                          ORDER BY R.[{Constants.RatableEntity_PublishDate}] DESC";
                    using (var adapter = new SqlDataAdapter(selectSql, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(CommentsPerPage * (pageNumber - 1), CommentsPerPage, table);
                        var comments = table.AsEnumerable()
                                            .Select(x => new Comment()
                                            {
                                                Id = Guid.Parse(x[0].ToString()),
                                                PostId = postId,
                                                Content = x[1].ToString(),
                                                AuthorId = x[2].ToString() == "" ? (Guid?)null : Guid.Parse(x[2].ToString()),
                                                PublishDate = DateTime.Parse(x[4].ToString()),
                                                AuthorUsername = x[3].ToString()
                                            });
                        foreach (var c in comments)
                            yield return c;
                    }
                }
            }

        }

        public Post GetPost(Guid postId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var post = ctx.Posts.Include(x => x.RatableEntity).Include(x => x.RatableEntity.User).Where(x => x.Id == postId).FirstOrDefault();
                if (post == null)
                    throw new PostNotFoundException();
                return new Post()
                {
                    Id = post.Id,
                    AuthorId = post.RatableEntity.UserId,
                    Content = post.Content,
                    PublishDate = post.RatableEntity.PublishDate,
                    Title = post.Title,
                    AuthorUsername = post.RatableEntity.User.Username
                };
            }
        }

        public IEnumerable<Post> GetPosts(int pageNumber)
        {
            if (pageNumber <= 0)
                throw new InvalidPageNumberException();
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var posts = (from p in ctx.Posts
                             join r in ctx.RatableEntities on p.Id equals r.Id
                             join u in ctx.Users on r.UserId equals u.Id
                             orderby r.PublishDate descending
                             select new { AuthorId = r.UserId, Id = p.Id, Content = p.Content, PublishDate = r.PublishDate, Title = p.Title, AuthorUsername = u.Username })
                             .Skip((pageNumber - 1) * PostsPerPage)
                             .Take(PostsPerPage)
                             .ToList()
                             .Select(x => new Post() { AuthorId = x.AuthorId, Id = x.Id, Content = x.Content, PublishDate = x.PublishDate, Title = x.Title, AuthorUsername = x.AuthorUsername });
                foreach (var post in posts)
                    yield return post;
            }
        }

        public IEnumerable<ProgrammingLanguage> GetProgrammingLanguages()
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                foreach (var lang in ctx.ProgrammingLanguages)
                {
                    yield return new ProgrammingLanguage() { Id = lang.Id, Name = lang.Name };
                }
            }
        }

        public sbyte GetRatingByUser(Guid userId, Guid entityId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var rating = ctx.RatedEntities
                                .Where(x => x.UserId == userId && x.EntityId == entityId)
                                .FirstOrDefault();
                if (rating == null)
                    return 0;
                return (sbyte)rating.Rating;
            }
        }

        public IEnumerable<Post> GetSavedPosts(Guid userId, int pageNumber)
        {
            if (pageNumber <= 0)
                throw new InvalidPageNumberException();
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users
                              .Include(x => x.SavedPosts.Select(y => y.RatableEntity))
                              .Where(x => x.Id == userId)
                              .FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();

                var saved = user
                            .SavedPosts
                            .Skip((pageNumber - 1) * SavedPostsPerPage)
                            .Take(SavedPostsPerPage)
                            .AsEnumerable()
                            .Select(x => new Post()
                            {
                                Id = x.Id,
                                AuthorId = x.RatableEntity.UserId,
                                Content = x.Content,
                                PublishDate = x.RatableEntity.PublishDate,
                                Title = x.Title
                            });
                foreach (var s in saved)
                    yield return s;
            }
        }

        public int GetTotalRating(Guid entityId)
        {

            using (var ctx = new DB.CodingIdeasEntities())
            {
                var allRatings = from r in ctx.RatedEntities
                                 where r.EntityId == entityId
                                 select (int)r.Rating;
                if (allRatings.Count() == 0)
                    return 0;
                return allRatings.AsEnumerable().Aggregate((x, y) => x + y);
            }
        }
        
        public User GetUser(Guid userId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users.Include(x => x.UserSkills.Select(y => y.ProgrammingLanguage)).Where(x => x.Id == userId).FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();
                var skills = ctx.UserSkills
                                .Where(x => x.UserId == userId)
                                .ToList()
                                .Select(x => new UserSkill()
                                {
                                    ProgrammingLanguage = new ProgrammingLanguage()
                                    {
                                        Id = x.ProgrammingLanguageId,
                                        Name = x.ProgrammingLanguage.Name
                                    },
                                    Proficiency = (byte)x.Proficiency
                                })
                                .ToList();
                return new User()
                {
                    Id = user.Id,
                    DateOfBirth = user.DOB,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Skills = skills,
                    Username = user.Username
                };
            }
        }

        public Guid GetUserId(string login, string passwordHash)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users.Where(x => (x.Email == login || x.Username == login) && x.Password == passwordHash).FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();
                return user.Id;
            }
        }

        public IEnumerable<dynamic> GetMostActivePosters(int howMany)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var posters = ctx.Posts
                                 .Include(x => x.RatableEntity)
                                 .Include(x => x.RatableEntity.User)
                                 .GroupBy(x => x.RatableEntity.UserId)
                                 .Where(x => x.Key != null)
                                 .OrderByDescending(x => x.Count())
                                 .Take(howMany)
                                 .Select(x => new { UserId = x.Key.Value, NumberOfPosts =  x.Count(), Username=x.FirstOrDefault().RatableEntity.User.Username });
                foreach (var p in posters)
                    yield return p;
            }
        }
    }
}
