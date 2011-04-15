using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit.Parsing.PurchaseOrders
{
    [TestClass]
    public class PurchaseOrderParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.PurchaseOrders." + filename);
        }

        [TestMethod]
        public void ParseExample1ToXml()
        {
            // Sample EDI from http://www.pgwglass.com/manufacturing/Supplier%20Information/EDI%20Specifications/PGW_850_Specs.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_PGWGlass.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseExample1AndUnparse()
        {
            string originalX12 = new StreamReader(GetEdi("Example1_PGWGlass.txt")).ReadToEnd();

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_PGWGlass.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(originalX12, x12);
        }

        [TestMethod]
        public void ParseExample2ToXml()
        {
            // Sample EDI from http://www.adobe.com/partnerportal/edi/pdf/TLP_ANSI_000.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_Adobe_TLP.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseExample2AndUnparse()
        {
            string originalX12 = new StreamReader(GetEdi("Example2_Adobe_TLP.txt")).ReadToEnd();

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_Adobe_TLP.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(originalX12, x12);
        }

        [TestMethod]
        public void ParseExample3ToXml()
        {
            // Sample EDI from http://www.adobe.com/partnerportal/edi/pdf/CLP_ANSI_000.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example3_Adobe_CLP.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseExample3AndUnparse()
        {
            string originalX12 = new StreamReader(GetEdi("Example3_Adobe_CLP.txt")).ReadToEnd();

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example3_Adobe_CLP.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(originalX12, x12);
        }

        [TestMethod]
        public void ParseExample4ToXml()
        {
            // Sample EDI from http://www.adobe.com/partnerportal/edi/pdf/850_ANSIX12_SW_update100108_000.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example4_Adobe_ShrinkWrapped.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseExample4AndUnparse()
        {
            string originalX12 = new StreamReader(GetEdi("Example4_Adobe_ShrinkWrapped.txt")).ReadToEnd();

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example4_Adobe_ShrinkWrapped.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(originalX12, x12);
        }
    }
}
