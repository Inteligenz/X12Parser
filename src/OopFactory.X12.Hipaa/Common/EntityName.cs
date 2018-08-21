namespace OopFactory.X12.Hipaa.Common
{
    using System.Text;
    using System.Xml.Serialization;

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
            if (this.Identification == null)
            {
                this.Identification = new Identification();
            }
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
            if (this.Type == null || this.Type.Qualifier == EntityNameQualifierEnum.NonPerson)
            {
                return this.LastName;
            }
            else
            {
                var name = new StringBuilder();

                name.Append(this.LastName);
                if (!string.IsNullOrWhiteSpace(this.Suffix))
                {
                    name.AppendFormat(" {0}", this.Suffix);
                }

                name.Append(",");
                if (!string.IsNullOrWhiteSpace(this.Prefix))
                {
                    name.AppendFormat(" {0}", this.Prefix);
                }

                name.AppendFormat(" {0}", this.FirstName);
                if (!string.IsNullOrWhiteSpace(this.MiddleName))
                {
                    name.AppendFormat(
                        this.MiddleName.Length == 1 ? " {0}." : " {0}",
                        this.MiddleName);
                }
                
                return name.ToString().TrimEnd();
            }
        }

        public override string ToString()
        {
            return this.Formatted();
        }
    }
}
