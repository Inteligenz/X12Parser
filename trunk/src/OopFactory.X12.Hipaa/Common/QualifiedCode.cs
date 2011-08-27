using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class QualifiedCode
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlText]
        public string Code { get; set; }
    }
}
