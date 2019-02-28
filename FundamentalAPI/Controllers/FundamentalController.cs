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
    public class FundamentalController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST fundamental                                                |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamental([FromBody]Key value)
        {
            return Process("fundamental", value);
        }

        // POST fundamental/{symbol}
        [HttpPost]
        [Route("fundamental/{symbol}")]
        public dynamic PostFundamental(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/{symbol}", value);
        }

        // POST fundamental/{symbol}/{year}
        [HttpPost]
        [Route("fundamental/{symbol}/{year}")]
        public dynamic PostFundamental(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/{symbol}/{year}", value);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST fundamental/finance_info_yearly                             |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearly([FromBody]Key value)
        {
            return Process("fundamental/finance_info_yearly", value);
        }

        // POST fundamental/finance_info_yearly/{symbol}
        [HttpPost]
        [Route("fundamental/finance_info_yearly/{symbol}")]
        public dynamic PostFinanceInfoYearly(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/{symbol}", value);
        }

        // POST fundamental/finance_info_yearly/{symbol}/{year}
        [HttpPost]
        [Route("fundamental/finance_info_yearly/{symbol}/{year}")]
        public dynamic PostFinanceInfoYearly(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/{symbol}/{year}", value);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST fundamental/finance_info_quarter                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarter([FromBody]Key value)
        {
            return Process("fundamental/finance_info_quarter", value);
        }

        // POST fundamental/finance_info_quarter/{symbol}
        [HttpPost]
        [Route("fundamental/finance_info_quarter/{symbol}")]
        public dynamic PostFinanceInfoQuarter(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/{symbol}", value);
        }

        // POST fundamental/finance_info_quarter/{symbol}/{year}
        [HttpPost]
        [Route("fundamental/finance_info_quarter/{symbol}/{year}")]
        public dynamic PostFinanceInfoQuarter(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/{symbol}/{year}", value);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST fundamental/finance_stat_yearly                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearly([FromBody]Key value)
        {
            return Process("fundamental/finance_stat_yearly", value);
        }

        // POST fundamental/finance_stat_yearly/{symbol}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly/{symbol}")]
        public dynamic PostFinancerStatYearly(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/{symbol}", value);
        }

        // POST fundamental/finance_stat_yearly/{symbol}/{year}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly/{symbol}/{year}")]
        public dynamic PostFinancerStatYearly(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/{symbol}/{year}", value);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST fundamental/finance_stat_dail                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDaily([FromBody]Key value)
        {
            return Process("fundamental/finance_stat_daily", value);
        }

        // POST fundamental/finance_stat_daily/{symbol}
        [HttpPost]
        [Route("fundamental/finance_stat_daily/{symbol}")]
        public dynamic PostFinancerStatDaily(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/{symbol}", value);
        }

        // POST fundamental/finance_stat_daily/{symbol}/{year}
        [HttpPost]
        [Route("fundamental/finance_stat_daily/{symbol}/{year}")]
        public dynamic PostFinancerStatDaily(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/{symbol}/{year}", value);
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
