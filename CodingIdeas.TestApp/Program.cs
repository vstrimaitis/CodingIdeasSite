using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingIdeas.Core;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;

namespace CodingIdeas.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataTransferManager mgr = new DataTransferManager(5, 10, 15);
            /*mgr.AddProgrammingLanguage(new ProgrammingLanguage("Ruby"));
            var langTable = mgr.GetProgrammingLanguages();
            foreach (DataRow row in langTable.Rows)
            {
                Console.WriteLine("{0}: {1}", row["Id"], row["Name"]);
            }*/

            /*mgr.AddUser(new User()
            {
                Username = "god",
                Email = "god@gmail.com",
                PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8"
            });*/

            

            string allLangsJson = JsonConvert.SerializeObject(mgr.GetProgrammingLanguages().ToList(), Formatting.Indented);
            Console.WriteLine(allLangsJson);

            //mgr.AddComment(new Comment(Guid.Parse("A5E98261-A753-4249-B092-AF7ADA603B36"), DateTime.Now, Guid.Parse("04F64928-39EB-4798-8295-2ABD23BD92DF"), "sample comment"));
            //mgr.RemoveComment(new Comment() { Id = Guid.Parse("A5DB3883-CD62-4421-AB4F-5F89BC6B061A") });
            string allCommentsJson = JsonConvert.SerializeObject(mgr.GetComments(new Post() { Id = Guid.Parse("04F64928-39EB-4798-8295-2ABD23BD92DF") }, 1).ToList(), Formatting.Indented);
            Console.WriteLine(allCommentsJson);

            //mgr.AddPost(new Post(Guid.Parse("A5E98261-A753-4249-B092-AF7ADA603B36"), DateTime.Now.AddDays(-2), "Sample title", "Sample content content content"));
            //mgr.RemovePost(new Post() { Id = Guid.Parse("0C6D26C3-3C72-42AF-86F4-C3C75F5E339F") });
            string allPostsJson = JsonConvert.SerializeObject(mgr.GetPosts(1).ToList(), Formatting.Indented);
            Console.WriteLine(allPostsJson);

            string userJson = JsonConvert.SerializeObject(mgr.GetUser("vstrimaitis@gmail.com", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8"), Formatting.Indented);
            Console.WriteLine(userJson);

            string savedPosts = JsonConvert.SerializeObject(mgr.GetSavedPosts(new User() { Id = Guid.Parse("A5E98261-A753-4249-B092-AF7ADA603B36") }, 1), Formatting.Indented);
            Console.WriteLine(savedPosts);
            

            /*string json = JsonConvert.SerializeObject(langTable, Formatting.Indented);
            Console.WriteLine(json);

            var langs = JsonConvert.DeserializeObject<ProgrammingLanguage[]>(json);*/
        }
    }
}
