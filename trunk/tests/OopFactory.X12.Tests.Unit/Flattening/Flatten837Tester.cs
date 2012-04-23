using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using System.Xml;
using System.Xml.Xsl;


namespace OopFactory.X12.Tests.Unit.Flattening
{
    [TestClass]
    public class Flatten837Tester
    {
        [TestMethod]
        public void FlattenUsingXslt()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing._SampleEdiFiles.INS._837P._4010.FromNth.837_DeIdent_01.dat");

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();

            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Flattening.837-XML-to-claim-level-csv.xslt")));
            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), writer);
            System.Diagnostics.Trace.WriteLine(writer.GetStringBuilder().ToString());

        }
    }
}
