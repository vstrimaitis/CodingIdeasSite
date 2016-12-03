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
        public IEnumerable<Comment> GetComments(Post post, int pageNumber)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                using (var conn = new SqlConnection(ctx.Database.Connection.ConnectionString))
                {
                    string selectSql = $@"SELECT C.[{Constants.Comment_Id}], C.[{Constants.Comment_Content}], R.[{Constants.RatableEntity_UserId}], R.[{Constants.RatableEntity_PublishDate}]
                                          FROM [{Constants.Schema}].[{Constants.Comment}] as C
                                          LEFT JOIN [{Constants.Schema}].[{Constants.RatableEntity}] AS R ON R.[{Constants.RatableEntity_Id}] = C.[{Constants.Comment_Id}] AND C.[{Constants.Comment_PostId}] = '{post.Id}'
                                          ORDER BY R.[{Constants.RatableEntity_PublishDate}] DESC";
                    using (var adapter = new SqlDataAdapter(selectSql, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        foreach (DataRow row in table.Rows)
                        {
                            var comment = new Comment()
                            {
                                Id = Guid.Parse(row[0].ToString()),
                                PostId = post.Id,
                                Content = row[1].ToString(),
                                AuthorId = Guid.Parse(row[2].ToString()),
                                PublishDate = DateTime.Parse(row[3].ToString())
                            };
                            yield return comment;
                        }
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

        public sbyte GetRatingByUser(User user, IRatable entity)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                return ctx.RatedEntities
                          .Where(x => x.UserId == user.Id && x.EntityId == entity.Id)
                          .Select(x => (sbyte)x.Rating)
                          .FirstOrDefault();
            }
        }

        public IEnumerable<Post> GetSavedPosts(User user, int pageNumber)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var saved = ctx.Users
                            .Where(x => x.Id == user.Id)
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

        public int GetTotalRating(IRatable entity)
        {

            using (var ctx = new DB.CodingIdeasEntities())
            {
                return (from r in ctx.RatedEntities
                        where r.EntityId == entity.Id
                        select (int)r.Rating).AsEnumerable().Aggregate((x, y) => x + y);
            }
        }

        public User GetUser(string login, string passwordHash)
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
