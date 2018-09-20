namespace OopFactory.X12.Hipaa.Eligibility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    /// <summary>
    /// Represents an Eligibility Benefit Document
    /// </summary>
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class EligibilityBenefitDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EligibilityBenefitDocument"/> class
        /// </summary>
        public EligibilityBenefitDocument()
        {
            if (this.EligibilityBenefitInquiries == null)
            {
                this.EligibilityBenefitInquiries = new List<EligibilityBenefitInquiry>();
            }

            if (this.EligibilityBenefitResponses == null)
            {
                this.EligibilityBenefitResponses = new List<EligibilityBenefitResponse>();
            }

            if (this.RequestValidations == null)
            {
                this.RequestValidations = new List<RequestValidation>();
            }
        }

        /// <summary>
        /// Gets or sets the collection of benefits inquiries
        /// </summary>
        [XmlElement(ElementName = "EligibilityBenefitInquiry")]
        public List<EligibilityBenefitInquiry> EligibilityBenefitInquiries { get; set; }

        /// <summary>
        /// Gets or sets the collection of benefit responses
        /// </summary>
        [XmlElement(ElementName = "EligibilityBenefitResponse")]
        public List<EligibilityBenefitResponse> EligibilityBenefitResponses { get; set; }

        /// <summary>
        /// Gets or sets the collection of request validations
        /// </summary>
        [XmlElement(ElementName = "RequestValidation")]
        public List<RequestValidation> RequestValidations { get; set; }
        
        /// <summary>
        /// Deserializes an XML string into its <see cref="EligibilityBenefitDocument"/> object
        /// </summary>
        /// <param name="xml">XML string to deserialize</param>
        /// <returns>Object deserialized from XML</returns>
        public static EligibilityBenefitDocument Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(EligibilityBenefitDocument));
            return (EligibilityBenefitDocument)serializer.Deserialize(new StringReader(xml));
        }

        /// <summary>
        /// Serializes a <see cref="EligibilityBenefitDocument"/> to its XMl representation
        /// </summary>
        /// <returns>XML string serialized from object</returns>
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(EligibilityBenefitDocument)).Serialize(writer, this);
            return writer.ToString();
        }
    }
}
