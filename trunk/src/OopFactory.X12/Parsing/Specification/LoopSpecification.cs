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
    public class LoopSpecification
    {
        [XmlAttribute]
        public string LoopId { get; set; }
        [XmlAttribute]
        public UsageEnum Usage { get; set; }
        [XmlAttribute]
        public int LoopRepeat { get; set; }
        [XmlIgnore]
        public bool LoopRepeatSpecified { get; set; }

        public string Name { get; set; }
        public StartingSegment StartingSegment { get; set; }

        [XmlElement("SegmentSpecification")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        [XmlElement("LoopSpecification")]
        public List<LoopSpecification> LoopSpecifications { get; set; }
    }
}
