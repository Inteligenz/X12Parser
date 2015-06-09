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
        public EntityName()
        {
            if (Identification == null) Identification = new Identification();
        }
        public EntityType Type { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }

        [XmlAttribute]
        public string PriorAuthorizationNumber { get; set; }
        [XmlAttribute]
        public string Suffix { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }
        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string MiddleName { get; set; }

        public Identification Identification { get; set; }
        
        public string Formatted()
        {
            if (Type == null || Type.Qualifier == EntityNameQualifierEnum.NonPerson)
                return LastName;
            else
            {
                StringBuilder name = new StringBuilder();

                name.Append(LastName);
                if (!string.IsNullOrWhiteSpace(Suffix))
                {
                    name.AppendFormat(" {0}", Suffix);
                }
                name.Append(",");
                if (!string.IsNullOrWhiteSpace(Prefix))
                    name.AppendFormat(" {0}", Prefix);
                name.AppendFormat(" {0}", FirstName);
                if (!string.IsNullOrWhiteSpace(MiddleName))
                {
                    if (MiddleName.Length == 1)
                        name.AppendFormat(" {0}.", MiddleName);
                    else
                        name.AppendFormat(" {0}", MiddleName);
                }
                
                return name.ToString().TrimEnd();
            }
        }

        public override string ToString()
        {
            return Formatted();
        }
    }
}
