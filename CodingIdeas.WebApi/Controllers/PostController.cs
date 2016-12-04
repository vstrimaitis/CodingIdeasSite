using CodingIdeas.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class PostController : ApiController
    {
        // GET: api/Post/1
        [HttpGet]
        public IEnumerable<Post> Get(int pageNumber)
        {
            var mgr = WebApiApplication.Manager;
            return mgr.GetPosts(pageNumber);
        }

        // GET: api/Post/abdsdf-sdfsdf-sdfsdf-sdfsdf
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            int intId;
            if (int.TryParse(id, out intId))
                return Ok(Get(intId));
            var mgr = WebApiApplication.Manager;
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var post = mgr.GetPost(guid);
            return Ok(post);
        }

        // POST: api/Post
        [HttpPost]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var post = value.ToObject<Post>();
            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();
            mgr.AddPost(post);
            return Ok();
        }

        // PUT: api/Post
        [HttpPut]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var mgr = WebApiApplication.Manager;
            var post = value.ToObject<Post>();
            mgr.UpdatePost(post.Id, post, PostProperties.All);
            return Ok();
        }

        // DELETE: api/Post/abdsdf-sdfsdf-sdfsdf-sdfsdf
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var mgr = WebApiApplication.Manager;
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            mgr.RemovePost(guid);
            return Ok();
        }
    }
}
