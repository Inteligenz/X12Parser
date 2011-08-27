using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Common;
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

            BenefitResponse response = new BenefitResponse
            {
                Source = new Entity
                {
                    Name = new EntityName
                    {
                        Identifier = "PR",
                        Qualifier = EntityNameQualifierEnum.NonPerson,
                        LastName = "ABC Company",
                        Identification = new Identification { Qualifier = "PI", Id = "842610001" }
                    }
                },
                Receiver = new Provider
                {
                    Name = new EntityName
                    {
                        Identifier = "1P",
                        Qualifier = EntityNameQualifierEnum.NonPerson,
                        LastName = "BONE AND JOIN CLINIC",
                        Identification = new Identification { Qualifier = "SV", Id = "2000035" }
                    }
                },
                Subscriber = new BenefitMember
                {
                    Gender = GenderEnum.Male,
                    DateOfBirth = DateTime.Parse("1963-05-19"),
                    Name = new EntityName
                    {
                        Qualifier = EntityNameQualifierEnum.Person,
                        LastName = "SMITH",
                        FirstName = "JOHN",
                        Identification = new Identification { Qualifier = "MI", Id = "123456789" }
                    },
                    Address = new PostalAddress
                    {
                        Line1 = "15197 BROADWAY AVENUE",
                        Line2 = "APT 215",
                        City = "KANSAS CITY",
                        StateCode = "MO",
                        PostalCode = "64108"
                    }
                }
            };

            response.Subscriber.Dates.Add(new QualifiedDate { Qualifier = "346", Date = DateTime.Parse("2006-01-01") });

            responses.Add(response);

            string xml = BenefitResponse.SerializeList(responses);

            System.Diagnostics.Trace.WriteLine(xml);
        }

        [TestMethod]
        public void TransformToModelTest1()
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_1_2.txt");

            var responses = service.Transform271ToBenefitResponse(stream);

            System.Diagnostics.Trace.Write(BenefitResponse.SerializeList(responses));

        }

        [TestMethod]
        public void TransformToModelTest2()
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_2_2.txt");

            var responses = service.Transform271ToBenefitResponse(stream);

            System.Diagnostics.Trace.Write(BenefitResponse.SerializeList(responses));

        }
    }
}
