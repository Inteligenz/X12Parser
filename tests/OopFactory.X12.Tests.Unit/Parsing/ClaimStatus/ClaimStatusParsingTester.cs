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

namespace OopFactory.X12.Tests.Unit.Parsing.ClaimStatus
{
    [TestClass]
    public class ClaimStatusParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.ClaimStatus." + filename);
        }

        [TestMethod]
        public void ParseSample1ToX12Xml()
        {
            var parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1_276Request.txt"));

            string xml = interchange.Serialize();

            Trace.Write(xml);
        }
    }
}
