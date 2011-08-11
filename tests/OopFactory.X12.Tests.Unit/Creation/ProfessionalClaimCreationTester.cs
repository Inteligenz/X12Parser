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
                                  InterchangeReceiverId = "RECEIVERS.ID"
                              };
            message.SetElement(12, "00501");
            var group = message.AddFunctionGroup("HC", DateTime.Now, 000905, "005010X222");
            group.ApplicationSendersCode = "901234572000";
            group.ApplicationReceiversCode = "908887732000";

            var transaction = group.AddTransaction("837", "0034");
            transaction.SetElement(2, "5010X837");
            var bhtSegment = transaction.AddSegment("BHT");

            var submitterLoop = transaction.AddLoop(new TypedLoopNM1("41")); //submitter identifier code
            submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            submitterLoop.NM103_NameLastOrOrganizationName = "My Submitter";
            submitterLoop.NM104_NameFirst = "First Name < 25 Chars";

            var perSegment = submitterLoop.AddSegment(new TypedSegmentPER());
            perSegment.PER01_ContactFunctionCode = "IC"; //information contact function code
            perSegment.PER02_Name = "My Contact";
            perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone;
            perSegment.PER04_CommunicationNumber = "18885551212";

            var provider2000AHLoop = transaction.AddHLoop("1", "20", true); //Information Source
            provider2000AHLoop.AddSegment("PRV"); //Specialty Segment
            var provider2010AALoop = provider2000AHLoop.AddLoop(new TypedLoopNM1("85"));  // changed this from PE to 85 because in the spec xml for the LoopId 2000A Starting Segment NM1 the EntityIdentifier value is 85
            provider2010AALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            provider2010AALoop.NM103_NameLastOrOrganizationName = "DOE";
            provider2010AALoop.NM104_NameFirst = "JOHN";

            var provider2010ACLoop = provider2000AHLoop.AddLoop(new TypedLoopNM1("85"));  // I think this is looking for the same element 85 as above
            provider2010ACLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            provider2010ACLoop.NM103_NameLastOrOrganizationName = "Pay-To Plan Name";
            var provider2010AC_N3Segment = provider2010ACLoop.AddSegment(new TypedSegmentN3());
            provider2010AC_N3Segment.N301_AddressInformation = "1234 Main St";

            var provider2010AC_N4Segment = provider2010ACLoop.AddSegment(new TypedSegmentN4());
            provider2010AC_N4Segment.N401_CityName = "Beverley Hills";
            provider2010AC_N4Segment.N402_StateOrProvinceCode = "CA";
            provider2010AC_N4Segment.N403_PostalCode = "90210";

            var subscriber2000BHLoop = provider2000AHLoop.AddHLoop("2", "22", false);
            var subscriberName2010BALoop = subscriber2000BHLoop.AddLoop(new TypedLoopNM1("IL"));
            var subscriber_DMGSegment = subscriberName2010BALoop.AddSegment(new TypedSegmentDMG());
            subscriber_DMGSegment.DMG01_DateTimePeriodFormatQualifier = "D8";
            subscriber_DMGSegment.DMG02_DateOfBirth = DateTime.Parse("3/3/2003");
            subscriber_DMGSegment.DMG03_Gender = Gender.Female;

            var claim2300Loop = subscriber2000BHLoop.AddLoop(new TypedLoopCLM());
            claim2300Loop.CLM01_PatientControlNumber = "1234567";
            claim2300Loop.CLM02_TotalClaimChargeAmount = Convert.ToDecimal(1234.56);
            claim2300Loop.CLM05._1_FacilityCodeValue = "11";
            claim2300Loop.CLM05._2_FacilityCodeQualifier = "B";
            claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = "1";
            claim2300Loop.CLM11._1_RelatedCausesCode = "AA";
            claim2300Loop.CLM11._2_RelatedCausesCode = "EM";
            claim2300Loop.CLM11._4_StateOrProvidenceCode = "TX";

            var hiSegment = claim2300Loop.AddSegment("HI");
            hiSegment.SetElement(12, "");

            var serviceFacility2310CLoop = claim2300Loop.AddLoop(new TypedLoopNM1("77"));
            serviceFacility2310CLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            serviceFacility2310CLoop.NM103_NameLastOrOrganizationName = "Service Facility Location";

            var per2310CSegment = serviceFacility2310CLoop.AddSegment(new TypedSegmentPER());
            per2310CSegment.PER02_Name = "Me";
            per2310CSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone;
            per2310CSegment.PER04_CommunicationNumber = "5555555555";

            var otherSubscriber2320Loop = claim2300Loop.AddLoop(new TypedLoopSBR());
            otherSubscriber2320Loop.SBR02_IndividualRelationshipCode = "18";

            var otherPayer2330BLoop = otherSubscriber2320Loop.AddLoop(new TypedLoopNM1("PR"));
            otherPayer2330BLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            otherPayer2330BLoop.NM103_NameLastOrOrganizationName = "Payer 2";
            
            var segmentN3_2330B = otherPayer2330BLoop.AddSegment(new TypedSegmentN3());
            segmentN3_2330B.N301_AddressInformation = "1234 Main St";
            segmentN3_2330B.N302_AddressInformation = "Suite 101";

            var segmentN4_2330B = otherPayer2330BLoop.AddSegment(new TypedSegmentN4());
            segmentN4_2330B.N401_CityName = "Austin";
            segmentN4_2330B.N402_StateOrProvinceCode = "TX";
            segmentN4_2330B.N403_PostalCode = "78701";
            


            var x12 = message.SerializeToX12(true);
            //Assert.AreEqual(new StreamReader(Extensions.GetEdi("INS._837P._5010.Example1_HealthInsurance.txt")).ReadToEnd(), message.SerializeToX12(true));
            Trace.Write(x12);
        }
    }
}
