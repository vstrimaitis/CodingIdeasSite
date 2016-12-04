using System;
using System.Collections.Generic;
using System.Linq;
using CodingIdeas.Core;
using Newtonsoft.Json;

namespace CodingIdeas.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataTransferManager mgr = new DataTransferManager(5, 4, 15);
            /*mgr.AddProgrammingLanguage(new ProgrammingLanguage("Ruby"));
            var langTable = mgr.GetProgrammingLanguages();
            foreach (DataRow row in langTable.Rows)
            {
                Console.WriteLine("{0}: {1}", row["Id"], row["Name"]);
            }*/

            //var usr = new User("god@gmail.com", "god", null, null, null, "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null);
            //var post = new Post(usr.Id, DateTime.Now.AddDays(-3), "Sample post", "Sample content");
            //var comment = new Comment(usr.Id, post.Id, DateTime.Now.AddDays(-2), "lol");
            //
            //mgr.AddUser(usr);
            //mgr.AddPost(post);
            //mgr.AddComment(comment);
            //mgr.Save(new User() { Id = userId }, new Post() { Id = postId });
            //var langs = new List<ProgrammingLanguage>()
            //{
            //    new ProgrammingLanguage("C"),
            //    new ProgrammingLanguage("C++"),
            //    new ProgrammingLanguage("C#"),
            //    new ProgrammingLanguage("JAVA"),
            //    new ProgrammingLanguage("Python"),
            //    new ProgrammingLanguage("Javascript"),
            //    new ProgrammingLanguage("PHP")
            //};
            //langs.ForEach(x => mgr.AddProgrammingLanguage(x));
            //mgr.AddSkill(new User() { Id = userId }, langs[1], 2);
            //mgr.AddSkill(new User() { Id = userId }, langs[0], 3);
            //mgr.AddSkill(new User() { Id = userId }, langs[6], 1);
            //mgr.AddSkill(new User() { Id = userId }, langs[3], 5);


            Guid userId = Guid.Parse("430F1043-9250-4E4E-8F77-2AEF475988A4");
            Guid postId = Guid.Parse("66D17B9D-3B67-461F-A6B9-8BEE86DA6148");
            var langs = new List<ProgrammingLanguage>()
            {
                new ProgrammingLanguage(){ Id = Guid.Parse("385E233A-D039-496D-9811-0915B8FA302F"), Name = "C++"},
                new ProgrammingLanguage(){ Id = Guid.Parse("F3A9F4CD-66E0-4300-94E7-177E0B607FD8"), Name = "JAVA"},
                new ProgrammingLanguage(){ Id = Guid.Parse("06A95082-4672-46E9-BDBE-80F7F71C6D90"), Name = "Javascript"},
                new ProgrammingLanguage(){ Id = Guid.Parse("0B52E310-AE60-46CE-A737-8555770EF994"), Name = "C#"},
                new ProgrammingLanguage(){ Id = Guid.Parse("50541CC1-EB66-4C75-853D-9BFB340522BD"), Name = "C"},
                new ProgrammingLanguage(){ Id = Guid.Parse("C086B3DA-DF58-490C-BE2F-C981A334D466"), Name = "Python"},
            };
            var commentIds = new List<Guid>()
            {
                Guid.Parse("C86A7B1A-F13B-4114-9BB3-07CCFDA7199F"),
                Guid.Parse("D748DD23-43BF-43EC-8846-0F9818BC12C1"),
                Guid.Parse("F519928F-9AAC-4642-A9C5-4AEA8D7D12E5"),
                Guid.Parse("6279842F-F37C-4EA9-A7A7-9E027BAFD9F4"),
                Guid.Parse("F38A1E7D-1A8F-4E68-8864-D2FFE4FA1F52"),
                Guid.Parse("DEF453F9-025B-4E97-A72D-FE6B13B29934"),
            };
            
            string allLangsJson = JsonConvert.SerializeObject(mgr.GetProgrammingLanguages().ToList(), Formatting.Indented);
            Console.WriteLine($"Programming languages:\n{allLangsJson}");

            //mgr.AddComment(new Comment(Guid.Parse("A5E98261-A753-4249-B092-AF7ADA603B36"), DateTime.Now, Guid.Parse("04F64928-39EB-4798-8295-2ABD23BD92DF"), "sample comment"));
            //mgr.RemoveComment(new Comment() { Id = Guid.Parse("A5DB3883-CD62-4421-AB4F-5F89BC6B061A") });
            string commentsJson = JsonConvert.SerializeObject(mgr.GetComments(postId, 1).ToList(), Formatting.Indented);
            Console.WriteLine($"Comments: \n{commentsJson}");

            //mgr.AddPost(new Post(Guid.Parse("A5E98261-A753-4249-B092-AF7ADA603B36"), DateTime.Now.AddDays(-2), "Sample title", "Sample content content content"));
            //mgr.RemovePost(new Post() { Id = Guid.Parse("0C6D26C3-3C72-42AF-86F4-C3C75F5E339F") });
            string postsJson = JsonConvert.SerializeObject(mgr.GetPosts(1).ToList(), Formatting.Indented);
            Console.WriteLine($"Posts:\n{postsJson}");

            string userJson = JsonConvert.SerializeObject(mgr.GetUserInfo(mgr.GetUserId("god@gmail.com", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")), Formatting.Indented);
            Console.WriteLine($"User:\n{userJson}");

            string savedPosts = JsonConvert.SerializeObject(mgr.GetSavedPosts(userId, 1), Formatting.Indented);
            Console.WriteLine($"Saved posts:\n{savedPosts}");
            

            /*string json = JsonConvert.SerializeObject(langTable, Formatting.Indented);
            Console.WriteLine(json);

            var langs = JsonConvert.DeserializeObject<ProgrammingLanguage[]>(json);*/
        }
    }
}
