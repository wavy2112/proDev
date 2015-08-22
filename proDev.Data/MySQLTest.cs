using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

using System.Data.Spatial;

namespace proDev.Data
{
    public class MySQLTest
    {
        public static void TestConnect()
        {
            using (DbConnection dbconn = DBProviderFacade.GetConnection())
            {
                try
                {
                    using (DbCommand cmd = dbconn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT NAME, AsWKB(POLY_GEOGRAPHY) as POLYWKB FROM COREENTITY";
                        cmd.CommandType = CommandType.Text;

                        dbconn.Open();
                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DbGeography dbg = DbGeography.MultiPolygonFromBinary((byte[])dr[1], 4326);
                                Console.WriteLine("name: " + dr[0]);
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
