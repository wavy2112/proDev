using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using proDev.Biz;
using proDev.WebService.APIModel;

namespace proDev.WebService.Controllers
{
    public class ParcelController : ApiController
    {
        // GET api/parcel
        public IEnumerable<Parcel> Get()
        {
            double areaSum = 0;
            List<Parcel> parcels = new List<APIModel.Parcel>();
            List<CoreEntity> coreEntities = CoreEntity.GetAdjacentEntities(2362, 10000);

            foreach (var entity in coreEntities)
            {
                parcels.Add(new Parcel(){
                    ID = entity.ID,
                    Name = entity.Name,
                    BeginDate = entity.BeginDate,
                    EndDate = entity.EndDate,
                    Area = entity.GeoPoly.Area.Value,
                    GML = entity.GeoPoly.AsGml(),
                    WKT = entity.GeoPoly.AsText()
                });
                areaSum += entity.GeoPoly.Area.Value;
            }

            return parcels;
        }

        // GET api/parcel/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/parcel
        public void Post([FromBody]string value)
        {
        }

        // PUT api/parcel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/parcel/5
        public void Delete(int id)
        {
        }
    }
}
