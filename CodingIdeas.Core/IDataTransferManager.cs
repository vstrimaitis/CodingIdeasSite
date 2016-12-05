using System;
using System.Collections.Generic;

namespace CodingIdeas.Core
{
    public interface IDataTransferManager
    {
        IEnumerable<Post> GetPosts(int pageNumber);                                         //+
        IEnumerable<Comment> GetComments(Guid postId, int pageNumber);                      //+
        int GetTotalRating(Guid entityId);                                                  //+
        User GetUser(Guid userId);                                                          //+
        Guid GetUserId(string login, string passwordHash);                                  //+
        IEnumerable<Post> GetSavedPosts(Guid userId, int pageNumber);                       //+
        sbyte GetRatingByUser(Guid userId, Guid entityId);                                  //+
        IEnumerable<ProgrammingLanguage> GetProgrammingLanguages();                         //+
        Post GetPost(Guid postId);                                                          //+

        void AddRating(Guid userId, Guid entityId, sbyte rating);                           //+
        void Save(Guid userId, Guid postToSaveId);                                          //+
        void AddUser(User user);                                                            //+
        void AddPost(Post post);                                                            //+
        void AddComment(Comment comment);                                                   //+
        void AddProgrammingLanguage(ProgrammingLanguage language);                          //+
        void AddSkill(Guid userId, Guid languageId, byte proficiency);                      //
        
        void UpdatePost(Guid oldPostId, Post @new, PostProperties propertiesToChange);      //+
        void UpdateComment(Guid oldCommentId, string newContent);                           //+
        void UpdateUser(Guid oldUserId, User @new, UserProperties propertiesToChange);      //+
        void UpdateRating(Guid userId, Guid entityId, sbyte newRating);                     //+
        void UpdateProgrammingLanguage(Guid oldLanguageId, string newName);                 //+
        void UpdateSkill(Guid userId, Guid languageId, byte proficiency);                   //

        void RemovePost(Guid postId);                                                       //+
        void RemoveComment(Guid commentId);                                                 //+
        void RemoveUser(Guid userId);                                                       //+
        void RemoveProgrammingLanguage(Guid languageId);                                    //+
        void Unsave(Guid userId, Guid postId);                                              //+
        void RemoveRating(Guid userId, Guid entityId);                                      //+
        void RemoveSkill(Guid userId, Guid languageId);                                     //
    }
}
