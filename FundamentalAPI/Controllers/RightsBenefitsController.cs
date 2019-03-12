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
    public class RightsBenefitsController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST Rights Benefits                                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("rights_benefits")]
        public dynamic PostFundamental([FromBody]Key value)
        {
            return Process("rights_benefits", value);
        }

        // POST rights_benefits?symbol={symbol}
        [HttpPost]
        [Route("rights_benefits")]
        public dynamic PostRightsBenefits(string symbol, [FromBody]Key value)
        {
            return Process($"rights_benefits/symbol/{symbol}", value);
        }

        // POST rights_benefits?symbol={symbol}&date={date}
        [HttpPost]
        [Route("rights_benefits")]
        public dynamic PostRightsBenefits(string symbol, string date, [FromBody]Key value)
        {
            return Process($"rights_benefits/symbol&date/{symbol}/{date}", value);
        }

        // POST rights_benefits?symbol={symbol}&sign={sign}
        [HttpPost]
        [Route("rights_benefits")]
        public dynamic PostRightsBenefitsSign(string symbol, string sign, [FromBody]Key value)
        {
            return Process($"rights_benefits/symbol&sign/{symbol}/{sign}", value);
        }

        // POST rights_benefits/latest?symbol={symbol}
        [HttpPost]
        [Route("rights_benefits/latest")]
        public dynamic PostNewsLatest(string symbol, [FromBody]Key value)
        {
            return Process($"rights_benefits/latest/{symbol}", value);
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
