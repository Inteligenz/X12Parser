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
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

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
                                Field01_01_ProviderLastName = "Doe",
                                Field01_02_ProviderFirstName = "FirstName",
                                Field17_PatientDischargeStatusSpecified = true
                            };


            //claim.Field39_41_ValueCodesAndAmounts.Add(new UB04ValueCodesAndAmounts { ValueCode = "A4", Amount = "45.67" });

            claim.Field42_49_ServiceLines.Add(new UB04ServiceLine { Field42_RevenueCode = "0300", Field47_TotalCharges = 100 });
            claim.Field42_49_ServiceLines.Add(new UB04ServiceLine { Field42_RevenueCode = "0301", Field47_TotalCharges = 200 });

            System.Diagnostics.Trace.Write(claim.Serialize());
        }

        [TestMethod]
        public void InstitutionalClaim1ToModel()
        {
            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim1.txt");

            var claimSvc = new ClaimTransformationService();
            var claim = claimSvc.TransformX12837ToUB04Model(stream);

            Assert.AreEqual("756048Q", claim.Field03a_PatientControlNumber);
            Assert.AreEqual(Convert.ToDecimal("89.93"), claim.Field55a_EstimatedAmountDue);
            Assert.AreEqual("19960911", claim.Field16_DischargeHour);
            Assert.AreEqual("JOHN", claim.Field58a_InsuredsName);
            Assert.AreEqual("T", claim.Field58b_InsuredsName);
            Assert.AreEqual("DOE", claim.Field58c_InsuredsName);
            Assert.AreEqual("030005074A", claim.Field60a_InsuredsIniqueIdentificationNumber);
            Assert.AreEqual("434", claim.Field14_TypeOfVisit);
            Assert.AreEqual("D8", claim.Field15_SourceOfAdmission);
            Assert.AreEqual("19960911", claim.Field16_DischargeHour);
            Assert.AreEqual("MEDICARE B", claim.Field50a_PayerName);
            Assert.AreEqual("00435", claim.Field57_OtherProviderIdentifier);

            //Assert.AreEqual("JONES HOSPITAL", claim.Field76_AttendingProviderLastName);

            // serialize the object to xml so we can view it     
            Trace.Write(claim.Serialize());
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

#if DEBUG
            // Use FO Processor to generate PDF document
            byte[] pdf = RenderFile(foXml);
            FileStream fs = new FileStream("C:\\Temp\\OopFactory Institutional Claim.pdf", FileMode.Create);
            fs.Write(pdf, 0, pdf.Length);
            fs.Close();
#endif

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
}
