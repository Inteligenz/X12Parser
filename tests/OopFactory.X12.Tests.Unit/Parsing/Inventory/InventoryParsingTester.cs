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

namespace OopFactory.X12.Tests.Unit.Parsing.Inventory
{
    [TestClass]
    public class InventoryParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Inventory." + filename);
        }

        [TestMethod]
        public void Parse4010Example1ToXml()
        {
            // Sample from http://www.sanmina-sci.com/Partners/edi_pdfs/846in.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_846Inquiry_4010.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example1AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("Example1_846Inquiry_4010.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_846Inquiry_4010.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
        }

        [TestMethod]
        public void Parse4010Example2ToXml()
        {
            // Sample from http://www.sanmina-sci.com/Partners/edi_pdfs/846in.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_846Inquiry_4010.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example2AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("Example2_846Inquiry_4010.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_846Inquiry_4010.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            //Assert.AreEqual(orignalX12, x12);
        }
    }
}
