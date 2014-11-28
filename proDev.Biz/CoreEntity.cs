using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Spatial;

namespace proDev.Biz
{
    public class CoreEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DbGeography GeoPoly { get; set; }

        public static List<CoreEntity> GetCoreEntities() {

            List<CoreEntity> coreEntities = new List<CoreEntity>();
            /*
            var x = new proDev.EF.PRODEVEntities();
            var y = x.COREENTITies;
            */
            
            return coreEntities;

        }
    }
}
