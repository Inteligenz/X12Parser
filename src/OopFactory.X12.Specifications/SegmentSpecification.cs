namespace OopFactory.X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications.Enumerations;

    /// <summary>
    /// Represents the specification of a segment within an X12 document
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class SegmentSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentSpecification"/> class
        /// </summary>
        public SegmentSpecification()
        {
            if (this.Standard == null)
            {
                this.Standard = new SegmentSpecificationStandard();
            }

            if (this.Elements == null)
            {
                this.Elements = new List<ElementSpecification>();
            }
        }

        /// <summary>
        /// Gets or sets the segment ID defined in the specification
        /// </summary>
        [XmlAttribute]
        public string SegmentId { get; set; }

        /// <summary>
        /// Gets or sets the usage (e.g. required, situational, not used, etc)
        /// </summary>
        [XmlAttribute]
        public UsageEnum Usage { get; set; }

        /// <summary>
        /// Gets or sets the number of times the segment is repeated in the document
        /// </summary>
        [XmlAttribute]
        public int Repeat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the segment includes a trailer
        /// (e.g. ISA segment)
        /// </summary>
        [XmlAttribute]
        public bool Trailer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the segment trailer is specified
        /// in the specification document
        /// </summary>
        [XmlIgnore]
        public bool TrailerSpecified { get; set; }

        public SegmentSpecificationStandard Standard { get; set; }

        [XmlElement(ElementName = "Element")]
        public List<ElementSpecification> Elements { get; set; }

        /// <summary>
        /// Gets the element indicated, if present, from the segment
        /// </summary>
        /// <param name="elementNumber">The position of the element to get from the segment</param>
        /// <returns>The element found; otherwise, null</returns>
        public ElementSpecification GetElement(int elementNumber)
        {
            return elementNumber >= 0 && elementNumber < this.Elements.Count
                       ? this.Elements[elementNumber - 1]
                       : null;
        }
    }
}
