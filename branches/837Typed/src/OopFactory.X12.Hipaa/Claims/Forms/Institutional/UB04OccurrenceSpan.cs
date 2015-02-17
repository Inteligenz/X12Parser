using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04OccurrenceSpan
    {
        public string Code { get; set; }
        public string FromDate { get; set; }
        public string ThroughDate { get; set; }

        public UB04OccurrenceSpan CopyFrom(CodedDate source)
        {
            Code = source.Code;
            FromDate = String.Format("{0:MMddyy}", source.Date);
            return this;
        }

        public UB04OccurrenceSpan CopyFrom(CodedDateRange source)
        {
            Code = source.Code;
            FromDate = String.Format("{0:MMddyy}", source.FromDate);
            ThroughDate = String.Format("{0:MMddyy}", source.ThroughDate);
            return this;
        }
 
    }
}
