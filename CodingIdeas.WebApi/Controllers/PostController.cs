using CodingIdeas.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class PostController : ApiController
    {
        [HttpGet]
        [Route("api/Post/InPage/{pageNumber}")]
        public IHttpActionResult Get(int pageNumber)
        {
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetPosts(pageNumber));
        }

        [HttpGet]
        [Route("api/Post/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            var post = mgr.GetPost(id);
            return Ok(post);
        }
        
        [HttpPost]
        [Route("api/Post")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var post = value.ToObject<Post>();
            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();
            mgr.AddPost(post);
            return Ok();
        }
        
        [HttpPut]
        [Route("api/Post")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var post = value.ToObject<Post>();
            mgr.UpdatePost(post.Id, post, PostProperties.All);
            return Ok();
        }
        
        [HttpDelete]
        [Route("api/Post/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            /*Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            mgr.RemovePost(guid);*/
            mgr.RemovePost(id);
            return Ok();
        }
    }
}
