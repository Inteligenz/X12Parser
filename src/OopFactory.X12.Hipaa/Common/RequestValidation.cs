namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class RequestValidation
    {
        [XmlAttribute]
        public bool ValidRequest { get; set; }

        public Lookup RejectReason { get; set; }

        public Lookup FollupAction { get; set; }
    }
}
