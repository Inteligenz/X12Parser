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

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ParseToXml()
        {
            Stream stream = GetEdi(Convert.ToString(TestContext.DataRow["ResourcePath"]));

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(stream);
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void Parse4010Example1AndUnparse()
        {
            Stream stream = GetEdi(Convert.ToString(TestContext.DataRow["ResourcePath"]));
            string orignalX12 = new StreamReader(stream).ReadToEnd();
            stream = GetEdi(Convert.ToString(TestContext.DataRow["ResourcePath"]));
            Interchange interchange = new X12Parser().Parse(stream);
            string x12 = interchange.SerializeToX12(true);

            Assert.AreEqual(orignalX12, x12);
            Trace.Write(x12);
        }

        [DeploymentItem("tests\\OopFactory.X12.Tests.Unit\\Parsing\\_SampleEdiFiles\\SampleEdiFileInventory.xml"), DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\SampleEdiFileInventory.xml", "EdiFile", DataAccessMethod.Sequential)]
        [TestMethod]
        public void Parse270_4010Example1ToHtml()
        {
            string resourcePath = Convert.ToString(TestContext.DataRow["ResourcePath"]);
            Stream stream = GetEdi(resourcePath);
            var service = new X12HtmlTransformationService(new X12EdiParsingService(false));
            string html = service.Transform(new StreamReader(stream).ReadToEnd());

            Trace.Write(html);

#if DEBUG
            new FileStream(@"c:\Temp\" + resourcePath.Replace(".txt", ".htm"), FileMode.Create).PrintHtmlToFile(html);
#endif
        }
    }
}
