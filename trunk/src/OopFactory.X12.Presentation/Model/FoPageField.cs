using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OopFactory.X12.Presentation.Model
{
    public class FoPageField
    {
        [XmlAttribute]
        public decimal Top { get; set; }
        [XmlAttribute]
        public decimal Left { get; set; }
        [XmlAttribute]
        public decimal Width { get; set; }
        [XmlAttribute]
        public decimal Height { get; set; }
        [XmlAttribute]
        public string Align { get; set; }
        [XmlText]
        public string Content { get; set; }

        public FoPageField WithAlign(string align)
        {
            this.Align = align;
            return this;
        }
    }
}
