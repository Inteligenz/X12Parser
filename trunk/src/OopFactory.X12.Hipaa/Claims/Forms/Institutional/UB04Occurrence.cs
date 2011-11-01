using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Occurrence
    {
        public string Code { get; set; }
        public string Date { get; set; }

        public UB04Occurrence CopyFrom(UB04Occurrence source)
        {
            Code = source.Code;
            Date = source.Date;
            return this;
        }

        public UB04Occurrence CopyFrom(CodedDate source)
        {
            Code = source.Code;
            Date = String.Format("{0:MMddyy}", source.Date);
            return this;
        }

        public UB04Occurrence CopyFrom(InstitutionalProcedure source)
        {
            Code = source.Code;
            Date = String.Format("{0:MMddyy}", source.Date);
            return this;
        }
    }
}
