using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class CodedDate
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
    }
}
