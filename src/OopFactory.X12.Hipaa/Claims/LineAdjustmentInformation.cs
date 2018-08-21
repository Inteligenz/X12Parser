namespace OopFactory.X12.Hipaa.Claims
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class LineAdjustmentInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineAdjustmentInformation"/> class
        /// </summary>
        public LineAdjustmentInformation()
        {
            if (this.Adjustments == null)
            {
                this.Adjustments = new List<ClaimsAdjustment>();
            }

            if (this.Amounts == null)
            {
                this.Amounts = new List<QualifiedAmount>();
            }

            if (this.Dates == null)
            {
                this.Dates = new List<QualifiedDate>();
            }
        }

        [XmlAttribute]
        public string OtherPayerPrimaryIdentifier { get; set; }

        [XmlAttribute]
        public decimal ServiceLinePaidAmount { get; set; }

        [XmlAttribute]
        public decimal PaidServiceUnitCount { get; set; }

        [XmlIgnore]
        public bool PaidServiceUnitCountSpecified { get; set; }

        [XmlAttribute]
        public string BundledLineNumber { get; set; }

        public MedicalProcedure Procedure { get; set; }
        
        [XmlElement(ElementName = "Adjustment")]
        public List<ClaimsAdjustment> Adjustments { get; set; }

        [XmlElement(ElementName = "Amount")]
        public List<QualifiedAmount> Amounts { get; set; }

        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }

        public DateTime RemittanceDate
        {
            get
            {
                return this.Dates.Exists(d => d.Qualifier == "573")
                    ? this.Dates.First(d => d.Qualifier == "573").Date
                    : DateTime.MinValue;
            }
        }

        public decimal? RemainingPatientLiability
        {
            get
            {
                if (this.Amounts.Exists(a => a.Qualifier == "EAF"))
                {
                    return this.Amounts.First(a => a.Qualifier == "EAF").Amount;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
