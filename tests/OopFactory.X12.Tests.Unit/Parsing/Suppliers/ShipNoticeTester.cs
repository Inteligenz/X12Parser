using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Presentation.Suppliers;

namespace OopFactory.X12.Tests.Unit.Parsing.Suppliers
{
    [TestClass]
    public class ShipNoticeTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Suppliers." + filename);
        }
        [TestMethod]
        public void ParseToX12Xml_Sample1()
        {
            var xml = new X12Parser().Parse(GetEdi("Sample1.edi")).Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample2()
        {
            var xml = new X12Parser().Parse(GetEdi("Sample2.edi")).Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample3()
        {
            var xml = new X12Parser().Parse(GetEdi("Sample2.edi")).Serialize();
            Trace.Write(xml);
        }

        
    }
}
