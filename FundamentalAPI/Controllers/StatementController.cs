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
    public class StatementController : ApiController
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class Key
        {
            public string AccessToken { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement                                                |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement")]
        public dynamic PostStatement([FromBody]Key value)
        {
            return Process("statement", value);
        }

        // POST statement?symbol={symbol}
        [HttpPost]
        [Route("statement")]
        public dynamic PostStatement(string symbol, [FromBody]Key value)
        {
            return Process($"statement/symbol/{symbol}", value);
        }

        // POST statement/lastupdate
        [HttpPost]
        [Route("statement/lastupdate")]
        public dynamic PostStatementLastUpdate([FromBody]Key value)
        {
            return Process($"statement/lastupdate", value);
        }

        // POST statement/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/lastupdate")]
        public dynamic PostStatementLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/asset                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/asset")]
        public dynamic PostStatementAssets([FromBody]Key value)
        {
            return Process("statement/asset", value);
        }

        // POST statement/asset?symbol={symbol}
        [HttpPost]
        [Route("statement/asset")]
        public dynamic PostStatementAssets(string symbol, [FromBody]Key value)
        {
            return Process($"statement/asset/symbol/{symbol}", value);
        }

        // POST statement/asset/lastupdate
        [HttpPost]
        [Route("statement/asset/lastupdate")]
        public dynamic PostStatementAssetsLastUpdate([FromBody]Key value)
        {
            return Process($"statement/asset/lastupdate", value);
        }

        // POST statement/asset/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/asset/lastupdate")]
        public dynamic PostStatementAssetsLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/asset/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/debt                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/debt")]
        public dynamic PostStatementDebt([FromBody]Key value)
        {
            return Process("statement/debt", value);
        }

        // POST statement/debt?symbol={symbol}
        [HttpPost]
        [Route("statement/debt")]
        public dynamic PostStatementDebt(string symbol, [FromBody]Key value)
        {
            return Process($"statement/debt/symbol/{symbol}", value);
        }

        // POST statement/debt/lastupdate
        [HttpPost]
        [Route("statement/debt/lastupdate")]
        public dynamic PostStatementDebtLastUpdate([FromBody]Key value)
        {
            return Process($"statement/debt/lastupdate", value);
        }

        // POST statement/debt/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/debt/lastupdate")]
        public dynamic PostStatementDebtLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/debt/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/share_holder_equity                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/share_holder_equity")]
        public dynamic PostStatementShareHolderEquity([FromBody]Key value)
        {
            return Process("statement/share_holder_equity", value);
        }

        // POST statement/share_holder_equity?symbol={symbol}
        [HttpPost]
        [Route("statement/share_holder_equity")]
        public dynamic PostStatementShareHolderEquity(string symbol, [FromBody]Key value)
        {
            return Process($"statement/share_holder_equity/symbol/{symbol}", value);
        }

        // POST statement/share_holder_equity/lastupdate
        [HttpPost]
        [Route("statement/share_holder_equity/lastupdate")]
        public dynamic PostStatementShareHolderEquityLastUpdate([FromBody]Key value)
        {
            return Process($"statement/share_holder_equity/lastupdate", value);
        }

        // POST statement/share_holder_equity/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/share_holder_equity/lastupdate")]
        public dynamic PostStatementShareHolderEquityLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/share_holder_equity/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/income                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/income")]
        public dynamic PostStatementIncome([FromBody]Key value)
        {
            return Process("statement/income", value);
        }

        // POST statement/income?symbol={symbol}
        [HttpPost]
        [Route("statement/income")]
        public dynamic PostStatementIncome(string symbol, [FromBody]Key value)
        {
            return Process($"statement/income/symbol/{symbol}", value);
        }

        // POST statement/income/lastupdate
        [HttpPost]
        [Route("statement/income/lastupdate")]
        public dynamic PostStatementIncomeLastUpdate([FromBody]Key value)
        {
            return Process($"statement/income/lastupdate", value);
        }

        // POST statement/income/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/income/lastupdate")]
        public dynamic PostStatementIncomeLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/income/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/comprehensive_income                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/comprehensive_income")]
        public dynamic PostStatementComprehensiveIncome([FromBody]Key value)
        {
            return Process("statement/comprehensive_income", value);
        }

        // POST statement/comprehensive_income?symbol={symbol}
        [HttpPost]
        [Route("statement/comprehensive_income")]
        public dynamic PostStatementComprehensiveIncome(string symbol, [FromBody]Key value)
        {
            return Process($"statement/comprehensive_income/symbol/{symbol}", value);
        }

        // POST statement/comprehensive_income/lastupdate
        [HttpPost]
        [Route("statement/comprehensive_income/lastupdate")]
        public dynamic PostStatementComprehensiveIncomeLastUpdate([FromBody]Key value)
        {
            return Process($"statement/comprehensive_income/lastupdate", value);
        }

        // POST statement/comprehensive_income/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/comprehensive_income/lastupdate")]
        public dynamic PostStatementComprehensiveIncomeLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/comprehensive_income/lastupdate&symbol/{symbol}", value);
        }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | POST statement/cash_flow                              |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpPost]
        [Route("statement/cash_flow")]
        public dynamic PostStatementCashFlow([FromBody]Key value)
        {
            return Process("statement/cash_flow", value);
        }

        // POST statement/cash_flow?symbol={symbol}
        [HttpPost]
        [Route("statement/cash_flow")]
        public dynamic PostStatementCashFlow(string symbol, [FromBody]Key value)
        {
            return Process($"statement/cash_flow/symbol/{symbol}", value);
        }

        // POST statement/cash_flow/lastupdate
        [HttpPost]
        [Route("statement/cash_flow/lastupdate")]
        public dynamic PostStatementCashFlowLastUpdate([FromBody]Key value)
        {
            return Process($"statement/cash_flow/lastupdate", value);
        }

        // POST statement/cash_flow/lastupdate?symbol={symbol}
        [HttpPost]
        [Route("statement/cash_flow/lastupdate")]
        public dynamic PostStatementCashFlowIncomeLastUpdate(string symbol, [FromBody]Key value)
        {
            return Process($"statement/cash_flow/lastupdate&symbol/{symbol}", value);
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
