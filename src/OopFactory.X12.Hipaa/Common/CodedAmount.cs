namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class CodedAmount
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlIgnore]
        public decimal? Amount { get; set; }

        [XmlAttribute(AttributeName = "Amount")]
        public decimal SerializableAmount
        {
            get { return this.Amount ?? decimal.Zero; }
            set { this.Amount = value; }
        }

        [XmlIgnore]
        public bool SerializableAmountSpecified => this.Amount.HasValue;
    }
}
