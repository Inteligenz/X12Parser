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
            if (Notes == null) Notes = new List<string>();
        }

        [XmlAttribute]
        public int LineNumber { get; set; }
        [XmlAttribute]
        public decimal Charges { get; set; }
        [XmlAttribute]
        public decimal Quantity { get; set; }
        [XmlAttribute]
        public string RevenueCode { get; set; }
        [XmlAttribute]
        public string Unit { get; set; }
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
        public List<string> Notes { get; set; }

        
    }
}
