using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class Phone
    {
        [XmlAttribute]
        public string Ext { get; set; }
        [XmlText]
        public string Number { get; set; }
    }
}
