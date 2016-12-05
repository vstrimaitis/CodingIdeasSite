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
        [HttpGet]
        [Route("api/ProgrammingLanguage")]
        public IHttpActionResult Get()
        {
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetProgrammingLanguages());
        }
        
        [HttpGet]
        [Route("api/ProgrammingLanguage/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var langs = WebApiApplication.Manager.GetProgrammingLanguages();
            var lang = langs.Where(x => x.Id == id);
            if(lang.Count() == 0)
                return Content(HttpStatusCode.NotFound, "Programming language with the specified ID does not exist.");
            return Ok(lang.First());
        }
        
        [HttpPost]
        [Route("api/ProgrammingLanguage")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var lang = value.ToObject<ProgrammingLanguage>();
            if (lang.Id == Guid.Empty)
                lang.Id = Guid.NewGuid();
            mgr.AddProgrammingLanguage(lang);
            return Ok(new { Id = lang.Id });
        }
        
        [HttpPut]
        [Route("api/ProgrammingLanguage")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var lang = value.ToObject<ProgrammingLanguage>();
            mgr.UpdateProgrammingLanguage(lang.Id, lang.Name);
            return Ok();
        }

        [HttpDelete]
        [Route("api/ProgrammingLanguage/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            mgr.RemoveProgrammingLanguage(id);
            return Ok();
        }
    }
}
