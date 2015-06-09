using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    [DebuggerStepThrough()]
    [XmlType(AnonymousType = true)]
    public class SegmentSpecificationStandard
    {
        [XmlAttribute]
        public string Position { get; set; }

        [XmlAttribute]
        public RequirementEnum Requirement { get; set; }

        [XmlIgnore]
        public bool RequirementSpecified { get; set; }

        [XmlAttribute]
        public int MaxUse { get; set; }

        [XmlIgnore]
        public bool MaxUseSpecified { get; set; }
    }
}
