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

        private string GetTransactionType(string rawX12)
        {
            string elementDelimiter = rawX12.Substring(3, 1);

            int index = rawX12.IndexOf("ST" + elementDelimiter);

            return rawX12.Substring(index + 3, 3);    
        }

        public string ParseToXml(string rawX12)
        {
            return ParseToXml(GetTransactionType(rawX12), new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }

        public string ParseToXml(string transactionType, Stream stream)
        {
            var parser = new X12Parser(transactionType);
            return parser.Parse(stream).Serialize();
        }

        public string ParseToDomainXml(string rawX12)
        {
            string transactionType = GetTransactionType(rawX12);
            return ParseToDomainXml(transactionType, new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }
        public string ParseToDomainXml(string transactionType, Stream stream)
        {
            XslCompiledTransform transform;

            switch (transactionType)
            {
                case "835":
                    transform = EmbeddedResources.Get835Transform();
                    break;
                case "837":
                    transform = EmbeddedResources.Get837Transform();
                    break;
                case "856":
                    transform = EmbeddedResources.Get856Transform();
                    break;
                default:
                    throw new NotSupportedException(String.Format("Transaction Type {0} is not supported.", transactionType));
            }

            var writer = new StringWriter();
            XsltArgumentList list = new XsltArgumentList();
            list.AddParam("verbose", "", _verbose ? "1" : "0");
            transform.Transform(XmlReader.Create(new StringReader(ParseToXml(transactionType, stream))), list, writer);
            return writer.GetStringBuilder().ToString();
        }
    }
}
