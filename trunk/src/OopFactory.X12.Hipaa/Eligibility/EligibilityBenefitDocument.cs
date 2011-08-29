using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class EligibilityBenefitDocument
    {
        public EligibilityBenefitDocument()
        {
            if (EligibilityBenefitInquiries == null) EligibilityBenefitInquiries = new List<EligibilityBenefitInquiry>();
            if (EligibilityBenefitResponses == null) EligibilityBenefitResponses = new List<EligibilityBenefitResponse>();
            if (RequestValidations == null) RequestValidations = new List<RequestValidation>();
        }

        [XmlElement(ElementName="EligibilityBenefitInquiry")]
        public List<EligibilityBenefitInquiry> EligibilityBenefitInquiries { get; set; }

        [XmlElement(ElementName="EligibilityBenefitResponse")]
        public List<EligibilityBenefitResponse> EligibilityBenefitResponses { get; set; }

        [XmlElement(ElementName="RequestValidation")]
        public List<RequestValidation> RequestValidations { get; set; }

        #region Serialization Methods
        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(EligibilityBenefitDocument)).Serialize(writer, this);
            return writer.ToString();
        }

        public static EligibilityBenefitDocument Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EligibilityBenefitDocument));
            return (EligibilityBenefitDocument)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
