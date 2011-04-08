using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using OopFactory.X12;
using OopFactory.X12.Model;
using OopFactory.X12.Model.Claims;
using System.Reflection;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit.Parsing.Claims
{
    [TestClass]
    public class InstitutionalClaimParsingTester
    {

        private Stream GetEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Claims." + filename);
        }

        private Claim ParseX12(Stream stream)
        {
            var service = new X12ParsingService(true);

            var xml = service.ParseToDomainXml(stream);
            Trace.Write(xml);
            var transactions = ModelExtensions.Deserialize<List<OopFactory.X12.Model.Claims.Transaction>>(xml);

            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(1, transactions[0].Claims.Count);
            return transactions[0].Claims[0];
        }

        [TestMethod]
        public void ParseToX12Xml()
        {
            var service = new X12ParsingService(true);

            var xml = service.ParseToXml( GetEdi("Sample1.txt"));
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseAndUnparseToX12()
        {
            string orignalX12 = new StreamReader(GetEdi("Sample1.txt")).ReadToEnd();
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(GetEdi("Sample1.txt"));
            string x12 = interchange.SerializeToX12(true);
            Trace.Write(x12);

            Assert.AreEqual(orignalX12, x12);
        }

        [TestMethod, Ignore]
        public void ParseAndValidateBillingProviderLoopTest()
        {
            Claim claim = ParseX12(GetEdi("Sample1.txt"));
            Provider billingProvider = claim.Providers.First(p => p.Name.Qualifier == "85");

            Assert.IsNotNull(billingProvider);
            Assert.AreEqual("0123456789", billingProvider.Npi);
            Assert.IsFalse(billingProvider.Name.IsPerson);
            Assert.AreEqual("HOWDEE HOSPITAL", billingProvider.Name.Last);
            Assert.AreEqual("123 HOWDEE BOULEVARD", billingProvider.Address.Line1);
            Assert.AreEqual("DURHAM", billingProvider.Address.City);
            Assert.AreEqual("NC", billingProvider.Address.StateCode);
            Assert.AreEqual("27701", billingProvider.Address.PostalCode);

            Contact contact = billingProvider.Contacts.FirstOrDefault();
            Assert.IsNotNull(contact);
            Assert.AreEqual("BETTY RUBBLE", contact.Name);
            Assert.AreEqual("9195551111", contact.Phone.Number);
            Assert.AreEqual("6145551212", contact.Fax);

            Assert.AreEqual("1J", billingProvider.Identifications[1].Qualifier);
            Assert.AreEqual("654", billingProvider.Identifications[1].Number);

            Assert.AreEqual("203BA0200N", billingProvider.Speciality.Code);
        }

        [TestMethod, Ignore]
        public void ParseAndValidateSubscriberLoopTest()
        {
            Claim claim = ParseX12(GetEdi("Sample1.txt"));
            // NM1 SEGMENT
            Assert.IsTrue(claim.Subscriber.Name.IsPerson);
            Assert.AreEqual("DOUGH", claim.Subscriber.Name.Last);
            Assert.AreEqual("MARY", claim.Subscriber.Name.First);
            Assert.AreEqual("DOUGH, MARY", claim.Subscriber.Name.FullName);
            Assert.AreEqual("12312312312", claim.Subscriber.MemberId);

            // N3 SEGMENT
            Assert.AreEqual("BOX 12312", claim.Subscriber.Address.Line1);
            Assert.IsTrue(string.IsNullOrEmpty(claim.Subscriber.Address.Line2));
            Assert.AreEqual("DURHAM", claim.Subscriber.Address.City);
            Assert.AreEqual("NC", claim.Subscriber.Address.StateCode);
            Assert.AreEqual("27715", claim.Subscriber.Address.PostalCode);

            // DMG SEGMENT
            Assert.AreEqual(DateTime.Parse("1967-08-07"), claim.Subscriber.DateOfBirth);
            Assert.AreEqual(GenderEnum.Female, claim.Subscriber.Gender);
        }

        [TestMethod, Ignore]
        public void ParseAndValidateOccurrences()
        {
            Claim claim = ParseX12(GetEdi("Sample1.txt"));
            Assert.AreEqual(4, claim.Occurrences.Count);
            Assert.AreEqual("41", claim.Occurrences[0].Code);
            Assert.AreEqual(DateTime.Parse("2007-05-01"), claim.Occurrences[0].Date);
            Assert.AreEqual("27", claim.Occurrences[1].Code);
            Assert.AreEqual(DateTime.Parse("2007-07-15"), claim.Occurrences[1].Date);
            Assert.AreEqual("33", claim.Occurrences[2].Code);
            Assert.AreEqual(DateTime.Parse("2007-04-15"), claim.Occurrences[2].Date);
            Assert.AreEqual("C2", claim.Occurrences[3].Code);
            Assert.AreEqual(DateTime.Parse("2007-04-10"), claim.Occurrences[3].Date);
        }

        [TestMethod, Ignore]
        public void ParseAndValidateServiceLines()
        {
            Claim claim = ParseX12(GetEdi("Sample1.txt"));
            Assert.AreEqual(3, claim.ServiceLines.Count);
            Assert.AreEqual(1, claim.ServiceLines[0].AssignedNumber);
            Assert.AreEqual("0300", claim.ServiceLines[0].Revenue.Code);
            Assert.AreEqual("81000", claim.ServiceLines[0].Procedure.Code);
            Assert.AreEqual(120m, claim.ServiceLines[0].ChargeAmount);
            Assert.AreEqual("UN", claim.ServiceLines[0].Unit);
            Assert.AreEqual(1m, claim.ServiceLines[0].Quantity);
            Assert.AreEqual(DateTime.Parse("2007-07-30"), claim.ServiceLines[0].DateOfServiceFrom);
            Assert.IsFalse(claim.ServiceLines[0].DateOfServiceTo.HasValue);
            Assert.IsFalse(claim.ServiceLines[0].AssessmentDate.HasValue);
        }
    }
}
