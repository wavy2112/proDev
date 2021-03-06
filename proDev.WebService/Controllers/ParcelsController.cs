﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using proDev.Biz;
using proDev.WebService.APIModel;

namespace proDev.WebService.Controllers
{
    public class ParcelsController : ApiController
    {        
        // GET WS/Parcels/{id}/Adjacent
        /// <summary>
        /// Get all entities within the supplied distance from the supplied parcel
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [Route("Parcels/{parcelID:int}/Adjacent")]
        [HttpGet]
        public IEnumerable<Parcel> Adjacent(int parcelID, int distance)
        {
            List<Parcel> parcels = new List<APIModel.Parcel>();
            List<CoreEntity> coreEntities = CoreEntity.GetAdjacentEntities(parcelID, distance);

            foreach (var entity in coreEntities)
            {
                parcels.Add(new Parcel()
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    BeginDate = entity.BeginDate,
                    EndDate = entity.EndDate,
                    Area = entity.PolyGeography.Area.Value,
                    GML = entity.PolyGeography.AsGml(),
                    WKT = entity.PolyGeography.AsText()
                });
            }

            return parcels;
        }

        // GET WS/Parcels/Root/{tolerance}
        /// <summary>
        /// Get root/top-level parcels (parcels that do not have a parent)
        /// </summary>
        /// <param name="tolerance"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [Route("Parcels/Root/{tolerance:int}")]
        [HttpGet]
        public IEnumerable<Parcel> Root(int tolerance = 1)
        {

            List<Parcel> parcels = new List<APIModel.Parcel>();
            List<CoreEntity> coreEntities = CoreEntity.GetRootEntities(-.0001 * tolerance);

            foreach (var entity in coreEntities)
            {
                parcels.Add(new Parcel()
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    BeginDate = entity.BeginDate,
                    EndDate = entity.EndDate,
                    Area = entity.PolyGeography.Area.Value,
                    GML = entity.PolyGeography.AsGml(),
                    WKT = entity.PolyGeography.AsText()
                });
            }

            return parcels;
        }
        

        // GET WS/Parcels
        /// <summary>
        /// Get all parcels
        /// </summary>
        /// <returns>JSON of all parcels</returns>
        public IEnumerable<Parcel> Get()
        {
            double areaSum = 0;
            List<Parcel> parcels = new List<APIModel.Parcel>();
            List<CoreEntity> coreEntities = CoreEntity.GetCoreEntities();

            foreach (var entity in coreEntities)
            {
                parcels.Add(new Parcel()
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    BeginDate = entity.BeginDate,
                    EndDate = entity.EndDate,
                    Area = entity.PolyGeography.Area.Value,
                    GML = entity.PolyGeography.AsGml(),
                    WKT = entity.PolyGeography.AsText()
                });
                areaSum += entity.PolyGeography.Area.Value;
            }

            return parcels;
        }

        // GET api/parcels/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/parcels
        public void Post([FromBody]string value)
        {
        }

        // PUT api/parcels/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/parcels/5
        public void Delete(int id)
        {
        }
    }
}
