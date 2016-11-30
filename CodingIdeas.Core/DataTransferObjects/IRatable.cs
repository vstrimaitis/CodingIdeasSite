using System;

namespace CodingIdeas.Core
{
    public interface IRatable
    {
        Guid Id { get; set; }
        DateTime PublishDate { get; set; }
        Guid? AuthorId { get; set; }
    }
}
