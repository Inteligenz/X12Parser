using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class PostalAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }

        [XmlAttribute]
        public string StateCode { get; set; }

        [XmlAttribute]
        public string PostalCode { get; set; }
    }
}
