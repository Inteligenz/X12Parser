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
using System.Xml.Xsl;

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
        public void X12ToHcfaPdfTest2() {
            // Setup to test new HCFA 02/12 form
            var hcfa = GetHcfa();
            var service = new ProfessionalClaimToHcfa1500FormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\TestData\Images\CMS_1500_claim_form_2012-4.gif");

            OopFactory.X12.Hipaa.Claims.Forms.FormDocument form = new OopFactory.X12.Hipaa.Claims.Forms.FormDocument();
            form.Pages.AddRange(service.TransformHcfa1500ToClaimFormFoXml(hcfa));

            var xml = form.Serialize();

            var transformStream = typeof(ClaimTransformationService).Assembly.GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Services.Xsl.FormDocument-To-FoXml.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;

            XmlDocument foDocument = new XmlDocument();

            foDocument.LoadXml(new StreamReader(outputStream).ReadToEnd());

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream("c:\\Temp\\Pdfs\\ProfessionalClaim2.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(foDocument, outputFile);
#endif
        }

        [TestMethod]
        public void X12ToHcfaPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");

            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\TestData\Images\HCFA1500_Red.gif"),
				new InstitutionalClaimToUB04ClaimFormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\TestDate\Images\UB04_Red.gif"),
                new ProfessionalClaimToHcfa1500FormTransformation(@"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\TestDate\Images\HCFA1500_Red.gif")
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


        protected OopFactory.X12.Hipaa.Claims.Forms.Professional.HCFA1500Claim GetHcfa() {
            var claim = new OopFactory.X12.Hipaa.Claims.Forms.Professional.HCFA1500Claim();

            claim.Field01_TypeOfCoverageIsChampVa = true;
            claim.Field01_TypeOfCoverageIsFECABlkLung = true;
            claim.Field01_TypeOfCoverageIsGroupHealthPlan = true;
            claim.Field01_TypeOfCoverageIsMedicaid = true;
            claim.Field01_TypeOfCoverageIsMedicare = true;
            claim.Field01_TypeOfCoverageIsOther = true;
            claim.Field01_TypeOfCoverageIsTricareChampus = true;
            claim.Field01a_InsuredsIDNumber = "insured id".ToUpper();
            claim.Field02_PatientsName = "patient name".ToUpper();
            claim.Field03_PatientsDateOfBirth = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field03_PatientsSexFemale = true;
            claim.Field03_PatientsSexMale = true;
            claim.Field04_InsuredsName = "insured name".ToUpper();
            claim.Field05_PatientsAddress_City = "city".ToUpper();
            claim.Field05_PatientsAddress_State = "mm".ToUpper();
            claim.Field05_PatientsAddress_Street = "1234 street".ToUpper();
            claim.Field05_PatientsAddress_Zip = "43235-1234";
            claim.Field05_PatientsTelephone = "614-123-4567";
            claim.Field06_PatientRelationshipToInsuredIsChildOf = true;
            claim.Field06_PatientRelationshipToInsuredIsOther = true;
            claim.Field06_PatientRelationshipToInsuredIsSelf = true;
            claim.Field06_PatientRelationshipToInsuredIsSpouseOf = true;
            claim.Field07_InsuredsAddress_City = "city".ToUpper();
            claim.Field07_InsuredsAddress_State = "mm".ToUpper();
            claim.Field07_InsuredsAddress_Street = "1234 street".ToUpper();
            claim.Field07_InsuredsAddress_Zip = "432351234";
            claim.Field07_InsuredsAreaCode = "614";
            claim.Field07_InsuredsPhoneNumber = "1234567";
            claim.Field08_Reserved = "reserved".ToUpper();
            claim.Field09_OtherInsuredsName = "other name".ToUpper();
            claim.Field09a_OtherInsuredsPolicyOrGroup = "other policy".ToUpper();
            claim.Field09b_Reserved = "reserved".ToUpper();
            claim.Field09c_Reserved = "reserved".ToUpper();
            claim.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = "other plan".ToUpper();
            claim.Field10a_PatientConditionRelatedToEmployment = true;
            claim.Field10b_PatientConditionRelatedToAutoAccident = true;
            claim.Field10b_PatientConditionRelToAutoAccidentState = "oh".ToUpper();
            claim.Field10c_PatientConditionRelatedToOtherAccident = true;
            claim.Field10d_ReservedForLocalUse = "reserved".ToUpper();
            claim.Field11_InsuredsPolicyGroupOfFECANumber = "group feca number".ToUpper();
            claim.Field11a_InsuredsDateOfBirth = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field11a_InsuredsSexIsFemale = true;
            claim.Field11a_InsuredsSexIsMale = true;
            claim.Field11b_OtherClaimId = "oth1234".ToUpper();
            claim.Field11c_InsuredsPlanOrProgramName = "insured program".ToUpper();
            claim.Field11d_IsThereOtherHealthBenefitPlan = true;
            claim.Field12_PatientsOrAuthorizedSignature = "signature on file".ToUpper();
            claim.Field12_PatientsOrAuthorizedSignatureDate = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field13_InsuredsOrAuthorizedSignature = "signature not on file".ToUpper();
            claim.Field14_DateOfCurrentIllnessInjuryOrPregnancy = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field15_DatePatientHadSameOrSimilarIllness = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field16_DatePatientUnableToWork_End = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field16_DatePatientUnableToWork_Start = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field17_ReferringProviderOrOtherSource_Name = "referring".ToUpper();
            claim.Field17a_OtherID_Number = "oth1234".ToUpper();
            claim.Field17a_OtherID_Qualifier = "mm".ToUpper();
            claim.Field17b_NationalProviderIdentifier = "1234567";
            claim.Field18_HospitalizationDateFrom = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field18_HospitalizationDateTo = new Hipaa.Claims.Forms.FormDate() {
                MM = "01",
                DD = "01",
                YY = "15"
            };
            claim.Field19_AdditionalClaimInfo = "additional claim information".ToUpper();
            claim.Field20_OutsideLab = true;
            claim.Field20_OutsideLabCharges = 1223.56M;
            claim.Field21_DiagnosisA = "888.88";
            claim.Field21_DiagnosisB = "888.88";
            claim.Field21_DiagnosisC = "888.88";
            claim.Field21_DiagnosisD = "888.88";
            claim.Field21_DiagnosisE = "888.88";
            claim.Field21_DiagnosisF = "888.88";
            claim.Field21_DiagnosisG = "888.88";
            claim.Field21_DiagnosisH = "888.88";
            claim.Field21_DiagnosisI = "888.88";
            claim.Field21_DiagnosisJ = "888.88";
            claim.Field21_DiagnosisK = "888.88";
            claim.Field21_DiagnosisL = "888.88";
            claim.Field22_MedicaidSubmissionCode = "CODE";
            claim.Field22_OriginalReferenceNumber = "orig1234".ToUpper();
            claim.Field23_PriorAuthorizationNumber = "pri1234".ToUpper();
            claim.Field25_FederalTaxIDNumber = "888888888";
            claim.Field25_IsEIN = true;
            claim.Field25_IsSSN = true;
            claim.Field26_PatientAccountNumber = "pat1234".ToUpper();
            claim.Field27_AcceptAssignment = true;
            claim.Field28_TotalCharge = 12345.88M;
            claim.Field29_AmountPaid = 12345.88M;
            claim.Field30_BalanceDue = 12345.88M;
            claim.Field31_PhysicianOrSupplierSignatureIsOnFile = true;
            claim.Field32_ServiceFacilityLocation_City = "city".ToUpper();
            claim.Field32_ServiceFacilityLocation_Name = "facility name".ToUpper();
            claim.Field32_ServiceFacilityLocation_State = "oh".ToUpper();
            claim.Field32_ServiceFacilityLocation_Street = "1234 street".ToUpper();
            claim.Field32_ServiceFacilityLocation_Zip = "432341234";
            claim.Field32a_ServiceFacilityLocation_Npi = "1234567890";
            claim.Field32b_ServiceFacilityLocation_OtherID = "410002348";
            claim.Field33_BillingProvider_City = "city".ToUpper();
            claim.Field33_BillingProvider_Name = "billing name".ToUpper();
            claim.Field33_BillingProvider_State = "oh".ToUpper();
            claim.Field33_BillingProvider_Street = "1234 street".ToUpper();
            claim.Field33_BillingProvider_TelephoneNumber = "419-123-4567";
            claim.Field33_BillingProvider_Zip = "43235";
            claim.Field33a_BillingProvider_Npi = "122355656";
            claim.Field33b_BillingProvider_OtherID = "3415451345";

            for (var i = 1; i <= 9; i++) {
                var line = new OopFactory.X12.Hipaa.Claims.Forms.Professional.HCFA1500ServiceLine();
                claim.Field24_ServiceLines.Add(line);

                line.DateFrom = new Hipaa.Claims.Forms.FormDate() {
                    MM = "01",
                    DD = i.ToString("00"),
                    YY = "15"
                };

                line.DateTo = new Hipaa.Claims.Forms.FormDate() {
                    MM = "01",
                    DD = i.ToString("00"),
                    YY = "15"
                };

                line.PlaceOfService = "11";
                line.EmergencyIndicator = "N";
                line.Mod1 = "M1";
                line.Mod2 = "M2";
                line.Mod3 = "M3";
                line.Mod4 = "M4";
                line.DiagnosisPointerA = "A";
                line.DiagnosisPointerB = "B";
                line.DiagnosisPointerC = "C";
                line.DiagnosisPointerD = "D";
                line.Charges = 9999M;
                line.DaysOrUnits = 1.5M;
                line.EarlyPeriodicScreeningDiagnosisAndTreatment = "N";
                line.RenderingProviderId = "340008264";
                line.RenderingProviderIdQualifier = "X4";
                line.RenderingProviderNpi = "174564121";
                line.CommentLine = "line comment".ToUpper();
            }

            return claim;
        }
    }
}
