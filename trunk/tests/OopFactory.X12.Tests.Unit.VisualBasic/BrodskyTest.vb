Imports System.Text
Imports OopFactory.X12.Parsing
Imports OopFactory.X12.Parsing.Model
Imports OopFactory.X12.Parsing.Model.Typed

<TestClass()>
Public Class BrodskyTest

    <TestMethod()>
    Public Sub TestMethod1()
        Dim message As New Interchange(Today, 1, True)
        Dim group = message.AddFunctionGroup("HC", Today, 999999, "005010X222")
        Dim transaction = group.AddTransaction("837", "0034")
        Dim HL_2000C = transaction.AddHLoop("9999", "23", True)
        Dim CLM_2300 = HL_2000C.AddLoop(New TypedLoopCLM)
        Dim NM1_2310C = CLM_2300.AddLoop(New TypedLoopNM1("77"))

        Dim PER_2310C = NM1_2310C.AddSegment(New TypedSegmentPER)       '<<< error: PER_2310C = Nothing

        '-----------------------------------------------------------------------------------------------
        Dim SBR_2320 = CLM_2300.AddLoop(New TypedLoopSBR)
        Dim NM1_2330B = SBR_2320.AddLoop(New TypedLoopNM1("PR"))

        Dim N3_2330B = NM1_2330B.AddSegment(New TypedSegmentN3)         '<<< error: N3_2330B = Nothing
        Dim N4_2330B = NM1_2330B.AddSegment(New TypedSegmentN4)         '<<< error: N4_2330B = Nothing
    End Sub

End Class
