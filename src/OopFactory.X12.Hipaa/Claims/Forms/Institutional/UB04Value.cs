using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Value
    {
        public string Code { get; set; }
        public decimal? Amount { get; set; }

        public UB04Value CopyFrom(CodedAmount source)
        {
            Code = source.Code;
            Amount = source.Amount;
            return this;
        }
    }
}
