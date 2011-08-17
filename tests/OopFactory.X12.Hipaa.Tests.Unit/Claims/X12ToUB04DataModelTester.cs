using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using OopFactory.X12.Hipaa.Claims;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

namespace OopFactory.X12.Hipaa.Tests.Unit.Claims
{
    [TestClass]
    public class X12ToUB04DataModelTester
    {
        [TestMethod]
        public void InstitutionalClaim1ToModel()
        {
            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Claims.TestData.InstitutionalClaim1.txt");

            var service = new ClaimTransformationService();
            string xmlModel = service.TransformX12837ToUB04Model(stream);
            System.Diagnostics.Trace.Write(xmlModel); 
            
            UB04Claim claim = UB04Claim.Deserialize(xmlModel);

            Assert.AreEqual("756048Q", claim.Field03a_PatientControlNumber);
        }
    }
}
