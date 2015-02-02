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
        public DbGeography PolyGeography { get; set; }
        public DbGeometry PolyGeometry { get; set; }

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
                        PolyGeography = ce.POLY_GEOGRAPHY,
                        PolyGeometry = ce.POLY_GEOMETRY
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
                    .FirstOrDefault().POLY_GEOGRAPHY;

                adjEntities = db.COREENTITies
                    .Select(ce => new CoreEntity()
                    {
                        ID = ce.ID,
                        Name = ce.NAME,
                        BeginDate = ce.DATEBEGIN,
                        EndDate = ce.DATEEND,
                        PolyGeometry = ce.POLY_GEOMETRY,
                        PolyGeography = ce.POLY_GEOGRAPHY
                    })
                    .Where(ce => ce.PolyGeography.Distance(pivotPoly) <= unitDistance)
                    .ToList();              
            }

            return adjEntities;
        }

        /// <summary>
        /// Get all entities that do not have parents
        /// </summary>
        /// <returns></returns>
        public static List<CoreEntity> GetRootEntities(float buffer)
        {

            List<CoreEntity> coreEntities;
            using (var db = new proDev.EF.PRODEVEntities())
            {
                coreEntities = db.COREENTITies
                    //.Where(ce => DbGeometry.FromBinary().con  ce.GEOPOLY.
                    .Select(ce => new CoreEntity()
                    {
                        ID = ce.ID,
                        Name = ce.NAME,
                        BeginDate = ce.DATEBEGIN,
                        EndDate = ce.DATEEND,
                        PolyGeometry = ce.POLY_GEOMETRY,
                        PolyGeography = ce.POLY_GEOGRAPHY
                    })
                    .ToList();
            }

            return coreEntities;

        }
    

    }
}
