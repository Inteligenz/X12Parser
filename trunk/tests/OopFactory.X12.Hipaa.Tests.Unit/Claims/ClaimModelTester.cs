using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Claims;
using OopFactory.X12.Hipaa.Claims.Services;

namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    [TestClass]
    public class ClaimModelTester
    {
        [TestMethod]
        public void SerializationTest1()
        {
            var document = new ClaimDocument();

            var claim = new Claim
            {
                Type = ClaimTypeEnum.Institutional,
                PatientControlNumber = "756048Q",
                TotalClaimChargeAmount = 89.93M
            };

            document.Claims.Add(claim);
            string xml = document.Serialize();

            Trace.Write(xml);
        }

        [TestMethod]
        public void TransformToClaimTest1()
        {

            var service = new ClaimTransformationService();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim1.txt");

            var document = service.Transform837ToClaimDocument(stream);

            string xml = document.Serialize();
            Trace.Write(xml);

            Assert.AreEqual(1, document.Claims.Count, "Expected one claim");

            Claim claim = document.Claims.First();

            // Box 1
            Assert.AreEqual("JONES HOSPITAL", claim.ServiceLocation.Name.LastName, "Unexpected Billing Provider Last Name");
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.ServiceLocation.Address.Line1, "Unexpected Billing Provider Adddress Line 1");
            Assert.AreEqual("CENTERVILLE", claim.ServiceLocation.Address.City, "Unexpected Billing Provider Address City");
            Assert.AreEqual("PA", claim.ServiceLocation.Address.StateCode, "Unexpected Billing Provider Address State Code");
            Assert.AreEqual("17111", claim.ServiceLocation.Address.PostalCode, "Unexpected Billing Provider Address Postal Code");
            // Box 2
            Assert.AreEqual(ClaimTypeEnum.Institutional, claim.Type);
            Assert.AreEqual("JONES HOSPITAL", claim.PayToProvider.Name.LastName, "Unexpected Billing Provider Last Name");
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.PayToProvider.Address.Line1, "Unexpected Billing Provider Adddress Line 1");
            Assert.AreEqual("CENTERVILLE", claim.PayToProvider.Address.City, "Unexpected Billing Provider Address City");
            Assert.AreEqual("PA", claim.PayToProvider.Address.StateCode, "Unexpected Billing Provider Address State Code");
            Assert.AreEqual("17111", claim.PayToProvider.Address.PostalCode, "Unexpected Billing Provider Address Postal Code");
            // Box 3a
            Assert.AreEqual("756048Q", claim.PatientControlNumber, "Unexpected PatientControlNumber");
            // Box 3b
            Assert.AreEqual("TEST MEDICAL RECORD NUMBER", claim.MedicalRecordNumber, "Unexpected MedicalRecordNumber");
            // Box 4
        }
    }
}
