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
            // To do: determine the specification from the header elements.

            TransactionSpecification specification = Get837TransactionSpecification();
            XslCompiledTransform transform = Get837Transform();

            var parser = new X12Parser(rawX12, specification);
            string x12Xml = parser.Parse().Serialize();
            var writer = new StringWriter();
            XsltArgumentList list = new XsltArgumentList();
            list.AddParam("verbose", "", _verbose ? "1" : "0");
            transform.Transform(XmlReader.Create(new StringReader(x12Xml)), list, writer);
            return writer.GetStringBuilder().ToString();

        }

        private static TransactionSpecification _837specification;

        internal static TransactionSpecification Get837TransactionSpecification()
        {
            if (_837specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Claims.Ansi-837-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837specification;
        }

        private static XslCompiledTransform _837Transform;

        internal static XslCompiledTransform Get837Transform()
        {
            if (_837Transform == null)
            {
                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Claims.Ansi-837-To-Claim.xslt"));
                _837Transform = new XslCompiledTransform();
                _837Transform.Load(xsltReader);
            }
            return _837Transform;
        }



    }
}
