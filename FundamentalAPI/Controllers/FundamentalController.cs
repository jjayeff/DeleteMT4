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

        // POST fundamental?symbol={symbol}
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamental(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/{symbol}", value);
        }

        // POST fundamental?year={year}
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamentalYear(string year, [FromBody]Key value)
        {
            return Process($"fundamental/year/{year}", value);
        }

        // POST fundamental?date={date}
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamentalDate(string date, [FromBody]Key value)
        {
            return Process($"fundamental/date/{date}", value);
        }

        // POST fundamental?symbol={symbol}&year={year}
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamental(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/{symbol}/{year}", value);
        }

        // POST fundamental?symbol={symbol}&date={date}
        [HttpPost]
        [Route("fundamental")]
        public dynamic PostFundamentalDate(string symbol, string date, [FromBody]Key value)
        {
            return Process($"fundamental/date/{symbol}/{date}", value);
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

        // POST fundamental/finance_info_yearly?symbol={symbol}
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearly(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/{symbol}", value);
        }

        // POST fundamental/finance_info_yearly?year={year}
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearlyYear(string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/year/{year}", value);
        }

        // POST fundamental/finance_info_yearly?date={date}
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearlyDate(string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/date/{date}", value);
        }

        // POST fundamental/finance_info_yearly?symbol={symbol}&year={year}
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearly(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/{symbol}/{year}", value);
        }

        // POST fundamental/finance_info_yearly?symbol={symbol}&date={date}
        [HttpPost]
        [Route("fundamental/finance_info_yearly")]
        public dynamic PostFinanceInfoYearlyDate(string symbol, string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_yearly/date/{symbol}/{date}", value);
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

        // POST fundamental/finance_info_quarter?symbol={symbol}
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarter(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/{symbol}", value);
        }

        // POST fundamental/finance_info_quarter?year={year}
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarterYear(string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/year/{year}", value);
        }

        // POST fundamental/finance_info_quarter?date={date}
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarterDate(string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/date/{date}", value);
        }

        // POST fundamental/finance_info_quarter?symbol={symbol}&year={year}
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarter(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/{symbol}/{year}", value);
        }

        // POST fundamental/finance_info_quarter?symbol={symbol}&date={date}
        [HttpPost]
        [Route("fundamental/finance_info_quarter")]
        public dynamic PostFinanceInfoQuarterDate(string symbol, string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_info_quarter/date/{symbol}/{date}", value);
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

        // POST fundamental/finance_stat_yearly?symbol={symbol}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearly(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/{symbol}", value);
        }

        // POST fundamental/finance_stat_yearly?year={year}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearlyYear(string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/year/{year}", value);
        }

        // POST fundamental/finance_stat_yearly?date={date}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearlyDate(string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/date/{date}", value);
        }

        // POST fundamental/finance_stat_yearly?symbol={symbol}&year={year}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearly(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/{symbol}/{year}", value);
        }

        // POST fundamental/finance_stat_yearly?symbol={symbol}&date={date}
        [HttpPost]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic PostFinancerStatYearlyDate(string symbol, string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_yearly/date/{symbol}/{date}", value);
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

        // POST fundamental/finance_stat_daily?symbol={symbol}
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDaily(string symbol, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/{symbol}", value);
        }

        // POST fundamental/finance_stat_daily?year={year}
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDailyYear(string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/year/{year}", value);
        }

        // POST fundamental/finance_stat_daily?date={date}
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDailyDate(string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/date/{date}", value);
        }

        // POST fundamental/finance_stat_daily?symbol={symbol}&year={year}
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDaily(string symbol, string year, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/{symbol}/{year}", value);
        }

        // POST fundamental/finance_stat_daily?symbol={symbol}&date={date}
        [HttpPost]
        [Route("fundamental/finance_stat_daily")]
        public dynamic PostFinancerStatDailyDate(string symbol, string date, [FromBody]Key value)
        {
            return Process($"fundamental/finance_stat_daily/date/{symbol}/{date}", value);
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
