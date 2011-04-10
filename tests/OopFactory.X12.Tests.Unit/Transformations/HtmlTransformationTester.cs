using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using OopFactory.X12.Transformations;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Transformations
{
    [TestClass]
    public class HtmlTransformationTester
    {

        private Stream GetProfessionalClaimEdi(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Tests.Unit.Parsing.Claims.ProfessionalClaims." + filename);
        }

        [TestMethod]
        public void HtmlTransformationTest()
        {
            var htmlService = new X12HtmlTransformationService(new X12EdiParsingService(suppressComments: false));

            Stream ediFile = new FileStream("c:\\5010_Example1_HealthInsurance.txt", FileMode.Open, FileAccess.Read);

            string html = htmlService.Transform(new StreamReader(ediFile).ReadToEnd());

            Trace.Write(html);
        }
    }
}
