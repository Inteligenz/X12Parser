using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Provider
    {
        public Provider()
        {
        }

        [XmlAttribute]
        public string Npi { get; set; }

        [XmlAttribute]
        public string TaxId { get; set; }

        public EntityName Name { get; set; }

        public PostalAddress Address { get; set; }

        [XmlElement(ElementName = "Identification")]
        public List<QualifiedNumber> Identifications { get; set; }

        [XmlElement(ElementName = "Contact")]
        public List<Contact> Contacts { get; set; }

        public CodedLookup Speciality { get; set; }
    }
}
