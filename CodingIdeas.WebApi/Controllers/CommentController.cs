using CodingIdeas.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class CommentController : ApiController
    {
        // GET: api/Comment/abdsdf-sdfsdf-sdfsdf-sdfsdf/1
        [HttpGet]
        [Route("api/Comment/{postId}/{pageNumber}")]
        public IHttpActionResult Get(string postId, int pageNumber)
        {
            Guid guid;
            if (!Guid.TryParse(postId, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetComments(guid, pageNumber));
        }

        // POST: api/Comment
        [HttpPost]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var comment = value.ToObject<Comment>();
            if (comment.Id == Guid.Empty)
                comment.Id = Guid.NewGuid();
            mgr.AddComment(comment);
            return Ok();
        }

        // PUT: api/Comment
        [HttpPut]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var comment = value.ToObject<Comment>();
            mgr.UpdateComment(comment.Id, comment.Content);
            return Ok();
        }

        // DELETE: api/Comment/abdsdf-sdfsdf-sdfsdf-sdfsdf
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var mgr = WebApiApplication.Manager;
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            mgr.RemoveComment(guid);
            return Ok();
        }
    }
}
