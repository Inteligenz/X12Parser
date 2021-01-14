namespace X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class ProviderInformation : Identification
    {
        [XmlAttribute]
        public string ProviderCode { get; set; }

        [XmlAttribute]
        public string ProviderDescription { get; set; }
    }
}
