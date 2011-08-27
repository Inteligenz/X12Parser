using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Eligibility;

namespace OopFactory.X12.Hipaa.Tests.Unit.Eligibility
{
    [TestClass]
    public class BenefitResponseTester
    {
        [TestMethod]
        public void SerializationTest()
        {
            List<BenefitResponse> responses = new List<BenefitResponse>();

            BenefitResponse response = new BenefitResponse();


            responses.Add(response);

            string xml = BenefitResponse.SerializeList(responses);

            System.Diagnostics.Trace.WriteLine(xml);
        }
    }
}
