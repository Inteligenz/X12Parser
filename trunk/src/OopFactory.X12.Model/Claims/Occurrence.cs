using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Occurrence : CodedLookup
    {
        [XmlIgnore]
        public DateTime? Date { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public DateTime XmlSerializableDate
        {
            get { return Date ?? DateTime.MinValue; }
            set { Date = value; }
        }

        [XmlIgnore]
        public bool XmlSerializableDateSpecified
        {
            get { return Date.HasValue; }
            set { }
        }
    }
}
