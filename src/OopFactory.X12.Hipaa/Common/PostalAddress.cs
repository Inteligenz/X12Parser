namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class PostalAddress
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        [XmlAttribute]
        public string City { get; set; }

        [XmlAttribute]
        public string StateCode { get; set; }

        [XmlAttribute]
        public string PostalCode { get; set; }

        [XmlAttribute]
        public string CountryCode { get; set; }

        [XmlAttribute]
        public string CountrySubdivisionCode { get; set; }

        public string Locale => $"{this.City}, {this.StateCode} {this.PostalCode}";
    }
}
