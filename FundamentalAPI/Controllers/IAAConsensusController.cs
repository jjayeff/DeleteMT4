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
    public class IAAConsensusController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST IAA Consensus                                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("iaa_consensus")]
        public dynamic PostIAAConsensus([FromBody]Key value)
        {
            return Process("iaa_consensus", value);
        }

        // POST iaa_consensus?symbol={symbol}
        [HttpPost]
        [Route("iaa_consensus")]
        public dynamic PostIAAConsensus(string symbol, [FromBody]Key value)
        {
            return Process($"iaa_consensus/symbol/{symbol}", value);
        }

        // POST iaa_consensus?broker={broker}
        [HttpPost]
        [Route("iaa_consensus")]
        public dynamic PostIAAConsensusBroker(string broker, [FromBody]Key value)
        {
            return Process($"iaa_consensus/broker/{broker}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST IAA Consensus Summary                                      |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("iaa_consensus_summary")]
        public dynamic PostIAAConsensusSummary([FromBody]Key value)
        {
            return Process("iaa_consensus_summary", value);
        }

        // POST iaa_consensus_summary?symbol={symbol}
        [HttpPost]
        [Route("iaa_consensus_summary")]
        public dynamic PostIAAConsensusSummary(string symbol, [FromBody]Key value)
        {
            return Process($"iaa_consensus_summary/{symbol}", value);
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
