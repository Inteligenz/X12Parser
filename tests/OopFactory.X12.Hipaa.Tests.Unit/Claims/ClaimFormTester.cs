namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OopFactory.X12.Hipaa.Claims;
    using OopFactory.X12.Hipaa.Claims.Services;

    /// <summary>
    /// Collection of tests for Claim Documents
    /// </summary>
    [TestClass]
    public class ClaimFormTester
    {
        private static readonly string TestDirectory = @"C:\Temp\Pdfs";

        private static readonly string TestImageDirectory = @"..\..\..\tests\OopFactory.X12.Hipaa.Tests.Unit\Claims\TestData\Images\";

        /// <summary>
        /// Initializes the test being performed by ensuring the testing directory has been created and is empty.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // C:\Temp is a standard folder for Windows. However, we'll
            // want to verify that the \Pdfs folder exists and is empty
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }

            Directory.CreateDirectory(TestDirectory);
        }

        /// <summary>
        /// Tests that X12 that's read in and transformed to a claim document has correct structure
        /// </summary>
        [TestMethod]
        public void X12ToClaimModelTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");              

             var service = new ProfessionalClaimToHcfa1500FormTransformation(string.Empty);

            // send the x12 stream in to obtain a claim object
            var document = service.Transform837ToClaimDocument(stream);
            var hcfaclaim = service.TransformClaimToHcfa1500(document.Claims.First());
            Assert.AreEqual("SMITH, TED", hcfaclaim.Field02_PatientsName);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicare);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicaid);
            Assert.IsFalse(hcfaclaim.Field03_PatientsSexFemale);
            Assert.IsTrue(hcfaclaim.Field03_PatientsSexMale);
            Assert.AreEqual("99213", hcfaclaim.Field24_ServiceLines.First().ProcedureCode);
            Assert.AreEqual("87070", hcfaclaim.Field24_ServiceLines[1].ProcedureCode);
            Assert.AreEqual("99214", hcfaclaim.Field24_ServiceLines[2].ProcedureCode);
            Assert.AreEqual("86663", hcfaclaim.Field24_ServiceLines[3].ProcedureCode);
            Trace.Write(hcfaclaim.Serialize());
        }

        /// <summary>
        /// Tests that 3 different documents can be properly transformed and combined into a Claim PDF document
        /// </summary>
        [TestMethod]
        public void X12ToHcfaPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");

            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation($"{TestImageDirectory}\\HCFA1500_Red.gif"),
                new InstitutionalClaimToUb04ClaimFormTransformation($"{TestImageDirectory}\\UB04_Red.gif"),
                new ProfessionalClaimToHcfa1500FormTransformation($"{TestImageDirectory}\\HCFA1500_Red.gif"));

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            var fonetDocument = new XmlDocument();
            string fonetXml = service.TransformClaimDocumentToFoXml(document);
            fonetDocument.LoadXml(fonetXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream($"{TestDirectory}\\ProfessionalClaim1.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(fonetDocument, outputFile);            
#endif
        }

        /// <summary>
        /// Tests that a UB04 document has correct PDF layout
        /// </summary>
        [TestMethod]
        public void X12ToUbPdfLayoutTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");
            
            var transformation = new InstitutionalClaimToUb04ClaimFormTransformation($"{TestImageDirectory}\\UB04_Red.gif");
                
            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(transformation, transformation, transformation); 

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            var fonetDocument = new XmlDocument();
            string fonetXml = service.TransformClaimDocumentToFoXml(document);
            fonetDocument.LoadXml(fonetXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            var outputFile = new FileStream($"{TestDirectory}\\InstitutionalClaimPlaceholders.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(fonetDocument, outputFile);
#endif
        }

        /// <summary>
        /// Tests that an X12 837 document can be properly transformed to a claim, then a UB04 PDF document
        /// </summary>
        [TestMethod]
        public void X12ToUbPdfTest()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim5010.txt");

            var transformation = new InstitutionalClaimToUb04ClaimFormTransformation($"{TestImageDirectory}\\UB04_Red.gif");
            
            // new up a ClaimTransformationService object
            var service = new ClaimFormTransformationService(transformation, transformation, transformation);

            ClaimDocument document = service.Transform837ToClaimDocument(stream);

            var ub04 = transformation.TransformClaimToUB04(document.Claims.First());
            Trace.WriteLine(ub04.Serialize());

            var fonetDocument = new XmlDocument();
            string fonetXml = service.TransformClaimDocumentToFoXml(document);
            fonetDocument.LoadXml(fonetXml);

#if DEBUG
            var driver = Fonet.FonetDriver.Make();

            FileStream outputFile = new FileStream($"{TestDirectory}\\InstitutionalClaim5010.pdf", FileMode.Create, FileAccess.Write);
            driver.Render(fonetDocument, outputFile);
#endif
        }
    }
}
