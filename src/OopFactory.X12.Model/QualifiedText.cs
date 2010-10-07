using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Model
{
    public class QualifiedText
    {
        public CodedLookup Qualifier { get; set; }
        public string Text { get; set; }
    }
}
