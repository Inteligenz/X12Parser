using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public enum EntityNameQualifierEnum
    {
        Person,
        NonPerson
    }

    public class EntityName
    {
        public EntityName()
        {
            if (Identification == null) Identification = new Identification();
        }
        [XmlAttribute]
        public string Identifier { get; set; }
        [XmlAttribute]
        public EntityNameQualifierEnum Qualifier { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }
        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string MiddleName { get; set; }
        [XmlAttribute]
        public string LastName { get; set; }
        [XmlAttribute]
        public string Suffix { get; set; }

        public Identification Identification { get; set; }
    }
}
