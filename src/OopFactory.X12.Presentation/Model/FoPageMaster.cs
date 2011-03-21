using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OopFactory.X12.Presentation.Model
{
    public class FoPageMaster
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string FontFamily { get; set; }
        [XmlAttribute]
        public decimal FontSize { get; set; }
        [XmlAttribute]
        public decimal Height { get; set; }
        [XmlAttribute]
        public decimal Width { get; set; }
        [XmlAttribute]
        public decimal MarginLeft { get; set; }
        [XmlAttribute]
        public decimal MarginRight { get; set; }
        [XmlAttribute]
        public decimal MarginTop { get; set; }
        [XmlAttribute]
        public decimal MarginBottom { get; set; }
        [XmlAttribute]
        public string BackgroundImageUri { get; set; }
        [XmlAttribute]
        public bool BackgroundImageScaledToFit { get; set; }
        [XmlAttribute]
        public decimal XPointOffset { get; set; }
        [XmlAttribute]
        public decimal XPointScale { get; set; }
        [XmlAttribute]
        public decimal YPointOffset { get; set; }
        [XmlAttribute]
        public decimal YPointScale { get; set; }
    }
}
