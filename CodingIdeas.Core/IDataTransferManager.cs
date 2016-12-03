using System.Collections.Generic;
using System.Data;

namespace CodingIdeas.Core
{
    public interface IDataTransferManager
    {
        IEnumerable<Post> GetPosts(int pageNumber);
        IEnumerable<Comment> GetComments(Post post, int pageNumber);
        int GetTotalRating(IRatable entity);
        User GetUser(string login, string passwordHash); // GetUserInfo? login = username or email
        IEnumerable<Post> GetSavedPosts(User user, int pageNumber);
        sbyte GetRatingByUser(User user, IRatable entity);
        IEnumerable<ProgrammingLanguage> GetProgrammingLanguages();

        void AddRating(User user, IRatable entity, sbyte rating);
        void Save(User user, Post postToSave);
        void AddUser(User user);
        void AddPost(Post post);
        void AddComment(Comment comment);
        void AddProgrammingLanguage(ProgrammingLanguage language);
        void AddSkill(User user, ProgrammingLanguage language, byte proficiency);

        void UpdatePost(Post old, Post @new);
        void UpdateComment(Comment old, Comment @new);
        void UpdateUser(User old, User @new, UserProperties propertiesToChange);
        void UpdateRating(User user, IRatable entity, sbyte newRating);
        void UpdateProgrammingLanguage(ProgrammingLanguage old, ProgrammingLanguage @new);
        void UpdateSkill(User user, ProgrammingLanguage language, byte proficiency);

        void RemovePost(Post post);
        void RemoveComment(Comment comment);
        void RemoveUser(User user);
        void RemoveProgrammingLanguage(ProgrammingLanguage language);
        void Unsave(User user, Post post);
        void RemoveRating(User user, IRatable entity);
        void RemoveSkill(User user, ProgrammingLanguage language);
    }
}
