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
        public void TransformToInstitutionalClaim4010Test()
        {

            var service = new ClaimTransformationService();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim4010.txt");

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
            Assert.AreEqual(DateTime.Parse("1996-9-11"), claim.StatementToDate, "Unexpected statement through date");
            // Box 7 - Filler

            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Box 8 - Patient Name
            Assert.AreEqual("DOE", patient.Name.LastName, "Unexpected patient last name");
            Assert.AreEqual("JOHN", patient.Name.FirstName, "Unexpected patient first name");
            Assert.AreEqual("T", patient.Name.MiddleName, "Unexpected patient middle name");
            Assert.AreEqual("030005074A", patient.MemberId);
           // Box 9 Patient Address
            Assert.AreEqual("125 CITY AVENUE", patient.Address.Line1, "Unexpected patient address line 1");
            Assert.AreEqual("CENTERVILLE", patient.Address.City, "Unexpected patient address city");
            Assert.AreEqual("PA", patient.Address.StateCode, "Unexpected patient address state code");
            Assert.AreEqual("17111", patient.Address.PostalCode, "Unexpected patient address postal code");
            // Box 10 Birthdate
            Assert.AreEqual(DateTime.Parse("1926-11-11"), patient.DateOfBirth);
            // Box 11 Sex
            Assert.AreEqual(GenderEnum.Male, patient.Gender);
            // Box 12 & 13 Admission Date and Hour
            Assert.AreEqual(DateTime.Parse("1996-09-10 2:02 PM"), claim.AdmissionDate);
            // Box 14 Admission Type

            // Box 15 Admission Source


            Assert.AreEqual(2, claim.ServiceLines.Count, "Unexpected number of service lines.");

            ServiceLine line = claim.ServiceLines[0];
            Assert.AreEqual("305", line.RevenueCode);
            Assert.AreEqual("85025", line.Procedure.ProcedureCode);
        }
        [TestMethod]
        public void TransformToInstitutionalClaim5010Test()
        {

            var service = new ClaimTransformationService();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

            var document = service.Transform837ToClaimDocument(stream);

            string xml = document.Serialize();

            Assert.AreEqual(1, document.Claims.Count, "Expected one claim");

            Claim claim = document.Claims.First();
            Trace.Write(claim.Serialize());
            Assert.AreEqual(ClaimTypeEnum.Institutional, claim.Type);
            // Box 1 - Service Location
            Assert.AreEqual("JONES HOSPITAL", claim.ServiceLocation.Name.LastName, "Unexpected Billing Provider Last Name");
            Assert.AreEqual("225 MAIN STREET BARKLEY BUILDING", claim.ServiceLocation.Address.Line1, "Unexpected Billing Provider Adddress Line 1");
            Assert.AreEqual("CENTERVILLE", claim.ServiceLocation.Address.City, "Unexpected Billing Provider Address City");
            Assert.AreEqual("PA", claim.ServiceLocation.Address.StateCode, "Unexpected Billing Provider Address State Code");
            Assert.AreEqual("17111", claim.ServiceLocation.Address.PostalCode, "Unexpected Billing Provider Address Postal Code");
            // Box 2 - Pay To Provider
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
            Assert.AreEqual("567891234", claim.PayToProvider.TaxId, "Unexpected Federal Tax ID");
            // Box 6 Statement Covers Period
            Assert.AreEqual(DateTime.Parse("1996-9-11"), claim.StatementFromDate, "Unexpected statement from date");
            Assert.AreEqual(DateTime.Parse("1996-9-11"), claim.StatementToDate, "Unexpected statement through date");
            // Box 7 - Filler

            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Box 8 - Patient Name
            Assert.AreEqual("DOE", patient.Name.LastName, "Unexpected patient last name");
            Assert.AreEqual("JOHN", patient.Name.FirstName, "Unexpected patient first name");
            Assert.AreEqual("T", patient.Name.MiddleName, "Unexpected patient middle name");
            Assert.AreEqual("030005074A", patient.MemberId);
            // Box 9 Patient Address
            Assert.AreEqual("125 CITY AVENUE", patient.Address.Line1, "Unexpected patient address line 1");
            Assert.AreEqual("CENTERVILLE", patient.Address.City, "Unexpected patient address city");
            Assert.AreEqual("PA", patient.Address.StateCode, "Unexpected patient address state code");
            Assert.AreEqual("17111", patient.Address.PostalCode, "Unexpected patient address postal code");
            // Box 10 Birthdate
            Assert.AreEqual(DateTime.Parse("1926-11-11"), patient.DateOfBirth);
            // Box 11 Sex
            Assert.AreEqual(GenderEnum.Male, patient.Gender);
            // Box 12 & 13 Admission Date and Hour
            Assert.AreEqual(DateTime.Parse("1996-09-11 2:02 PM"), claim.AdmissionDate);
            // Box 14 Admission Type

            // Box 15 Admission Source

            // Box 16 Discharge Hour

            // Box 17 Discharge Status

            // Box 18 through 28 Condition Codes
            Assert.AreEqual(14, claim.Conditions.Count);
            Assert.AreEqual("01", claim.Conditions[0].Code);
            Assert.AreEqual("02", claim.Conditions[1].Code);
            Assert.AreEqual("03", claim.Conditions[2].Code);
            Assert.AreEqual("04", claim.Conditions[3].Code);
            Assert.AreEqual("05", claim.Conditions[4].Code);
            Assert.AreEqual("06", claim.Conditions[5].Code);
            Assert.AreEqual("07", claim.Conditions[6].Code);
            Assert.AreEqual("08", claim.Conditions[7].Code);
            Assert.AreEqual("09", claim.Conditions[8].Code);
            Assert.AreEqual("10", claim.Conditions[9].Code);
            Assert.AreEqual("11", claim.Conditions[10].Code);

            // Box 29

            // Box 30

            // Box 31 through 34 - Occurrences
            Assert.AreEqual(8, claim.Occurrences.Count);
            Assert.AreEqual("A1", claim.Occurrences[0].Code);
            Assert.AreEqual(DateTime.Parse("1926-11-11"), claim.Occurrences[0].Date);
            Assert.AreEqual("A2", claim.Occurrences[1].Code);
            Assert.AreEqual(DateTime.Parse("1991-11-01"), claim.Occurrences[1].Date);
            Assert.AreEqual("B1", claim.Occurrences[2].Code);
            Assert.AreEqual(DateTime.Parse("1926-11-11"), claim.Occurrences[2].Date);
            Assert.AreEqual("B2", claim.Occurrences[3].Code);
            Assert.AreEqual(DateTime.Parse("1987-1-1"), claim.Occurrences[3].Date);
            Assert.AreEqual("C1", claim.Occurrences[4].Code);
            Assert.AreEqual(DateTime.Parse("1926-11-11"), claim.Occurrences[4].Date);
            Assert.AreEqual("C2", claim.Occurrences[5].Code);
            Assert.AreEqual(DateTime.Parse("1991-11-1"), claim.Occurrences[5].Date);
            Assert.AreEqual("D1", claim.Occurrences[6].Code);
            Assert.AreEqual(DateTime.Parse("1926-11-11"), claim.Occurrences[6].Date);
            Assert.AreEqual("D2", claim.Occurrences[7].Code);
            Assert.AreEqual(DateTime.Parse("1987-1-1"), claim.Occurrences[7].Date);
            
            // Box 35 through 36 - Occurrence Spans
            Assert.AreEqual(4, claim.OccurrenceSpans.Count);
            Assert.AreEqual("A1", claim.OccurrenceSpans[0].Code);
            Assert.AreEqual(DateTime.Parse("1926-11-11"), claim.OccurrenceSpans[0].FromDate);
            Assert.AreEqual(DateTime.Parse("1927-12-31"), claim.OccurrenceSpans[0].ThroughDate);

            // Box 37 - Filler

            // Box 38 - Responsible Party

            // Box 39 through 41 - Value Codes
            Assert.AreEqual(14, claim.Values.Count);
            Assert.AreEqual("A2", claim.Values[0].Code);
            Assert.AreEqual(15.31m, claim.Values[0].Amount);
            Assert.AreEqual("N2", claim.Values[13].Code);
            Assert.AreEqual(28.31m, claim.Values[13].Amount);

            // Box 42 through 49 - Service Lines

            Assert.AreEqual(2, claim.ServiceLines.Count, "Unexpected number of service lines.");

            ServiceLine line = claim.ServiceLines[0];
            Assert.AreEqual("0305", line.RevenueCode);
            Assert.AreEqual("85025", line.Procedure.ProcedureCode);
            Assert.AreEqual(13.39m, line.ChargeAmount);
            Assert.AreEqual(1, line.Quantity);
            Assert.AreEqual(DateTime.Parse("1996-9-11"), line.ServiceDateFrom);
            Assert.IsNull(line.OperatingPhysician);

            line = claim.ServiceLines[1];
            Assert.AreEqual("0730", line.RevenueCode);
            Assert.AreEqual("93005", line.Procedure.ProcedureCode);
            Assert.AreEqual("AA", line.Procedure.Modifier1);
            Assert.AreEqual("BB", line.Procedure.Modifier2);
            Assert.AreEqual("CC", line.Procedure.Modifier3);
            Assert.AreEqual("DD", line.Procedure.Modifier4);
            Assert.AreEqual("Test Procedure", line.Procedure.Description);
            Assert.AreEqual(76.54m, line.ChargeAmount);
            Assert.AreEqual(3, line.Quantity);
            Assert.AreEqual(11.15m, line.NonCoveredChargeAmount);
            Assert.AreEqual(DateTime.Parse("1996-9-11"), line.ServiceDateFrom);
            Assert.AreEqual(1, line.Notes.Count);
            Assert.AreEqual("TPO", line.Notes[0].Code);

            Assert.IsNotNull(line.OperatingPhysician);
            Assert.AreEqual("JONES", line.OperatingPhysician.Name.LastName);
            Assert.AreEqual("JOHN", line.OperatingPhysician.Name.FirstName);
            Assert.AreEqual("B99937", line.OperatingPhysician.Npi);
            Assert.AreEqual("363LP0200N", line.OperatingPhysician.ProviderInfo.Id);

            // Box 50 through 55 - Payers

            // Box 56 - NPI

            // Box 57 - Other Provider ID

            // Box 68 through 62 - Insured

            // Box 63 through 65 - Authorizations

            // Box 66 - Diagnosis Version

            // Box 67 - Diagnosis
            Assert.AreEqual("3669", claim.Diagnoses.First(d => d.DiagnosisType == DiagnosisTypeEnum.Principal).Code);
            // Box 68

            // Box 69 - Admitting Diagnosis

            // Box 70 - Patient Reason Diagnosis

            // Box 71 - PPS Code

            // Box 72 - ECI

            // Box 73

            // Box 74 - Procedures
            Assert.AreEqual(3, claim.Procedures.Count);
            var principal = claim.Procedures.FirstOrDefault(p => p.IsPrincipal);
            Assert.IsNotNull(principal);
            Assert.AreEqual("BBR", principal.Qualifier);
            Assert.AreEqual("0B110F5", principal.Code);
            Assert.AreEqual(DateTime.Parse("2005-3-21"), principal.Date);
            
            // Box 75 - Blank

            // Box 76 - Attending Physician
            Assert.IsNotNull(claim.AttendingProvider);
            Assert.AreEqual("JOHN", claim.AttendingProvider.Name.FirstName);
            Assert.AreEqual("1G", claim.AttendingProvider.Identifications[0].Qualifier);
            Assert.AreEqual("B99937A", claim.AttendingProvider.Identifications[0].Id);

            // Box 77 - Operating Physician
            Assert.IsNotNull(claim.OperatingPhysician);
            Assert.AreEqual("JANE", claim.OperatingPhysician.Name.FirstName);

            // Box 78 - Other
            Assert.IsNotNull(claim.OtherOperatingPhysician);
            Assert.AreEqual("JOE", claim.OtherOperatingPhysician.Name.FirstName);

            // Box 79 - Other

            // Box 80 - Remarks

            // Box 81CC
        }

        [TestMethod]
        public void TransformToInstitutionalClaim5010_PayerObjectTest()
        {

            var service = new ClaimTransformationService();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

            var document = service.Transform837ToClaimDocument(stream);

            Claim claim = document.Claims.First();
            Trace.Write(claim.Serialize());

            Assert.AreEqual("PI", claim.Payer.Name.Identification.Qualifier);
            Assert.AreEqual("00435", claim.Payer.Name.Identification.Id);
            Assert.AreEqual("G2", claim.Payer.Identifications.First().Qualifier);
            Assert.AreEqual("330127", claim.Payer.Identifications.First().Id);

        }

        [TestMethod]
        public void TransToDentalClaim5010()
        {
            string x12 = @"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*1*T*:~
  GS*HC*901234572000*908887732000*20070816*1615*31*X*005010X096A1~
    ST*837*873501~
      BHT*0019*00*0125*19970411*1524*CH~
      REF*87*004010X097~
      NM1*41*2*DENTAL ASSOCIATES*****46*579999999~
        PER*IC*SYDNEY SNOW*TE*2125557987~
      NM1*40*2*HEISMAN INSURANCE COMPANY*****46*555667777~
      HL*1**20*1~
        NM1*85*2*DENTAL ASSOCIATES*****XX*591PD123~
          N3*10 1/2 SHOEMAKER STREET~
          N4*COBBLER*CA*99997~
          REF*TJ*579999999~
        HL*2*1*22*1~
          SBR*P*****6***LM~
          NM1*IL*1*HOWLING*HAL****MI*B99977791G~
          NM1*PR*2*HEISMAN INSURANCE COMPANY*****PI*999888777~
          HL*3*2*23*0~
            PAT*41~
            NM1*QC*1*DIMPSON*D*J***34*567324788~
              N3*32 BUFFALO RUN~
              N4*ROCKING HORSE*CA*99666~
              DMG*D8*19480601*M~
              REF*Y4*32323232~
            CLM*900000032*390***11::1*Y**Y*Y**AA:::CA~
              DTP*439*D8*19970201~
              DTP*472*D8*19970202~
              NM1*82*1*MOGLIE*BRUNO****34*224873702~
                PRV*PE*ZZ*122300000N~
              LX*1~
                SV3*AD:D0330*40****1~
              LX*2~
                SV3*AD:D5820*350***I*1~
                TOO*JP*8~
                TOO*JP*9*M~
                TOO*JP*13*M:O~
    SE*35*873501~
  GE*1*31~
IEA*1*000000031~";

            var service = new ClaimTransformationService();

            var x12Parser = new Parsing.X12Parser();
            var document = service.Transform837ToClaimDocument(x12Parser.ParseMultiple(x12).First());

            Claim claim = document.Claims.First();
            Trace.Write(claim.Serialize());

            Assert.AreEqual(3, claim.ServiceLines.Sum(sl=>sl.ToothInformations.Count));

            
        }
    }
}
