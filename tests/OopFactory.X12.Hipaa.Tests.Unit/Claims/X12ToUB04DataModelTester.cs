using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using OopFactory.X12.Hipaa.Claims;
#if DEBUG
using OopFactory.X12.Hipaa.Claims.Services;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;
/*
namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    [TestClass]
    public class X12ToUB04DataModelTester
    {
        [TestMethod]
        public void SerializeUB04Claim()
        {
            
            var claim = new UB04Claim
                            {
                                Field01_01_BillingProviderLastName = "Doe",
                                Field17_PatientDischargeStatusSpecified = true
                            };

            //claim.Field39a_Amount.Add(new UB04ValueCodesAndAmounts { ValueCode = "A4", Amount = "45.67" });
            //claim.Field42_49_ServiceLines.Add(new UB04ServiceLine_2300Loop { Field42_RevenueCode = "0300", Field47_TotalCharges = 100 });
            //claim.Field42_49_ServiceLines.Add(new UB04ServiceLine_2300Loop { Field42_RevenueCode = "0301", Field47_TotalCharges = 200 });
            System.Diagnostics.Trace.Write(claim.Serialize());
            
        }
        
        [TestMethod]
        public void InstitutionalClaim1ToModel()
        {
            // get the x12 doc into a stream
            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim4010.txt");
            // new up a ClaimTransformationService object
            var claimSvc = new ClaimTransformationService();

            // send the x12 stream in to obtain a claim object
            var claim = claimSvc.TransformX12837ToUB04Model(stream);

            Assert.AreEqual("756048Q", claim.Field03a_PatientControlNumber);
            Assert.AreEqual(Convert.ToDecimal("89.93"), claim.Field47_SummaryTotalCharges);
            Assert.AreEqual(Convert.ToDecimal("89.93"), claim.Field48_SummaryTotalNonCoveredCharges);
            Assert.AreEqual("JOHN", claim.Field02_02_PayToFirstName);
            Assert.AreEqual("T", claim.Field02_03_PayToMiddleName);
            Assert.AreEqual("DOE", claim.Field02_01_PayToLastName);
            Assert.AreEqual("125 CITY AVENUE", claim.Field02_04_PayToAddress1);
            Assert.AreEqual("CENTERVILLE", claim.Field02_06_PayToCity);
            Assert.AreEqual("PA", claim.Field02_08_PayToState);
            Assert.AreEqual("17111", claim.Field02_09_PayToZip);
            Assert.AreEqual("756048Q", claim.Field03a_PatientControlNumber);
            Assert.AreEqual("14A1", claim.Field04_TypeOfBill);
            Assert.AreEqual("3", claim.Field14_TypeOfVisit);
            Assert.AreEqual("987654080", claim.Field05_FederalTaxId);
            Assert.AreEqual("1", claim.Field15_SourceOfAdmission);
            Assert.AreEqual("09", claim.Field18_ConditionCode01);
            Assert.AreEqual("A1", claim.Field31_OccurrenceCode_a);
            Assert.AreEqual(Convert.ToDateTime("1926-11-11T00:00:00"), claim.Field31_OccurrenceCodeDate_a);
            Assert.AreEqual("A2", claim.Field32_OccurrenceCode_a);
            Assert.AreEqual(Convert.ToDateTime("1991-11-01T00:00:00"), claim.Field32_OccurrenceCodeDate_a);
            Assert.AreEqual("B1", claim.Field33_OccurrenceCode_a);
            Assert.AreEqual(Convert.ToDateTime("1926-11-11T00:00:00"), claim.Field33_OccurrenceCodeDate_a);
            Assert.AreEqual(Convert.ToDateTime("1987-01-01T00:00:00"), claim.Field34_OccurrenceCodeDate_a);
            //Assert.AreEqual("030005074A", claim.Field60a_InsuredsIniqueIdentificationNumber);
            //Assert.AreEqual("434", claim.Field14_TypeOfVisit);
            //Assert.AreEqual("D8", claim.Field15_SourceOfAdmission);
            //Assert.AreEqual("19960911", claim.Field16_DischargeHour);
            //Assert.AreEqual("00435", claim.Field57_OtherProviderIdentifier);
            Assert.AreEqual("JONES HOSPITAL", claim.Field01_01_BillingProviderLastName);
            Assert.AreEqual("JONES", claim.Field76_AttendingProviderLastName);
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.Field01_04_BillingProviderAddress1);
            Assert.AreEqual("CENTERVILLE", claim.Field01_06_BillingProviderCity);
            Assert.AreEqual("PA", claim.Field01_08_BillingProviderState);
            Assert.AreEqual("17111", claim.Field01_09_BillingProviderZip);

            // serialize the object to xml so we can view it 
            Console.Write(claim.Serialize());
        }

        [TestMethod]
        public void InstitutionalClaimModelToFoXml()
        {
            // Load Input Mock data
            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.UB04ClaimModel1.xml");
            string xml = new StreamReader(stream).ReadToEnd();
            UB04Claim claim = UB04Claim.Deserialize(xml);

            // Transform to FO-XML
            var service = new ClaimTransformationService();
            string foXml = service.TransformUB04ClaimToFoXml(claim, @"C:\Temp\UB04_Red.gif");

            Trace.Write(foXml);

            // Use FO Processor to generate PDF document
            byte[] pdf = RenderFile(foXml);
            FileStream fs = new FileStream("C:\\Temp\\OopFactory Institutional Claim.pdf", FileMode.Create);
            fs.Write(pdf, 0, pdf.Length);
            fs.Close();

        }

        private byte[] RenderFile(string foXml)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(foXml);
            var driver = Fonet.FonetDriver.Make();
            MemoryStream mstream = new MemoryStream();
            driver.Render(doc, mstream);
            return mstream.ToArray();
        }
    }
}*/
#endif
