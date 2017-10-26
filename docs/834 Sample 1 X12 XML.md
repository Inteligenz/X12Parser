## Sample 1 output serialized to XML

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
      <BGN>
        <!--Transaction Set Purpose Code-->
        <BGN01>00<!--Original--></BGN01>
        <!--Reference Identification-->
        <BGN02>123456</BGN02>
        <!--Date-->
        <BGN03>19980502</BGN03>
        <!--Time-->
        <BGN04>1200</BGN04>
        <BGN05 />
        <BGN06 />
        <BGN07 />
        <!--Action Code-->
        <BGN08>2<!--Change (Update)--></BGN08>
      </BGN>
      <Loop LoopId="1000A" Name="SPONSOR NAME">
        <N1>
          <!--Entity Identifier Code-->
          <N101>P5<!--Plan Sponsor--></N101>
          <N102 />
          <!--Identification Code Qualifier-->
          <N103>FI<!--Federal Taxpayer's Identification Number--></N103>
          <!--Identification Code-->
          <N104>999888777</N104>
        </N1>
      </Loop>
      <Loop LoopId="1000B" Name="PAYER">
        <N1>
          <!--Entity Identifier Code-->
          <N101>IN<!--Insurer--></N101>
          <N102 />
          <!--Identification Code Qualifier-->
          <N103>FI<!--Federal Taxpayer's Identification Number--></N103>
          <!--Identification Code-->
          <N104>654456654</N104>
        </N1>
      </Loop>
      <Loop LoopId="2000" Name="MEMBER LEVEL DETAIL">
        <INS>
          <!--Insured Indicator-->
          <INS01>Y<!--Yes--></INS01>
          <!--Individual Relationship Code-->
          <INS02>18<!--Self--></INS02>
          <!--Maintenance Type Code-->
          <INS03>021<!--Addition--></INS03>
          <!--Maintenance Reason Code-->
          <INS04>20<!--Active--></INS04>
          <!--Benefit Status Code-->
          <INS05>A<!--Active--></INS05>
          <INS06 />
          <INS07 />
          <!--Employment Status Code-->
          <INS08>FT<!--Full-time active employee--></INS08>
        </INS>
        <REF>
          <!--Reference Identification Qualifier-->
          <REF01>0F<!--Subscriber Number--></REF01>
          <!--Reference Identification-->
          <REF02>123456789</REF02>
        </REF>
        <REF>
          <!--Reference Identification Qualifier-->
          <REF01>1L<!--Group or Policy Number--></REF01>
          <!--Reference Identification-->
          <REF02>123456001</REF02>
        </REF>
        <DTP>
          <!--Data/Time Qualifier-->
          <DTP01>356<!--Eligibility Begin--></DTP01>
          <!--Date Time Period Format Qualifier-->
          <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
          <!--Date Time Period-->
          <DTP03>19960523</DTP03>
        </DTP>
        <Loop LoopId="2100A" Name="MEMBER NAME">
          <NM1>
            <!--Entity Identifier Code-->
            <NM101>IL<!--Insured or Subscriber--></NM101>
            <!--Entity Type Qualifier-->
            <NM102>1<!--Person--></NM102>
            <!--Name Last or Organization Name-->
            <NM103>DOE</NM103>
            <!--Name First-->
            <NM104>JOHN</NM104>
            <!--Name Middle-->
            <NM105>P</NM105>
            <NM106 />
            <NM107 />
            <!--Identification Code Qualifier-->
            <NM108>34<!--Social Security Number--></NM108>
            <!--Identification Code-->
            <NM109>123456789</NM109>
          </NM1>
          <PER>
            <!--Contact Function Code-->
            <PER01>IP<!--Insured Party--></PER01>
            <PER02 />
            <!--Communication Number Qualifier-->
            <PER03>HP<!--Home Phone Number--></PER03>
            <!--Communication Number-->
            <PER04>7172343334</PER04>
            <!--Communication Number Qualifier-->
            <PER05>WP<!--Work Phone Number--></PER05>
            <!--Communication Number-->
            <PER06>7172341240</PER06>
          </PER>
          <N3>
            <!--Address Information-->
            <N301>100 MARKET ST</N301>
            <!--Address Information-->
            <N302>APT 3G</N302>
          </N3>
          <N4>
            <!--City Name-->
            <N401>CAMP HILL</N401>
            <!--State or Provice Code-->
            <N402>PA</N402>
            <!--Postal Code-->
            <N403>17011</N403>
            <N404 />
            <!--Location Qualifier-->
            <N405>CY<!--County/Parish--></N405>
            <!--Location Identifier-->
            <N406>CUMBERLAND</N406>
          </N4>
          <DMG>
            <!--Date Time Period Format Qualifier-->
            <DMG01>D8<!--Date Expressed in Format CCYYMMDD--></DMG01>
            <!--Date Time Period-->
            <DMG02>19400816</DMG02>
            <!--Gender Code-->
            <DMG03>M<!--Male--></DMG03>
          </DMG>
        </Loop>
        <Loop LoopId="2300" Name="HEALTH COVERAGE">
          <HD>
            <!--Maintenance Type Code-->
            <HD01>021<!--Addition--></HD01>
            <HD02 />
            <!--Insurance Line Code-->
            <HD03>HLT<!--Health--></HD03>
          </HD>
          <DTP>
            <!--Data/Time Qualifier-->
            <DTP01>348</DTP01>
            <!--Date Time Period Format Qualifier-->
            <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
            <!--Date Time Period-->
            <DTP03>19960601</DTP03>
          </DTP>
          <Loop LoopId="2320" Name="COORDINATION OF BENEFITS">
            <COB>
              <!--Payer Responsibility Sequence Number Code-->
              <COB01>P<!--Primary--></COB01>
              <!--Reference Identification-->
              <COB02>890111</COB02>
              <!--Coordination of Benefits Code-->
              <COB03>5<!--Unknown--></COB03>
            </COB>
            <N1>
              <!--Entity Identifier Code-->
              <N101>IN<!--Insurer--></N101>
              <!--Name-->
              <N102>ABC INSURANCE CO</N102>
            </N1>
          </Loop>
        </Loop>
        <Loop LoopId="2300" Name="HEALTH COVERAGE">
          <HD>
            <!--Maintenance Type Code-->
            <HD01>021<!--Addition--></HD01>
            <HD02 />
            <!--Insurance Line Code-->
            <HD03>DEN<!--Dental--></HD03>
          </HD>
          <DTP>
            <!--Data/Time Qualifier-->
            <DTP01>348</DTP01>
            <!--Date Time Period Format Qualifier-->
            <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
            <!--Date Time Period-->
            <DTP03>19960601</DTP03>
          </DTP>
        </Loop>
        <Loop LoopId="2300" Name="HEALTH COVERAGE">
          <HD>
            <!--Maintenance Type Code-->
            <HD01>021<!--Addition--></HD01>
            <HD02 />
            <!--Insurance Line Code-->
            <HD03>VIS<!--Vision--></HD03>
          </HD>
          <DTP>
            <!--Data/Time Qualifier-->
            <DTP01>348</DTP01>
            <!--Date Time Period Format Qualifier-->
            <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
            <!--Date Time Period-->
            <DTP03>19960601</DTP03>
          </DTP>
        </Loop>
      </Loop>
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
