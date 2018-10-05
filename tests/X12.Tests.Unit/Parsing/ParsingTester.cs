namespace X12.Tests.Unit.Parsing
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    
    using NUnit.Framework;

    using X12.Parsing;
    using X12.Shared.Models;
    using X12.Specifications.Finders;
    using X12.Transformations;

    /// <summary>
    /// Summary description for ParsingTester
    /// </summary>
    [TestFixture]
    public class ParsingTester
    {
        /// <summary>
        /// Tests that the X12Parser can parse an EDI file and transform it to XML
        /// </summary>
        /// <param name="resourcePath">File path for a sample EDI file to test</param>
        [Test]
        public void ParseToXml(
            [ValueSource(typeof(ResourcePathManager), nameof(ResourcePathManager.ResourcePaths))]
            string resourcePath)
        {
            Stream stream = GetEdi(resourcePath);

            var parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(stream).First();
            string xml = interchange.Serialize();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            int index = 1;
            string query = this.GetXPathQuery(resourcePath, index);
            while (!string.IsNullOrWhiteSpace(query))
            {
                string expected = this.GetExpectedValue(resourcePath, index);
                XmlNode node = doc.SelectSingleNode(query);
                Assert.IsNotNull(node, "Query '{0}' not found in {1}.", query, resourcePath);
                Assert.AreEqual(expected, node.InnerText, "Value {0} not expected from query {1} in {2}.", node.InnerText, query, resourcePath);
                query = this.GetXPathQuery(resourcePath, ++index);
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
        /// <param name="resourcePath">File path for a sample EDI file to test</param>
        [Test]
        public void ParseAndUnparse(
            [ValueSource(typeof(ResourcePathManager), nameof(ResourcePathManager.ResourcePaths))]
            string resourcePath)
        {
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
        /// <param name="resourcePath">File path for a sample EDI file to test</param>
        [Test]
        public void ParseAndTransformToX12(
            [ValueSource(typeof(ResourcePathManager), nameof(ResourcePathManager.ResourcePaths))]
            string resourcePath)
        {
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
        /// <param name="resourcePath">File path for a sample EDI file to test</param>
        [Test]
        public void ParseModifyAndTransformBackToX12(
            [ValueSource(typeof(ResourcePathManager), nameof(ResourcePathManager.ResourcePaths))]
            string resourcePath)
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
        /// <param name="resourcePath">File path for a sample EDI file to test</param>
        [Test]
        public void ParseToHtml(
            [ValueSource(typeof(ResourcePathManager), nameof(ResourcePathManager.ResourcePaths))]
            string resourcePath)
        {
            // arrange
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
        [Test]
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
        /// <exception cref="AssertionException">Thrown if method is executed</exception>
        private void Parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            throw new AssertionException($"ParserWarning executed by {sender}: '{args.Message}'");
        }

        private string GetXPathQuery(string resourcePath, int index)
        {
            return ResourcePathManager.QueryMap[resourcePath].Any(q => q.Key.Contains($"Query{index}"))
                       ? ResourcePathManager.QueryMap[resourcePath][$"Query{index}"]
                       : null;
        }

        private string GetExpectedValue(string resourcePath, int index)
        {
            return ResourcePathManager.ExpectedValuesMap[resourcePath].Any(e => e.Key.Contains($"Expected{index}"))
                       ? ResourcePathManager.ExpectedValuesMap[resourcePath][$"Expected{index}"]
                       : null;
        }
    }
}
