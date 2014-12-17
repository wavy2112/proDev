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
    }
}