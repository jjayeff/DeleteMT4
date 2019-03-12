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
    public class NewsController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST News                                                       |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("news")]
        public dynamic PostFundamental([FromBody]Key value)
        {
            return Process("news", value);
        }

        // POST news?symbol={symbol}
        [HttpPost]
        [Route("news")]
        public dynamic PostNews(string symbol, [FromBody]Key value)
        {
            return Process($"news/symbol/{symbol}", value);
        }

        // POST news?symbol={symbol}&date={date}
        [HttpPost]
        [Route("news")]
        public dynamic PostNews(string symbol, string date, [FromBody]Key value)
        {
            return Process($"news/symbol&date/{symbol}/{date}", value);
        }

        // POST news/latest?symbol={symbol}
        [HttpPost]
        [Route("news/latest")]
        public dynamic PostNewsLatest(string symbol, [FromBody]Key value)
        {
            return Process($"news/latest/{symbol}", value);
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
                if (process.IndexOf("Wrong parameter input !!") > -1)
                {
                    string error = json.message;
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, error);
                }
                else
                    return json;
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Wrong AccessToken !!");
        }
    }
}
