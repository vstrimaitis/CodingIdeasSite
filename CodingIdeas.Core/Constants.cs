using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingIdeas.Core
{
    /// <summary>
    /// TableName_ColumnName
    /// </summary>
    struct Constants
    {
        public const string Schema                      = "coding_ideas";

        public const string Comment                     = "Comment";
        public const string Post                        = "Post";
        public const string ProgrammingLanguage         = "Programming_Language";
        public const string RatableEntity               = "Ratable_Entity";
        public const string RatedEntities               = "Rated_Entities";
        public const string SavedPosts                  = "Saved_Posts";
        public const string User                        = "User";
        public const string UserSkills                  = "User_Skills";

        public const string Comment_Id                  = "Id";
        public const string Comment_PostId              = "Post_Id";
        public const string Comment_Content             = "Content";
        public const string Post_Id                     = "Id";
        public const string Post_Title                  = "Title";
        public const string Post_Content                = "Content";
        public const string ProgrammingLanguage_Id      = "Id";
        public const string ProgrammingLanguage_Name    = "Name";
        public const string RatableEntity_Id            = "Id";
        public const string RatableEntity_UserId        = "User_Id";
        public const string RatableEntity_PublishDate   = "Publish_Date";
        public const string RatedEntities_EntityId      = "Entity_Id";
        public const string RatedEntities_UserId        = "User_Id";
        public const string RatedEntities_Rating        = "Rating";
        public const string SavedPosts_PostId           = "Post_Id";
        public const string SavedPosts_UserId           = "User_Id";
        public const string User_Id                     = "Id";
        public const string User_Email                  = "Email";
        public const string User_Username               = "Username";
        public const string User_FirstName              = "First_Name";
        public const string User_LastName               = "Last_Name";
        public const string User_Password               = "Password";
        public const string User_DOB                    = "DOB";


    }
}
