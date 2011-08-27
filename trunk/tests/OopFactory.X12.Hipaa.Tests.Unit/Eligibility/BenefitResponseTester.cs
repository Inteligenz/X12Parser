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
        public void TransformToModel1Test()
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_1_2.txt");

            var responses = service.Transform271ToBenefitResponse(stream);

            System.Diagnostics.Trace.Write(BenefitResponse.SerializeList(responses));

        }

        [TestMethod]
        public void TransformToModel2Test()
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_2_2.txt");

            var responses = service.Transform271ToBenefitResponse(stream);

            System.Diagnostics.Trace.Write(BenefitResponse.SerializeList(responses));
        }

        [TestMethod]
        public void TransformModel2ToHtmlTest()
        {
            var service = new EligibilityTransformationService();

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("OopFactory.X12.Hipaa.Tests.Unit.Eligibility.TestData._271._5010.Example_3_2_2.txt");

            var responses = service.Transform271ToBenefitResponse(stream);

            string html = service.TransformBenefitResponseToHtml(responses.First());
            
            html = String.Format(
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
</html>", html);

            System.Diagnostics.Trace.Write(html);
        }
    }
}
