//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proDev.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class COREENTITY
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public System.Data.Spatial.DbGeography GEOPOLY { get; set; }
        public Nullable<System.DateTime> DATEBEGIN { get; set; }
        public Nullable<System.DateTime> DATEEND { get; set; }
    }
}
