using System;

namespace CodingIdeas.Core
{
    [Flags]
    public enum PostProperties
    {
        None = 0,
        Title = 1,
        Content = 2,
        All = 3
    }

    public struct Post : IRatable
    {
        public Guid Id { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Post(Guid authorId, DateTime publishDate, string title, string content)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            PublishDate = publishDate;
            Title = title;
            Content = content;
        }
    }
}
