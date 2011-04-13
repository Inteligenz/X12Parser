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
        public void Parse4010Example1ToXmlWithoutComments()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_CaliforniaISO.txt"));
            string xml = interchange.Serialize(true);
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
    }
}
