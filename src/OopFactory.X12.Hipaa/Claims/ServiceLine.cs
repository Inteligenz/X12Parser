namespace OopFactory.X12.Hipaa.Claims
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class ServiceLine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLine"/> class
        /// </summary>
        public ServiceLine()
        {
            if (this.SupplementalInformations == null)
            {
                this.SupplementalInformations = new List<Paperwork>();
            }

            if (this.Identifications == null)
            {
                this.Identifications = new List<Identification>();
            }

            if (this.Amounts == null)
            {
                this.Amounts = new List<QualifiedAmount>();
            }

            if (this.Dates == null)
            {
                this.Dates = new List<QualifiedDate>();
            }

            if (this.DateRanges == null)
            {
                this.DateRanges = new List<QualifiedDateRange>();
            }

            if (this.Notes == null)
            {
                this.Notes = new List<Lookup>();
            }

            if (this.Providers == null)
            {
                this.Providers = new List<Provider>();
            }

            if (this.OralCavityDesignations == null)
            {
                this.OralCavityDesignations = new List<Lookup>();
            }

            if (this.ToothInformations == null)
            {
                this.ToothInformations = new List<ToothInformation>();
            }
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
        
        [XmlAttribute(AttributeName = "NonCoveredChargeAmount")]
        public decimal SerializableNonCoveredChargeAmount
        {
            get { return this.NonCoveredChargeAmount ?? decimal.Zero; }
            set { this.NonCoveredChargeAmount = value; }
        }

        [XmlIgnore]
        public bool SerializableNonCoveredChargeAmountSpecified => this.NonCoveredChargeAmount.HasValue;

        [XmlAttribute(DataType = "date")]
        public DateTime ServiceDateFrom
        {
            get
            {
                var range = this.DateRanges.FirstOrDefault(dr => dr.Qualifier == "472");
                if (range != null)
                {
                    return range.BeginDate;
                }

                var date = this.Dates.FirstOrDefault(dr => dr.Qualifier == "472");
                return date?.Date ?? DateTime.MinValue;
            }
        }

        [XmlAttribute(DataType = "date")]
        public DateTime ServiceDateTo
        {
            get
            {
                var range = this.DateRanges.FirstOrDefault(dr => dr.Qualifier == "472");
                if (range != null)
                {
                    return range.EndDate;
                }

                var date = this.Dates.FirstOrDefault(dr => dr.Qualifier == "472");
                return date?.Date ?? DateTime.MinValue;
            }
        }

        public decimal? ServiceTaxAmount
        {
            get
            {
                if (this.Amounts.Exists(a => a.Qualifier == "GT"))
                {
                    return this.Amounts.First(a => a.Qualifier == "GT").Amount;
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? FacilityTaxAmount
        {
            get
            {
                if (this.Amounts.Exists(a => a.Qualifier == "N8"))
                {
                    return this.Amounts.First(a => a.Qualifier == "N8").Amount;
                }
                else
                {
                    return null;
                }
            }
        }

        public Lookup PlaceOfService { get; set; }

        public MedicalProcedure Procedure { get; set; }

        public DrugIdentification Drug { get; set; }

        [XmlElement(ElementName = "SupplementalInformation")]
        public List<Paperwork> SupplementalInformations { get; set; }

        [XmlElement(ElementName = "Identification")]
        public List<Identification> Identifications { get; set; }

        [XmlElement(ElementName = "Amount")]
        public List<QualifiedAmount> Amounts { get; set; }

        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }

        [XmlElement(ElementName = "DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }

        [XmlElement(ElementName = "Note")]
        public List<Lookup> Notes { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }

        [XmlElement(ElementName = "LineAdjustmentInformation")]
        public List<LineAdjustmentInformation> LineAdjustmentInformations { get; set; }

        public Provider OperatingPhysician => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "72");

        public Provider OtherOperatingPhysician => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "ZZ"); 

        public Provider RenderingProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "82");

        public Provider ReferringProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DN" || p.Name.Type.Identifier == "P3");

        public Provider PurchasedServiceProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "QB");

        public Provider ServiceFacilityLocation => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "77");

        public Provider SupervisingProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DQ");

        public Provider OrderingProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DK");

        public Provider AmbulancePickupLocation => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "PW");

        public Provider AmbulanceDropoffLocation => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "45");

        [XmlElement(ElementName = "OralCavityDesignation")]
        public List<Common.Lookup> OralCavityDesignations { get; set; }

        [XmlElement(ElementName = "ToothInformation")]
        public List<ToothInformation> ToothInformations { get; set; }
    }
}
