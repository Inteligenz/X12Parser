using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class OccurrenceSpan : CodedLookup
    {
        [XmlIgnore]
        public DateTime? FromDate { get; set; }

        [XmlAttribute(AttributeName = "FromDate")]
        public DateTime XmlSerializableFromDate
        {
            get { return FromDate ?? DateTime.MinValue; }
            set { FromDate = value; }
        }

        [XmlIgnore]
        public bool XmlSerializableFromDateSpecified
        {
            get { return FromDate.HasValue; }
            set { }
        }

        [XmlIgnore]
        public DateTime? ThroughDate { get; set; }

        [XmlAttribute(AttributeName = "ThroughDate")]
        public DateTime XmlSerializableThroughDate
        {
            get { return ThroughDate ?? DateTime.MinValue; }
            set { ThroughDate = value; }
        }

        [XmlIgnore]
        public bool XmlSerializableThroughDateSpecified
        {
            get { return ThroughDate.HasValue; }
            set { }
        }
    }
}
