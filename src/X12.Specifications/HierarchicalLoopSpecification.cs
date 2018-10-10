namespace X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using X12.Specifications.Enumerations;
    using X12.Specifications.Interfaces;

    /// <summary>
    /// Provides definition and base structure for Hierarchical Loops
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class HierarchicalLoopSpecification : IContainerSpecification
    {
        /// <summary>
        /// Gets or sets the ID of the loop
        /// </summary>
        [XmlAttribute]
        public string LoopId { get; set; }

        /// <summary>
        /// Gets or sets the loop level code
        /// </summary>
        [XmlAttribute]
        public string LevelCode { get; set; }

        /// <summary>
        /// Gets or sets the usage indicators for the loop
        /// </summary>
        [XmlAttribute]
        public Usage Usage { get; set; }

        /// <summary>
        /// Gets or sets the loop segment name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of segment specifications
        /// </summary>
        [XmlElement(X12Elements.Segment)]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        /// <summary>
        /// Gets or sets the collection of loop specifications
        /// </summary>
        [XmlElement(X12Elements.Loop)]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        /// <summary>
        /// Gets or sets the collection of hierarchical loop specifications
        /// </summary>
        [XmlElement(X12Elements.HierarchicalLoop)]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }
    }
}
