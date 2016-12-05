using CodingIdeas.Core;
using CodingIdeas.WebApi.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CodingIdeas.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private const string _passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,64}$";
        [HttpGet]
        [Route("api/User/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            var user = mgr.GetUser(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("api/User/Validate/{login}/{password}")]
        public IHttpActionResult Get(string login, string password)
        {
            var mgr = WebApiApplication.Manager;
            var id = mgr.GetUserId(login, Hasher.ComputeSha256(password));
            return Ok(id);
        }

        [HttpGet]
        [Route("api/User/MostActivePosters/{howMany}")]
        public IHttpActionResult GetMostActivePosters(int howMany)
        {
            var mgr = WebApiApplication.Manager;
            return Ok(mgr.GetMostActivePosters(howMany));
        }

        [HttpPost]
        [Route("api/User")]
        public IHttpActionResult Post([FromBody]JObject value)
        {
            var user = value.ToObject<User>();
            var mgr = WebApiApplication.Manager;
            if (user.Id == Guid.Empty)
                user.Id = Guid.NewGuid();
            if (user.Password == null || !Regex.IsMatch(user.Password, _passwordPattern))
                return Content(System.Net.HttpStatusCode.Forbidden, "Invalid password");
            user.Password = Hasher.ComputeSha256(user.Password);
            mgr.AddUser(user);
            return Ok(new { Id = user.Id });
        }

        [HttpPut]
        [Route("api/User")]
        public IHttpActionResult Put([FromBody]JObject value)
        {
            var user = value.ToObject<User>();
            var mgr = WebApiApplication.Manager;
            UserProperties props = UserProperties.None;
            if (value[nameof(Core.User.DateOfBirth)] != null)
                props |= UserProperties.DateOfBirth;
            if (value[nameof(Core.User.Email)] != null)
                props |= UserProperties.Email;
            if (value[nameof(Core.User.FirstName)] != null)
                props |= UserProperties.FirstName;
            if (value[nameof(Core.User.LastName)] != null)
                props |= UserProperties.LastName;
            if (value[nameof(Core.User.Username)] != null)
                props |= UserProperties.Username;
            if (value[nameof(Core.User.Password)] != null)
            {
                props |= UserProperties.Password;
                if (user.Password == null || !Regex.IsMatch(user.Password, _passwordPattern))
                    return Content(System.Net.HttpStatusCode.Forbidden, "Invalid password");
                user.Password = Hasher.ComputeSha256(user.Password);
            }
            mgr.UpdateUser(user.Id, user, props);
            return Ok();
        }

        [HttpDelete]
        [Route("api/User/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var mgr = WebApiApplication.Manager;
            mgr.RemoveUser(id);
            return Ok();
        }
    }
}
