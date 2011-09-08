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
            var claim = claimSvc.Transform837ToClaimDocument(stream);

            Assert.AreEqual("foo", "foo");
            Trace.Write(claim.Serialize());
        }

    }
}
