namespace OopFactory.X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications.Enumerations;

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class SegmentSpecification
    {
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

        [XmlAttribute]
        public string SegmentId { get; set; }

        [XmlAttribute]
        public UsageEnum Usage { get; set; }

        [XmlAttribute]
        public int Repeat { get; set; }

        [XmlAttribute]
        public bool Trailer { get; set; }

        [XmlIgnore]
        public bool TrailerSpecified { get; set; }

        public SegmentSpecificationStandard Standard { get; set; }

        [XmlElement(ElementName="Element")]
        public List<ElementSpecification> Elements { get; set; }

        public ElementSpecification GetElement(int elementNumber)
        {
            return elementNumber >= 0 && elementNumber < this.Elements.Count
                       ? this.Elements[elementNumber - 1]
                       : null;
        }
    }
}
