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

namespace OopFactory.X12.Tests.Unit.Unbundling
{
    [TestClass]
    public class UnbundlingTester
    {
        [TestMethod]
        public void UnbundleItemsFrom856Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("ORD._856.Example1.txt"));

            var list = parser.UnbundleByLoop(interchange, "ITEM");
            foreach (var item in list)
            {
                Trace.WriteLine("...");
                Trace.WriteLine(item.SerializeToX12(true));
            }
        }

        #region Expected Values
        private string expectedClaim1 = @"ISA*00*          *01*SECRET    *ZZ*SUBMITTERS.ID  *ZZ*RECEIVERS.ID   *030101*1253*^*00501*000000905*1*T*:~
  GS*HC*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X222~
    ST*837*0021*005010X222~
      BHT*0019*00*244579*20061015*1023*CH~
      NM1*41*2*PREMIER BILLING SERVICE*****46*TGJ23~
        PER*IC*JERRY*TE*3055552222*EX*231~
      NM1*40*2*KEY INSURANCE COMPANY*****46*66783JJT~
      HL*1**20*1~
        PRV*BI*PXC*203BF0100Y~
        NM1*85*2*BEN KILDARE SERVICE*****XX*9876543210~
          N3*234 SEAWAY ST~
          N4*MIAMI*FL*33111~
          REF*EI*587654321~
        NM1*87*2~
          N3*2345 OCEAN BLVD~
          N4*MAIMI*FL*33111~
        HL*2*1*22*1~
          SBR*P**2222-SJ******CI~
          NM1*IL*1*SMITH*JANE****MI*JS00111223333~
            DMG*D8*19430501*F~
          NM1*PR*2*KEY INSURANCE COMPANY*****PI*999996666~
            REF*G2*KA6663~
          HL*3*2*23*0~
            PAT*19~
            NM1*QC*1*SMITH*TED~
              N3*236 N MAIN ST~
              N4*MIAMI*FL*33413~
              DMG*D8*19730501*M~
            CLM*26463774*100***11:B:1*Y*A*Y*I~
              REF*D9*17312345600006351~
              HI*BK:0340*BF:V7389~
              LX*1~
                SV1*HC:99213*40*UN*1***1~
                DTP*472*D8*20061003~
              LX*2~
                SV1*HC:87070*15*UN*1***1~
                DTP*472*D8*20061003~
              LX*3~
                SV1*HC:99214*35*UN*1***2~
                DTP*472*D8*20061010~
              LX*4~
                SV1*HC:86663*10*UN*1***2~
                DTP*472*D8*20061010~
    SE*42*0021~
  GE*1*1~
IEA*1*000000905~";

        private string expectedClaim2 = @"ISA*00*          *01*SECRET    *ZZ*SUBMITTERS.ID  *ZZ*RECEIVERS.ID   *030101*1253*^*00501*000000905*1*T*:~
  GS*HC*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X222~
    ST*837*0021*005010X222~
      BHT*0019*00*244579*20061015*1023*CH~
      NM1*41*2*PREMIER BILLING SERVICE*****46*TGJ23~
        PER*IC*JERRY*TE*3055552222*EX*231~
      NM1*40*2*KEY INSURANCE COMPANY*****46*66783JJT~
      HL*4**20*1~
        NM1*85*1*KILDARE*BEN****XX*1999996666~
          N3*1234 SEAWAY ST~
          N4*MIAMI*FL*33111~
          REF*EI*123456789~
          PER*IC*CONNIE*TE*3055551234~
        NM1*87*2~
          N3*2345 OCEAN BLVD~
          N4*MIAMI*FL*33111~
        HL*5*4*22*1~
          SBR*P*******CI~
          NM1*IL*1*SMITH*JANE****MI*111223333~
            DMG*D8*19430501*F~
          NM1*PR*2*KEY INSURANCE COMPANY*****PI*999996666~
            N3*3333OCEAN ST~
            N4*SOUTH MIAMI*FL*33000~
            REF*G2*PBS3334~
          HL*6*5*23*0~
            PAT*19~
            NM1*QC*1*SMITH*TED~
              N3*236 N MAIN ST~
              N4*MIAMI*FL*33413~
              DMG*D8*19730501*M~
            CLM*26407789*79.04***11:B:1*Y*A*Y*I*P~
              HI*BK:4779*BF:2724*BF:2780*BF:53081~
              NM1*82*1*KILDARE*BEN****XX*1999996666~
                PRV*PE*PXC*204C00000X~
                REF*G2*KA6663~
              NM1*77*2*KILDARE ASSOCIATES*****XX*1581234567~
                N3*2345 OCEAN BLVD~
                N4*MIAMI*FL*33111~
              SBR*S*01*******CI~
                DMG*D8*19430501*F~
                OI***Y*P**Y~
                NM1*IL*1*SMITH*JACK****MI*T55TY666~
                  N3*236 N MAIN ST~
                  N4*MIAMI*FL*33111~
                NM1*PR*2*KEY INSURANCE COMPANY*****PI*999996666~
              LX*1~
                SV1*HC:99213*43*UN*1***1:2:3:4~
                DTP*472*D8*20051003~
              LX*2~
                SV1*HC:90782*15*UN*1***1:2~
                DTP*472*D8*20051003~
              LX*3~
                SV1*HC:J3301*21.04*UN*1***1:2~
                DTP*472*D8*20051003~
    SE*53*0021~
  GE*1*1~
IEA*1*000000905~";

        private string expectedClaim3 = @"ISA*00*          *01*SECRET    *ZZ*SUBMITTERS.ID  *ZZ*RECEIVERS.ID   *030101*1253*^*00501*000000905*1*T*:~
  GS*HC*SENDER CODE*RECEIVER CODE*19991231*0802*1*X*005010X222~
    ST*837*0022*005010X222~
      BHT*0019*00*0123*20061015*1023*RP~
      NM1*41*2*PREMIER BILLING SERVICE*****46*TGJ23~
        PER*IC*JERRY*TE*3055552222*EX*231~
      NM1*40*2*AHLIC*****46*66783JJT~
      HL*1**20*1~
        PRV*BI*PXC*203BF0100Y~
        NM1*85*2*BEN KILDARE SERVICE*****XX*9876543210~
          N3*234 SEAWAY ST~
          N4*MIAMI*FL*33111~
          REF*EI*587654321~
        NM1*87*2~
          N3*2345 OCEAN BLVD~
          N4*MIAMI*FL*33111~
        HL*2*1*22*0~
          SBR*P*18*12312-A******HM~
          NM1*IL*1*SMITH*TED****MI*00221111~
            N3*236 N MAIN ST~
            N4*MIAMI*FL*33413~
            DMG*D8*19430501*M~
          NM1*PR*2*ALLIANCE HEALTH AND LIFE INSURANCE*****PI*741234~
          CLM*26462967*100***11:B:1*Y*A*Y*I~
            DTP*431*D8*19981003~
            REF*D9*17312345600006351~
            HI*BK:0340*BF:V7389~
            NM1*77*2*KILDARE ASSOCIATES*****XX*5812345679~
              N3*2345 OCEAN BLVD~
              N4*MIAMI*FL*33111~
            LX*1~
              SV1*HC:99213*40*UN*1***1~
              DTP*472*D8*20061003~
            LX*2~
              SV1*HC:87072*15*UN*1***1~
              DTP*472*D8*20061003~
            LX*3~
              SV1*HC:99214*35*UN*1***2~
              DTP*472*D8*20061010~
            LX*4~
              SV1*HC:86663*10*UN*1***2~
              DTP*472*D8*20061010~
    SE*41*0022~
  GE*1*1~
IEA*1*000000905~";

        #endregion

        [TestMethod]
        public void UnbundleClaimsFrom837Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._837P._5010.Example1_2_And_3_Combined.txt"));

            var list = parser.UnbundleByLoop(interchange, "2300");
            Assert.AreEqual(3, list.Count);
            foreach (var item in list)
            {
                Trace.WriteLine("...");
                Trace.WriteLine(item.SerializeToX12(true));
            }

            Assert.AreEqual(expectedClaim1, list[0].SerializeToX12(true));
            Assert.AreEqual(expectedClaim2, list[1].SerializeToX12(true));
            Assert.AreEqual(expectedClaim3, list[2].SerializeToX12(true));
        }

        [TestMethod]
        public void Unbundle835FromNthTest()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._835._4010.FromNth.835_DeIdent_02.dat"));

            var list = parser.UnbundleByLoop(interchange, "2000");
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void Unbundling835ByLoop2100()
        {
            string thirdUnbundledClaim = @"ISA*00*          *00*          *ZZ*ASHTB          *ZZ*01017          *040315*1005*U*00401*004075123*0*P*:~
  GS*HP*ASHTB*01017*20040315*1005*1*X*004010X091A1~
    ST*835*07504123~
      BPR*H*5.75*C*NON************20040315~
      TRN*1*A04B001017.07504*1346000128~
      DTM*405*20040308~
      N1*PR*ASHTABULA COUNTY ADAMH BD~
        N3*4817 STATE ROAD SUITE 203~
        N4*ASHTABULA*OH*44004~
      N1*PE*LAKE AREA RECOVERY CENTER *FI*346608640~
        N3*2801 C. COURT~
        N4*ASHTABULA*OH*44004~
        REF*PQ*1017~
      PLB*123456*19960930*CV:9876514*-1.27~
      LX*1~
        CLP*888888*4*162.13*0*162.13*MC*0000000456789123*11~
          NM1*QC*1*SQUAREPANTS*BOB* ***MI*2222222~
          NM1*82*2*BIKINI AGENCY*****FI*310626223~
          REF*F8*H57B10401~
          SVC*ZZ:M151000:F0*162.13*0**1.9~
            DTM*472*20020920~
            CAS*CO*29*162.13*0*42*0*0~
            REF*6R*888888~
    SE*22*07504123~
  GE*1*1~
IEA*1*004075123~";

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._835._4010.Example1_GripElements.txt"));

            var list = parser.UnbundleByLoop(interchange, "2100");
            Assert.AreEqual(9, list.Count);
            Assert.AreEqual(thirdUnbundledClaim, list[2].SerializeToX12(true));
            Trace.WriteLine(list[2].SerializeToX12(true));
        }

        [TestMethod]
        public void Unbundling835ByLoop2110()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._835._4010.Example1_GripElements.txt"));

            var list = parser.UnbundleByLoop(interchange, "2110");
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void UnbundleClaimsIn837FromNthTest()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._837P._4010.FromNth.837_DeIdent_05.dat"));

            var list = parser.UnbundleByLoop(interchange, "2300");
            Assert.AreEqual(186, list.Count);
        }

        [TestMethod]
        public void UnbundleClaimsIn837ByServiceLineTest()
        {
            string lastServiceLine = @"ISA*00*          *01*SECRET    *ZZ*SUBMITTERS.ID  *ZZ*RECEIVERS.ID   *930602*1253*^*00401*000000905*1*T*:~
  GS*HC*SENDER CODE*RECEIVER CODE*1994033*0802*1*X*004010X098A1~
    ST*837*0021~
      BHT*0019*00*0123*19981015*1023*RP~
      REF*87*004010X098~
      NM1*41*2*PREMIER BILLING SERVICE*****46*TGJ23~
        PER*IC*JERRY*TE*3055552222*EX*231~
      NM1*40*2*REPRICER XYZ*****46*66783JJT~
      HL*1**20*1~
        NM1*85*2*PREMIER BILLING SERVICE*****24*587654321~
          N3*234 Seaway St~
          N4*Miami*FL*33111~
        NM1*87*2*KILDARE ASSOC*****24*581234567~
          N3*2345 OCEAN BLVD~
          N4*MIAMI*FL*33111~
        HL*2*1*22*0~
          SBR*P*18*12312-A******HM~
          NM1*IL*1*SMITH*TED****34*000221111~
            N3*236 N MAIN ST~
            N4*MIAMI*FL*33413~
            DMG*D8*19430501*M~
          NM1*PR*2*ALLIANCE HEALTH AND LIFE INSURANCE*****PI*741234~
          CLM*26462967*100***11::1*Y*A*Y*Y*C~
            DTP*431*D8*19981003~
            REF*D9*17312345600006351~
            HI*BK:0340*BF:V7389~
            NM1*82*1*KILDARE*BEN****34*112233334~
              PRV*PE*ZZ*203BF0100Y~
            NM1*77*2*KILDARE ASSOCIATES*****24*581234567~
              N3*2345 OCEAN BLVD~
              N4*MIAMI*FL*33111~
            LX*4~
              SV1*HC:86663*10*UN*1***2**N~
              DTP*472*D8*19981010~
    SE*33*0021~
  GE*1*1~
IEA*1*000000905~";

            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(Extensions.GetEdi("INS._837P._4010.Spec_4.1.1_PatientIsSubscriber.txt"));

            var list = parser.UnbundleByLoop(interchange, "2400");

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(lastServiceLine, list[3].SerializeToX12(true));
        }
    }
}
