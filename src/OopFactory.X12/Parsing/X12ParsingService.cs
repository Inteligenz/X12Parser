using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    public class X12ParsingService
    {
        TransactionSpecification _specification;
        public X12ParsingService(TransactionSpecification specification)
        {
            _specification = specification;
        }

        public string ParseToXml(string rawX12)
        {
            var parser = new X12Parser(rawX12, _specification);
            return parser.Parse().Serialize();
        }
    }
}
