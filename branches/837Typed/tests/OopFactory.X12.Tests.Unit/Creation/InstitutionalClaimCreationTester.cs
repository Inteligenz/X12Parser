using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using OopFactory.X12;
using System.Reflection;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Tests.Unit.Creation
{
    [TestClass]
    public class InstitutionalClaimCreationTester
    {
        private const string InterchangeSample1 =
@"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*1*T*:~
IEA*0*000000031~";

        private Interchange CreateSample1InterChange(DateTime date)
        {
            Interchange interchange = new Interchange(date, 31, false);
            interchange.InterchangeSenderId = "9012345720000";
            interchange.InterchangeReceiverId = "9088877320000";

            return interchange;
        }

        private const string FunctionGroupSample1 =
@"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*1*T*:~
  GS*HC*901234572000*908887732000*20070816*1615*31*X*004010X096A1~
  GE*0*31~
IEA*1*000000031~";

        private const string TransactionSample1 =
@"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*1*T*:~
  GS*HC*901234572000*908887732000*20070816*1615*31*X*004010X096A1~
    ST*837*0034~
    SE*2*0034~
  GE*1*31~
IEA*1*000000031~";

        private Interchange CreateSample1WithFunctionGroup()
        {
            Interchange interchange = CreateSample1InterChange(DateTime.Parse("2002-08-16 11:44AM"));
            FunctionGroup fg = interchange.AddFunctionGroup("HC", DateTime.Parse("2007-08-16 4:15PM"), 31);
            fg.ApplicationSendersCode = "901234572000";
            fg.ApplicationReceiversCode = "908887732000";
            return interchange;
        }
        
        [TestMethod]
        public void SerializeSegmentSet()
        {
            SegmentSet set = new SegmentSet { Name = "4010" };
            SegmentSpecification isa = new SegmentSpecification { SegmentId = "ISA" };
            set.Segments.Add(isa);
            isa.Elements.Add(new ElementSpecification
            {
                Name = "Author Info Qualifier",
                Required = true,
                MinLength = 2,
                MaxLength = 2,
                Type = ElementDataTypeEnum.Identifier
            });
            string xml = set.Serialize();
            Trace.Write(xml);
            SegmentSet copy = SegmentSet.Deserialize(xml);
            SegmentSpecification isaCopy = copy.Segments.FirstOrDefault(s => s.SegmentId == "ISA");
            Assert.IsNotNull(isaCopy);
            Assert.AreEqual("ISA", isaCopy.SegmentId);
            Assert.AreEqual("Author Info Qualifier", isaCopy.Elements[0].Name);
            Assert.AreEqual(2, isaCopy.Elements[0].MinLength);
                
        }

        [TestMethod]
        public void InterchangeCreationTest()
        {
            DateTime date = DateTime.Parse("2002-08-16 11:44AM");
            Interchange interchange = CreateSample1InterChange(date);
            
            string actualX12 = interchange.SerializeToX12(true);
            Assert.AreEqual(InterchangeSample1, actualX12);
            Assert.AreEqual("00", interchange.AuthorInfoQualifier);
            Assert.AreEqual("00", interchange.SecurityInfoQualifier);
            Assert.AreEqual("01", interchange.InterchangeSenderIdQualifier);
            Assert.AreEqual("01", interchange.InterchangeReceiverIdQualifier);
            Assert.AreEqual(date, interchange.InterchangeDate);
        }

        [TestMethod]
        public void InterchangeSenderIdQualifierValidationTest()
        {
            try
            {
                Interchange interchange = CreateSample1InterChange(DateTime.Parse("2002-08-16 11:44AM"));
                interchange.InterchangeSenderIdQualifier = "ER";
                Assert.Fail("An ElementValidationException was expected.");
            }
            catch (ElementValidationException exc) {
                if (exc.ElementId != "ISA05")
                    Assert.Fail(string.Format("Exception expected on ISA05, but got exception on {0} instead.", exc.ElementId));
            }
        }

        [TestMethod]
        public void FunctionGroupCreationTest()
        {
            Interchange interchange = CreateSample1WithFunctionGroup();

            Assert.AreEqual(FunctionGroupSample1, interchange.SerializeToX12(true));
        }

        [TestMethod]
        public void TransactionCreationTest()
        {
            Interchange interchange = CreateSample1WithFunctionGroup();
            interchange.FunctionGroups.First().AddTransaction("837", "0034");

            Assert.AreEqual(TransactionSample1, interchange.SerializeToX12(true));
        }

        [TestMethod]
        public void TransactionCreationWithSegmentFromStringTest()
        {
            Interchange interchange = CreateSample1WithFunctionGroup();
            Transaction transaction = interchange.FunctionGroups.First().AddTransaction("837", "0034");
            Segment bht = transaction.AddSegment("BHT*0019*00*3920394930203*20070816*1615*CH");
            Assert.AreEqual("0019", bht.GetElement(1));
            Trace.Write(interchange.SerializeToX12(true));
        }
        [TestMethod]
        public void TransactionCreationWithSegmentToStringTest()
        {
            Interchange interchange = CreateSample1WithFunctionGroup();
            Transaction transaction = interchange.FunctionGroups.First().AddTransaction("837", "0034");
            Segment bht = transaction.AddSegment("BHT");
            bht.SetElement(1, "0019");
            bht.SetElement(2, "00");
            bht.SetElement(3, "3920394930203");
            bht.SetElement(4, "20070816");
            bht.SetElement(5, "1615");
            bht.SetElement(6, "CH");
            Assert.AreEqual("BHT*0019*00*3920394930203*20070816*1615*CH", bht.SegmentString);
            Trace.Write(interchange.SerializeToX12(false));
        }

        [TestMethod]
        public void ClaimCreationTest()
        {
            Interchange interchange = CreateSample1WithFunctionGroup();
            Transaction transaction = interchange.FunctionGroups.First().AddTransaction("837", "0034");
            Segment bhtSegment = transaction.AddSegment("BHT*0019*00*3920394930203*20070816*1615*CH");
            Segment refSegment = transaction.AddSegment("REF*87*004010X096A1");
            Loop senderLoop = transaction.AddLoop("NM1*41*2*HOWDEE HOSPITAL*****XX*0123456789");
            senderLoop.AddSegment("PER*IC*BETTY RUBBLE*TE*9195551111");
            Loop receiverLoop = transaction.AddLoop("NM1*40*2*BLUE CROSS BLUE SHIELD OF NC*****46*987654321");
            HierarchicalLoop providerLoop = transaction.AddHLoop("1", "20", true);
            providerLoop.AddSegment("PRV*BI*ZZ*203BA0200N");
            var billingProvider = providerLoop.AddLoop("NM1*85*2*HOWDEE HOSPITAL*****XX*0123456789");
            billingProvider.AddSegment("N3*123 HOWDEE BOULEVARD");
            billingProvider.AddSegment("N4*DURHAM*NC*27701");
            billingProvider.AddSegment("REF*1J*654");
            billingProvider.AddSegment("PER*IC*BETTY RUBBLE*TE*9195551111*FX*6145551212");
            HierarchicalLoop subscriberLoop = providerLoop.AddHLoop("2", "22", false);
            subscriberLoop.AddSegment("SBR*P*18*XYZ1234567******BL");
            subscriberLoop.AddSegment("PAT*********Y");
            var subscriberNameLoop = subscriberLoop.AddLoop("NM1*IL*1*DOUGH*MARY****MI*12312312312");
            subscriberNameLoop.AddSegment("N3*BOX 12312");
            subscriberNameLoop.AddSegment("N4*DURHAM*NC*27715");
            subscriberNameLoop.AddSegment("DMG*D8*19670807*F");
            subscriberLoop.AddLoop("NM1*PR*2*BLUE CROSS BLUE SHIELD OF NC*****PI*987654321");
            var claimLoop = subscriberLoop.AddLoop("CLM*2235057*200***13:A:1*Y**Y*A*********N");
            claimLoop.AddSegment("DTP*434*RD8*20070730-20070730");
            claimLoop.AddSegment("CL1*1*9*01");
            claimLoop.AddSegment("AMT*C5*160");
            claimLoop.AddSegment("REF*F8*ASD0000123");
            claimLoop.AddSegment("HI*BK:25000");
            claimLoop.AddSegment("HI*BF:78901");
            claimLoop.AddSegment("HI*BR:4491:D8:20070730");
            claimLoop.AddSegment("HI*BH:41:D8:20070501*BH:27:D8:20070715*BH:33:D8:20070415*BH:C2:D8:20070410");
            claimLoop.AddSegment("HI*BE:30:::20");
            claimLoop.AddSegment("HI*BG:01");
            var physicianLoop = claimLoop.AddLoop("NM1*71*1*SMITH*ELIZABETH*AL***34*243898989");
            physicianLoop.AddSegment("REF*1G*P97777");
            var claimLineLoop = claimLoop.AddLoop("LX*1");
            claimLineLoop.AddSegment("SV2*0300*HC:81000*120*UN*1");
            claimLineLoop.AddSegment("DTP*472*D8*20070730");
            claimLineLoop = claimLoop.AddLoop("LX*2");
            claimLineLoop.AddSegment("SV2*0320*HC:76092*50*UN*1");
            claimLineLoop.AddSegment("DTP*472*D8*20070730");
            claimLineLoop = claimLoop.AddLoop("LX*3");
            claimLineLoop.AddSegment("SV2*0270*HC:J1120*30*UN*1");
            claimLineLoop.AddSegment("DTP*472*D8*20070730");

            Assert.AreEqual(new StreamReader(Extensions.GetEdi("INS._837I._4010.Example1.txt")).ReadToEnd(), interchange.SerializeToX12(true));
        }

        [TestMethod]
        public void ElementValidationTwoArgsTester()
        {
            try
            {
                throw new ElementValidationException("Element {0} cannot contain the value '{1}' with the segment terminator.", "NM1", "AB~CD");
            }
            catch (ElementValidationException exc)
            {
                Assert.AreEqual("Element NM1 cannot contain the value 'AB~CD' with the segment terminator.\r\nParameter name: NM1", exc.Message);
            }
        }

        [TestMethod]
        public void ElementValidationThreeArgsTester()
        {
            try
            {
                throw new ElementValidationException("Element {0} cannot contain the value '{1}' with the segment terminator {2}.", "NM1", "AB~CD", '~');
            }
            catch (ElementValidationException exc)
            {
                Assert.AreEqual("Element NM1 cannot contain the value 'AB~CD' with the segment terminator ~.\r\nParameter name: NM1", exc.Message);
            }
        }

        [TestMethod]
        public void ElementValidationFiveArgsTester()
        {
            try
            {
                throw new ElementValidationException("Element {0} cannot contain the value '{1}' with the segment terminator {2}. Use a value without delimiters {2} {3} or {4}.", "NM1", "AB~CD", '~', '*', ':');
            }
            catch (ElementValidationException exc)
            {
                Assert.AreEqual("Element NM1 cannot contain the value 'AB~CD' with the segment terminator ~. Use a value without delimiters ~ * or :.\r\nParameter name: NM1", exc.Message);
            }
        }
    }
}
