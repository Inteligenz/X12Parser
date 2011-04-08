using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12;
using OopFactory.X12.Model;
using OopFactory.X12.Model.Claims;
using System.IO;
using System.Reflection;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit.Parsing.Payments
{
    [TestClass]
    public class PaymentParsingTester
    {
        // Sample1 from http://b9962ed140049a571a710839f1f71c989aaf09ce.gripelements.com/ois/macsis/claims/macsis.hipaa.edi.sample.835.pdf

        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Payments." + filename);
        }

        [TestMethod]
        public void ParseToX12Xml()
        {
            var service = new X12ParsingService(true);

            var xml = service.ParseToXml(GetEdi("Sample1.txt"));
            
            //var xml = service.ParseToDomainXml(GetEdi("Sample1.txt"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseAndUnparseToX12()
        {
            string orignalX12 = new StreamReader(GetEdi("Sample1.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
        }
    }
}
