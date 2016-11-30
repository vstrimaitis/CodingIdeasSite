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
            
            
            
            string allLangsJson = JsonConvert.SerializeObject(mgr.GetProgrammingLanguages().ToList(), Formatting.Indented);
            Console.WriteLine(allLangsJson);

            foreach(var comment in mgr.GetComments(new Post() { Id = Guid.Parse("04F64928-39EB-4798-8295-2ABD23BD92DF") }, 1))
            {
                //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", comment.Id, comment.AuthorId, comment.Content, comment.PostId, comment.PublishDate, mgr.GetTotalRating(comment));
                string json = JsonConvert.SerializeObject(comment, Formatting.Indented);
                Console.WriteLine(json);
            }

            foreach(var post in mgr.GetPosts(1))
            {
                string json = JsonConvert.SerializeObject(post, Formatting.Indented);
                Console.WriteLine(json);
            }
            /*string json = JsonConvert.SerializeObject(langTable, Formatting.Indented);
            Console.WriteLine(json);

            var langs = JsonConvert.DeserializeObject<ProgrammingLanguage[]>(json);*/
        }
    }
}
