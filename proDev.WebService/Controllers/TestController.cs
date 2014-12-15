using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace proDev.WebService.Controllers
{
    public class TestController : ApiController
    {
        // GET <controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST <controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT <controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE <controller>/5
        public void Delete(int id)
        {
        }
    }
}