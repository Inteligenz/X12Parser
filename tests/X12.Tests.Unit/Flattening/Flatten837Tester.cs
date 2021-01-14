﻿namespace X12.Tests.Unit.Flattening
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Xsl;

    using NUnit.Framework;

    using X12.Parsing;
    using X12.Shared.Models;

    [TestFixture]
    public class Flatten837Tester
    {
        [Test]
        public void FlattenUsingXslt()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles.INS._837P._4010.FromNth.837_DeIdent_01.dat");

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();

            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Flattening.837-XML-to-claim-level-csv.xslt")));
            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), writer);
        }
    }
}
