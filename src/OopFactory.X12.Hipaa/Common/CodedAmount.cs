using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class CodedAmount
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlIgnore]
        public decimal? Amount { get; set; }

        [XmlAttribute(AttributeName = "Amount")]
        public decimal SerializableAmount
        {
            get { return Amount ?? decimal.Zero; }
            set { Amount = value; }
        }

        [XmlIgnore]
        public bool SerializableAmountSpecified
        {
            get { return Amount.HasValue; }
            set { }
        }
    }
}
