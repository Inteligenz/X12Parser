namespace X12.Tests.Unit.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using X12.Parsing;
    using X12.Shared.Models;
    using X12.Specifications.Finders;
    using X12.Transformations;

    /// <summary>
    /// Summary description for ParsingTester
    /// </summary>
    [TestClass]
    public class ParsingTester
    {
        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToXml()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();
#if DEBUG
            new FileStream(resourcePath.Replace(".txt", ".xml"), FileMode.Create).PrintToFile(xml);
#endif
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            int index = 1;
            string query = this.GetXPathQuery(index);
            while (!string.IsNullOrWhiteSpace(query))
            {
                string expected = this.GetExpectedValue(index);
                XmlNode node = doc.SelectSingleNode(query);
                Assert.IsNotNull(node, "Query '{0}' not found in {1}.", query, resourcePath);
                Assert.AreEqual(expected, node.InnerText, "Value {0} not expected from query {1} in {2}.", node.InnerText, query, resourcePath);
                Trace.WriteLine(string.Format("Query '{0}' succeeded.", query));
                query = this.GetXPathQuery(++index);
            }

            if (resourcePath.Contains("_837D"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new DentalClaimSpecificationFinder());
                interchange = parser.ParseMultiple(stream).First();
                xml = interchange.Serialize();
#if DEBUG
            new FileStream(resourcePath.Replace(".txt", "_837D.xml"), FileMode.Create).PrintToFile(xml);
#endif
            }

            if (resourcePath.Contains("_837I"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new InstitutionalClaimSpecificationFinder());
                interchange = parser.ParseMultiple(stream).First();
                xml = interchange.Serialize();
#if DEBUG
            new FileStream(resourcePath.Replace(".txt", "_837I.xml"), FileMode.Create).PrintToFile(xml);
#endif
            }
        }

        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndUnparse()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);
            string orignalX12 = new StreamReader(stream).ReadToEnd();
            stream = GetEdi(resourcePath);
            var parser = new X12Parser();
            parser.ParserWarning += this.Parser_ParserWarning;
            List<Interchange> interchanges = parser.ParseMultiple(stream);

            if (resourcePath.Contains("_811"))
            {
                Trace.Write(string.Empty);
            }

            StringBuilder x12 = new StringBuilder();
            foreach (var interchange in interchanges)
            {
                x12.AppendLine(interchange.SerializeToX12(true));
            }

            Assert.AreEqual(orignalX12, x12.ToString().Trim());
            Trace.Write(x12.ToString());
        }

        private void Parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            Trace.Write(args.Message);
        }

        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndTransformToX12()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);  // "INS._837P._4010.Spec_4.1.1_PatientIsSubscriber.txt";
            if (!resourcePath.Contains("_0x1D"))
            {
                Trace.WriteLine(resourcePath);
                Stream stream = GetEdi(resourcePath);

                var parser = new X12Parser();
                Interchange interchange = parser.ParseMultiple(stream).First();
                string originalX12 = interchange.SerializeToX12(true);

                string xml = interchange.Serialize();
                string x12 = parser.TransformToX12(xml);

                Interchange newInterchange = parser.ParseMultiple(x12).First();
                string newX12 = newInterchange.SerializeToX12(true);

                Assert.AreEqual(originalX12, newX12);
                Trace.Write(x12);
            }
        }

        [TestMethod]
        public void ParseModifyAndTransformBackToX12()
        {
            var stream = GetEdi("INS._270._4010.Example1_DHHS.txt");

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();

            var doc = new XmlDocument
            {
                PreserveWhitespace = true
            };
            doc.LoadXml(xml);

            XmlElement dmgElement = (XmlElement)doc.GetElementsByTagName("DMG")[0];
            dmgElement.ParentNode.RemoveChild(dmgElement);
            
            Console.WriteLine(doc.OuterXml);
            string x12 = parser.TransformToX12(doc.OuterXml);

            Console.WriteLine("ISA Segmemt:");
            Console.WriteLine(x12.Substring(0, 106));
            Console.WriteLine("Directly from XML:");
            Console.WriteLine(x12); 
            
            var modifiedInterchange = parser.ParseMultiple(x12).First();
            string newX12 = modifiedInterchange.SerializeToX12(true);

            Console.WriteLine("After passing through interchange object:");
            Console.WriteLine(newX12);
 
            var seSegment = modifiedInterchange.FunctionGroups.First().Transactions.First().TrailerSegments.FirstOrDefault(ts => ts.SegmentId == "SE");

            Assert.IsNotNull(seSegment);
            Assert.AreEqual("0001", seSegment.GetElement(2));
            Assert.AreEqual("15", seSegment.GetElement(1));
        }

        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToHtml()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);
            if (!resourcePath.Contains("MultipleInterchanges"))
            {
                var service = new X12HtmlTransformationService(new X12EdiParsingService(false));
                string html = service.Transform(new StreamReader(stream).ReadToEnd());

                Trace.Write(html);
#if DEBUG
                new FileStream(resourcePath.Replace(".txt", ".htm"), FileMode.Create).PrintHtmlToFile(html);
#endif
            }
        }

        [TestMethod]
        public void CreateTestFile()
        {
            string filename = @"..\..\..\tests\X12.Tests.Unit\Parsing\_SampleEdiFiles\INS\_270\_5010\Example1_IG_0x1D.txt";
            string edi = File.ReadAllText(filename);
            edi = edi.Replace('~', '\x1D');

            // act - assert
            File.WriteAllText(filename, edi);
        }

        [TestMethod]
        public void CreateTestFileWithTrailingBlanks()
        {
            // arrange
            string filename = @"..\..\..\tests\X12.Tests.Unit\Parsing\_SampleEdiFiles\INS\_837P\_5010\MedicaidExample_WithTrailingBlanks.txt";
            var edi = new StringBuilder(File.ReadAllText(filename));
            edi.Append((char)0);
            edi.Append((char)0);
            edi.Append((char)0);
            edi.Append((char)0);
            edi.Append((char)0);
            edi.Append((char)0);

            // act - assert
            File.WriteAllText(filename, edi.ToString());
        }

        [TestMethod]
        public void ParseUnicodeFile()
        {
            // arrange
            var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles.INS._837P._5010.UnicodeExample.txt");
            var parser = new X12Parser();

            // act - assert
            var interchange = parser.ParseMultiple(fs, Encoding.Unicode);
            Trace.Write(interchange.First().Serialize());
        }

        private static Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles." + resourcePath);
        }

        private string GetXPathQuery(int index)
        {
            if (this.TestContext.DataRow.Table.Columns.Contains($"Query{index}"))
            {
                return Convert.ToString(this.TestContext.DataRow[$"Query{index}"]);
            }

            return null;
        }

        private string GetExpectedValue(int index)
        {
            if (this.TestContext.DataRow.Table.Columns.Contains($"Expected{index}"))
            {
                return Convert.ToString(this.TestContext.DataRow[$"Expected{index}"]);
            }

            return null;
        }
    }
}
