using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class RatingController : ApiController
    {
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
        
        [HttpPost]
        [Route("api/Rating")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var typeExample = new { EntityId = Guid.Empty, UserId = Guid.Empty, Rating = (sbyte)0 };
            var rating = JsonConvert.DeserializeAnonymousType(value.ToString(), typeExample);
            var mgr = WebApiApplication.Manager;
            mgr.AddRating(rating.UserId, rating.EntityId, rating.Rating);
            return Ok();
        }
        
        [HttpPut]
        [Route("api/Rating")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var typeExample = new { EntityId = Guid.Empty, UserId = Guid.Empty, Rating = (sbyte)0 };
            var rating = JsonConvert.DeserializeAnonymousType(value.ToString(), typeExample);
            var mgr = WebApiApplication.Manager;
            mgr.UpdateRating(rating.UserId, rating.EntityId, rating.Rating);
            return Ok();
        }
        
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
