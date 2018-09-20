namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents an object's coded amount
    /// </summary>
    public class CodedAmount
    {
        /// <summary>
        /// Gets or sets the object code
        /// </summary>
        [XmlAttribute]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the object amount (nullable)
        /// </summary>
        [XmlIgnore]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the object amount for serialization
        /// </summary>
        [XmlAttribute(AttributeName = Enums.ClaimElements.Amount)]
        public decimal SerializableAmount
        {
            get { return this.Amount ?? decimal.Zero; }
            set { this.Amount = value; }
        }

        /// <summary>
        /// Indicates whether the amount has a value that can be serialized
        /// </summary>
        [XmlIgnore]
        public bool SerializableAmountSpecified => this.Amount.HasValue;
    }
}
