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

namespace OopFactory.X12.Tests.Unit.Parsing.PurchaseOrders.Acknowledgements
{
    [TestClass]
    public class PurchaseOrderAcknowledgementTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.PurchaseOrders.Acknowledgements." + filename);
        }

        [TestMethod]
        public void ParseExample1ToXml()
        {
            // Sample EDI from http://www.adobe.com/partnerportal/edi/pdf/855_Outbound_ANSIX12_4010_29jun04_000.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_Adobe.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseExample1AndUnparse()
        {
            string originalX12 = new StreamReader(GetEdi("Example1_Adobe.txt")).ReadToEnd();

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_Adobe.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(originalX12, x12);
        }
    }
}
