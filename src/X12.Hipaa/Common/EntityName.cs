namespace X12.Hipaa.Common
{
    using System.Text;
    using System.Xml.Serialization;

    using X12.Hipaa.Enums;

    public class EntityType
    {
        [XmlAttribute]
        public string Identifier { get; set; }

        [XmlAttribute]
        public EntityNameQualifier Qualifier { get; set; }

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
            if (this.Type == null || this.Type.Qualifier == EntityNameQualifier.NonPerson)
            {
                return this.LastName;
            }

            var name = new StringBuilder();

            name.Append(this.LastName);
            if (!string.IsNullOrWhiteSpace(this.Suffix))
            {
                name.Append($" {this.Suffix}");
            }

            name.Append(",");
            if (!string.IsNullOrWhiteSpace(this.Prefix))
            {
                name.Append($" {this.Prefix}");
            }

            name.Append($" {this.FirstName}");
            if (!string.IsNullOrWhiteSpace(this.MiddleName))
            {
                name.Append(this.MiddleName.Length == 1 ? $" {this.MiddleName}." : $" {this.MiddleName}");
            }
            
            return name.ToString().TrimEnd();
        }

        public override string ToString()
        {
            return this.Formatted();
        }
    }
}
