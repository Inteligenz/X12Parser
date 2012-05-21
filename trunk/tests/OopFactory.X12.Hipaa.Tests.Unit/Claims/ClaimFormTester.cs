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
    }
}
