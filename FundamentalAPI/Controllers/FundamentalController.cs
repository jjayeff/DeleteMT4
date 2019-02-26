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
        // GET
        [HttpGet]
        [Route("api/fundamental")]
        public dynamic GetFundamental()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/Fundamental");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/{symbol}")]
        public dynamic GetFundamentalBySymbol(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient($"api/fundamental/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_info_yearly")]
        public dynamic GetFinanceInfoYearly()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_info_yearly");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_info_yearly/{symbol}")]
        public dynamic GetFinanceInfoYearly(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_info_yearly/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_info_quarter")]
        public dynamic GetFinanceInfoQuarter()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_info_quarter");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_info_quarter/{symbol}")]
        public dynamic GetFinanceInfoQuarter(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_info_quarter/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_stat_yearly")]
        public dynamic GetFinancerStatYearly()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_stat_yearly");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_stat_yearly/{symbol}")]
        public dynamic GetFinancerStatYearly(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_stat_yearly/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_stat_daily")]
        public dynamic GetFinancerStatDaily()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_stat_daily");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/fundamental/finance_stat_daily/{symbol}")]
        public dynamic GetFinancerStatDaily(string symbol)
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/fundamental/finance_stat_daily/{symbol}");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

    }
}
