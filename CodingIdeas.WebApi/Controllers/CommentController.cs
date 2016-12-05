using CodingIdeas.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class CommentController : ApiController
    {
        [HttpGet]
        [Route("api/Comment/{postId}/{pageNumber}")]
        public IHttpActionResult Get(Guid postId, int pageNumber)
        {
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetComments(postId, pageNumber));
        }
        
        [HttpPost]
        [Route("api/Comment")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var comment = value.ToObject<Comment>();
            if (comment.Id == Guid.Empty)
                comment.Id = Guid.NewGuid();
            mgr.AddComment(comment);
            return Ok();
        }
        
        [HttpPut]
        [Route("api/Comment")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var comment = value.ToObject<Comment>();
            mgr.UpdateComment(comment.Id, comment.Content);
            return Ok();
        }
        
        [HttpDelete]
        [Route("api/Comment/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            mgr.RemoveComment(id);
            return Ok();
        }
    }
}
