using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class Contact
    {
        public Contact()
        {
            if (Numbers == null) Numbers = new List<ContactNumber>();
        }

        [XmlAttribute]
        public string FunctionCode { get; set; }
        public string Name { get; set; }
        [XmlElement(ElementName="Number")]
        public List<ContactNumber> Numbers { get; set; }
    }

    public class ContactNumber
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlText]
        public string Number { get; set; }
    }
}
