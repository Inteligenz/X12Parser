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
        private Stream GetShipNoticeEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Suppliers." + filename);
        }

        [TestMethod]
        public void UnbundleItemsFrom856Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetShipNoticeEdi("Sample1.edi"));

            var list = parser.UnbundleLoop(interchange, "ITEM");
            foreach (var item in list)
            {
                Trace.WriteLine("...");
                Trace.WriteLine(item.SerializeToX12(true));
            }
        }
    }
}
