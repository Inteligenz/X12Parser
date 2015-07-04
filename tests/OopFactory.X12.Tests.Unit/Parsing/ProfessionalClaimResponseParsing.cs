using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Tests.Unit.Parsing
{
    [TestClass]
    public class ProfessionalClaimResponseParsing
    {

        public string sample835EDI = "ISA*00*          *00*          *ZZ*Sender ID      *ZZ*Receiver ID    *150704*0516*^*00501*069881995*0*P*:~GS*HP*Sender Org*Receiver Org*20150506*0516*10046529*X*005010X221A1~ST*835*0001~BPR*I*300.56*C*ACH*CCP*01*XXXYYYZZZ*DA*AAABBBCCC*1234567890**01*120123456789*SG*ACCT123149382398*20150704~TRN*1*234098193482*1234567890~REF*EV*RECEIVERID~DTM*405*20150704~N1*PR*PAYER NAME~N3*PAYER ADDRESS~N4*ANYWHERE*FL*32820~PER*CX**TE*8881112222~PER*BL*PAYER TECH DEPT*TE*8005557777*EM*ADDRESS@PAYER.COM~PER*IC**UR*WWW.PAYER.COM~N1*PE*PAYEE NAME*XX*PAYEENPI~N3*PAYEE ADDRESS~N4*ANYWHERE*FL*32820~REF*TJ*PAYEETAXID~LX*1~CLP*PAYEECLAIMID*1*483*300.56**12*PAYERCLAIMID*11*1~NM1*QC*1*PATIENTLASTNAME*PATIENTFIRSTNAME****MI*PATIENTMEMBERID~NM1*IL*1*SUBSCRIBERLASTNAME*SUBSCRIBERFIRSTNAME****MI*SUBSCRIBERMEMBERID~NM1*82*1*PROVIDERLASTNAME*PROVIDERFIRSTNAME****XX*PROVIDERNPI~REF*1W*MEMBERIDENTIFIERASSIGNEDBYPAYER~DTM*050*20150703~SVC*HC:90460*105*55.02**3~DTM*472*20150703~CAS*CO*45*49.98~AMT*B6*55.02~SVC*HC:90461*50*19.24**2~DTM*472*20150703~CAS*CO*45*30.76~AMT*B6*19.24~SVC*HC:90648*54*31.3**1~DTM*472*20150703~CAS*CO*45*22.7~AMT*B6*31.3~SVC*HC:90670*222*164**1~DTM*472*20150703~CAS*CO*45*58~AMT*B6*164~SVC*HC:90700*52*31**1~DTM*472*20150703~CAS*CO*45*21~AMT*B6*31~SE*43*0001~GE*1*10046529~IEA*1*069881995~";

        [TestMethod]
        public void Parse835Response()
        {
            var parser = new X12Parser();
            var envelopes = parser.ParseMultiple(sample835EDI);
            Assert.AreEqual(1, envelopes.Count, "Envelope count (ISA) is wrong");

            var isa = envelopes[0];
            Assert.AreEqual(1, isa.FunctionGroups.Count(), "Function Group (GS) count is wrong");

            var gs = isa.FunctionGroups.ElementAt(0);
            Assert.AreEqual(1, gs.Transactions.Count, "Transactions (ST) count is wrong");

            var st = gs.Transactions[0];
            Assert.AreEqual(3, st.Loops.Count(), "Transaction Loop count is wrong");

            var n1Loops = st.Loops.Where(t => t.SegmentId.Equals("N1")).Select(t => new TypedLoopN1(t));
            Assert.AreEqual(2, n1Loops.Count(), "The count of N1 Loops is wrong");

            Assert.AreEqual(1, n1Loops.Count(t => t.N101_EntityIdentifierCodeEnum == EntityIdentifierCode.Payer), "Payer N1 Loop is missing");
            Assert.AreEqual(1, n1Loops.Count(t => t.N101_EntityIdentifierCodeEnum == EntityIdentifierCode.Payee), "Payee N1 Loop is missing");

            var lxLoops = st.Loops.Where(t => t.SegmentId.Equals("LX")).Select(t => new TypedLoopLX(t));
            Assert.AreEqual(1, lxLoops.Count(), "The count of LX Loops is wrong");

            var lxLoop = lxLoops.ElementAt(0);
            Assert.AreEqual(1, lxLoop.Loop.Loops.Count(t => t.SegmentId.Equals("CLP")), "The count of CLP loops is wrong.");

            var clpLoop = new TypedLoopCLP(lxLoop.Loop.Loops.First());

            Assert.AreEqual("PAYEECLAIMID", clpLoop.CLP01_ClaimSubmittersIdentifier, "CLP 01 (PAYEE CLAIM ID) Value is wrong.");

            Assert.AreEqual(483, clpLoop.CLP03_MonetaryAmount, "CLP 03 (Charge Amount) Value is wrong.");

            Assert.AreEqual("PAYERCLAIMID", clpLoop.CLP07_ReferenceIdentification, "CLP 07 (PAYER CLAIM ID) Value is wrong.");
            Validate2100Loops(clpLoop.Loop);
            Validate2100Loops(clpLoop.Loop);

        }

        private void ValidateClpegments(Loop clp)
        {
            Assert.AreEqual(10, clp.Segments.Count(), "Invalid amount of segments within the CLP loop.");
            var nm1Segs = clp.Segments.Where(t => t.SegmentId.Equals("NM1")).Select(t => new TypedSegmentNM1(t));
            Assert.AreEqual(3, nm1Segs, "Invalid amount of NM1 segments within the CLP loop.");

            Assert.AreEqual(1, nm1Segs.Count(t => t.NM101_EntityIdentifierCodeEnum == EntityIdentifierCode.Patient), "Missing Patient NM1 Loop.");
            Assert.AreEqual(1, nm1Segs.Count(t => t.NM101_EntityIdentifierCodeEnum == EntityIdentifierCode.Subscriber), "Missing Subscriber NM1 Loop.");
            Assert.AreEqual(1, nm1Segs.Count(t => t.NM101_EntityIdentifierCodeEnum == EntityIdentifierCode.Provider), "Missing Provider NM1 Loop.");
        }

        private void Validate2100Loops(Loop clp)
        {
            var svcLoops = clp.Loops.Where(t => t.SegmentId.Equals("SVC")).Select(t => new TypedLoopSVC(t));
            Assert.AreEqual(5, svcLoops.Count(), "Invalid amount of child loops within the CLP loop.");
            var svcLoop0 = svcLoops.ElementAt(0);
            Assert.AreEqual(ProductOrServiceIdQualifiers.HealthCareFinancingAdministrationCommonProceduralCodingSystem, svcLoop0.SVC01_CompositeMedicalProcedureIdentifier._1_ProductOrServiceIdQualifier, "Invalid Service Qualifier on SVC 0.");
            Assert.AreEqual("90460", svcLoop0.SVC01_CompositeMedicalProcedureIdentifier._2_ProcedureCode, "Invalid procedure code on SVC 0.");
            Assert.AreEqual(105m, svcLoop0.SVC02_MonetaryAmount, "Invalid Charge amount.");
            Assert.AreEqual(55.02m, svcLoop0.SVC03_MonetaryAmount, "Invalid payment amount.");
            Assert.AreEqual(3m, svcLoop0.SVC05_Quantiy, "Invalid Quanity.");

            Assert.AreEqual(3, svcLoop0.Loop.Segments.Count(), "Invalid count of segments with SVC 0 Loop.");

            var dtmUSeg = svcLoop0.Loop.Segments.FirstOrDefault(t => t.SegmentId.Equals("DTM"));
            var casUSeg = svcLoop0.Loop.Segments.FirstOrDefault(t => t.SegmentId.Equals("CAS"));
            var amtUSeg = svcLoop0.Loop.Segments.FirstOrDefault(t => t.SegmentId.Equals("AMT"));

            Assert.IsNotNull(dtmUSeg, "DTM segment was not found in the SVC 0 loop.");
            Assert.IsNotNull(casUSeg, "CAS segment was not found in the SVC 0 loop.");
            Assert.IsNotNull(amtUSeg, "AMT segment was not found in the SVC 0 loop.");

            var dtmSeg = new TypedSegmentDTM(dtmUSeg);
            var casSeg = new TypedSegmentCAS(casUSeg);
            var amtSeg = new TypedSegmentCAS(amtUSeg);

            Assert.AreEqual("DTM*472*20150703", dtmUSeg.ToString(), "Invalid DTM segment.");
            Assert.AreEqual("CAS*CO*45*49.98", casUSeg.ToString(), "Invalid CAS segment.");
            Assert.AreEqual("AMT*B6*55.02", amtUSeg.ToString(), "Invalid AMT segment.");

            Assert.AreEqual(ClaimAdjustmentGroupCodes.ContractualObligations, casSeg.CAS01_ClaimAdjustmentGroupCode, "Invalid adjustment group code on CAS segment.");
            Assert.AreEqual(49.98m, casSeg.CAS03_MonetaryAmount, "Invalid adjustment monetary amount on CAS segment.");

        }
    }
}
