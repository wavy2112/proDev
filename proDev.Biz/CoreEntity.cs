using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.Spatial;

namespace proDev.Biz
{
    public class CoreEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DbGeography GeoPoly { get; set; }

        /// <summary>
        /// Get all core entities
        /// </summary>
        /// <returns></returns>
        public static List<CoreEntity> GetCoreEntities() {

            List<CoreEntity> coreEntities;
            using (var db = new proDev.EF.PRODEVEntities()) {
                coreEntities = db.COREENTITies
                    .Select(ce => new CoreEntity()
                    {
                        ID = ce.ID,
                        Name = ce.NAME,
                        BeginDate = ce.DATEBEGIN,
                        EndDate = ce.DATEEND,
                        GeoPoly = ce.GEOPOLY
                    })
                    .ToList();              
            }
            
            return coreEntities;

        }

        /// <summary>
        /// Get all coreentities within "unitDistance" of the supplied entity
        /// </summary>
        /// <param name="pivotEntityID"></param>
        /// <param name="unitDistance"></param>
        /// <returns></returns>
        public static List<CoreEntity> GetAdjacentEntities(int pivotEntityID, int unitDistance){
            DbGeography pivotPoly;

            List<CoreEntity> adjEntities;

            using (var db = new proDev.EF.PRODEVEntities())
            {
                pivotPoly = db.COREENTITies
                    .Where(ce => ce.ID  == pivotEntityID)
                    .FirstOrDefault().GEOPOLY;

                adjEntities = db.COREENTITies
                    .Select(ce => new CoreEntity()
                    {
                        ID = ce.ID,
                        Name = ce.NAME,
                        BeginDate = ce.DATEBEGIN,
                        EndDate = ce.DATEEND,
                        GeoPoly = ce.GEOPOLY
                    })
                    .Where(ce => ce.GeoPoly.Distance(pivotPoly) <= unitDistance)
                    .ToList();              
            }

            return adjEntities;
        }
    
    }
}
