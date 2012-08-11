﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    [DebuggerStepThrough()]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "http://tempuri.org/X12ParserSpecification.xsd")]
    public class TransactionSpecification : IContainerSpecification
    {
        [XmlAttribute]
        public string TransactionSetIdentifierCode { get; set; }

        [XmlElement("Segment")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }
        [XmlElement("Loop")]
        public List<LoopSpecification> LoopSpecifications { get; set; }
        [XmlElement("HierarchicalLoop")]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }

        public static TransactionSpecification Deserialize(string xml)
        {
            System.IO.StringReader stringReader = new System.IO.StringReader(xml);
            System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(stringReader);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TransactionSpecification));
            return ((TransactionSpecification)(xmlSerializer.Deserialize(xmlTextReader)));
        }

        string IContainerSpecification.LoopId
        {
            get { return ""; }
        }

        
    }
}
