namespace X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class Identification
    {
        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
