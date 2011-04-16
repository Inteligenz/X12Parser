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
using OopFactory.X12.Transformations;

namespace OopFactory.X12.Tests.Unit.Parsing.Eligibility
{
    [TestClass]
    public class EligibilityInquiryParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Eligibility." + filename);
        }

        private void PrintHtmlToFile(string html, string fileName)
        {
            System.IO.FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("<html><body>");
            writer.WriteLine(html);
            writer.WriteLine("</body></html>");
            writer.Close();
            fs.Close();

        }

        [TestMethod]
        public void Parse270_4010Example1ToXml()
        {
            // Example from http://www.dhhs.state.sc.us/dhhsnew/hipaa/webfiles/270-271.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_270Inquiry_4010_DHHS.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse270_4010Example1ToHtml()
        {
            // Example from http://www.dhhs.state.sc.us/dhhsnew/hipaa/webfiles/270-271.pdf
            X12Parser parser = new X12Parser();
            var service = new X12HtmlTransformationService(new X12EdiParsingService(false));
            string html = service.Transform(new StreamReader(GetEdi("Example1_270Inquiry_4010_DHHS.txt")).ReadToEnd());

            Trace.Write(html);

#if DEBUG
            PrintHtmlToFile(html, @"c:\Temp\270-4010Inquiry.html");            
#endif
        }
        
        [TestMethod]
        public void Parse270_4010Example1AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("Example1_270Inquiry_4010_DHHS.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example1_270Inquiry_4010_DHHS.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
        }

        [TestMethod]
        public void Parse271_4010Example2ToXml()
        {
            // Example from http://www.dhhs.state.sc.us/dhhsnew/hipaa/webfiles/270-271.pdf
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_271Response_4010_DHHS.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse270_4010Example2ToHtml()
        {
            // Example from http://www.dhhs.state.sc.us/dhhsnew/hipaa/webfiles/270-271.pdf
            X12Parser parser = new X12Parser();
            var service = new X12HtmlTransformationService(new X12EdiParsingService(false));
            string html = service.Transform(new StreamReader(GetEdi("Example2_271Response_4010_DHHS.txt")).ReadToEnd());

            Trace.Write(html);

#if DEBUG
            PrintHtmlToFile(html, @"c:\Temp\271-4010Response.html");
#endif
        }

        [TestMethod]
        public void Parse271_4010Example2AndUnparse()
        {
            string orignalX12 = new StreamReader(GetEdi("Example2_271Response_4010_DHHS.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Example2_271Response_4010_DHHS.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
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
