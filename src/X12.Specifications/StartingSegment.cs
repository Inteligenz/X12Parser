namespace X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using X12.Specifications.Enumerations;

    /// <summary>
    /// Represents the starting segment in an X12 document
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class StartingSegment : SegmentSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartingSegment"/> class
        /// </summary>
        public StartingSegment()
        {
            if (this.EntityIdentifiers == null)
            {
                this.EntityIdentifiers = new List<Lookup>();
            }
        }

        /// <summary>
        /// Gets or sets the collection of Entity ID lookups
        /// </summary>
        [XmlElement(X12Elements.EntityIdentifier)]
        public List<Lookup> EntityIdentifiers { get; set; }
    }
}
