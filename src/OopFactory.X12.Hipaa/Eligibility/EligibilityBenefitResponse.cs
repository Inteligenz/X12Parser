namespace OopFactory.X12.Hipaa.Eligibility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a response object for Eligibility Benefit information
    /// </summary>
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class EligibilityBenefitResponse : EligibilityBenefitBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EligibilityBenefitResponse"/> class
        /// </summary>
        public EligibilityBenefitResponse()
        {
            if (this.BenefitInfos == null)
            {
                this.BenefitInfos = new List<EligibilityBenefitInformation>();
            }
        }

        /// <summary>
        /// Gets or sets the transaction control number for the object
        /// </summary>
        [XmlAttribute]
        public string TransactionControlNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of benefit information
        /// </summary>
        [XmlElement(ElementName = "BenefitInfo")]
        public List<EligibilityBenefitInformation> BenefitInfos { get; set; }
        
        #region Serialization Methods
        /// <summary>
        /// Deserializes the XML string to its <see cref="EligibilityBenefitResponse"/> object
        /// </summary>
        /// <param name="xml">XML string representation to be deserialized</param>
        /// <returns>Object deserialized from XML string</returns>
        public static EligibilityBenefitResponse Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(EligibilityBenefitResponse));
            return (EligibilityBenefitResponse)serializer.Deserialize(new StringReader(xml));
        }

        /// <summary>
        /// Serializes the <see cref="EligibilityBenefitResponse"/> to XML
        /// </summary>
        /// <returns>XML string representation of benefit response</returns>
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(EligibilityBenefitResponse)).Serialize(writer, this);
            return writer.ToString();
        }
        #endregion
    }
}
