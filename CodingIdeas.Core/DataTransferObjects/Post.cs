using System;

namespace CodingIdeas.Core
{
    public struct Post : IRatable
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Post(User author, DateTime publishDate, string title, string content)
        {
            Id = Guid.NewGuid();
            Author = author;
            PublishDate = publishDate;
            Title = title;
            Content = content;
        }
    }
}
