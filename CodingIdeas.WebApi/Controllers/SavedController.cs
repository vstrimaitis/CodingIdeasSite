using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class SavedController : ApiController
    {
        [HttpGet]
        [Route("api/Saved/{userId}/{pageNumber}")]
        public IHttpActionResult Get(Guid userId, int pageNumber)
        {
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetSavedPosts(userId, pageNumber));
        }
        
        [HttpPost]
        [Route("api/Saved")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var template = new { UserId = Guid.Empty, PostId = Guid.Empty };
            var saved = JsonConvert.DeserializeAnonymousType(value.ToString(), template);
            var mgr = WebApiApplication.Manager;
            mgr.Save(saved.UserId, saved.PostId);
            return Ok();
        }
        
        [HttpDelete]
        [Route("api/Saved/{userId}/{postId}")]
        public IHttpActionResult Delete(Guid userId, Guid postId)
        {
            var mgr = WebApiApplication.Manager;
            mgr.Unsave(userId, postId);
            return Ok();
        }
    }
}
