using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class RatingController : ApiController
    {
        // GET: api/Rating/{entityId}
        [HttpGet]
        [Route("api/Rating/{entityId}")]
        public IHttpActionResult Get(string entityId)
        {
            Guid guid;
            if (!Guid.TryParse(entityId, out guid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetTotalRating(guid));
        }

        // GET: api/Rating/{entityId}/{userId}
        [HttpGet]
        [Route("api/Rating/{entityId}/{userId}")]
        public IHttpActionResult Get(string entityId, string userId)
        {
            Guid entityGuid, userGuid;
            if (!Guid.TryParse(entityId, out entityGuid) || !Guid.TryParse(userId, out userGuid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetRatingByUser(userGuid, entityGuid));
        }

        // POST: api/Rating
        [HttpPost]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var typeExample = new { EntityId = Guid.Empty, UserId = Guid.Empty, Rating = (sbyte)0 };
            var rating = JsonConvert.DeserializeAnonymousType(value.ToString(), typeExample);
            var mgr = WebApiApplication.Manager;
            mgr.AddRating(rating.UserId, rating.EntityId, rating.Rating);
            return Ok();
        }

        // PUT: api/Rating
        [HttpPut]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var typeExample = new { EntityId = Guid.Empty, UserId = Guid.Empty, Rating = (sbyte)0 };
            var rating = JsonConvert.DeserializeAnonymousType(value.ToString(), typeExample);
            var mgr = WebApiApplication.Manager;
            mgr.UpdateRating(rating.UserId, rating.EntityId, rating.Rating);
            return Ok();
        }

        // DELETE: api/Rating/{entityId}/{userId}
        [HttpDelete]
        [Route("api/Rating/{entityId}/{userId}")]
        public IHttpActionResult Delete(string entityId, string userId)
        {
            Guid entityGuid, userGuid;
            if (!Guid.TryParse(entityId, out entityGuid) || !Guid.TryParse(userId, out userGuid))
                return Content(HttpStatusCode.BadRequest, "Invalid GUID format");
            var mgr = WebApiApplication.Manager;
            mgr.RemoveRating(userGuid, entityGuid);
            return Ok();
        }
    }
}
