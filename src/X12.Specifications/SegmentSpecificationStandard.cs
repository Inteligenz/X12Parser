namespace X12.Specifications
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    using X12.Specifications.Enumerations;

    /// <summary>
    /// Represents the segment standard for an X12 document
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class SegmentSpecificationStandard
    {
        /// <summary>
        /// Gets or sets the segment position in the X12 document
        /// </summary>
        [XmlAttribute]
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the segment usage
        /// </summary>
        [XmlAttribute]
        public Requirement Requirement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the requirement value is specified
        /// </summary>
        [XmlIgnore]
        public bool RequirementSpecified { get; set; }

        /// <summary>
        /// Gets or sets the max number of times the segment is used in the document
        /// </summary>
        [XmlAttribute]
        public int MaxUse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the MaxUse property is specified
        /// </summary>
        [XmlIgnore]
        public bool MaxUseSpecified { get; set; }
    }
}
