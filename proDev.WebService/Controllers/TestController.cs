using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using proDev.Biz;

namespace proDev.WebService.Controllers
{
    public class TestController : ApiController
    {
        // GET <controller>
        public List<string> Get()
        {
            double areaSum = 0;
            List<string> entities = new List<string>();

            //            List<CoreEntity> coreEntities = CoreEntity.GetCoreEntities();

            List<CoreEntity> coreEntities = CoreEntity.GetAdjacentEntities(2362, 10000);

            foreach (var entity in coreEntities)
            {
                entities.Add("Entity: " + entity.Name + " | Area: " + entity.GeoPoly.Area.Value + " | " + entity.BeginDate.ToString() + " to " + entity.EndDate.ToString());
                areaSum += entity.GeoPoly.Area.Value;
            }
            
            return entities;
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