using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12
{
    public class X12ParsingService
    {
        private bool _verbose;

        public X12ParsingService(bool verbose)
        {
            _verbose = verbose;
        }

        public string ParseToXml(string rawX12)
        {
            string elementDelimiter = rawX12.Substring(3, 1);

            int index = rawX12.IndexOf("ST" + elementDelimiter);

            string transactionType = rawX12.Substring(index + 3, 3);            
            
            TransactionSpecification specification = null;
            switch (transactionType)
            {
                case "835":
                    specification = EmbeddedResources.Get835TransactionSpecification();
                    break;
                case "837":
                    specification = EmbeddedResources.Get837TransactionSpecification();
                    break;
            }


            var parser = new X12Parser(rawX12, specification);
            return parser.Parse().Serialize();
        }

        public string ParseToDomainXml(string rawX12)
        {
            // To do: determine the specification from the header elements.
            XslCompiledTransform transform = EmbeddedResources.Get837Transform();
            var writer = new StringWriter();
            XsltArgumentList list = new XsltArgumentList();
            list.AddParam("verbose", "", _verbose ? "1" : "0");
            transform.Transform(XmlReader.Create(new StringReader(ParseToXml(rawX12))), list, writer);
            return writer.GetStringBuilder().ToString();
        }
    }
}
