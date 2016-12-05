using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class SkillController : ApiController
    {
        [HttpPost]
        [Route("api/Skill")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var template = new { UserId = Guid.Empty, LanguageId = Guid.Empty, Proficiency = (byte)0 };
            var skill = JsonConvert.DeserializeAnonymousType(value.ToString(), template);
            var mgr = WebApiApplication.Manager;
            mgr.AddSkill(skill.UserId, skill.LanguageId, skill.Proficiency);
            return Ok();
        }

        [HttpPut]
        [Route("api/Skill")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var template = new { UserId = Guid.Empty, LanguageId = Guid.Empty, Proficiency = (byte)0 };
            var skill = JsonConvert.DeserializeAnonymousType(value.ToString(), template);
            var mgr = WebApiApplication.Manager;
            mgr.UpdateSkill(skill.UserId, skill.LanguageId, skill.Proficiency);
            return Ok();
        }

        [HttpDelete]
        [Route("api/Skill/{userId}/{languageId}")]
        public IHttpActionResult Delete(Guid userId, Guid languageId)
        {
            var mgr = WebApiApplication.Manager;
            mgr.RemoveSkill(userId, languageId);
            return Ok();
        }
    }
}
