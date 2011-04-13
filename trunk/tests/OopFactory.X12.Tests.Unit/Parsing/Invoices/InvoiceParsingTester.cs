using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Parsing.Invoices
{
    [TestClass]
    public class InvoiceParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Invoices." + filename);
        }
        [TestMethod]
        public void Parse4010Example1ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_CaliforniaISO.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example1AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("4010_Example1_CaliforniaISO.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_CaliforniaISO.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
        }

        [TestMethod]
        public void Parse4010Example2ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example2_ManualBilling.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example2AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("4010_Example2_ManualBilling.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example2_ManualBilling.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            //Assert.AreEqual(orignalX12, x12);
        }
        [TestMethod]
        public void Parse4010Example3ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example3_MultiInvoice.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example3AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("4010_Example3_MultiInvoice.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example3_MultiInvoice.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            //Assert.AreEqual(orignalX12, x12);
        }
    }
}
