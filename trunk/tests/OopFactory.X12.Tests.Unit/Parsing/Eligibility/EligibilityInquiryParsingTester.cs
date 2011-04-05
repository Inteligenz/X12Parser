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

namespace OopFactory.X12.Tests.Unit.Parsing.Eligibility
{
    [TestClass]
    public class EligibilityInquiryParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Eligibility." + filename);
        }

        [TestMethod]
        public void ParseSample1InquiryAndFormat()
        {
            var parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1_270Inquiry.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }

        [TestMethod]
        public void ParseSample1InquiryToXml()
        {
            var parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1_270Inquiry.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }
    }
}
