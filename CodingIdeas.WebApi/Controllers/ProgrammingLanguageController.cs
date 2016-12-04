using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using CodingIdeas.Core;
using Newtonsoft.Json.Linq;

namespace CodingIdeas.WebApi.Controllers
{
    public class ProgrammingLanguageController : ApiController
    {
        // GET: api/ProgrammingLanguage
        public IEnumerable<ProgrammingLanguage> Get()
        {
            var mgr = WebApiApplication.Manager;
            return mgr.GetProgrammingLanguages();
        }

        // GET: api/ProgrammingLanguage/5
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var langs = WebApiApplication.Manager.GetProgrammingLanguages();
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var lang = langs.Where(x => x.Id == guid);
            if(lang.Count() == 0)
                return Content(HttpStatusCode.NotFound, "Programming language with the specified ID does not exist.");
            return Ok(lang.First());
        }

        // POST: api/ProgrammingLanguage
        [HttpPost]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var lang = value.ToObject<ProgrammingLanguage>();
            if (string.IsNullOrWhiteSpace(lang.Name))
                return Content(HttpStatusCode.BadRequest, "Name cannot be null");
            if (lang.Id == Guid.Empty)
                lang.Id = Guid.NewGuid();
            mgr.AddProgrammingLanguage(lang);
            return Ok();
        }

        // PUT: api/ProgrammingLanguage/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var lang = value.ToObject<ProgrammingLanguage>();
            if (string.IsNullOrWhiteSpace(lang.Name))
                return Content(HttpStatusCode.BadRequest, "Name cannot be null");
            if (lang.Id == Guid.Empty)
                lang.Id = Guid.NewGuid();
            mgr.UpdateProgrammingLanguage(lang.Id, lang.Name);
            return Ok();
        }

        // DELETE: api/ProgrammingLanguage/5
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var mgr = WebApiApplication.Manager;
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            mgr.RemoveProgrammingLanguage(guid);
            return Ok();
        }
    }
}
