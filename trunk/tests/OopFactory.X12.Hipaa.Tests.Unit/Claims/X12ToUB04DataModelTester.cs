using System;
using System.Diagnostics;
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

            var claimSvc = new ClaimTransformationService();
            UB04Claim claim = claimSvc.TransformX12837ToUB04Model(stream);

            Assert.AreEqual("756048Q", claim.Field03a_PatientControlNumber);
            Assert.AreEqual("89.93", claim.Field55a_EstimatedAmountDue);
            Assert.AreEqual("19960911", claim.Field16_DischargeHour);

            // serialize the object to xml so we can view it
            var x = new System.Xml.Serialization.XmlSerializer(claim.GetType());
            x.Serialize(Console.Out, claim);      
        }
    }
}
