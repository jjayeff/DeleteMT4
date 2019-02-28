using FundamentalAPI.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FundamentalAPI.Controllers
{
    public class AccessTokenController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Authorization
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | GET fundamental                                                 |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("accessToken")]
        public dynamic PostAccessToken([FromBody]Authorization value)
        {
            var security = new EncryptionHelper();
            value.Username = security.Encrypt(value.Username);
            value.Password = security.Encrypt(value.Password);
            if (value == null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Please POST json data !!");
            var client = new ClientSocket();
            var result = client.StartClient(JsonConvert.SerializeObject(value));
            if (result != "false")
                return new Key() { AccessToken = result };
            else
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Wrong User or Password !!");
        }
    }
}
