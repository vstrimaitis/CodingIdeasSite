using System;

namespace CodingIdeas.Core
{
    [Flags]
    public enum PostProperties
    {
        None = 0,
        Id = 1,
        AuthorId = 2,
        PublishDate = 4,
        Title = 8,
        Content = 16,
        All = 31
    }

    public struct Post : IRatable
    {
        public Guid Id { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorUsername { get; set; }

        public Post(Guid authorId, DateTime publishDate, string title, string content, string authorUsername)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            PublishDate = publishDate;
            Title = title;
            Content = content;
            AuthorUsername = authorUsername;
        }
    }
}
