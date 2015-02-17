using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms
{
    public class FormDate
    {
        [XmlAttribute]
        public string MM { get; set; }
        [XmlAttribute]
        public string DD { get; set; }
        [XmlAttribute]
        public string YY { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", MM, DD, YY);
        }
    }
}
