using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class CodedDateRange
    {
        [XmlAttribute]
        public string Code { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime FromDate { get; set; }
        [XmlAttribute(DataType = "date")]
        public DateTime ThroughDate { get; set; }
    }
}
