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

namespace OopFactory.X12.Tests.Unit.Parsing.BenefitEnrollments
{
    [TestClass]
    public class BenefitEnrollmentParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.BenefitEnrollments." + filename);
        }

        private void ParseX12(Stream stream)
        {
            var xml = new X12Parser().Parse(stream).Serialize();
            Trace.Write(xml);            
        }

        [TestMethod]
        public void ParseSample1ToX12Xml()
        {
            var stream = GetEdi("Sample1.txt");

            ParseX12(stream);
        }

        [TestMethod]
        public void ParseAndUnparseX12()
        {
            string before = new StreamReader(GetEdi("Sample1.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1.txt"));

            string after = interchange.SerializeToX12(true);
            Trace.Write(after);
            Assert.AreEqual(before, after);
        }
    }
}
