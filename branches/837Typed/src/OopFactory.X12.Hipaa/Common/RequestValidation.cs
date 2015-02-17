using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class RequestValidation
    {
        [XmlAttribute]
        public bool ValidRequest { get; set; }
        public Lookup RejectReason { get; set; }
        public Lookup FollupAction { get; set; }
    }
}
