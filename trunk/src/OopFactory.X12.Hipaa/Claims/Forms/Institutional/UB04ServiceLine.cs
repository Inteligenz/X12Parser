using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04ServiceLine
    {
        public string Field42_RevenueCode { get; set; }
        public string Field43_Description { get; set; }
        public string Field44_ProcedureCodes { get; set; }
        public string Field45_ServiceDate { get; set; }
        public string Field46_ServiceUnits { get; set; }
        public decimal? Field47_TotalCharges { get; set; }
        public decimal? Field48_NonCoveredCharges { get; set; }
        public string Field49 { get; set; }
    }
}
