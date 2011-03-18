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

        private const string SAMPLE_2 =
@"ISA*00*          *00*          *01*201495124      *01*207663412      *010420*1312*U*00401*000000001*1*P*:~
GS*SH*123456789*987654321*030619*1235*1*X*003060~
ST*856*0001~
BSN*00**030619*1235~
DTM*011*030619*1445~
DTM*017*030620*0800~
HL*1**S~
MEA*PD*G*3100*LB~
MEA*PD*N*2800*LB~
TD1* BIN52*5~
TD5*B*2*MPNF*M~
TD3*TL**287532~
REF*BM*25673~
REF*PK*18392~
REF*CN*88145~
N1*SU*MNO ASSEMBLY*92*123456789~
N1*ST*ABC MANUFACTURING*92*987654321~
N1*SF*XYZ WAREHOUSE*92*567891234~
HL*1*1*I~
LIN**BP*rt2371~
SN1**1400*PC*6000~
PRF*B00000389*0005***~
CLD*2*700* BIN52~
REF*LS*798412~
REF*LS*798413~
HL*2*1*I~
LIN**BP*lt2372~
SN1**1500*PC*6000~
PRF*B00003489*0002***~
CLD*1*700* BIN52~
REF*LS*798514~
CLD*2*400* BIN52~
REF*LS*798515~
REF*LS*798516~
CTT*2*2900~
SE*35*0001~
GE*1*1~
IEA*1*000000001*~";

        [TestMethod]
        public void ParseToX12Xml()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(SAMPLE_1);
            Trace.Write(xml);
        }

        [TestMethod]
        public void ParseToX12Xml_Sample2()
        {
            var service = new X12ParsingService(true);
            var xml = service.ParseToXml(SAMPLE_2);
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
