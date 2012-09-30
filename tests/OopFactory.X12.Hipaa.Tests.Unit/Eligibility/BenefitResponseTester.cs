using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Eligibility;
using OopFactory.X12.Hipaa.Eligibility.Services;

namespace OopFactory.X12.Hipaa.Tests.Unit.Eligibility
{
    [TestClass]
    public class BenefitResponseTester
    {
        [TestMethod]
        public void SerializationTest()
        {
            EligibilityBenefitDocument document = new EligibilityBenefitDocument();

            EligibilityBenefitResponse response = new EligibilityBenefitResponse
            {
                Source = new Entity
                {
                    Name = new EntityName
                    {
                        Type = new EntityType
                        {
                            Identifier = "PR",
                            Qualifier = EntityNameQualifierEnum.NonPerson
                        },
                        LastName = "ABC Company",
                        Identification = new Identification { Qualifier = "PI", Id = "842610001" }
                    }
                },
                Receiver = new Provider
                {
                    Name = new EntityName
                    {
                        Type = new EntityType
                        {
                            Identifier = "1P",
                            Qualifier = EntityNameQualifierEnum.NonPerson
                        },
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
                        Type = new EntityType
                        {
                            Qualifier = EntityNameQualifierEnum.Person
                        },
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
            response.BenefitInfos.Add(new EligibilityBenefitInformation
            {
                InfoType = new Lookup { Code = "1", Description = "Active Coverage" }
            });
            document.EligibilityBenefitResponses.Add(response);

            document.RequestValidations.Add(
                new RequestValidation
                {
                    ValidRequest = true,
                    RejectReason = new Lookup { Code = "15", Description = "Required application data missing" }
                });


            string xml = document.Serialize();

            System.Diagnostics.Trace.WriteLine(xml);
        }

        private EligibilityBenefitDocument TransformToModel(string resourcePath)
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

            return service.Transform271ToBenefitResponse(stream);

        }

        [TestMethod]
        public void Transform4010ToModel1Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._4010.Example1_DHHS.txt");
            Trace.Write(responses.Serialize());
        }

        [TestMethod]
        public void Transform4010ToModel2Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._4010.Example2_TMHP.txt");
            Trace.Write(responses.Serialize());
        }

        [TestMethod]
        public void Transform4010ToModel3Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._4010.Example3_CMS_HETS.txt");
            Trace.Write(responses.Serialize());
        }
        
        [TestMethod]
        public void Transform5010ToModel1Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_1_2.txt");
            Trace.Write(responses.Serialize());
        }

        [TestMethod]
        public void Transform5010ToModel2Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_2_2.txt");
            Trace.Write(responses.Serialize());
        }

        [TestMethod]
        public void Transform5010ToModel3Test()
        {
            var responses = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_1_3.txt");
            Trace.Write(responses.Serialize());
        }

        [TestMethod]
        public void ValidationOnAll_WhenTransform5010ToModelTest_ShouldHaveAllRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationOnAll.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.RequestValidations.Count, 2);
            Assert.AreEqual(response.EligibilityBenefitResponses[0].Dependent.RequestValidations.Count, 1);
            Assert.AreEqual(response.EligibilityBenefitResponses[0].Receiver.RequestValidations.Count, 2);
            Assert.AreEqual(response.EligibilityBenefitResponses[0].Source.RequestValidations.Count, 2);
            Assert.AreEqual(response.EligibilityBenefitResponses[0].Subscriber.RequestValidations.Count, 1);
            Assert.AreEqual(response.EligibilityBenefitResponses[0].BenefitInfos[0].RequestValidations.Count, 2);
        }

        [TestMethod]
        public void ValidationForDependentBenefitInfo_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForDependentBenefitInfo.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.EligibilityBenefitResponses[0].BenefitInfos[0].RequestValidations.Count, 1);
        }

        [TestMethod]
        public void ValidationForDependentName_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForDependentName.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.EligibilityBenefitResponses[0].Dependent.RequestValidations.Count, 1);
        }

        [TestMethod]
        public void ValidationForSource_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForSource.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.RequestValidations.Count, 1);
        }

        [TestMethod]
        public void ValidationForSourceName_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForSourceName.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.EligibilityBenefitResponses[0].Source.RequestValidations.Count, 1);
        }

        [TestMethod]
        public void ValidationForSubscriberBenefitInfo_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForSubscriberBenefitInfo.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.EligibilityBenefitResponses[0].BenefitInfos[0].RequestValidations.Count, 2);
        }

        [TestMethod]
        public void ValidationForSubscriberName_WhenTransform5010ToModelTest_ShouldHaveRequestValidations()
        {
            var response = TransformToModel("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.ValidationForSubscriberName.txt");
            Trace.Write(response.Serialize());

            Assert.AreEqual(response.EligibilityBenefitResponses[0].Subscriber.RequestValidations.Count, 2);
        }

        private string TransformModelToHtml(string resourcePath)
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(resourcePath);

            var responses = service.Transform271ToBenefitResponse(stream);

            string html = service.TransformBenefitResponseToHtml(responses.EligibilityBenefitResponses.First());

            return String.Format(
            #region HTML Constant
@"<html>
    <head>
        <title>Eligibility Response</title>
    <style type=""text/css"">
        h1
        {{
            color: #003B64;
            margin: 4px 0 4px 0;
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            width: 600px;
        }}
        /*START Eligibility Table Layout*/div.eligibilityTable table
        {{
            width: 600px;
            margin-bottom: 16px;
            border: none;
        }}
        div.eligibilityTable th, div.eligibilityTable td
        {{
            font-size: 10px;
            font-family: Verdana, ""Lucida Grande"" , Geneva, Tahoma, Sans-Serif;
            font-weight: bold;
            padding: 4px 2px 2px 2px;
            vertical-align: top;
            border: none;
        }}
        div.eligibilityTable th
        {{
            font-weight: normal;
            color: #5a5b51;
            text-align: left;
        }}
        div.eligibilityTable th.col1
        {{
            width: 120px;
        }}
        div.eligibilityTable td.col2
        {{
            width: 180px;
        }}
        div.eligibilityTable th.col3
        {{
            width: 110px;
        }}
        div.eligibilityTable td.col4
        {{
            width: 190px;
        }}
        /*END Eligibility Table Layout*//*START Eligibility Grid Layout*/div.eligibilityGrid th, div.eligibilityGrid td
        {{
            font-size: 10px;
            font-family: Verdana, ""Lucida Grande"" , Geneva, Tahoma, Sans-Serif;
            padding: 4px 2px 2px 2px;
            border: none;
        }}
        div.eligibilityGrid table
        {{
            border-collapse: collapse;
            width: 600px;
            margin-bottom: 16px;
            border: solid 1px #003b64;
        }}
        div.eligibilityGrid td, div.eligibilityGrid th
        {{
            border: solid 1px #003b64;
            padding-bottom: 4px;
        }}
        div.eligibilityGrid td
        {{
            font-weight: normal;
            vertical-align: top;
        }}
        div.eligibilityGrid th
        {{
            background-color: #003b64;
            color: White !important;
            text-align: left;
            vertical-align: middle;
            font-weight: bold;
            border-right: solid 1px white;
        }}
        div.eligibilityGrid th.end
        {{
            border-right: solid 1px #003b64;
        }}
        div.eligibilityGrid td.right, div.eligibilityGrid th.right
        {{
            text-align: right;
        }}
        /*END Eligibility Grid Layout*//*START One column table layout*/div.oneColumnTable table
        {{
            width: 600px;
            margin-bottom: 16px;
            border: none;
        }}
        div.oneColumnTable td, div.oneColumnTable th
        {{
            font-size: 10px;
            font-family: Verdana, ""Lucida Grande"" , Geneva, Tahoma, Sans-Serif;
            font-weight: normal;
            padding: 4px 2px 2px 2px;
            vertical-align: top;
            border: none;
        }}
        div.oneColumnTable th
        {{
            text-align: left;
            font-weight: bold;
        }}
        /*END One column table layout*/div.disclaimer
        {{
            width: 600px;
            font-family: Verdana, ""Lucida Grande"" , Geneva, Tahoma, Sans-Serif;
            font-size: 10px;
            color: #5a5b51;
            line-height: 1.7em;
        }}
    </style>
    </head>
    <body>{0}</body>
</html>"
            #endregion
                , html);
        }

        [TestMethod]
        public void Transform4010Model3ToHtmlTest()
        {
            string html = TransformModelToHtml("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._4010.Example3_CMS_HETS.txt");

            Trace.Write(html);
        }

        [TestMethod]
        public void Transform5010Model2ToHtmlTest()
        {
            string html = TransformModelToHtml("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_2_2.txt");

            Trace.Write(html);
        }
    }
}
