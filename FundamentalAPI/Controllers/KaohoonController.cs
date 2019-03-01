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
    public class KaohoonController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST Kaohoon                                                    |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("kaohoon")]
        public dynamic PostKaohoon([FromBody]Key value)
        {
            return Process("kaohoon", value);
        }

        // POST kaohoon?symbol={symbol}
        [HttpPost]
        [Route("kaohoon")]
        public dynamic PostFundamental(string symbol, [FromBody]Key value)
        {
            return Process($"kaohoon/{symbol}", value);
        }

        // POST kaohoon?&date={date}
        [HttpPost]
        [Route("kaohoon")]
        public dynamic PostFundamentalDate(string date, [FromBody]Key value)
        {
            return Process($"kaohoon/date/{date}", value);
        }

        // POST kaohoon?symbol={symbol}&date={date}
        [HttpPost]
        [Route("kaohoon")]
        public dynamic PostFundamental(string symbol, string date, [FromBody]Key value)
        {
            return Process($"kaohoon/{symbol}/{date}", value);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public dynamic Process(string input, Key value)
        {
            if (value == null)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Please POST json data !!");
            var client = new ClientSocket();
            if (Convert.ToBoolean(client.StartClient(JsonConvert.SerializeObject(value))))
            {
                var process = client.StartClient(input);
                dynamic json = JsonConvert.DeserializeObject(process);
                return json;
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Wrong AccessToken !!");
        }
    }
}
