using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using System.IO;
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
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml("856", GetEdi("Sample1.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml("856", GetEdi("Sample2.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample3()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml("856", GetEdi("Sample3.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToDomainXml_Sample1()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample1.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToDomainXml_Sample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample2.edi"));
            Trace.Write(xml);
        }
        
        [TestMethod]
        public void ParseToDomainXml_Sample3()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample3.edi"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void RenderSample1()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample1.edi"));

            var renderingService = new BillOfLadingPortraitRenderingService();
            byte[] pdf = renderingService.CreatePdf(xml);
            System.IO.FileStream fs = new FileStream(@"c:\Temp\BillOfLadenPortraitSample1.pdf", FileMode.Create, FileAccess.Write);
            fs.Write(pdf, 0, pdf.Length);
            fs.Close();
        }
        [TestMethod]
        public void RenderSample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample2.edi"));

            var renderingService = new BillOfLadingPortraitRenderingService();
            byte[] pdf = renderingService.CreatePdf(xml);
            System.IO.FileStream fs = new FileStream(@"c:\Temp\BillOfLadenPortraitSample2.pdf", FileMode.Create, FileAccess.Write);
            fs.Write(pdf, 0, pdf.Length);
            fs.Close();
        }
        [TestMethod]
        public void RenderSample3()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml("856", GetEdi("Sample3.edi"));

            var renderingService = new BillOfLadingPortraitRenderingService();
            byte[] pdf = renderingService.CreatePdf(xml);
            System.IO.FileStream fs = new FileStream(@"c:\Temp\BillOfLadenPortraitSample3.pdf", FileMode.Create, FileAccess.Write);
            fs.Write(pdf, 0, pdf.Length);
            fs.Close();
        }
    }
}
