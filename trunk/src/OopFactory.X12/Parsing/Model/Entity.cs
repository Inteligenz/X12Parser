using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public class Entity : Loop
    {
        internal Entity(X12DelimiterSet delimiters, string segment, LoopSpecification loopSpecification)
            : base(delimiters, segment, loopSpecification)
        {
        }

        public string EntityIdentifierCode { get { return DataElements[0]; } }
    }
}
