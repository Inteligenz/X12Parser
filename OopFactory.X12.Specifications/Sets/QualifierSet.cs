namespace OopFactory.X12.Specifications.Sets
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications;

    public class QualifierSet
    {
        public QualifierSet()
        {
            if (this.AllowedIdentifiers == null)
            {
                this.AllowedIdentifiers = new List<AllowedIdentifier>();
            }
        }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
        
        [XmlElement(ElementName = "Allowed")]
        public List<AllowedIdentifier> AllowedIdentifiers { get; set; }
    }
}
