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

            // Box 1 - Service Location
            Assert.AreEqual("JONES HOSPITAL", claim.ServiceLocation.Name.LastName, "Unexpected Billing Provider Last Name");
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.ServiceLocation.Address.Line1, "Unexpected Billing Provider Adddress Line 1");
            Assert.AreEqual("CENTERVILLE", claim.ServiceLocation.Address.City, "Unexpected Billing Provider Address City");
            Assert.AreEqual("PA", claim.ServiceLocation.Address.StateCode, "Unexpected Billing Provider Address State Code");
            Assert.AreEqual("17111", claim.ServiceLocation.Address.PostalCode, "Unexpected Billing Provider Address Postal Code");
            // Box 2 - Pay To Provider
            Assert.AreEqual(ClaimTypeEnum.Institutional, claim.Type);
            Assert.AreEqual("JONES HOSPITAL", claim.PayToProvider.Name.LastName, "Unexpected Billing Provider Last Name");
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.PayToProvider.Address.Line1, "Unexpected Billing Provider Adddress Line 1");
            Assert.AreEqual("CENTERVILLE", claim.PayToProvider.Address.City, "Unexpected Billing Provider Address City");
            Assert.AreEqual("PA", claim.PayToProvider.Address.StateCode, "Unexpected Billing Provider Address State Code");
            Assert.AreEqual("17111", claim.PayToProvider.Address.PostalCode, "Unexpected Billing Provider Address Postal Code");
            // Box 3a - Patient Control Number
            Assert.AreEqual("756048Q", claim.PatientControlNumber, "Unexpected PatientControlNumber");
            // Box 3b - Type of Bill
            Assert.AreEqual("TEST MEDICAL RECORD NUMBER", claim.MedicalRecordNumber, "Unexpected MedicalRecordNumber");
            // Box 4 - Type of Bill
            Assert.AreEqual("14", claim.ServiceLocationInfo.FacilityCode, "Unexpected facility code");
            Assert.AreEqual("A", claim.ServiceLocationInfo.Qualifier, "Unexpected facility code qualifier");
            Assert.AreEqual("1", claim.ServiceLocationInfo.FrequencyTypeCode, "Unexpected frequency type code");
            // Box 5 - Federal Tax Number
            Assert.AreEqual("123456789", claim.PayToProvider.TaxId, "Unexpected Federal Tax ID");
            // Box 6 Statement Covers Period
            Assert.AreEqual(DateTime.Parse("1996-9-11"), claim.StatementFromDate, "Unexpected statement from date");
            Assert.AreEqual(DateTime.Parse("1996-9-11"), claim.StatementThroughDate, "Unexpected statement through date");
            // Box 7 - Filler

            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Box 8 - Patient Name
            Assert.AreEqual("DOE", patient.Name.LastName, "Unexpected patient last name");
            Assert.AreEqual("JOHN", patient.Name.FirstName, "Unexpected patient first name");
            Assert.AreEqual("T", patient.Name.MiddleName, "Unexpected patient middle name");
            Assert.AreEqual("030005074A", patient.MemberId);
           
        }
    }
}
