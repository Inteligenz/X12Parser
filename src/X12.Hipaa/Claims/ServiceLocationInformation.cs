namespace X12.Hipaa.Claims
{
    using System.Xml.Serialization;

    public class ServiceLocationInformation
    {
        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string FacilityCode { get; set; }

        [XmlAttribute]
        public string FrequencyTypeCode { get; set; }
    }
}
