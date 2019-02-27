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
        // | GET fundamental                                                 |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpGet]
        [Route("fundamental")]
        public dynamic GetFundamental()
        {
            var client = new ClientSocket();
            var input = client.StartClient("fundamental");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/{symbol}
        [HttpGet]
        [Route("fundamental/{symbol}")]
        public dynamic GetFundamental(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/{symbol}/{year}
        [HttpGet]
        [Route("fundamental/{symbol}/{year}")]
        public dynamic GetFundamental(string symbol, string year)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/{symbol}/{year}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | GET fundamental/finance_info_yearly                             |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpGet]
        [Route("fundamental/finance_info_yearly")]
        public dynamic GetFinanceInfoYearly()
        {
            var client = new ClientSocket();
            var input = client.StartClient("fundamental/finance_info_yearly");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_info_yearly/{symbol}
        [HttpGet]
        [Route("fundamental/finance_info_yearly/{symbol}")]
        public dynamic GetFinanceInfoYearly(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_info_yearly/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_info_yearly/{symbol}/{year}
        [HttpGet]
        [Route("fundamental/finance_info_yearly/{symbol}/{year}")]
        public dynamic GetFinanceInfoYearly(string symbol, string year)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_info_yearly/{symbol}/{year}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | GET fundamental/finance_info_quarter                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpGet]
        [Route("fundamental/finance_info_quarter")]
        public dynamic GetFinanceInfoQuarter()
        {
            var client = new ClientSocket();
            var input = client.StartClient("fundamental/finance_info_quarter");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_info_quarter/{symbol}
        [HttpGet]
        [Route("fundamental/finance_info_quarter/{symbol}")]
        public dynamic GetFinanceInfoQuarter(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_info_quarter/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_info_quarter/{symbol}/{year}
        [HttpGet]
        [Route("fundamental/finance_info_quarter/{symbol}/{year}")]
        public dynamic GetFinanceInfoQuarter(string symbol,string year)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_info_quarter/{symbol}/{year}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | GET fundamental/finance_stat_yearly                             |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpGet]
        [Route("fundamental/finance_stat_yearly")]
        public dynamic GetFinancerStatYearly()
        {
            var client = new ClientSocket();
            var input = client.StartClient("fundamental/finance_stat_yearly");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_stat_yearly/{symbol}
        [HttpGet]
        [Route("fundamental/finance_stat_yearly/{symbol}")]
        public dynamic GetFinancerStatYearly(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_stat_yearly/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_stat_yearly/{symbol}/{year}
        [HttpGet]
        [Route("fundamental/finance_stat_yearly/{symbol}/{year}")]
        public dynamic GetFinancerStatYearly(string symbol,string year)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_stat_yearly/{symbol}/{year}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | GET fundamental/finance_stat_dail                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        [HttpGet]
        [Route("fundamental/finance_stat_daily")]
        public dynamic GetFinancerStatDaily()
        {
            var client = new ClientSocket();
            var input = client.StartClient("fundamental/finance_stat_daily");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_stat_daily/{symbol}
        [HttpGet]
        [Route("fundamental/finance_stat_daily/{symbol}")]
        public dynamic GetFinancerStatDaily(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_stat_daily/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET fundamental/finance_stat_daily/{symbol}/{year}
        [HttpGet]
        [Route("fundamental/finance_stat_daily/{symbol}/{year}")]
        public dynamic GetFinancerStatDaily(string symbol, string year)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"fundamental/finance_stat_daily/{symbol}/{year}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

    }
}
