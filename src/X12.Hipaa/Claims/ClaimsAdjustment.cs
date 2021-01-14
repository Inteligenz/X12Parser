namespace X12.Hipaa.Claims
{
    using System.Xml.Serialization;

    using X12.Hipaa.Enums;

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

        [XmlAttribute(AttributeName = ClaimElements.Quantity)]
        public decimal SerializableQuantity
        {
            get { return this.Quantity.GetValueOrDefault(); }
            set { this.Quantity = value; }
        }

        [XmlIgnore]
        public bool SerializableQuantitySpecified => this.Quantity.HasValue;
    }
}
