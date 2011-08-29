using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Claims;

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
    }
}
