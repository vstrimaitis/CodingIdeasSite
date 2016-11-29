using System.Data;

namespace CodingIdeas.Core
{
    public interface IDataTransferManager
    {
        DataTable GetPosts(int pageNumber);
        DataTable GetComments(Post post, int pageNumber);
        int GetTotalRating(IRatable entity);
        User GetUser(string login, string passwordHash); // GetUserInfo? login = username or email
        DataTable GetSavedPosts(User user, int pageNumber);
        sbyte GetRatingByUser(User user, IRatable entity);
        DataTable GetProgrammingLanguages();

        void AddRating(User user, IRatable entity, sbyte rating);
        void Save(User user, Post postToSave);
        void AddUser(User user);
        void AddPost(Post post);
        void AddComment(Comment comment);
        void AddProgrammingLanguage(ProgrammingLanguage language);
        void AddSkill(User user, ProgrammingLanguage language, byte proficiency);

        void UpdatePost(Post old, Post @new);
        void UpdateComment(Comment old, Comment @new);
        void UpdateUser(User old, User @new);
        void UpdateRating(User user, IRatable entity, sbyte newRating);
        void UpdateProgrammingLanguage(ProgrammingLanguage old, ProgrammingLanguage @new);

        void RemovePost(Post post);
        void RemoveComment(Comment comment);
        void RemoveUser(User user);
        void RemoveProgrammingLanguage(ProgrammingLanguage language);
        void Unsave(User user, Post post);
        void RemoveRating(User user, IRatable entity);
        void RemoveSkill(User user, ProgrammingLanguage language);
    }
}
