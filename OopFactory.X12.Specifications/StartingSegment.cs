namespace OopFactory.X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class StartingSegment : SegmentSpecification
    {
        public StartingSegment()
        {
            if (this.EntityIdentifiers == null)
            {
                this.EntityIdentifiers = new List<Lookup>();
            }
        }

        [XmlElement("EntityIdentifier")]
        public List<Lookup> EntityIdentifiers { get; set; }
    }
}
