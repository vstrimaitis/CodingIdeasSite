using System;

namespace CodingIdeas.Core
{
    public struct Comment : IRatable
    {
        public User Author { get; set; }
        public Guid Id { get; set; }
        public DateTime PublishDate { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }

        public Comment(User author, DateTime publishDate, Post post, string content)
        {
            Id = Guid.NewGuid();
            Author = author;
            PublishDate = publishDate;
            Post = post;
            Content = content;
        }
    }
}
