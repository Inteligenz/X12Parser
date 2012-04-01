using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ClaimsAdjustment
    {
        [XmlAttribute]
        public string GroupCode { get; set; }
        [XmlAttribute]
        public string ReasonCode { get; set; }
        [XmlAttribute]
        public decimal Amount { get; set; }

        [XmlIgnore]
        public decimal? Quantity { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public decimal SerializableQuantity
        {
            get { return Quantity ?? decimal.Zero; }
            set { Quantity = value; }
        }

        [XmlIgnore]
        public bool SerializableQuantitySpecified
        {
            get { return Quantity.HasValue; }
            set { }
        }

    }
}
