using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ServiceLine
    {
        public ServiceLine()
        {
            if (Identifications == null) Identifications = new List<Identification>();
            if (Amounts == null) Amounts = new List<QualifiedAmount>();
            if (Dates == null) Dates = new List<QualifiedDate>();
            if (DateRanges == null) DateRanges = new List<QualifiedDateRange>();
            if (Notes == null) Notes = new List<Lookup>();
            if (Providers == null) Providers = new List<Provider>();
        }

        [XmlAttribute]
        public int LineNumber { get; set; }
        [XmlAttribute]
        public string RevenueCode { get; set; }
        [XmlAttribute]
        public decimal Quantity { get; set; }
        [XmlAttribute]
        public string Unit { get; set; }
        [XmlAttribute]
        public decimal ChargeAmount { get; set; }
        
        [XmlIgnore]
        public decimal? NonCoveredChargeAmount { get; set; }
        
        [XmlAttribute(AttributeName="NonCoveredChargeAmount")]
        public decimal SerializableNonCoveredChargeAmount
        {
            get { return NonCoveredChargeAmount ?? decimal.Zero; }
            set { NonCoveredChargeAmount = value; }
        }

        [XmlIgnore]
        public bool SerializableNonCoveredChargeAmountSpecified
        {
            get { return NonCoveredChargeAmount.HasValue; }
            set { }
        }

        [XmlAttribute(DataType = "date")]
        public DateTime ServiceDateFrom
        {
            get
            {
                var range = DateRanges.FirstOrDefault(dr => dr.Qualifier == "472");
                if (range != null)
                    return range.BeginDate;
                var date = Dates.FirstOrDefault(dr => dr.Qualifier == "472");
                if (date != null)
                    return date.Date;
                return DateTime.MinValue;
            }
            set { }
        }

        [XmlAttribute(DataType="date")]
        public DateTime ServiceDateTo
        {
            get
            {
                var range = DateRanges.FirstOrDefault(dr => dr.Qualifier == "472");
                if (range != null)
                    return range.EndDate;
                var date = Dates.FirstOrDefault(dr => dr.Qualifier == "472");
                if (date != null)
                    return date.Date;
                return DateTime.MinValue;
            }
            set { }
        }

        public MedicalProcedure Procedure { get; set; }
        [XmlElement(ElementName="Identification")]
        public List<Identification> Identifications { get; set; }
        [XmlElement(ElementName="Amount")]
        public List<QualifiedAmount> Amounts { get; set; }
        [XmlElement(ElementName="Date")]
        public List<QualifiedDate> Dates { get; set; }
        [XmlElement(ElementName="DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }
        [XmlElement(ElementName="Note")]
        public List<Lookup> Notes { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }
        
    }
}
