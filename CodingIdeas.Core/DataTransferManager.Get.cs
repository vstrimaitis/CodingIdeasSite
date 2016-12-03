using CodingIdeas.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        public IEnumerable<Comment> GetComments(Guid postId, int pageNumber)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                using (var conn = new SqlConnection(ctx.Database.Connection.ConnectionString))
                {
                    string selectSql = $@"SELECT C.[{Constants.Comment_Id}], C.[{Constants.Comment_Content}], R.[{Constants.RatableEntity_UserId}], R.[{Constants.RatableEntity_PublishDate}]
                                          FROM [{Constants.Schema}].[{Constants.Comment}] as C
                                          LEFT JOIN [{Constants.Schema}].[{Constants.RatableEntity}] AS R ON R.[{Constants.RatableEntity_Id}] = C.[{Constants.Comment_Id}] AND C.[{Constants.Comment_PostId}] = '{postId}'
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
                                                AuthorId = Guid.Parse(x[2].ToString()),
                                                PublishDate = DateTime.Parse(x[3].ToString())
                                            });
                        foreach (var c in comments)
                            yield return c;
                    }
                }
            }

        }

        public IEnumerable<Post> GetPosts(int pageNumber)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var posts = (from p in ctx.Posts
                             join r in ctx.RatableEntities on p.Id equals r.Id
                             orderby r.PublishDate
                             select new { AuthorId = r.UserId, Id = p.Id, Content = p.Content, PublishDate = r.PublishDate, Title = p.Title })
                             .Skip((pageNumber - 1) * PostsPerPage)
                             .Take(PostsPerPage)
                             .ToList()
                             .Select(x => new Post() { AuthorId = x.AuthorId, Id = x.Id, Content = x.Content, PublishDate = x.PublishDate, Title = x.Title });
                foreach (var post in posts)
                    yield return post;
            }
        }

        public IEnumerable<ProgrammingLanguage> GetProgrammingLanguages()
        {
            var table = new DataTable();
            table.Columns.Add(nameof(ProgrammingLanguage.Id), typeof(Guid));
            table.Columns.Add(nameof(ProgrammingLanguage.Name), typeof(string));

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
                return ctx.RatedEntities
                          .Where(x => x.UserId == userId && x.EntityId == entityId)
                          .Select(x => (sbyte)x.Rating)
                          .FirstOrDefault();
            }
        }

        public IEnumerable<Post> GetSavedPosts(Guid userId, int pageNumber)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var saved = ctx.Users
                            .Where(x => x.Id == userId)
                            .FirstOrDefault()
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
                return (from r in ctx.RatedEntities
                        where r.EntityId == entityId
                        select (int)r.Rating).AsEnumerable().Aggregate((x, y) => x + y);
            }
        }

        public User GetUserInfo(string login, string passwordHash)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var user = ctx.Users.Where(x => (x.Email == login || x.Username == login) && x.Password == passwordHash).FirstOrDefault();
                if (user == null)
                    throw new UserNotFoundException();
                var skills = ctx.UserSkills.ToList().Select(x => new UserSkill()
                {
                    ProgrammingLanguage = new ProgrammingLanguage() { Id = x.ProgrammingLanguage.Id, Name = x.ProgrammingLanguage.Name },
                    Proficiency = (byte)x.Proficiency
                }).ToList();
                return new User()
                {
                    Id = user.Id,
                    DateOfBirth = user.DOB,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PasswordHash = user.Password,
                    Skills = skills,
                    Username = user.Username
                };
            }
        }
    }
}
