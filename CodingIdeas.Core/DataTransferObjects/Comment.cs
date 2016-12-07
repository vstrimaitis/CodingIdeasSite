using System;

namespace CodingIdeas.Core
{
    [Flags]
    public enum CommentProperties
    {
        None = 0,
        AuthorId = 1,
        Id = 2,
        PublishDate = 4,
        PostId = 8,
        Content = 16,
        All = 31
    }

    public struct Comment : IRatable
    {
        public Guid? AuthorId { get; set; }
        public Guid Id { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public string AuthorUsername { get; set; }

        public Comment(Guid? authorId, Guid postId, DateTime publishDate, string content, string authorUsername)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            PublishDate = publishDate;
            PostId = postId;
            Content = content;
            AuthorUsername = authorUsername;
        }
    }
}
