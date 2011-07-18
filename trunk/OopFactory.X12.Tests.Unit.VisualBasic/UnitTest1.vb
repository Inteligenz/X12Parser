Imports System.Text
Imports OopFactory.X12.Parsing
Imports OopFactory.X12.Parsing.Model

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
        Dim bhtSegment = transaction.AddSegment("BHT")

        Dim submitterLoop = transaction.AddLoop("NM1*41") 'Submitter Identifer Code
        submitterLoop.SetElement(2, "2") 'Non-Person Entity
        submitterLoop.SetElement(3, "My Submitter") 'Organization Name

        Dim perSegment = submitterLoop.AddSegment("PER")
        perSegment.SetElement(1, "IC") 'Information Contact Function Code
        perSegment.SetElement(2, "My Contact") 'Name
        perSegment.SetElement(3, "TE") 'Telephone Qualifier
        perSegment.SetElement(4, "18005555555") 'Communication Number

        Dim providerHLoop = transaction.AddHLoop(1, "20", True) 'Information Source
        providerHLoop.AddSegment("PRV") 'Speciality Segment
        Dim providerNameLoop = providerHLoop.AddLoop("NM1*85")
        providerNameLoop.SetElement(2, "1") 'Person Entity
        providerNameLoop.SetElement(3, "Doe") 'Last Name
        providerNameLoop.SetElement(4, "John") 'First Name


        Dim x12 = message.SerializeToX12(True)

        System.Diagnostics.Trace.Write(x12)

    End Sub

End Class
