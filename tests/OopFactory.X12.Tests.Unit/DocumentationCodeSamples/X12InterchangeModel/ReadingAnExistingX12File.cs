using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Transformations;

namespace OopFactory.X12.Tests.Unit.DocumentationCodeSamples.X12InterchangeModel
{
    [TestClass]
    public class ReadingAnExistingX12File
    {
        private string inquiry = @"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*1*T*:~
            GS*HS*901234572000*908887732000*20070816*1615*31*X*005010X279~
            ST*270*1234*005010X279~
            BHT*0022*13*10001234*20060501*1319~
            HL*1**20*1~
            NM1*PR*2*ABC COMPANY*****PI*842610001~
            HL*2*1*21*1~
            NM1*1P*2*BONE AND JOINT CLINIC*****SV*2000035~
            HL*3*2*22*0~
            TRN*1*93175-012547*9877281234~
            NM1*IL*1*SMITH*ROBERT****MI*11122333301~
            DMG*D8*19430519~
            DTP*291*D8*20060501~
            EQ*30~
            SE*13*1234~
            GE*1*31~
            IEA*1*000000031~";
        
        [TestMethod]
        public void Read270Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(inquiry))).First();

            Assert.AreEqual("9088877320000  ", interchange.InterchangeReceiverId);

            Transaction transaction = interchange.FunctionGroups.First().Transactions.First();
            Segment bht = transaction.Segments.First();

            Assert.AreEqual("10001234", bht.GetElement(3));

            HierarchicalLoop subscriberLoop = transaction.FindHLoop("3");

            Loop subscriberNameLoop = subscriberLoop.Loops.First();

            Assert.AreEqual("SMITH", subscriberNameLoop.GetElement(3), "Subscriber last name not expected.");
            Assert.AreEqual("11122333301", subscriberNameLoop.GetElement(9), "Subscriber member id not expected.");

        }

        [TestMethod]
        public void Transform270ToHtml()
        {
            var htmlService = new X12HtmlTransformationService(new X12EdiParsingService(suppressComments: false));

            Stream ediFile = new MemoryStream(Encoding.ASCII.GetBytes(inquiry));

            string html = htmlService.Transform(new StreamReader(ediFile).ReadToEnd());

            Trace.Write(html);
        }

    }
}
