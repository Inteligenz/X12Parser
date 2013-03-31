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
    public class LoopSpecification : IContainerSpecification
    {
        public LoopSpecification()
        {
            if (SegmentSpecifications == null) SegmentSpecifications = new List<SegmentSpecification>();
            if (LoopSpecifications == null) LoopSpecifications = new List<LoopSpecification>();
        }

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

        [XmlElement("Segment")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        [XmlElement("Loop")]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        [XmlElement("HierarchicalLoop")]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }
    }
}
