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
    public class HierarchicalLoopSpecification
    {

        [XmlAttribute]
        public string LoopId { get; set; }

        [XmlAttribute]
        public string LevelCode { get; set; }

        [XmlAttribute]
        public UsageEnum Usage { get; set; }

        public string Name { get; set; }

        [XmlElement("SegmentSpecification")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        [XmlElement("LoopSpecification")]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        [XmlElement("HierarchicalLoopSpecification")]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }
    }
}
