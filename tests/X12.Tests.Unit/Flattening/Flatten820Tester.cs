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
    public class Flatten820Tester
    {
        [Test]
        public void FlattenUsingXmlDocument()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles.ORD._820.Example1_MortgageBankers.txt");

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var fstream = new FileStream("ORD._820.Example1.txt", FileMode.Create);
            var writer = new StreamWriter(fstream);

            writer.WriteLine("Transaction,Creation Date,Submitter Name, Borrower Last Name, Remittance ID");
            foreach (XmlElement transaction in doc.SelectNodes("/Interchange/FunctionGroup/Transaction"))
            {
                foreach (XmlElement entity in transaction.SelectNodes("Loop[@LoopId='ENT']"))
                {
                    foreach (XmlElement remit in entity.SelectNodes("Loop[@LoopId='RMR']"))
                    {
                        writer.WriteLine(
                            "{0},{1},{2},{3},{4}",
                            transaction.SelectSingleNode("ST/ST02").InnerText,
                            transaction.SelectSingleNode("DTM[DTM01='097']/DTM02").InnerText,
                            transaction.SelectSingleNode("Loop[@LoopId='N1']/N1[N101='41']/N102").InnerText,
                            entity.SelectSingleNode("Loop[@LoopId='NM1']/NM1[NM101='BW']/NM103").InnerText,
                            remit.SelectSingleNode("RMR/RMR02").InnerText);
                    }
                }
            }
            
            writer.Close();
            fstream.Close();
        }

        [Test]
        public void FlattenUsingXslt()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles.ORD._820.Example1_MortgageBankers.txt");

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();

            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Flattening.820-XML-to-csv.xslt")));
            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), writer);
        }
    }
}
