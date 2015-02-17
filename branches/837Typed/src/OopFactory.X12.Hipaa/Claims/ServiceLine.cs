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
            if (SupplementalInformations == null) SupplementalInformations = new List<Paperwork>();
            if (Identifications == null) Identifications = new List<Identification>();
            if (Amounts == null) Amounts = new List<QualifiedAmount>();
            if (Dates == null) Dates = new List<QualifiedDate>();
            if (DateRanges == null) DateRanges = new List<QualifiedDateRange>();
            if (Notes == null) Notes = new List<Lookup>();
            if (Providers == null) Providers = new List<Provider>();

            if (OralCavityDesignations == null) OralCavityDesignations = new List<Lookup>();
            if (ToothInformations == null) ToothInformations = new List<ToothInformation>();
        }

        [XmlAttribute]
        public int LineNumber { get; set; }
        [XmlAttribute]
        public string RevenueCode { get; set; }
        [XmlAttribute]
        public string RevenueCodeDescription { get; set; }
        [XmlAttribute]
        public decimal Quantity { get; set; }
        [XmlAttribute]
        public string Unit { get; set; }
        [XmlAttribute]
        public string EmergencyIndicator { get; set; }
        [XmlAttribute]
        public string EpsdtIndicator { get; set; }
        [XmlAttribute]
        public string DiagnosisCodePointer1 { get; set; }
        [XmlAttribute]
        public string DiagnosisCodePointer2 { get; set; }
        [XmlAttribute]
        public string DiagnosisCodePointer3 { get; set; }
        [XmlAttribute]
        public string DiagnosisCodePointer4 { get; set; }
        [XmlAttribute]
        public string PurchasedServiceIdentifier { get; set; } // NPI
        [XmlAttribute]
        public string PurchasedServiceAmount { get; set; }
        
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

        public decimal? ServiceTaxAmount
        {
            get
            {
                if (Amounts.Exists(a => a.Qualifier == "GT"))
                    return Amounts.First(a => a.Qualifier == "GT").Amount;
                else
                    return null;
            }
        }

        public decimal? FacilityTaxAmount
        {
            get
            {
                if (Amounts.Exists(a => a.Qualifier == "N8"))
                    return Amounts.First(a => a.Qualifier == "N8").Amount;
                else
                    return null;
            }
        }

        public Lookup PlaceOfService { get; set; }
        public MedicalProcedure Procedure { get; set; }
        public DrugIdentification Drug { get; set; }

        [XmlElement(ElementName="SupplementalInformation")]
        public List<Paperwork> SupplementalInformations { get; set; }
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

        [XmlElement(ElementName = "LineAdjustmentInformation")]
        public List<LineAdjustmentInformation> LineAdjustmentInformations { get; set; }

        public Provider OperatingPhysician { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "72"); } }
        public Provider OtherOperatingPhysician { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "ZZ"); } }
        public Provider RenderingProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "82"); } }
        public Provider ReferringProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DN" || p.Name.Type.Identifier == "P3"); } }
        public Provider PurchasedServiceProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "QB"); } }
        public Provider ServiceFacilityLocation { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "77"); } }
        public Provider SupervisingProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DQ"); } }

        public Provider OrderingProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DK"); } }
        public Provider AmbulancePickupLocation { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "PW"); } }
        public Provider AmbulanceDropoffLocation { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "45"); } }

        [XmlElement(ElementName = "OralCavityDesignation")]
        public List<Common.Lookup> OralCavityDesignations { get; set; }

        [XmlElement(ElementName = "ToothInformation")]
        public List<ToothInformation> ToothInformations { get; set; }
    }
}
