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


        Dim x12 = message.SerializeToX12(True)

        System.Diagnostics.Trace.Write(x12)

    End Sub

End Class
