namespace X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;
    
    using X12.Specifications.Enumerations;
    using X12.Specifications.Interfaces;

    /// <summary>
    /// Represents an X12 loop
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class LoopSpecification : IContainerSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoopSpecification"/> class
        /// </summary>
        public LoopSpecification()
        {
            if (this.SegmentSpecifications == null)
            {
                this.SegmentSpecifications = new List<SegmentSpecification>();
            }

            if (this.LoopSpecifications == null)
            {
                this.LoopSpecifications = new List<LoopSpecification>();
            }
        }

        /// <summary>
        /// Gets or sets the loop Id
        /// </summary>
        [XmlAttribute]
        public string LoopId { get; set; }

        /// <summary>
        /// Gets or sets the loop usage indicator
        /// </summary>
        [XmlAttribute]
        public Usage Usage { get; set; }

        /// <summary>
        /// Gets or sets the number of times the loop repeats
        /// </summary>
        [XmlAttribute]
        public int LoopRepeat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not loop repeat is specified
        /// </summary>
        [XmlIgnore]
        public bool LoopRepeatSpecified { get; set; }

        /// <summary>
        /// Gets or sets the loop name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the loop starting segment
        /// </summary>
        public StartingSegment StartingSegment { get; set; }

        /// <summary>
        /// Gets or sets the collection of segments contained in the loop
        /// </summary>
        [XmlElement(X12Elements.Segment)]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        /// <summary>
        /// Gets or sets the collection of nested loops
        /// </summary>
        [XmlElement(X12Elements.Loop)]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        /// <summary>
        /// Gets or sets the collection of hierarchical loops
        /// </summary>
        [XmlElement(X12Elements.HierarchicalLoop)]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }
    }
}
