using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;

using System.Configuration;

namespace proDev.Data
{
    public static class DBProviderFacade
    {
        /// <summary>
        /// Return a dbprovider factory using provider specified in config
        /// </summary>
        /// <returns></returns>
        public static DbProviderFactory GetFactory() {
            string providerName = ConfigurationManager.ConnectionStrings["PRODEVADONet"].ProviderName;
            DbProviderFactory factory = null;
            try
            {
                factory = DbProviderFactories.GetFactory(providerName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return factory;            
        }

        /// <summary>
        /// Return a dbconnection using provider specified in config
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetConnection() {
            string connStr = ConfigurationManager.ConnectionStrings["PRODEVADONet"].ConnectionString;

            DbConnection dbconn = null;
            if (connStr != null)
            {
                try
                {
                    DbProviderFactory factory = GetFactory();
                    dbconn = factory.CreateConnection();
                    dbconn.ConnectionString = connStr;
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created. 
                    if (dbconn != null)
                        dbconn = null;
                    Console.WriteLine(ex.Message);
                }
            }
            return dbconn;
        }

    }
}
