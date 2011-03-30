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
            return ParseToXml(new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }

        public string ParseToXml(Stream stream)
        {
            return new X12Parser().Parse(stream).Serialize();
        }

        public string ParseToDomainXml(string rawX12)
        {
            return ParseToDomainXml(new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }
        public string ParseToDomainXml(Stream stream)
        {
            string xml = ParseToXml(stream);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string transactionType = doc.SelectSingleNode("Interchange/FunctionGroup/Transaction/ST/ST01").InnerText;

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
            transform.Transform(XmlReader.Create(new StringReader(xml)), list, writer);
            return writer.GetStringBuilder().ToString();
        }
    }
}
