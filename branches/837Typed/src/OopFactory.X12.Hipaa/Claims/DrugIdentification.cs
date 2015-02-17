using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class DrugIdentification
    {
        [XmlAttribute]
        public string Ndc { get; set; }

        [XmlAttribute]
        public decimal Quantity { get; set; }

        public Lookup UnitOfMeasure { get; set; }

        public Identification Identification { get; set; }
    }
}
