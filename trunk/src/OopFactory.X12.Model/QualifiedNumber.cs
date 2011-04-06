﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class QualifiedNumber
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlText]
        public string Number { get; set; }
    }
}