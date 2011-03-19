using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace OopFactory.X12.Tests.Unit.Parsing.Suppliers
{
    [TestClass]
    public class ShipNoticeTester
    {
        private string GetEdi(string filename)
        {
            Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Suppliers." + filename);
            StreamReader reader = new StreamReader(input);
            return reader.ReadToEnd();
        }
        [TestMethod]
        public void ParseToX12Xml_Sample1()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(GetEdi("Sample1.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(GetEdi("Sample2.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample3()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(GetEdi("Sample3.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToDomainXml_Sample1()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml(GetEdi("Sample1.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToDomainXml_Sample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml(GetEdi("Sample2.edi"));
            Trace.Write(xml);
        }
        
        [TestMethod]
        public void ParseToDomainXml_Sample3()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml(GetEdi("Sample3.edi"));
            Trace.Write(xml);
        }
    }
}
