using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Data;
using System.Data.Common;

using System.Data.Spatial;

namespace proDev.Data
{
    public class DBProviderTest
    {
        /// <summary>
        /// Test connection to database
        /// Access to MySQL and MS SQL Server DB providers are implemented.
        /// Comment/uncomment "PRODEVADONet" connection strings in Web.config in proDev.Web
        /// to switch between DB providers
        /// </summary>
        public static void TestConnect()
        {
            using (DbConnection dbconn = DBProviderFacade.GetConnection())
            {
                try
                {
                    using (DbCommand cmd = dbconn.CreateCommand())
                    {

                        if (DBProviderFacade.GetCurrentDBProvider() == DBProviderFacade.DBProviders.MySQL)
                            cmd.CommandText = "SELECT NAME, AsWKB(POLY_GEOGRAPHY) as POLYWKB FROM COREENTITY";
                        else
                            cmd.CommandText = "SELECT NAME, POLY_GEOGRAPHY.STAsBinary() as POLYWKB FROM COREENTITY";
                        
                        cmd.CommandType = CommandType.Text;

                        dbconn.Open();
                        //Use DBDataReader for read only access
                        //Use DBDataAdapter for read/write
                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DbGeography dbg = DbGeography.MultiPolygonFromBinary((byte[])dr[1], 4326);
                                Debug.WriteLine("name: " + dr[0]);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception.Message: {0}", ex.Message);
                }
            }


        }
    }
}
