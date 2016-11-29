using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingIdeas.Core;
using System.Data;
using Newtonsoft.Json;

namespace CodingIdeas.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataTransferManager mgr = new DataTransferManager(5,10,15);
            /*mgr.AddProgrammingLanguage(new ProgrammingLanguage("Ruby"));
            var langTable = mgr.GetProgrammingLanguages();
            foreach (DataRow row in langTable.Rows)
            {
                Console.WriteLine("{0}: {1}", row["Id"], row["Name"]);
            }*/
            var comments = mgr.GetComments(new Post() { Id = Guid.Parse("04F64928-39EB-4798-8295-2ABD23BD92DF") }, 1);
            foreach(DataRow row in comments.Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", row[0], row[1], row[2], row[3]);
            }
            /*string json = JsonConvert.SerializeObject(langTable, Formatting.Indented);
            Console.WriteLine(json);

            var langs = JsonConvert.DeserializeObject<ProgrammingLanguage[]>(json);*/
        }
    }
}
