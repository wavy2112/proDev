using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

using System.Configuration;
using System.Data.Spatial;

namespace proDev.Data
{
    public class MySQLTest
    {
        public static void TestConnect()
        {
            string connStr = ConfigurationManager.AppSettings["Temp_MySqlConnString"];

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT NAME, AsWKB(POLY_GEOGRAPHY) as POLYWKB FROM COREENTITY"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {                      
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                DbGeography dbg = DbGeography.MultiPolygonFromBinary( (byte[]) row["POLYWKB"], 4326);                               
                                Console.WriteLine("name: " + row["NAME"]);
                            }
                        }
                    }
                }
            }
            
        }
    
    }
}
