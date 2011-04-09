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

namespace OopFactory.X12.Tests.Unit.Unbundling
{
    [TestClass]
    public class UnbundlingTester
    {
        private Stream GetProfessionalClaimEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Claims.ProfessionalClaims." + filename);
        }

        private Stream GetShipNoticeEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Suppliers." + filename);
        }

        [TestMethod]
        public void UnbundleItemsFrom856Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetShipNoticeEdi("Sample1.edi"));

            var list = parser.UnbundleByLoop(interchange, "ITEM");
            foreach (var item in list)
            {
                Trace.WriteLine("...");
                Trace.WriteLine(item.SerializeToX12(true));
            }
        }

        [TestMethod]
        public void UnbundleClaimsFrom837Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetProfessionalClaimEdi("5010_Example2_WithRepeats.txt"));

            var list = parser.UnbundleByLoop(interchange, "2300");
            foreach (var item in list)
            {
                Trace.WriteLine("...");
                Trace.WriteLine(item.SerializeToX12(true));
            }
        }
    }
}
