namespace OopFactory.X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications.Enumerations;
    using OopFactory.X12.Specifications.Interfaces;

    [DebuggerStepThrough()]
    [XmlType(AnonymousType = true)]
    public class HierarchicalLoopSpecification : IContainerSpecification
    {

        [XmlAttribute]
        public string LoopId { get; set; }

        [XmlAttribute]
        public string LevelCode { get; set; }

        [XmlAttribute]
        public UsageEnum Usage { get; set; }

        public string Name { get; set; }

        [XmlElement("Segment")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        [XmlElement("Loop")]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        [XmlElement("HierarchicalLoop")]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }
    }
}
