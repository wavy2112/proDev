using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proDev.WebService.APIModel
{
    public class Parcel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Area { get; set; }
        public string GML { get; set; }
        public string WKT { get; set; }

        /// <summary>
        /// Read only property
        /// Get GeoJSON using simple conversion from WKT property
        /// </summary>
        public string GeoJSON { 

            get{
                List<string> coordList;
                string coordString = string.Empty;
                string[] coordPair = new string[2];
                string geoJSONCoords;

                coordString = WKT.Substring(WKT.IndexOf("(((") + 3);
                coordString = coordString.Substring(0, coordString.IndexOf(")))"));
                coordList = coordString.Split(',').ToList();

                //Create a string of the form
                //"[102.0, 2.0], [103.0, 2.0], [103.0, 3.0], [102.0, 3.0], [102.0, 2.0]"
                geoJSONCoords = string.Empty;
                foreach (var coord in coordList)
                {
                    coordPair = coord.Trim().Split(' ');
                    if (geoJSONCoords.Length > 0) geoJSONCoords = geoJSONCoords + ",";
                    geoJSONCoords = geoJSONCoords + "[" + coordPair[0] + "," + coordPair[1] + "]";
                }

                return "{ \"type\": \"MultiPolygon\"," +
                            "\"coordinates\":[[[" +
                            geoJSONCoords +
                            "]]]}";
            }
        }
    }
}