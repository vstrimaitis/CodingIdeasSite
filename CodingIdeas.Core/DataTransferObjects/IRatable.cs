using System;

namespace CodingIdeas.Core
{
    public interface IRatable
    {
        Guid Id { get; set; }
        DateTime PublishDate { get; set; }
        User Author { get; set; }
    }
}
