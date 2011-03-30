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
    public class SegmentSpecification
    {
        public SegmentSpecification()
        {
            if (Standard == null) Standard = new SegmentSpecificationStandard();
            if (Elements == null) Elements = new List<ElementSpecification>();
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
            if (elementNumber >= 0 && elementNumber < Elements.Count)
                return Elements[elementNumber - 1];
            else
                return null;
        }
    }
}
