# Parsing an 834 Transaction
An 834 is a Benefit Enrollment transaction.  This starts the health care process be specifying the coverage of each subscriber and her dependents.
The following example will show you how to:
# Take an 834 file and create X12 XML that shows the hieararchical relationships of the X12 segments with comments related to their values.
# Take an 834 file and format it as X12 to reveal it's heirarchical relationship.

A sample 834 file that looks like this:

```csharp
ISA**00** **00** **01**9012345720000 **01**9088877320000 **020816**1144**U**00401**000000031**1**T**:~
GS**BE**901234572000**908887732000**20070816**1615**31**X**004010X096A1~
ST**834**12345~
BGN**00**123456**19980502**1200********2~
N1**P5****FI**999888777~
N1**IN****FI**654456654~
INS**Y**18**021**20**A******FT~
REF**0F**123456789~
REF**1L**123456001~
DTP**356**D8**19960523~
NM1**IL**1**DOE**JOHN**P******34**123456789~
PER**IP****HP**7172343334**WP**7172341240~
N3**100 MARKET ST**APT 3G~
N4**CAMP HILL**PA**17011****CY**CUMBERLAND~
DMG**D8**19400816**M~
HD**021****HLT~
DTP**348**D8**19960601~
COB**P**890111**5~
N1**IN**ABC INSURANCE CO~
HD**021****DEN~
DTP**348**D8**19960601~
HD**021****VIS~
DTP**348**D8**19960601~
SE**22**12345~
GE**1**31~
IEA**1**000000031~
{code:c#}

This can be parsed with the following lines of C# code:
{code:c#}
using System;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

public class Program
{
   FileStream fstream = new FileStream("Sample1.txt", FileMode.Open, FileAccess.Read);
   var parser = new X12Parser();
   Interchange interchange = parser.Parse(fstream);
   string xml = interchange.Serialize();
}
{code:c#}

The resulting output xml will look like this:

{code:xml}
<?xml version="1.0"?>
<Interchange>
  <ISA>
    <!--Author Information Qualifier-->
    <ISA01>00<!--No Authorization Information Present--></ISA01>
    <!--Author Information-->
    <ISA02>          </ISA02>
    <!--Security Information Qualifer-->
    <ISA03>00<!--No Security Information Present--></ISA03>
    <!--Security Information-->
    <ISA04>          </ISA04>
    <!--Interchange ID Qualifier-->
    <ISA05>01<!--Duns (Dun & Bradstreet)--></ISA05>
    <!--Interchange Sender ID-->
    <ISA06>9012345720000  </ISA06>
    <!--Interchange ID Qualifier-->
    <ISA07>01<!--Duns (Dun & Bradstreet)--></ISA07>
    <!--Interchange Receiver ID-->
    <ISA08>9088877320000  </ISA08>
    <!--Interchange Date-->
    <ISA09>020816</ISA09>
    <!--Interchange Time-->
    <ISA10>1144</ISA10>
    <!--Inter Control Standards Identifier-->
    <ISA11>U<!--U.S. EDI Community of ASC X12, TDCC, and UCS--></ISA11>
    <!--Inter Control Version Number-->
    <ISA12>00401</ISA12>
    <!--Inter Control Number-->
    <ISA13>000000031</ISA13>
    <!--Acknowlegment Requested-->
    <ISA14>1<!--Interchange Acknowledgment Requested--></ISA14>
    <!--Usage Indicator-->
    <ISA15>T<!--Test Data--></ISA15>
    <!--Component Element Separator-->
    <ISA16>
      <ISA1601 />
      <ISA1602 />
    </ISA16>
  </ISA>
  <FunctionGroup>
    <GS>
      <!--Functional Identifier Code-->
      <GS01>BE<!--Benefit Enrollment--></GS01>
      <!--Application Sender's Code-->
      <GS02>901234572000</GS02>
      <!--Application Receiver's Code-->
      <GS03>908887732000</GS03>
      <!--Date-->
      <GS04>20070816</GS04>
      <!--Time-->
      <GS05>1615</GS05>
      <!--Group Control Number-->
      <GS06>31</GS06>
      <!--Responsible Agency Code-->
      <GS07>X<!--Accredited Standards Committee X12--></GS07>
      <!--Version/Release/Industry Identifier Code-->
      <GS08>004010X096A1</GS08>
    </GS>
    <Transaction ControlNumber="12345">
      <ST>
        <!--Transaction Set Identifier Code-->
        <ST01>834</ST01>
        <!--Transaction Set Control Number-->
        <ST02>12345</ST02>
      </ST>
      ... see [834 Sample 1 X12 XML](834-Sample-1-X12-XML)
      <SE>
        <SE01>22</SE01>
        <SE02>12345</SE02>
      </SE>
    </Transaction>
    <GE>
      <!--Number of Transaction Sets Included-->
      <GE01>1</GE01>
      <!--Group Control Number-->
      <GE02>31</GE02>
    </GE>
  </FunctionGroup>
  <IEA>
    <!--Number of Included Functional Groups-->
    <IEA01>1</IEA01>
    <!--Interchange Control Number-->
    <IEA02>000000031</IEA02>
  </IEA>
</Interchange>
{code:xml}
See full output at [834 Sample 1 X12 XML](834-Sample-1-X12-XML)

In some cases you may only want to be able to see the hierarchy in the X12 without the need for it to be xml.  You can use the following code snippet to add whitespace to the stream:
{code:c#}
FileStream fstream = new FileStream("Sample1.txt", FileOpen.Open, FileAccess.Read);
var parser = new X12Parser();
Interchange interchange = parser.Parse(fstream);
string x12 = interchange.SerializeToX12(true);
{code:c#}

This will produce the following output:
{code:c#}
ISA**00** **00** **01**9012345720000 **01**9088877320000 **020816**1144**U**00401**000000031**1**T**:~
  GS**BE**901234572000**908887732000**20070816**1615**31**X**004010X096A1~
    ST**834**12345~
      BGN**00**123456**19980502**1200********2~
      N1**P5****FI**999888777~
      N1**IN****FI**654456654~
      INS**Y**18**021**20**A******FT~
        REF**0F**123456789~
        REF**1L**123456001~
        DTP**356**D8**19960523~
        NM1**IL**1**DOE**JOHN**P******34**123456789~
          PER**IP****HP**7172343334**WP**7172341240~
          N3**100 MARKET ST**APT 3G~
          N4**CAMP HILL**PA**17011****CY**CUMBERLAND~
          DMG**D8**19400816**M~
        HD**021****HLT~
          DTP**348**D8**19960601~
          COB**P**890111**5~
            N1**IN**ABC INSURANCE CO~
        HD**021****DEN~
          DTP**348**D8**19960601~
        HD**021****VIS~
          DTP**348**D8**19960601~
    SE**22**12345~
  GE**1**31~
IEA**1**000000031~
```