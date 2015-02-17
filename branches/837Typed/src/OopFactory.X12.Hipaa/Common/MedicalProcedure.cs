using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class MedicalProcedure
    {
        [XmlAttribute]        
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string ProcedureCode { get; set; }
        [XmlAttribute]
        public string ProcedureCodeEnd { get; set; }

        [XmlAttribute]
        public string Modifier1 { get; set; }
        [XmlAttribute]
        public string Modifier2 { get; set; }
        [XmlAttribute]
        public string Modifier3 { get; set; }
        [XmlAttribute]
        public string Modifier4 { get; set; }
        [XmlText]
        public string Description { get; set; }
    }
}
