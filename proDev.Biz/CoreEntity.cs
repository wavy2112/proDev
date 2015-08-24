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

proDev.Data.DBProviderTest.TestConnect();            

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
        public static List<CoreEntity> GetRootEntities(double buffer)
        {
            //This query is run as raw SQL because some spatial calls (such as Geography.STWithin) are not
            //available in this version of entity framework but are available as SQL

            //Shrink the poly on left side (ce). If, after it has been shrunk, it is still not contained
            //by another poly (parentID is null) then it is a top level poly

            //This SQL also works
            //select c1.* from [COREENTITY] c1
            //where not exists 
            //    (select [ID] from [COREENTITY] c2 
            //     where c2.POLY_GEOMETRY.STContains(c1.POLY_GEOMETRY.STBuffer(-.0001)) = 1  and c1.id <> c2.id)
            
            List<CoreEntity> rootEntities;

            using (var db = new proDev.EF.PRODEVEntities())
            {
                rootEntities = db.COREENTITies.SqlQuery("select ce.* " +
                                            " from [COREENTITY] ce " +
                                            " left outer join [COREENTITY] parent " +
                                            " on ce.[POLY_GEOGRAPHY].STBuffer(@p0).STWithin(parent.[POLY_GEOGRAPHY]) = 1 " +
                                            " and ce.[ID] <> parent.[ID] " +
                                            " where parent.ID is null " +
                                            " order by ce.[ID]", buffer)
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
            
            return rootEntities;

        }
    

    }
}
