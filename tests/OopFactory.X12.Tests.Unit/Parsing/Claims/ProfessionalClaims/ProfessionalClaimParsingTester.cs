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
using System.Xml;

namespace OopFactory.X12.Tests.Unit.Parsing.Claims.ProfessionalClaims
{
    [TestClass]
    public class ProfessionalClaimParsingTester
    {
        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Claims.ProfessionalClaims." + filename);
        }

        [TestMethod]
        public void Parse4010Example1ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_PatientIsSubscriber.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example1ToXmlWithoutComments()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_PatientIsSubscriber.txt"));
            string xml = interchange.Serialize(true);
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example1AndUnparse()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example1_PatientIsSubscriber.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }

        [TestMethod]
        public void Parse4010Example2ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example2_PatientIsNotSubscriber.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse4010Example2AndUnparse()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("4010_Example2_PatientIsNotSubscriber.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }

        [TestMethod]
        public void Parse5010Example1ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example1_HealthInsurance.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);
        }

        [TestMethod]
        public void Parse5010Example1AndUnparse()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example1_HealthInsurance.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }

        [TestMethod]
        public void Parse5010Example2ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example2_Encounter.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            Assert.AreEqual("SUBMITTERS.ID  ", doc.SelectSingleNode("/Interchange/ISA/ISA06").InnerText, "Submitter ID not expected.");
            Assert.AreEqual("HC", doc.SelectSingleNode("/Interchange/FunctionGroup/GS/GS01").InnerText, "Function Identifier Code Not expected.");
            Assert.AreEqual("0021", doc.SelectSingleNode("/Interchange/FunctionGroup/Transaction/@ControlNumber").Value, "Control Number not expected.");
            Assert.AreEqual("PREMIER BILLING SERVICE", doc.SelectSingleNode("/Interchange/FunctionGroup/Transaction/Loop[@LoopId='1000A']/NM1/NM103").InnerText, "Submitter name not expected.");
            Assert.AreEqual("BEN KILDARE SERVICE", doc.SelectSingleNode("/Interchange/FunctionGroup/Transaction/HierarchicalLoop[@Id='1']/Loop[@LoopId='2010AA']/NM1/NM103").InnerText, "Billing Provider Name not expected.");
            Assert.AreEqual("26462967", doc.SelectSingleNode("/Interchange/FunctionGroup/Transaction/HierarchicalLoop[@Id='1']/HierarchicalLoop[@Id='2']/Loop[@LoopId='2300']/CLM/CLM01").InnerText, "Claim Number not expected.");
        }

        [TestMethod]
        public void Parse5010Example2AndUnparse()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example2_Encounter.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }

        [TestMethod]
        public void Parse5010Example3ToXml()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example3_COB.txt"));
            string xml = interchange.Serialize();
            Trace.Write(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
        }
        [TestMethod]
        public void Parse5010Example3AndUnparse()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("5010_Example3_COB.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);
        }
    }
}
