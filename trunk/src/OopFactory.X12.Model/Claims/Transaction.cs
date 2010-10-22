using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Transaction
    {
        [XmlElement(ElementName = "Claim")]
        public List<Claim> Claims { get; set; }
    }
}
