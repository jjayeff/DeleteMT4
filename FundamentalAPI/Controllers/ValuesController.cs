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
    public class ValuesController : ApiController
    {
        // GET api/values
        public dynamic Get()
        {
            var client = new ClientSocket();
            var input = client.StartClient("api/values");
            dynamic json = JsonConvert.DeserializeObject(input);
            return json;
        }

        // GET
        [HttpGet]
        [Route("api/garmin/data")]
        public string GetGarminData()
        {
            return "value";
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
