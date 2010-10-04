using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using OopFactory.X12;
using OopFactory.X12.Model;
using OopFactory.X12.Model.Claims;

namespace OopFactory.X12.Tests.Unit.Parsing.Claims
{
    [TestClass]
    public class InstitutionalClaimParsingTester
    {
        #region constants
        private const string UB_SAMPLE_1 =
@"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00200*000000031*0*T*:~
  GS*HC*901234572000*908887732000*20070816*1615*31*X*004010X096A1~
    ST*837*0034~
      BHT*0019*00*3920394930203*20070816*1615*CH~
      REF*87*004010X096A1~
      NM1*41*2*HOWDEE HOSPITAL*****XX*0123456789~
        PER*IC*BETTY RUBBLE*TE*9195551111~
      NM1*40*2*BLUE CROSS BLUE SHIELD OF NC*****46*987654321~
      HL*1**20*1~
        PRV*BI*ZZ*203BA0200N~
        NM1*85*2*HOWDEE HOSPITAL*****XX*0123456789~
          N3*123 HOWDEE BOULEVARD~
          N4*DURHAM*NC*27701~
          REF*1J*654~
          PER*IC*BETTY RUBBLE*TE*9195551111*FX*6145551212~
        HL*2*1*22*0~
          SBR*P*18*XYZ1234567******BL~
          PAT*********Y~
          NM1*IL*1*DOUGH*MARY****MI*12312312312~
            N3*BOX 12312~
            N4*DURHAM*NC*27715~
            DMG*D8*19670807*F~
          NM1*PR*2*BLUE CROSS BLUE SHIELD OF NC*****PI*987654321~
          CLM*2235057*200***13:A:1*Y**Y*A*********N~
            DTP*434*RD8*20070730-20070730~
            CL1*1*9*01~
            AMT*C5*160~
            REF*F8*ASD0000123~
            HI*BK:25000~
            HI*BF:78901~
            HI*BR:4491:D8:20070730~
            HI*BH:41:D8:20070501*BH:27:D8:20070715*BH:33:D8:20070415*BH:C2:D8:20070410~
            HI*BE:30:::20~
            HI*BG:01~
            NM1*71*1*SMITH*ELIZABETH*AL***34*243898989~
              REF*1G*P97777~
            LX*1~
              SV2*0300*HC:81000*120*UN*1~
              DTP*472*D8*20070730~
            LX*2~
              SV2*0320*HC:76092*50*UN*1~
              DTP*472*D8*20070730~
            LX*3~
              SV2*0270*HC:J1120*30*UN*1~
              DTP*472*D8*20070730~
              SE*44*0034~
  GE*1*30~
IEA*1*000000031~";
        #endregion

        private Claim ParseX12(string edi)
        {
            var service = new X12ParsingService(true);

            var xml = service.ParseToDomainXml(edi);

            List<Claim> a = new List<Claim>();
            a.Add(new Claim());

            var claims = ModelExtensions.Deserialize<List<Claim>>(xml);

            Assert.AreEqual(1, claims.Count);
            return claims.First();
        }

        [TestMethod]
        public void ParseToX12Xml()
        {
            var service = new X12ParsingService(true);

            var xml = service.ParseToXml(UB_SAMPLE_1);
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseAndValidateBillingProviderLoopTest()
        {
            Claim claim = ParseX12(UB_SAMPLE_1);
            Provider billingProvider = claim.Providers.First(p => p.EntityIdentifier.Text == "Billing Provider");

            Assert.IsNotNull(billingProvider);
            Assert.AreEqual("0123456789", billingProvider.Npi);
            Assert.IsFalse(billingProvider.Name.Qualifier == EntityTypeQualifierEnum.Person);
            Assert.AreEqual("HOWDEE HOSPITAL", billingProvider.Name.Last);
            Assert.AreEqual("123 HOWDEE BOULEVARD", billingProvider.Address.Line1);
            Assert.AreEqual("DURHAM", billingProvider.Address.City);
            Assert.AreEqual("NC", billingProvider.Address.StateCode);
            Assert.AreEqual("27701", billingProvider.Address.PostalCode);

            Contact contact = billingProvider.Contacts.FirstOrDefault();
            Assert.IsNotNull(contact);
            Assert.AreEqual("BETTY RUBBLE", contact.Name);
            Assert.AreEqual(2, contact.Communications.Count);
            Assert.AreEqual("Telephone", contact.Communications[0].Qualifier.Text);
            Assert.AreEqual("9195551111", contact.Communications[0].Number);
            Assert.AreEqual("Facsimile", contact.Communications[1].Qualifier.Text);
            Assert.AreEqual("6145551212", contact.Communications[1].Number);

            Assert.AreEqual("1J", billingProvider.Identifications[0].Qualifier.Code);
            Assert.AreEqual("Facility ID Number", billingProvider.Identifications[0].Qualifier.Text);
            Assert.AreEqual("654", billingProvider.Identifications[0].Number);

            Assert.AreEqual("203BA0200N", billingProvider.Speciality.Code);
        }

        [TestMethod]
        public void ParseAndValidateSubscriberLoopTest()
        {
            Claim claim = ParseX12(UB_SAMPLE_1);
            // NM1 SEGMENT
            Assert.IsTrue(claim.Subscriber.Name.Qualifier == EntityTypeQualifierEnum.Person);
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

        [TestMethod]
        public void ParseAndValidateServiceLines()
        {
            Claim claim = ParseX12(UB_SAMPLE_1);
            Assert.AreEqual(3, claim.ServiceLines.Count);
            Assert.AreEqual(1, claim.ServiceLines[0].AssignedNumber);
            Assert.AreEqual("0300", claim.ServiceLines[0].Revenue.Code);
            Assert.AreEqual("81000", claim.ServiceLines[0].Procedure.Number);
            Assert.AreEqual(120m, claim.ServiceLines[0].ChargeAmount);
            Assert.AreEqual("UN", claim.ServiceLines[0].Unit);
            Assert.AreEqual(1m, claim.ServiceLines[0].Quantity);
            Assert.AreEqual(DateTime.Parse("2007-07-30"), claim.ServiceLines[0].DateOfServiceFrom);
            Assert.IsFalse(claim.ServiceLines[0].DateOfServiceTo.HasValue);
            Assert.IsFalse(claim.ServiceLines[0].AssessmentDate.HasValue);
        }
    }
}
