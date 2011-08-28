using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public class BenefitResponse : BenefitBase
    {
        public BenefitResponse()
        {
            if (Benefits == null) Benefits = new List<EligibilityBenefit>();
        }

        [XmlAttribute]
        public string TransactionControlNumber { get; set; }


        [XmlElement(ElementName = "Benefit")]
        public List<EligibilityBenefit> Benefits { get; set; }



        #region Serialization Methods
        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(BenefitResponse)).Serialize(writer, this);
            return writer.ToString();
        }

        public static BenefitResponse Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BenefitResponse));
            return (BenefitResponse)serializer.Deserialize(new StringReader(xml));

        }

        public static string SerializeList(List<BenefitResponse> list)
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(List<BenefitResponse>)).Serialize(writer, list);
            return writer.ToString();
        }

        public static List<BenefitResponse> DeserializeList(string xml) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<BenefitResponse>));
            return (List<BenefitResponse>)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
