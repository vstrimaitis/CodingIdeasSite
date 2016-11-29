﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core
{
    public class DataTransferManager : IDataTransferManager
    {
        private readonly int PostsPerPage;
        private readonly int CommentsPerPage;
        private readonly int SavedPostsPerPage;

        public DataTransferManager(int postsPerPage, int commentsPerPage, int savedPostsPerPage)
        {
            PostsPerPage = postsPerPage;
            CommentsPerPage = commentsPerPage;
            SavedPostsPerPage = savedPostsPerPage;
        }

        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void AddPost(Post post)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Comment> GetComments(Post post, int pageNumber)
        {
            /*var table = new DataTable();
            table.Columns.Add(nameof(IRatable.Id), typeof(Guid));
            table.Columns.Add(nameof(User.Username), typeof(string));
            table.Columns.Add(nameof(IRatable.PublishDate), typeof(DateTime));
            table.Columns.Add(nameof(DB.Comment.Content), typeof(string));
            table.Columns.Add("Rating", typeof(short));

            using (var ctx = new DB.CodingIdeasEntities())
            {
                var comments = (from c in ctx.Comments
                                where c.PostId == post.Id
                                join r in ctx.RatableEntities on c.Id equals r.Id
                                orderby r.PublishDate
                                select new
                                {
                                    Id = c.Id,
                                    AuthorUsername = (from u in ctx.Users
                                                      where u.Id == r.UserId
                                                      select u.Username).FirstOrDefault(),
                                    PublishDate = r.PublishDate,
                                    Content = c.Content,
                                    Rating = (from p in ctx.RatedEntities1
                                              where p.EntityId == r.Id
                                              select p.Rating)
                                              .Aggregate((x, y) => (short)(x + y)) // causes errors
                               })
                               .Skip((pageNumber-1) * CommentsPerPage)
                               .Take(CommentsPerPage);
                foreach(var comment in comments)
                    table.Rows.Add(comment.Id, comment.AuthorUsername, comment.PublishDate, comment.Content, comment.Rating);
            }
            
            return table;*/
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
                        foreach(DataRow row in table.Rows)
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

        public DataTable GetPosts(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public DataTable GetProgrammingLanguages()
        {
            var table = new DataTable();
            table.Columns.Add(nameof(ProgrammingLanguage.Id), typeof(Guid));
            table.Columns.Add(nameof(ProgrammingLanguage.Name), typeof(string));

            using (var ctx = new DB.CodingIdeasEntities())
            {
                foreach(var lang in ctx.ProgrammingLanguages)
                {
                    table.Rows.Add(lang.Id, lang.Name);
                }
            }
            return table;
        }

        public sbyte GetRatingByUser(User user, IRatable entity)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSavedPosts(User user, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public int GetTotalRating(IRatable entity)
        {

            using (var ctx = new DB.CodingIdeasEntities())
            {
                return (from r in ctx.RatedEntities1
                        where r.EntityId == entity.Id
                        select (int)r.Rating).AsEnumerable().Aggregate((x, y) => x + y);
            }
        }

        public User GetUser(string login, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void RemovePost(Post post)
        {
            throw new NotImplementedException();
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

        public void UpdateComment(Comment old, Comment @new)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post old, Post @new)
        {
            throw new NotImplementedException();
        }

        public void UpdateProgrammingLanguage(ProgrammingLanguage old, ProgrammingLanguage @new)
        {
            throw new NotImplementedException();
        }

        public void UpdateRating(User user, IRatable entity, sbyte newRating)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User old, User @new)
        {
            throw new NotImplementedException();
        }
    }
}
