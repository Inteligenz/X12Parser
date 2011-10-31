using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Claims;
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
            Assert.AreEqual("SMITH, JANE", hcfaclaim.Field02_PatientsName);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicare);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicaid);
            //Assert.AreEqual("1943-05-01T00:00:00", hcfaclaim.Field03_PatientsDateOfBirth);
            Assert.IsTrue(hcfaclaim.Field03_PatientsSexFemale);
            Assert.AreEqual("99213", hcfaclaim.Field24_ServiceLines.First().ProcedureCode);
            Assert.AreEqual("87070", hcfaclaim.Field24_ServiceLines[1].ProcedureCode);
            Assert.AreEqual("99214", hcfaclaim.Field24_ServiceLines[2].ProcedureCode);
            Assert.AreEqual("86663", hcfaclaim.Field24_ServiceLines[3].ProcedureCode);
            Assert.AreEqual("BEN KILDARE SERVICE", hcfaclaim.Field32_ServiceFacilityLocation_Name);
            Assert.AreEqual("234 SEAWAY ST", hcfaclaim.Field32_ServiceFacilityLocation_Street);
            Assert.AreEqual("MIAMI", hcfaclaim.Field32_ServiceFacilityLocation_City);
            Assert.AreEqual("FL", hcfaclaim.Field32_ServiceFacilityLocation_State);
            Assert.AreEqual("2345 OCEAN BLVD", hcfaclaim.Field33_BillingProvider_Street);
            Assert.AreEqual("MAIMI", hcfaclaim.Field33_BillingProvider_City);
            Assert.AreEqual("FL", hcfaclaim.Field33_BillingProvider_State);
            Assert.AreEqual("33111", hcfaclaim.Field33_BillingProvider_Zip);
            Trace.Write(hcfaclaim.Serialize());
        }

        [TestMethod]
        public void X12ToHcfaPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");

            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\HCFA1500_Red.gif"),
                new InstitutionalClaimToUB04ClaimFormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\UB04_Red.gif"),
                new ProfessionalClaimToHcfa1500FormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\HCFA1500_Red.gif")
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

            var transformation = new InstitutionalClaimToUB04ClaimFormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\UB04_Red.gif");
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
            ub.Field39a_Value.Value = 1.01m;
            ub.Field39b_Value.Code = "ZZ";
            ub.Field39b_Value.Value = 2.02m;
            ub.Field39c_Value.Code = "AA";
            ub.Field39c_Value.Value = 3.03m;
            ub.Field39d_Value.Code = "BB";
            ub.Field39d_Value.Value = 4.04m;
            ub.Field40a_Value.Code = "CC";
            ub.Field40a_Value.Value = 5.05m;
            ub.Field40b_Value.Code = "DD";
            ub.Field40b_Value.Value = 6.06m;
            ub.Field40c_Value.Code = "EE";
            ub.Field40c_Value.Value = 7.07m;
            ub.Field40d_Value.Code = "FF";
            ub.Field40d_Value.Value = 8.08m;
            ub.Field41a_Value.Code = "GG";
            ub.Field41a_Value.Value = 9.09m;
            ub.Field41b_Value.Code = "HH";
            ub.Field41b_Value.Value = 10.10m;
            ub.Field41c_Value.Code = "II";
            ub.Field41c_Value.Value = 11.11m;
            ub.Field41d_Value.Code = "JJ";
            ub.Field41d_Value.Value = 12.12m;
            
        }

        [TestMethod]
        public void X12ToUbPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\HCFA1500_Red.gif"),
                new InstitutionalClaimToUB04ClaimFormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\UB04_Red.gif"),
                new ProfessionalClaimToHcfa1500FormTransformation(@"C:\Projects\OopFactory\X12\trunk\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\Images\HCFA1500_Red.gif")
                );

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

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
