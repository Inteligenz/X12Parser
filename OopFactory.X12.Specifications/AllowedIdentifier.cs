namespace OopFactory.X12.Specifications
{
    using System.Xml.Serialization;

    public class AllowedIdentifier
    {
        [XmlAttribute]
        public string ID { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
