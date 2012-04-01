using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Claims;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;
using OopFactory.X12.Hipaa.Claims.Services;
using System.Xml;

namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    [TestClass]
    public class ClaimFormTester
    {
        [TestMethod]
        public void X12ToClaimModelTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");              

             var service = new ProfessionalClaimToHcfa1500FormTransformation("");

            // send the x12 stream in to obtain a claim object
             var document = service.Transform837ToClaimDocument(stream);
             var hcfaclaim = service.TransformClaimToHcfa1500(document.Claims.First());
            Assert.AreEqual("SMITH, TED", hcfaclaim.Field02_PatientsName);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicare);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicaid);
            //Assert.AreEqual("1943-05-01T00:00:00", hcfaclaim.Field03_PatientsDateOfBirth);
            Assert.IsFalse(hcfaclaim.Field03_PatientsSexFemale);
            Assert.IsTrue(hcfaclaim.Field03_PatientsSexMale);
            Assert.AreEqual("99213", hcfaclaim.Field24_ServiceLines.First().ProcedureCode);
            Assert.AreEqual("87070", hcfaclaim.Field24_ServiceLines[1].ProcedureCode);
            Assert.AreEqual("99214", hcfaclaim.Field24_ServiceLines[2].ProcedureCode);
            Assert.AreEqual("86663", hcfaclaim.Field24_ServiceLines[3].ProcedureCode);
            /*
             Assert.AreEqual("BEN KILDARE SERVICE", hcfaclaim.Field32_ServiceFacilityLocation_Name);
            Assert.AreEqual("234 SEAWAY ST", hcfaclaim.Field32_ServiceFacilityLocation_Street);
            Assert.AreEqual("MIAMI", hcfaclaim.Field32_ServiceFacilityLocation_City);
            Assert.AreEqual("FL", hcfaclaim.Field32_ServiceFacilityLocation_State);
            Assert.AreEqual("2345 OCEAN BLVD", hcfaclaim.Field33_BillingProvider_Street);
            Assert.AreEqual("MAIMI", hcfaclaim.Field33_BillingProvider_City);
            Assert.AreEqual("FL", hcfaclaim.Field33_BillingProvider_State);
            Assert.AreEqual("33111", hcfaclaim.Field33_BillingProvider_Zip);
             */
            Trace.Write(hcfaclaim.Serialize());
        }

        [TestMethod]
        public void X12ToHcfaPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");

            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(
				new ProfessionalClaimToHcfa1500FormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\HCFA1500_Red.gif"),
				new InstitutionalClaimToUB04ClaimFormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\UB04_Red.gif"),
				new ProfessionalClaimToHcfa1500FormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\HCFA1500_Red.gif")
                );

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            XmlDocument foDocument = new XmlDocument();
            string foXml = service.TransformClaimDocumentToFoXml(document);
            foDocument.LoadXml(foXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream("c:\\Temp\\Pdfs\\ProfessionalClaim1.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(foDocument, outputFile);            
#endif
        }

        [TestMethod]
        public void X12ToUbPdfLayoutTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

			
            var transformation = new InstitutionalClaimToUB04ClaimFormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\UB04_Red.gif");
            transformation.TransformCompleted += new InstitutionalClaimToUB04ClaimFormTransformation.TransformCompletedHandler(transformation_TransformCompleted);
                
            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(transformation, transformation, transformation); 

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            XmlDocument foDocument = new XmlDocument();
            string foXml = service.TransformClaimDocumentToFoXml(document);
            foDocument.LoadXml(foXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream("c:\\Temp\\Pdfs\\InstitutionalClaimPlaceholders.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(foDocument, outputFile);
#endif
        }

        void transformation_TransformCompleted(object sender, Ub04ClaimTransformationArgs args)
        {
            var ub = args.Target;
            ub.Field01_BillingProvider.Line1 = 'A'.Repeat(28);
            ub.Field01_BillingProvider.Line2 = 'B'.Repeat(28);
            ub.Field01_BillingProvider.Line3 = 'C'.Repeat(28);
            ub.Field01_BillingProvider.Line4 = 'D'.Repeat(28);

            ub.Field02_PayToProvider.Line1 = 'E'.Repeat(28);
            ub.Field02_PayToProvider.Line2 = 'F'.Repeat(28);
            ub.Field02_PayToProvider.Line3 = 'G'.Repeat(28);
            ub.Field02_PayToProvider.Line4 = 'H'.Repeat(28);

            ub.Field03a_PatientControlNumber = 'I'.Repeat(27);
            ub.Field03b_MedicalRecordNumber = 'J'.Repeat(27);
            ub.Field04_TypeOfBill = "0111";
            ub.Field05_FederalTaxId = "99-9999999";
            ub.Field06_StatementCoversPeriod.FromDate = "MMDDYY";
            ub.Field06_StatementCoversPeriod.ThroughDate = "MMDDYY";
            ub.Field07.Line1 = 'K'.Repeat(8);
            ub.Field07.Line2 = 'L'.Repeat(8);
            ub.Field08_PatientName_a = 'M'.Repeat(21);
            ub.Field08_PatientName_b = 'N'.Repeat(33);
            ub.Field09_PatientAddress.a_Street = 'O'.Repeat(48);
            ub.Field09_PatientAddress.b_City = 'P'.Repeat(37);
            ub.Field09_PatientAddress.c_State = "QQ";
            ub.Field09_PatientAddress.d_PostalCode = "99999-9999";
            ub.Field09_PatientAddress.e_CountryCode = "RRR";

            ub.Field10_Birthdate = "MMDDCCYY";
            ub.Field11_Sex = "U";
            ub.Field12_AdmissionDate = "MMDDYY";
            ub.Field13_AdmissionHour = "22";
            ub.Field14_AdmissionType = "33";
            ub.Field15_AdmissionSource = "44";
            ub.Field16_DischargeHour = "11";
            ub.Field17_DischargeStatus = "55";
            ub.Field18_ConditionCode01 = "SS";
            ub.Field19_ConditionCode02 = "TT";
            ub.Field20_ConditionCode03 = "UU";
            ub.Field21_ConditionCode04 = "VV";
            ub.Field22_ConditionCode05 = "WW";
            ub.Field23_ConditionCode06 = "XX";
            ub.Field24_ConditionCode07 = "YY";
            ub.Field25_ConditionCode08 = "ZZ";
            ub.Field26_ConditionCode09 = "AA";
            ub.Field27_ConditionCode10 = "BB";
            ub.Field28_ConditionCode11 = "CC";

            ub.Field29_AccidentState = "DD";
            ub.Field30 = 'E'.Repeat(14);
            ub.Field31a_Occurrence.Code = "FF";
            ub.Field31a_Occurrence.Date = "MMDDYY";
            ub.Field31b_Occurrence.Code = "GG";
            ub.Field31b_Occurrence.Date = "MMDDYY";
            ub.Field32a_Occurrence.Code = "HH";
            ub.Field32a_Occurrence.Date = "MMDDYY";
            ub.Field32b_Occurrence.Code = "II";
            ub.Field32b_Occurrence.Date = "MMDDYY";
            ub.Field33a_Occurrence.Code = "JJ";
            ub.Field33a_Occurrence.Date = "MMDDYY";
            ub.Field33b_Occurrence.Code = "KK";
            ub.Field33b_Occurrence.Date = "MMDDYY";
            ub.Field34a_Occurrence.Code = "LL";
            ub.Field34a_Occurrence.Date = "MMDDYY";
            ub.Field34b_Occurrence.Code = "MM";
            ub.Field34b_Occurrence.Date = "MMDDYY";
            ub.Field35a_OccurrenceSpan.Code = "NN";
            ub.Field35a_OccurrenceSpan.FromDate = "MMDDYY";
            ub.Field35a_OccurrenceSpan.ThroughDate = "MMDDYY";
            ub.Field35b_OccurrenceSpan.Code = "OO";
            ub.Field35b_OccurrenceSpan.FromDate = "MMDDYY";
            ub.Field35b_OccurrenceSpan.ThroughDate = "MMDDYY";
            ub.Field36a_OccurrenceSpan.Code = "PP";
            ub.Field36a_OccurrenceSpan.FromDate = "MMDDYY";
            ub.Field36a_OccurrenceSpan.ThroughDate = "MMDDYY";
            ub.Field36b_OccurrenceSpan.Code = "QQ";
            ub.Field36b_OccurrenceSpan.FromDate = "MMDDYY";
            ub.Field36b_OccurrenceSpan.ThroughDate = "MMDDYY";
            ub.Field37.Line1 = 'R'.Repeat(9);
            ub.Field37.Line2 = 'S'.Repeat(9);

            ub.Field38_ResponsibleParty.Line1 = 'T'.Repeat(48);
            ub.Field38_ResponsibleParty.Line2 = 'U'.Repeat(48);
            ub.Field38_ResponsibleParty.Line3 = 'V'.Repeat(48);
            ub.Field38_ResponsibleParty.Line4 = 'W'.Repeat(48);
            ub.Field38_ResponsibleParty.Line5 = 'X'.Repeat(48);

            ub.Field39a_Value.Code = "YY";
            ub.Field39a_Value.Amount = 1.01m;
            ub.Field39b_Value.Code = "ZZ";
            ub.Field39b_Value.Amount = 2.02m;
            ub.Field39c_Value.Code = "AA";
            ub.Field39c_Value.Amount = 3.03m;
            ub.Field39d_Value.Code = "BB";
            ub.Field39d_Value.Amount = 4.04m;
            ub.Field40a_Value.Code = "CC";
            ub.Field40a_Value.Amount = 5.05m;
            ub.Field40b_Value.Code = "DD";
            ub.Field40b_Value.Amount = 6.06m;
            ub.Field40c_Value.Code = "EE";
            ub.Field40c_Value.Amount = 7.07m;
            ub.Field40d_Value.Code = "FF";
            ub.Field40d_Value.Amount = 8.08m;
            ub.Field41a_Value.Code = "GG";
            ub.Field41a_Value.Amount = 9.09m;
            ub.Field41b_Value.Code = "HH";
            ub.Field41b_Value.Amount = 10.10m;
            ub.Field41c_Value.Code = "II";
            ub.Field41c_Value.Amount = 11.11m;
            ub.Field41d_Value.Code = "JJ";
            ub.Field41d_Value.Amount = 12.12m;

            ub.ServiceLines = new List<UB04ServiceLine>();
            for (int i = 0; i < 30; i++)
            {
                ub.ServiceLines.Add(new UB04ServiceLine
                {
                    Field42_RevenueCode = String.Format("{0:0000}", i),
                    Field43_Description = 'K'.Repeat(29),
                    Field44_ProcedureCodes = 'L'.Repeat(17),
                    Field45_ServiceDate = "MMDDYY",
                    Field46_ServiceUnits = String.Format("{0}", i + 1),
                    Field47_TotalCharges = 1 + i * 12345.67m,
                    Field48_NonCoveredCharges = 2 + i * 23456.78m,
                    Field49 = "MM"
                });
            }

            ub.Field47_Line23_TotalCharges = 99999.99m;
            ub.Field48_Line23_NonCoveredCharges = 88888.88m;
            ub.PayerA_Primary.Field50_PayerName = 'N'.Repeat(26);
            ub.PayerA_Primary.Field51_HealthPlanId = 'O'.Repeat(17);
            ub.PayerA_Primary.Field52_ReleaseOfInfoCertIndicator = "PP";
            ub.PayerA_Primary.Field53_AssignmentOfBenefitsCertIndicator = "QQ";
            ub.PayerA_Primary.Field54_PriorPayments = 7777.77m;
            ub.PayerA_Primary.Field55_EstimatedAmountDue = 6666.66m;
            ub.Field56_NationalProviderIdentifier = "9999999999";
            ub.Field57_OtherProviderIdA = '0'.Repeat(17);
            ub.Field57_OtherProviderIdB = '1'.Repeat(17);
            ub.Field57_OtherProviderIdC = '2'.Repeat(17);
            ub.PayerA_Primary.Field58_InsuredsName = 'R'.Repeat(29);
            ub.PayerA_Primary.Field59_PatientRelationship = "SS";
            ub.PayerA_Primary.Field60_InsuredsUniqueId = 'T'.Repeat(23);
            ub.PayerA_Primary.Field61_GroupName = 'U'.Repeat(17);
            ub.PayerA_Primary.Field62_InsuredsGroupNumber = 'V'.Repeat(21);
            ub.PayerB_Secondary = ub.PayerA_Primary;
            ub.PayerC_Tertiary = ub.PayerA_Primary;

            ub.Field63A_TreatmentAuthorizationCode = 'W'.Repeat(35);
            ub.Field63B_TreatmentAuthorizationCode = 'X'.Repeat(35);
            ub.Field63C_TreatmentAuthorizationCode = 'Y'.Repeat(35);
            ub.Field64A_DocumentControlNumber = 'Z'.Repeat(30);
            ub.Field64B_DocumentControlNumber = 'A'.Repeat(30);
            ub.Field64C_DocumentControlNumber = 'B'.Repeat(30);
            ub.Field65a_EmployerName = 'C'.Repeat(29);
            ub.Field65b_EmployerName = 'D'.Repeat(29);
            ub.Field65c_EmployerName = 'E'.Repeat(29);

            ub.Field66_Version = "9";

            ub.Field67_PrincipleDiagnosis.Code = "123.45";
            ub.Field67_PrincipleDiagnosis.PresentOnAdmissionIndicator = "Z";
            ub.Field67A_Diagnosis.Code = "234.56";
            ub.Field67A_Diagnosis.PresentOnAdmissionIndicator = "A";
            ub.Field67B_Diagnosis.Code = "345.67";
            ub.Field67B_Diagnosis.PresentOnAdmissionIndicator = "B";
            ub.Field67C_Diagnosis.Code = "456.78";
            ub.Field67C_Diagnosis.PresentOnAdmissionIndicator = "C";
            ub.Field67D_Diagnosis.Code = "567.89";
            ub.Field67D_Diagnosis.PresentOnAdmissionIndicator = "D";
            ub.Field67E_Diagnosis.Code = "678.90";
            ub.Field67E_Diagnosis.PresentOnAdmissionIndicator = "E";
            ub.Field67F_Diagnosis.Code = "789.01";
            ub.Field67F_Diagnosis.PresentOnAdmissionIndicator = "F";
            ub.Field67G_Diagnosis.Code = "890.12";
            ub.Field67G_Diagnosis.PresentOnAdmissionIndicator = "G";
            ub.Field67H_Diagnosis.Code = "901.23";
            ub.Field67H_Diagnosis.PresentOnAdmissionIndicator = "H";
            ub.Field67I_Diagnosis.Code = "012.34";
            ub.Field67I_Diagnosis.PresentOnAdmissionIndicator = "I";
            ub.Field67J_Diagnosis.Code = "111.11";
            ub.Field67J_Diagnosis.PresentOnAdmissionIndicator = "J";
            ub.Field67K_Diagnosis.Code = "222.22";
            ub.Field67K_Diagnosis.PresentOnAdmissionIndicator = "K";
            ub.Field67L_Diagnosis.Code = "333.33";
            ub.Field67L_Diagnosis.PresentOnAdmissionIndicator = "L";
            ub.Field67M_Diagnosis.Code = "444.44";
            ub.Field67M_Diagnosis.PresentOnAdmissionIndicator = "M";
            ub.Field67N_Diagnosis.Code = "555.55";
            ub.Field67N_Diagnosis.PresentOnAdmissionIndicator = "N";
            ub.Field67O_Diagnosis.Code = "666.66";
            ub.Field67O_Diagnosis.PresentOnAdmissionIndicator = "O";
            ub.Field67P_Diagnosis.Code = "777.77";
            ub.Field67P_Diagnosis.PresentOnAdmissionIndicator = "P";
            ub.Field67Q_Diagnosis.Code = "888.88";
            ub.Field67Q_Diagnosis.PresentOnAdmissionIndicator = "Q";
            ub.Field68.Line1 = 'R'.Repeat(9);
            ub.Field68.Line2 = 'S'.Repeat(9);

            ub.Field69_AdmittingDiagnosisCode = "987.65";
            ub.Field70a_PatientReasonDiagnosisCode = "876.54";
            ub.Field70b_PatientReasonDiagnosisCode = "765.43";
            ub.Field70c_PatientReasonDiagnosisCode = "654.32";
            ub.Field71_PPSCode = "ABCDE";
            ub.Field72a_ExternalCauseOfInjury.Code = "543.21";
            ub.Field72a_ExternalCauseOfInjury.PresentOnAdmissionIndicator = "T";
            ub.Field72b_ExternalCauseOfInjury.Code = "432.10";
            ub.Field72b_ExternalCauseOfInjury.PresentOnAdmissionIndicator = "U";
            ub.Field72c_ExternalCauseOfInjury.Code = "321.09";
            ub.Field72c_ExternalCauseOfInjury.PresentOnAdmissionIndicator = "V";
            ub.Field73 = 'W'.Repeat(10);

            ub.Field74_PrincipalProcedure.Code = 'X'.Repeat(8);
            ub.Field74_PrincipalProcedure.Date = "MMDDYY";
            ub.Field74a_OtherProcedure.Code = 'Y'.Repeat(8);
            ub.Field74a_OtherProcedure.Date = "MMDDYY";
            ub.Field74b_OtherProcedure.Code = 'Z'.Repeat(8);
            ub.Field74b_OtherProcedure.Date = "MMDDYY";
            ub.Field74c_OtherProcedure.Code = 'A'.Repeat(8);
            ub.Field74c_OtherProcedure.Date = "MMDDYY";
            ub.Field74d_OtherProcedure.Code = 'B'.Repeat(8);
            ub.Field74d_OtherProcedure.Date = "MMDDYY";
            ub.Field74e_OtherProcedure.Code = 'C'.Repeat(8);
            ub.Field74e_OtherProcedure.Date = "MMDDYY";

            ub.Field75.Line1 = 'D'.Repeat(4);
            ub.Field75.Line2 = 'E'.Repeat(4);
            ub.Field75.Line3 = 'F'.Repeat(4);
            ub.Field75.Line4 = 'G'.Repeat(4);

            ub.Field76_AttendingPhysician.Npi = "1111111111";
            ub.Field76_AttendingPhysician.IdentifierQualifier = "AB";
            ub.Field76_AttendingPhysician.Identifier = "ABCDEFGHIJ";
            ub.Field76_AttendingPhysician.LastName = 'K'.Repeat(18);
            ub.Field76_AttendingPhysician.FirstName = 'L'.Repeat(13);

            ub.Field77_OperatingPhysician = ub.Field76_AttendingPhysician;
            ub.Field78_OtherProvider = ub.Field76_AttendingPhysician;
            ub.Field79_OtherProvider = ub.Field76_AttendingPhysician;

            ub.Field80_Remarks.Line1 = 'M'.Repeat(27);
            ub.Field80_Remarks.Line2 = 'N'.Repeat(27);
            ub.Field80_Remarks.Line3 = 'O'.Repeat(27);

            ub.Field81a.Qualifier = "PQ";
            ub.Field81a.Code1 = 'R'.Repeat(10);
            ub.Field81a.Code2 = 'S'.Repeat(12);
            ub.Field81b = ub.Field81a;
            ub.Field81c = ub.Field81a;
            ub.Field81d = ub.Field81a;
        }

        [TestMethod]
        public void X12ToUbPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

			var transformation = new InstitutionalClaimToUB04ClaimFormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\UB04_Red.gif");
            
            
            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(transformation, transformation, transformation);

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            var ub04 = transformation.TransformClaimToUB04(document.Claims.First());
            Trace.WriteLine(ub04.Serialize());

            XmlDocument foDocument = new XmlDocument();
            string foXml = service.TransformClaimDocumentToFoXml(document);
            foDocument.LoadXml(foXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream("c:\\Temp\\Pdfs\\InstitutionalClaim5010.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(foDocument, outputFile);
#endif
        }
    }
}
