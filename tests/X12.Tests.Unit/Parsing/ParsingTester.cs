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
        /// Gets or sets the test context which provides information about and
        /// functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Tests that the X12Parser can parse an EDI file and transform it to XML
        /// </summary>
        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToXml()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Stream stream = GetEdi(resourcePath);

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();
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
                query = this.GetXPathQuery(++index);
            }

            if (resourcePath.Contains("_837D"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new DentalClaimSpecificationFinder());
                interchange = parser.ParseMultiple(stream).First();
                xml = interchange.Serialize();
            }

            if (resourcePath.Contains("_837I"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new InstitutionalClaimSpecificationFinder());
                interchange = parser.ParseMultiple(stream).First();
                xml = interchange.Serialize();
            }
        }

        /// <summary>
        /// Tests that X12Parser can parse an EDI and write it back to and EDI
        /// </summary>
        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndUnparse()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Stream stream = GetEdi(resourcePath);
            string orignalX12 = new StreamReader(stream).ReadToEnd();
            stream = GetEdi(resourcePath);
            var parser = new X12Parser();
            parser.ParserWarning += this.Parser_ParserWarning;
            List<Interchange> interchanges = parser.ParseMultiple(stream);

            StringBuilder x12 = new StringBuilder();
            foreach (var interchange in interchanges)
            {
                x12.AppendLine(interchange.SerializeToX12(true));
            }

            Assert.AreEqual(orignalX12, x12.ToString().Trim());
        }

        /// <summary>
        /// Tests and X12Parser can parse and EDI file, transform it to XMl and back to
        /// X12, and then write it back out
        /// </summary>
        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndTransformToX12()
        {
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            if (!resourcePath.Contains("_0x1D"))
            {
                // arrange
                Stream stream = GetEdi(resourcePath);

                // act
                var parser = new X12Parser();
                Interchange interchange = parser.ParseMultiple(stream).First();
                string originalX12 = interchange.SerializeToX12(true);

                string xml = interchange.Serialize();
                string x12 = parser.TransformToX12(xml);

                Interchange newInterchange = parser.ParseMultiple(x12).First();
                string newX12 = newInterchange.SerializeToX12(true);

                // assert
                Assert.AreEqual(originalX12, newX12);
            }
        }

        /// <summary>
        /// Tests that X12Parser can parse an EDI file, a change can be made to the model,
        /// and it can be transformed back to X12 with the modification
        /// </summary>
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
            
            string x12 = parser.TransformToX12(doc.OuterXml);
            
            var modifiedInterchange = parser.ParseMultiple(x12).First();
            string newX12 = modifiedInterchange.SerializeToX12(true);
 
            var seSegment = modifiedInterchange.FunctionGroups.First().Transactions.First().TrailerSegments.FirstOrDefault(ts => ts.SegmentId == "SE");

            Assert.IsNotNull(seSegment);
            Assert.AreEqual("0001", seSegment.GetElement(2));
            Assert.AreEqual("15", seSegment.GetElement(1));
        }

        /// <summary>
        /// Tests that X12Parser can parse an EDI file and transform it to HTML
        /// </summary>
        [DeploymentItem("tests\\X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToHtml()
        {
            // arrange
            string resourcePath = Convert.ToString(this.TestContext.DataRow["ResourcePath"]);
            Stream stream = GetEdi(resourcePath);
            if (!resourcePath.Contains("MultipleInterchanges"))
            {
                var service = new X12HtmlTransformationService(new X12EdiParsingService(false));

                // act - assert
                string html = service.Transform(new StreamReader(stream).ReadToEnd());
            }
        }

        /// <summary>
        /// Tests that X12Parser can parse a unicode encoded file
        /// </summary>
        [TestMethod]
        public void ParseUnicodeFile()
        {
            // arrange
            var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles.INS._837P._5010.UnicodeExample.txt");
            var parser = new X12Parser();

            // act - assert
            var interchange = parser.ParseMultiple(fs, Encoding.Unicode);
        }

        private static Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles." + resourcePath);
        }

        /// <summary>
        /// Event handler hook for X12Parser.ParserWarning, if this is tripped, then we'll fail the test
        /// </summary>
        /// <param name="sender">Object calling handler</param>
        /// <param name="args">Additional arguments, such as error message</param>
        /// <exception cref="AssertFailedException">Thrown if method is executed</exception>
        private void Parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            throw new AssertFailedException($"ParserWarning executed by {sender}: '{args.Message}'");
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
