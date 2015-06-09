using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class LineAdjustmentInformation
    {
        public LineAdjustmentInformation()
        {
            if (Adjustments == null) Adjustments = new List<ClaimsAdjustment>();
            if (Amounts == null) Amounts = new List<QualifiedAmount>();
            if (Dates == null) Dates = new List<QualifiedDate>();
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
                if (Dates.Exists(d => d.Qualifier == "573"))
                    return Dates.First(d => d.Qualifier == "573").Date;
                else
                    return DateTime.MinValue;
            }
        }

        public decimal? RemainingPatientLiability
        {
            get
            {
                if (Amounts.Exists(a => a.Qualifier == "EAF"))
                    return Amounts.First(a => a.Qualifier == "EAF").Amount;
                else
                    return null;
            }
        }



    }
}
