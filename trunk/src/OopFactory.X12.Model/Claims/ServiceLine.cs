using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class ServiceLine
    {
        [XmlAttribute]
        public int AssignedNumber { get; set; }
        [XmlAttribute]
        public decimal Quantity { get; set; }
        [XmlAttribute]
        public string Unit { get; set; }
        [XmlAttribute]
        public decimal ChargeAmount { get; set; }
        public DateTime? DateOfServiceFrom { get; set; }
        public DateTime? DateOfServiceTo { get; set; }
        public DateTime? AssessmentDate { get; set; }

        public CodedLookup Revenue { get; set; }
        public Procedure Procedure { get; set; }

    }
}
