using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Parsing.Suppliers
{
    [TestClass]
    public class ShipNoticeTester
    {
        private const string SAMPLE_1 = 
@"ISA~00~          ~00~          ~01~201495124      ~01~207663412      ~010420~1312~U~00401~000000001~1~P~`*
GS~SH~207663412~201495124~20010420~1312~1~X~004010*
ST~856~0001*
BSN~00~123456~20010420~1421*
DTM~011~20010420~1421*
DTM~017~20010422~0800*
HL~1~~S*
REF~PK~012345678901234*
N1~SF~ACME~ZZ~ACME ELECT00*
N1~ST~ESSAR Steel Algoma Inc.~1~201495124*
HL~2~1~O*
PRF~123456~1234*
HL~3~2~I*
LIN~001~CB~1234-123456*
SN1~~16~PC*
HL~4~2~I*
LIN~002~CB~1234-654321*
SN1~~23~LB*
HL~5~2~I*
LIN~004~CB~1234-223456*
SN1~~16~PC*
CTT~5*
SE~27~0001*
GE~1~1*
IEA~1~000000001*";
        [TestMethod]
        public void ParseToX12Xml()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(SAMPLE_1);
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToDomainXml()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToDomainXml(SAMPLE_1);
            Trace.Write(xml);
        }
    }
}
