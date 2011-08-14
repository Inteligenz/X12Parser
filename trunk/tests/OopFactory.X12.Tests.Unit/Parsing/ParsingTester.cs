using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Transformations;
using System.Diagnostics;
using System.Xml;
using OopFactory.X12.Transformations.Common;
using OopFactory.X12.Transformations.INS;
using OopFactory.X12.Rendering.Claims;

namespace OopFactory.X12.Tests.Unit.Parsing
{
    /// <summary>
    /// Summary description for ParsingTester
    /// </summary>
    [TestClass]
    public class ParsingTester
    {
        #region TestContext

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        private Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing._SampleEdiFiles." + resourcePath);
        }

        private string GetXPathQuery(int index)
        {
            if (TestContext.DataRow.Table.Columns.Contains(String.Format("Query{0}", index)))
                return Convert.ToString(TestContext.DataRow[String.Format("Query{0}", index)]);
            else
                return null;
        }

        private string GetExpectedValue(int index)
        {
            if (TestContext.DataRow.Table.Columns.Contains(String.Format("Expected{0}", index)))
                return Convert.ToString(TestContext.DataRow[String.Format("Expected{0}", index)]);
            else
                return null;
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToXml()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(stream);
            string xml = interchange.Serialize();
#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", ".xml"), FileMode.Create).PrintToFile(xml);
#endif
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            int index = 1;
            string query = GetXPathQuery(index);
            while (!string.IsNullOrWhiteSpace(query))
            {
                string expected = GetExpectedValue(index);
                XmlNode node = doc.SelectSingleNode((string)query);
                Assert.IsNotNull(node, "Query '{0}' not found in {1}.", query, resourcePath);
                Assert.AreEqual(expected, node.InnerText, "Value {0} not expected from query {1} in {2}.", node.InnerText, query, resourcePath);
                Trace.WriteLine(String.Format("Query '{0}' succeeded.", query));
                query = GetXPathQuery(++index);
            }

            if (resourcePath.Contains("_837D"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new DentalClaimSpecificationFinder());
                interchange = parser.Parse(stream);
                xml = interchange.Serialize();
#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_837D.xml"), FileMode.Create).PrintToFile(xml);
#endif
            }

            if (resourcePath.Contains("_837I"))
            {
                stream = GetEdi(resourcePath);
                parser = new X12Parser(new InstitutionalClaimSpecificationFinder());
                interchange = parser.Parse(stream);
                xml = interchange.Serialize();
#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_837I.xml"), FileMode.Create).PrintToFile(xml);
#endif
            }
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndUnparse()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);
            string orignalX12 = new StreamReader(stream).ReadToEnd();
            stream = GetEdi(Convert.ToString(TestContext.DataRow["ResourcePath"]));
            Interchange interchange = new X12Parser().Parse(stream);
            string x12 = interchange.SerializeToX12(true);

            Assert.AreEqual(orignalX12, x12);
            Trace.Write(x12);
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseAndTransformToX12()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);  // "INS._837P._4010.Spec_4.1.1_PatientIsSubscriber.txt";
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);
            
            var parser = new X12Parser();
            Interchange interchange = parser.Parse(stream);
            string originalX12 = interchange.SerializeToX12(true);

            string xml = interchange.Serialize();
            string x12 = parser.TransformToX12(xml);
            
            Interchange newInterchange = parser.Parse(x12);
            string newX12 = newInterchange.SerializeToX12(true);

            Assert.AreEqual(originalX12, newX12);
            Trace.Write(x12);
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToHtml()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);
            var service = new X12HtmlTransformationService(new X12EdiParsingService(false));
            string html = service.Transform(new StreamReader(stream).ReadToEnd());

            Trace.Write(html);

#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", ".htm"), FileMode.Create).PrintHtmlToFile(html);
#endif
        }
                               
        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void CommonSegmentsTransformTest()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);

            var service = new CommonSegmentsTransformer(new X12EdiParsingService(false));
            string xml = service.Transform(new StreamReader(stream).ReadToEnd());

            Trace.Write(xml);

#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_Common.xml"), FileMode.Create).PrintToFile(xml);
#endif

        }


        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ClaimTransformTest()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);

            var service = new ClaimTransformer();
            string xml = service.Transform(new StreamReader(stream).ReadToEnd());

            Trace.Write(xml);

#if DEBUG
            if (xml.Contains("<Claim"))
                new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_Claim.xml"), FileMode.Create).PrintToFile(xml);
#endif

        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod, Ignore]
        public void ClaimFormPdfTransformTest()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Trace.WriteLine(resourcePath);
            Stream stream = GetEdi(resourcePath);

            var service = new ClaimToFoTransformer();
            string foXml = service.Transform(new StreamReader(stream).ReadToEnd());

            Trace.Write(foXml);

#if DEBUG
            if (foXml.Contains("<fo:page"))
            {
                new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_Claim-FO.xml"), FileMode.Create).PrintToFile(foXml);

                var driver = Fonet.FonetDriver.Make();

                var output = new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", "_Claim-FO.pdf"), FileMode.Create);
                driver.Render(new MemoryStream(Encoding.ASCII.GetBytes(foXml)), output);
            }
#endif

        }
    }
}
