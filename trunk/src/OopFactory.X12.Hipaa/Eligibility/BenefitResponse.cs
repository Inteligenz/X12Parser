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
            if (Identifications == null) Identifications = new List<Identification>();

            if (RequestValidations == null) RequestValidations = new List<RequestValidation>();

            if (Dates == null) Dates = new List<QualifiedDate>();
            if (DateRanges == null) DateRanges = new List<QualifiedDateRange>();
            if (SubscriberBenefitRelatedEntities == null) SubscriberBenefitRelatedEntities = new List<RelatedEntity>();
            if (DependentBenefitRelatedEntities == null) DependentBenefitRelatedEntities = new List<RelatedEntity>();
        }

        [XmlElement(ElementName="Identification")]
        public List<Identification> Identifications { get; set; }

        [XmlElement(ElementName="RequestValidation")]
        public List<RequestValidation> RequestValidations { get; set; }

        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }

        [XmlElement(ElementName = "DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }

        [XmlElement(ElementName="SubscriberBenefitRelatedEntity")]
        public List<RelatedEntity> SubscriberBenefitRelatedEntities { get; set; }

        [XmlElement(ElementName="DependentBenefitRelatedEntity")]
        public List<RelatedEntity> DependentBenefitRelatedEntities { get; set; }

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
