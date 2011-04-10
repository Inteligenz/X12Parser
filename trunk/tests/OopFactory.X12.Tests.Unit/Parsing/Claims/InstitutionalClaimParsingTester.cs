using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using OopFactory.X12;
using System.Reflection;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit.Parsing.Claims
{
    [TestClass]
    public class InstitutionalClaimParsingTester
    {

        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Claims." + filename);
        }

        [TestMethod]
        public void ParseToX12Xml()
        {
            X12Parser parser = new X12Parser();

            var xml = parser.Parse(GetEdi("Sample1.txt")).Serialize();
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
