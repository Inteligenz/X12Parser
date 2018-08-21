namespace OopFactory.X12.Hipaa.Claims
{
    using System.Xml.Serialization;

    public class ClaimsAdjustment
    {
        [XmlAttribute]
        public string GroupCode { get; set; }

        [XmlAttribute]
        public string ReasonCode { get; set; }

        [XmlAttribute]
        public decimal Amount { get; set; }

        [XmlIgnore]
        public decimal? Quantity { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public decimal SerializableQuantity
        {
            get { return this.Quantity.GetValueOrDefault(); }
            set { this.Quantity = value; }
        }

        [XmlIgnore]
        public bool SerializableQuantitySpecified => this.Quantity.HasValue;

    }
}
