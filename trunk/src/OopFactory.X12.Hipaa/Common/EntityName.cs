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

    public class EntityType
    {
        [XmlAttribute]
        public string Identifier { get; set; }
        [XmlAttribute]
        public EntityNameQualifierEnum Qualifier { get; set; }
        [XmlText]
        public string Description { get; set; }
    }

    public class EntityName
    {
        public EntityType Type { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }
        [XmlAttribute]
        public string Suffix { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }
        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string MiddleName { get; set; }

        public Identification Identification { get; set; }
    }
}
