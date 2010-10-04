using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class Contact
    {
        public string Name { get; set; }

        [XmlElement(ElementName = "Communication")]
        public List<QualifiedNumber> Communications { get; set; }
    }
}
