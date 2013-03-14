using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;

namespace OopFactory.X12.Tests.Unit.Creation
{
    [TestClass]
    public class ProfessionalClaimCreationTester
    {
        [TestMethod]
        public void Create837_5010Version()
        {
            var message = new Interchange(Convert.ToDateTime("01/01/03"), 000905, false)
                              {
                                  InterchangeSenderIdQualifier = "ZZ",
                                  InterchangeSenderId = "SUBMITTERS.ID",
                                  InterchangeReceiverIdQualifier = "ZZ",
                                  InterchangeReceiverId = "RECEIVERS.ID",
                                  SecurityInfo = "SECRET",
                                  SecurityInfoQualifier = "01",
                              };
            message.SetElement(12, "00501");
            message.SetElement(10, "1253");
            message.SetElement(11, "^");

            var group = message.AddFunctionGroup("HC", DateTime.Now, 1, "005010X222");
            group.ApplicationSendersCode = "SENDER CODE";
            group.ApplicationReceiversCode = "RECEIVER CODE";
            group.Date = Convert.ToDateTime("12/31/1999");
            group.ControlNumber = 1;
            group.SetElement(5, "0802");


            var transaction = group.AddTransaction("837", "0021");
            transaction.SetElement(2, "0021");
            transaction.SetElement(3, "005010X222");

            var bhtSegment = transaction.AddSegment(new TypedSegmentBHT());
            bhtSegment.BHT01_HierarchicalStructureCode = "0019";
            bhtSegment.BHT02_TransactionSetPurposeCode = "00";
            bhtSegment.BHT03_ReferenceIdentification = "244579";
            bhtSegment.BHT04_Date = DateTime.Parse("2006-10-15");
            bhtSegment.BHT05_Time = "1023";
            bhtSegment.BHT06_TransactionTypeCode = "CH";

            var submitterLoop = transaction.AddLoop(new TypedLoopNM1("41")); //submitter identifier code
            submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            submitterLoop.NM103_NameLastOrOrganizationName = "PREMIER BILLING SERVICE";
            submitterLoop.NM104_NameFirst = "";
            submitterLoop.NM109_IdCode = "TGJ23";
            submitterLoop.NM108_IdCodeQualifier = "46";

            var perSegment = submitterLoop.AddSegment(new TypedSegmentPER());
            perSegment.PER01_ContactFunctionCode = "IC"; //information contact function code
            perSegment.PER02_Name = "JERRY";
            perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone;
            perSegment.PER04_CommunicationNumber = "3055552222";
            perSegment.PER05_CommunicationNumberQualifier = CommunicationNumberQualifer.TelephoneExtension;
            perSegment.PER06_CommunicationNumber = "231";

            var submitterLoop2 = transaction.AddLoop(new TypedLoopNM1("40"));
            submitterLoop2.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            submitterLoop2.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY";
            submitterLoop2.NM104_NameFirst = "";
            submitterLoop2.NM109_IdCode = "66783JJT";
            submitterLoop2.NM108_IdCodeQualifier = "46";

            var provider2000AHLoop = transaction.AddHLoop("1", "20", true); //*********HL 1 ******
            var prvSegment = provider2000AHLoop.AddSegment(new TypedSegmentPRV()); //Specialty Segment
            prvSegment.PRV01_ProviderCode = "BI";
            prvSegment.PRV02_ReferenceIdQualifier = "PXC";
            prvSegment.PRV03_ProviderTaxonomyCode = "203BF0100Y";

            var provider2010ACLoop = provider2000AHLoop.AddLoop(new TypedLoopNM1("85"));
            provider2010ACLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            provider2010ACLoop.NM103_NameLastOrOrganizationName = "BEN KILDARE SERVICE";
            provider2010ACLoop.NM109_IdCode = "9876543210";
            provider2010ACLoop.NM108_IdCodeQualifier = "XX";

            var provider2010AC_N3Segment = provider2010ACLoop.AddSegment(new TypedSegmentN3());
            provider2010AC_N3Segment.N301_AddressInformation = "234 SEAWAY ST";

            var provider2010AC_N4Segment = provider2010ACLoop.AddSegment(new TypedSegmentN4());
            provider2010AC_N4Segment.N401_CityName = "MIAMI";
            provider2010AC_N4Segment.N402_StateOrProvinceCode = "FL";
            provider2010AC_N4Segment.N403_PostalCode = "33111";

            var provider2010AC_REFSegment = provider2010ACLoop.AddSegment(new TypedSegmentREF());
            provider2010AC_REFSegment.REF01_ReferenceIdQualifier = "EI";
            provider2010AC_REFSegment.REF02_ReferenceId = "587654321";

            var provider2010ACLoop2 = provider2000AHLoop.AddLoop(new TypedLoopNM1("87"));
            provider2010ACLoop2.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;

            var provider2010AC_N3Segment2 = provider2010ACLoop2.AddSegment(new TypedSegmentN3());
            provider2010AC_N3Segment2.N301_AddressInformation = "2345 OCEAN BLVD";

            var provider2010AC_N4Segment2 = provider2010ACLoop2.AddSegment(new TypedSegmentN4());
            provider2010AC_N4Segment2.N401_CityName = "MAIMI";  // MISSPELLED IN COMPARETO DOC
            provider2010AC_N4Segment2.N402_StateOrProvinceCode = "FL";
            provider2010AC_N4Segment2.N403_PostalCode = "33111";

            var subscriber2000BHLoop = provider2000AHLoop.AddHLoop("2", "22", true);  // **** HL 2  ******

            var segmentSBR = subscriber2000BHLoop.AddSegment(new TypedSegmentSBR());
            segmentSBR.SBR01_PayerResponsibilitySequenceNumberCode = "P";
            segmentSBR.SBR03_PolicyOrGroupNumber = "2222-SJ";
            segmentSBR.SBR09_ClaimFilingIndicatorCode = "CI";

            var subscriberName2010BALoop = subscriber2000BHLoop.AddLoop(new TypedLoopNM1("IL"));
            subscriberName2010BALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            subscriberName2010BALoop.NM104_NameFirst = "JANE";
            subscriberName2010BALoop.NM103_NameLastOrOrganizationName = "SMITH";
            subscriberName2010BALoop.NM109_IdCode = "JS00111223333";
            subscriberName2010BALoop.NM108_IdCodeQualifier = "MI";

            var subscriber_DMGSegment = subscriberName2010BALoop.AddSegment(new TypedSegmentDMG());
            subscriber_DMGSegment.DMG01_DateTimePeriodFormatQualifier = "D8";
            subscriber_DMGSegment.DMG02_DateOfBirth = DateTime.Parse("5/1/1943");
            subscriber_DMGSegment.DMG03_Gender = Gender.Female;

            var subscriberName2010BALoop2 = subscriber2000BHLoop.AddLoop(new TypedLoopNM1("PR"));
            subscriberName2010BALoop2.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            subscriberName2010BALoop2.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY";
            subscriberName2010BALoop2.NM108_IdCodeQualifier = "PI";
            subscriberName2010BALoop2.NM109_IdCode = "999996666";

            var refSegment2 = subscriberName2010BALoop2.AddSegment(new TypedSegmentREF());
            refSegment2.REF01_ReferenceIdQualifier = "G2";
            refSegment2.REF02_ReferenceId = "KA6663";

            var HL3Loop = subscriber2000BHLoop.AddHLoop("3", "23", false);   // **** HL 3  ******

            var HL3PATSegment = HL3Loop.AddSegment(new TypedSegmentPAT());
            HL3PATSegment.PAT01_IndividualRelationshipCode = "19";

            var HL3NM1Segment = HL3Loop.AddLoop(new TypedLoopNM1("QC"));
            HL3NM1Segment.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            HL3NM1Segment.NM104_NameFirst = "TED";
            HL3NM1Segment.NM103_NameLastOrOrganizationName = "SMITH";


            // add N3 and N4 segments under the above NM1 loop

            var HL3NM1_N3_Segment = HL3NM1Segment.AddSegment(new TypedSegmentN3());
            HL3NM1_N3_Segment.N301_AddressInformation = "236 N MAIN ST";

            var HL3NM1_N4_Segment = HL3NM1Segment.AddSegment(new TypedSegmentN4());
            HL3NM1_N4_Segment.N401_CityName = "MIAMI";
            HL3NM1_N4_Segment.N402_StateOrProvinceCode = "FL";
            HL3NM1_N4_Segment.N403_PostalCode = "33413";

            var HL3NM1_DMG_Segment = HL3NM1Segment.AddSegment(new TypedSegmentDMG());
            HL3NM1_DMG_Segment.DMG01_DateTimePeriodFormatQualifier = "D8";
            HL3NM1_DMG_Segment.DMG02_DateOfBirth = Convert.ToDateTime("5/1/1973");
            HL3NM1_DMG_Segment.DMG03_Gender = Gender.Male;

            var claim2300Loop = HL3Loop.AddLoop(new TypedLoopCLM());
            claim2300Loop.CLM01_PatientControlNumber = "26463774";
            claim2300Loop.CLM02_TotalClaimChargeAmount = Convert.ToDecimal(100);
            claim2300Loop.CLM05._1_FacilityCodeValue = "11";
            claim2300Loop.CLM05._2_FacilityCodeQualifier = "B";
            claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = "1";
            claim2300Loop.CLM06_ProviderOrSupplierSignatureIndicator = true;
            claim2300Loop.CLM07_ProviderAcceptAssignmentCode = "A";
            claim2300Loop.CLM08_BenefitsAssignmentCerficationIndicator = "Y";
            claim2300Loop.CLM09_ReleaseOfInformationCode = "I";

            var refSegment = claim2300Loop.AddSegment(new TypedSegmentREF());
            refSegment.REF01_ReferenceIdQualifier = "D9";
            refSegment.REF02_ReferenceId = "17312345600006351";

            var hiSegment = claim2300Loop.AddSegment(new TypedSegmentHI());
            hiSegment.HI01_HealthCareCodeInformation = "BK:0340";
            hiSegment.HI02_HealthCareCodeInformation = "BF:V7389";

            var lxLoop = claim2300Loop.AddLoop(new TypedLoopLX("LX"));
            lxLoop.LX01_AssignedNumber = "1";

            var sv1Segment = lxLoop.AddSegment(new TypedSegmentSV1());
            sv1Segment.SV101_CompositeMedicalProcedure = "HC:99213";
            sv1Segment.SV102_MonetaryAmount = "40";
            sv1Segment.SV103_UnitBasisMeasCode = "UN";
            sv1Segment.SV104_Quantity = "1";
            sv1Segment.SV107_CompDiagCodePoint = "1";

            var dtpSegment = lxLoop.AddSegment(new TypedSegmentDTP());
            dtpSegment.DTP01_DateTimeQualifier  = DTPQualifier.Service;
            dtpSegment.DTP02_DateTimePeriodFormatQualifier = DTPFormatQualifier.CCYYMMDD;
            DateTime theDate = DateTime.ParseExact("20061003", "yyyyMMdd", null);
            dtpSegment.DTP03_Date = new DateTimePeriod(theDate);

            var lxLoop2 = claim2300Loop.AddLoop(new TypedLoopLX("LX"));
            lxLoop2.LX01_AssignedNumber = "2";

            var sv1Segment2 = lxLoop2.AddSegment(new TypedSegmentSV1());
            sv1Segment2.SV101_CompositeMedicalProcedure = "HC:87070";
            sv1Segment2.SV102_MonetaryAmount = "15";
            sv1Segment2.SV103_UnitBasisMeasCode = "UN";
            sv1Segment2.SV104_Quantity = "1";
            sv1Segment2.SV107_CompDiagCodePoint = "1";

            var dtpSegment2 = lxLoop2.AddSegment(new TypedSegmentDTP());
            dtpSegment2.DTP01_DateTimeQualifier = DTPQualifier.Service;
            dtpSegment2.DTP02_DateTimePeriodFormatQualifier = DTPFormatQualifier.CCYYMMDD;
            DateTime theDate2 = DateTime.ParseExact("20061003", "yyyyMMdd", null);
            dtpSegment2.DTP03_Date = new DateTimePeriod(theDate2);

            var lxLoop3 = claim2300Loop.AddLoop(new TypedLoopLX("LX"));
            lxLoop3.LX01_AssignedNumber = "3";

            var sv1Segment3 = lxLoop3.AddSegment(new TypedSegmentSV1());
            sv1Segment3.SV101_CompositeMedicalProcedure = "HC:99214";
            sv1Segment3.SV102_MonetaryAmount = "35";
            sv1Segment3.SV103_UnitBasisMeasCode = "UN";
            sv1Segment3.SV104_Quantity = "1";
            sv1Segment3.SV107_CompDiagCodePoint = "2";

            var dtpSegment3 = lxLoop3.AddSegment(new TypedSegmentDTP());
            dtpSegment3.DTP01_DateTimeQualifier = DTPQualifier.Service;
            dtpSegment3.DTP02_DateTimePeriodFormatQualifier = DTPFormatQualifier.CCYYMMDD;
            DateTime theDate3 = DateTime.ParseExact("20061010", "yyyyMMdd", null);
            dtpSegment3.DTP03_Date = new DateTimePeriod(theDate3);

            var lxLoop4 = claim2300Loop.AddLoop(new TypedLoopLX("LX"));
            lxLoop4.LX01_AssignedNumber = "4";

            var sv1Segment4 = lxLoop4.AddSegment(new TypedSegmentSV1());
            sv1Segment4.SV101_CompositeMedicalProcedure = "HC:86663";
            sv1Segment4.SV102_MonetaryAmount = "10";
            sv1Segment4.SV103_UnitBasisMeasCode = "UN";
            sv1Segment4.SV104_Quantity = "1";
            sv1Segment4.SV107_CompDiagCodePoint = "2";

            var dtpSegment4 = lxLoop4.AddSegment(new TypedSegmentDTP());
            dtpSegment4.DTP01_DateTimeQualifier = DTPQualifier.Service;
            dtpSegment4.DTP02_DateTimePeriodFormatQualifier = DTPFormatQualifier.CCYYMMDD_CCYYMMDD;
            DateTime theDate4 = DateTime.ParseExact("20061010", "yyyyMMdd", null);
            dtpSegment4.DTP03_Date = new DateTimePeriod(theDate4, DateTime.ParseExact("20061025", "yyyyMMdd", null));
            var x12 = message.SerializeToX12(true);
            Assert.AreEqual(new StreamReader(Extensions.GetEdi("INS._837P._5010.Example1_HealthInsurance.txt")).ReadToEnd(), message.SerializeToX12(true));

            //Trace.Write(new StreamReader(Extensions.GetEdi("INS._837P._5010.Example1_HealthInsurance.txt")).ReadToEnd());
            //Trace.Write(x12);
        }
    }
}
