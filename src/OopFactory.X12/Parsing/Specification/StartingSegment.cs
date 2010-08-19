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
    public class StartingSegment
    {
        public StartingSegment()
        {
            if (SegmentSpecification == null) SegmentSpecification = new SegmentSpecification();
            if (EntityIdentifiers == null) EntityIdentifiers = new List<Lookup>();
        }

        public SegmentSpecification SegmentSpecification { get; set; }
        [XmlElement("EntityIdentifier")]
        public List<Lookup> EntityIdentifiers { get; set; }
    }
}
