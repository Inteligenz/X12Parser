using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class CodedLookup
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
