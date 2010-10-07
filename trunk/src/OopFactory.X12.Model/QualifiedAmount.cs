using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Model
{
    public class QualifiedAmount
    {
        public CodedLookup Qualifier { get; set; }
        public decimal Amount { get; set; }
    }
}
