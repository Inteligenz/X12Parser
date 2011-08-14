Imports System.Text
Imports OopFactory.X12.Parsing
Imports OopFactory.X12.Parsing.Model
Imports OopFactory.X12.Parsing.Model.Typed

<TestClass()>
Public Class UnitTest1

    <TestMethod()>
    Public Sub Create837_5010Version()
        Dim message As New Interchange(DateTime.Now, 1, True)
        message.InterchangeSenderIdQualifier = "ZZ"
        message.InterchangeSenderId = "9012345720000"
        message.InterchangeReceiverIdQualifier = "ZZ"
        message.InterchangeReceiverId = "9088877320000"
        message.SetElement(12, "00501")

        Dim group = message.AddFunctionGroup("HC", DateTime.Now, 999999, "005010X222")
        group.ApplicationSendersCode = "901234572000"
        group.ApplicationReceiversCode = "908887732000"

        Dim transaction = group.AddTransaction("837", "0034")
        transaction.SetElement(3, "005010X222")
        Dim bhtSegment = transaction.AddSegment("BHT")

        Dim submitterLoop = transaction.AddLoop(New TypedLoopNM1("41")) 'Submitter Identifer Code
        submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity
        submitterLoop.NM103_NameLastOrOrganizationName = "My Submitter"
        submitterLoop.NM104_NameFirst = "First Name That Is > 25 Chars"

        Dim perSegment = submitterLoop.AddSegment(New TypedSegmentPER())
        perSegment.PER01_ContactFunctionCode = "IC" 'Information Contact Function Code
        perSegment.PER02_Name = "My Contact"
        perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone
        perSegment.PER04_CommunicationNumber = "18005555555" '

        Dim provider2000AHLoop = transaction.AddHLoop(1, "20", True) 'Information Source
        provider2000AHLoop.AddSegment("PRV") 'Speciality Segment
        Dim provider2010AALoop = provider2000AHLoop.AddLoop(New TypedLoopNM1("85"))
        provider2010AALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person
        provider2010AALoop.NM103_NameLastOrOrganizationName = "Doe"
        provider2010AALoop.NM104_NameFirst = "John"

        Dim provider2010ACLoop = provider2000AHLoop.AddLoop(New TypedLoopNM1("PE"))
        provider2010ACLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity
        provider2010ACLoop.NM103_NameLastOrOrganizationName = "Pay-To Plan Name"

        Dim provider2010AC_N3Segment = provider2010ACLoop.AddSegment(New TypedSegmentN3())
        provider2010AC_N3Segment.N301_AddressInformation = "1234 Main St"

        Dim provider2010AC_N4Segment = provider2010ACLoop.AddSegment(New TypedSegmentN4())
        provider2010AC_N4Segment.N401_CityName = "Beverley Hills"
        provider2010AC_N4Segment.N402_StateOrProvinceCode = "CA"
        provider2010AC_N4Segment.N403_PostalCode = "90210"

        Dim subscriber2000BHLoop = provider2000AHLoop.AddHLoop(2, "22", False) '
        Dim subscriberName2010BALoop = subscriber2000BHLoop.AddLoop(New TypedLoopNM1("IL"))
        Dim subscriber_DMGSegment = subscriberName2010BALoop.AddSegment(New TypedSegmentDMG())
        subscriber_DMGSegment.DMG01_DateTimePeriodFormatQualifier = "D8"
        subscriber_DMGSegment.DMG02_DateOfBirth = DateTime.Parse("3/3/2003")
        subscriber_DMGSegment.DMG03_Gender = Gender.Female

        Dim claim2300Loop = subscriber2000BHLoop.AddLoop(New TypedLoopCLM())
        claim2300Loop.CLM01_PatientControlNumber = "1234567"
        claim2300Loop.CLM02_TotalClaimChargeAmount = 1234.56
        claim2300Loop.CLM05._1_FacilityCodeValue = "11"
        claim2300Loop.CLM05._2_FacilityCodeQualifier = "B"
        claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = "1"
        claim2300Loop.CLM11._1_RelatedCausesCode = "AA"
        claim2300Loop.CLM11._2_RelatedCausesCode = "EM"
        claim2300Loop.CLM11._4_StateOrProvidenceCode = "TX"

        Dim hiSegment = claim2300Loop.AddSegment("HI")
        hiSegment.SetElement(12, "")

        Dim serviceFacility2310CLoop = claim2300Loop.AddLoop(New TypedLoopNM1("77"))
        serviceFacility2310CLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity
        serviceFacility2310CLoop.NM103_NameLastOrOrganizationName = "Service Facility Location"

        Dim per2310CSegment = serviceFacility2310CLoop.AddSegment(New TypedSegmentPER())
        per2310CSegment.PER02_Name = "Me"
        per2310CSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone
        per2310CSegment.PER04_CommunicationNumber = "5555555555"

        Dim otherSubscriber2320Loop = claim2300Loop.AddLoop(New TypedLoopSBR())
        otherSubscriber2320Loop.SBR02_IndividualRelationshipCode = "18"

        Dim otherPayer2330BLoop = otherSubscriber2320Loop.AddLoop(New TypedLoopNM1("PR"))
        otherPayer2330BLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity
        otherPayer2330BLoop.NM103_NameLastOrOrganizationName = "Payer 2"
        Dim segmentN3_2330B = otherPayer2330BLoop.AddSegment(New TypedSegmentN3())
        segmentN3_2330B.N301_AddressInformation = "1234 Main St"
        segmentN3_2330B.N302_AddressInformation = "Suite 101"
        Dim segmentN4_2330B = otherPayer2330BLoop.AddSegment(New TypedSegmentN4())
        segmentN4_2330B.N401_CityName = "Austin"
        segmentN4_2330B.N402_StateOrProvinceCode = "TX"
        segmentN4_2330B.N403_PostalCode = "78701"


        Dim patient2000CHLoop = subscriber2000BHLoop.AddHLoop(3, "23", False)

        claim2300Loop = patient2000CHLoop.AddLoop(New TypedLoopCLM())
        claim2300Loop.CLM01_PatientControlNumber = "1234568"
        claim2300Loop.CLM02_TotalClaimChargeAmount = 1234.56
        claim2300Loop.CLM05._1_FacilityCodeValue = "11"
        claim2300Loop.CLM05._2_FacilityCodeQualifier = "B"
        claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = "1"
        claim2300Loop.CLM11._1_RelatedCausesCode = "AA"
        claim2300Loop.CLM11._2_RelatedCausesCode = "EM"
        claim2300Loop.CLM11._4_StateOrProvidenceCode = "TX"

        Dim purchaseServiceProviderName2310CLoop = claim2300Loop.AddLoop(New TypedLoopNM1("77"))
        purchaseServiceProviderName2310CLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity

        per2310CSegment = purchaseServiceProviderName2310CLoop.AddSegment(New TypedSegmentPER())
        per2310CSegment.PER02_Name = "Service Provider Contact"

        Dim sbr = claim2300Loop.AddLoop(New TypedLoopSBR())
        Dim nm1 = sbr.AddLoop(New TypedLoopNM1("PR"))
        nm1.NM102_EntityTypeQualifier = EntityTypeQualifier.Person

        Dim n3 = nm1.AddSegment(New TypedSegmentN3())
        n3.N301_AddressInformation = "123 Main St"

        Dim n4 = nm1.AddSegment(New TypedSegmentN4())
        n4.N401_CityName = "Austin"



        Dim x12 = message.SerializeToX12(True)

        System.Diagnostics.Trace.Write(x12)

    End Sub

End Class
