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

namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    [TestClass]
    public class X12ToHCFADataModelTester
    {
#if DEBUG
        [TestMethod]
        public void ProfessionalClaim1ToModel()
        {
            Stream stream = null;
            
            // get the x12 doc into a stream
            try
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.ProfessionalClaim1.txt");              
            }
            catch(Exception ex)
            {
                string err = ex.Message + ex.InnerException;
            }

            // new up a ClaimTransformationService object
             var claimSvc = new ClaimTransformationService();

            // send the x12 stream in to obtain a claim object
            //var document = claimSvc.Transform837ToClaimDocument(stream);
            //var claim = document.Claims[0];
            //Assert.AreEqual("587654321", claim.BillingInfo.Providers[0].TaxId);
            var hcfaclaim = claimSvc.TransformX12837ToHcfa1500Model(stream);
            Assert.AreEqual("JANE", hcfaclaim.Field02_PatientsFirstName);
            Assert.AreEqual("SMITH", hcfaclaim.Field02_PatientsLastName);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicare);
            Assert.IsFalse(hcfaclaim.Field01_TypeOfCoverageIsMedicaid);
            //Assert.AreEqual("1943-05-01T00:00:00", hcfaclaim.Field03_PatientsDateOfBirth);
            if (hcfaclaim.Field03_PatientsSexFemale.HasValue)
            {
                Assert.IsTrue(hcfaclaim.Field03_PatientsSexFemale.Value);
            }
            Assert.AreEqual("99213", hcfaclaim.Field24_ServiceLines.First().Field24d_ProcedureCode);
            Assert.AreEqual("87070", hcfaclaim.Field24_ServiceLines[1].Field24d_ProcedureCode);
            Assert.AreEqual("99214", hcfaclaim.Field24_ServiceLines[2].Field24d_ProcedureCode);
            Assert.AreEqual("86663", hcfaclaim.Field24_ServiceLines[3].Field24d_ProcedureCode);
                             Assert.AreEqual("BEN KILDARE SERVICE", hcfaclaim.Field32_FacilityLocationInfo_Name);
            Assert.AreEqual("234 SEAWAY ST", hcfaclaim.Field32_FacilityLocationInfo_Street);
            Assert.AreEqual("MIAMI", hcfaclaim.Field32_FacilityLocationInfo_City);
            Assert.AreEqual("FL", hcfaclaim.Field32_FacilityLocationInfo_State);
            Assert.AreEqual("2345 OCEAN BLVD", hcfaclaim.Field33_BillingProvider_Street);
            Assert.AreEqual("MAIMI", hcfaclaim.Field33_BillingProvider_City);
            Assert.AreEqual("FL", hcfaclaim.Field33_BillingProvider_State);
            Assert.AreEqual("33111", hcfaclaim.Field33_BillingProvider_Zip);
            Trace.Write(hcfaclaim.Serialize());
        }
#endif

    }
}
