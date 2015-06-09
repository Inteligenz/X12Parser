using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class QualifiedDateRange
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlAttribute(DataType="date")]
        public DateTime BeginDate { get; set; }
        [XmlAttribute(DataType="date")]
        public DateTime EndDate { get; set; }
        [XmlText]
        public string Description { get; set; }
    }
}
