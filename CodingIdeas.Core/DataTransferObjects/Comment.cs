using System;

namespace CodingIdeas.Core
{
    public struct Comment : IRatable
    {
        public Guid? AuthorId { get; set; }
        public Guid Id { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }

        public Comment(Guid authorId, DateTime publishDate, Guid postId, string content)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            PublishDate = publishDate;
            PostId = postId;
            Content = content;
        }
    }
}
