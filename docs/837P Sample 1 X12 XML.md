## 837 Professional Claim, Sample 1 X12 XML
```xml
<?xml version="1.0"?>
<Interchange>
  <ISA>
    <!--Author Information Qualifier-->
    <ISA01>00<!--No Authorization Information Present--></ISA01>
    <!--Author Information-->
    <ISA02>          </ISA02>
    <!--Security Information Qualifer-->
    <ISA03>01<!--Password--></ISA03>
    <!--Security Information-->
    <ISA04>SECRET    </ISA04>
    <!--Interchange ID Qualifier-->
    <ISA05>ZZ<!--Mutually Defined--></ISA05>
    <!--Interchange Sender ID-->
    <ISA06>SUBMITTERS.ID  </ISA06>
    <!--Interchange ID Qualifier-->
    <ISA07>ZZ<!--Mutually Defined--></ISA07>
    <!--Interchange Receiver ID-->
    <ISA08>RECEIVERS.ID   </ISA08>
    <!--Interchange Date-->
    <ISA09>030101</ISA09>
    <!--Interchange Time-->
    <ISA10>1253</ISA10>
    <!--Inter Control Standards Identifier-->
    <ISA11>^</ISA11>
    <!--Inter Control Version Number-->
    <ISA12>00501</ISA12>
    <!--Inter Control Number-->
    <ISA13>000000905</ISA13>
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
      <GS01>HC<!--Health Care--></GS01>
      <!--Application Sender's Code-->
      <GS02>SENDER CODE</GS02>
      <!--Application Receiver's Code-->
      <GS03>RECEIVER CODE</GS03>
      <!--Date-->
      <GS04>19991231</GS04>
      <!--Time-->
      <GS05>0802</GS05>
      <!--Group Control Number-->
      <GS06>1</GS06>
      <!--Responsible Agency Code-->
      <GS07>X<!--Accredited Standards Committee X12--></GS07>
      <!--Version/Release/Industry Identifier Code-->
      <GS08>005010X222</GS08>
    </GS>
    <Transaction ControlNumber="0021">
      <ST>
        <!--Transaction Set Identifier Code-->
        <ST01>837<!--Health Care Claim--></ST01>
        <!--Transaction Set Control Number-->
        <ST02>0021</ST02>
        <ST03>005010X222</ST03>
      </ST>
      <BHT>
        <!--Hierarchical Structure Code-->
        <BHT01>0019</BHT01>
        <!--Transaction Set Purpose Code-->
        <BHT02>00</BHT02>
        <!--Reference Identification-->
        <BHT03>244579</BHT03>
        <!--Date-->
        <BHT04>20061015</BHT04>
        <!--Time-->
        <BHT05>1023</BHT05>
        <!--Transaction Type Code-->
        <BHT06>CH</BHT06>
      </BHT>
      <Loop LoopId="1000A" Name="SUBMITTER NAME">
        <NM1>
          <!--Entity Identifier Code-->
          <NM101>41</NM101>
          <!--Entity Type Qualifier-->
          <NM102>2<!--Non-Person Entity--></NM102>
          <!--Name Last or Organization Name-->
          <NM103>PREMIER BILLING SERVICE</NM103>
          <NM104 />
          <NM105 />
          <NM106 />
          <NM107 />
          <!--Identification Code Qualifier-->
          <NM108>46<!--Electronic Transmitter Identification Number (ETIN--></NM108>
          <!--Identification Code-->
          <NM109>TGJ23</NM109>
        </NM1>
        <PER>
          <!--Contact Function Code-->
          <PER01>IC</PER01>
          <!--Name-->
          <PER02>JERRY</PER02>
          <!--Communication Number Qualifier-->
          <PER03>TE<!--Telephone--></PER03>
          <!--Communication Number-->
          <PER04>3055552222</PER04>
          <!--Communication Number Qualifier-->
          <PER05>EX<!--Telephone Extension--></PER05>
          <!--Communication Number-->
          <PER06>231</PER06>
        </PER>
      </Loop>
      <Loop LoopId="1000B" Name="RECEIVER NAME">
        <NM1>
          <!--Entity Identifier Code-->
          <NM101>40</NM101>
          <!--Entity Type Qualifier-->
          <NM102>2<!--Non-Person Entity--></NM102>
          <!--Name Last or Organization Name-->
          <NM103>KEY INSURANCE COMPANY</NM103>
          <NM104 />
          <NM105 />
          <NM106 />
          <NM107 />
          <!--Identification Code Qualifier-->
          <NM108>46<!--Electronic Transmitter Identification Number (ETIN--></NM108>
          <!--Identification Code-->
          <NM109>66783JJT</NM109>
        </NM1>
      </Loop>
      <HierarchicalLoop LoopId="2000A" LoopName="BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL" Id="1" ParentId="">
        <HL>
          <!--Hierarchical ID Number-->
          <HL01>1</HL01>
          <HL02 />
          <!--Level Code-->
          <HL03>20<!--Information Source--></HL03>
          <!--Hierarchical Child Code-->
          <HL04>1<!--Additional Subordinate HL Data Segment in This Hierarchical Structure--></HL04>
        </HL>
        <PRV>
          <PRV01>BI</PRV01>
          <PRV02>PXC</PRV02>
          <PRV03>203BF0100Y</PRV03>
        </PRV>
        <Loop LoopId="2010AA" Name="BILLING PROVIDER NAME">
          <NM1>
            <!--Entity Identifier Code-->
            <NM101>85</NM101>
            <!--Entity Type Qualifier-->
            <NM102>2<!--Non-Person Entity--></NM102>
            <!--Name Last or Organization Name-->
            <NM103>BEN KILDARE SERVICE</NM103>
            <NM104 />
            <NM105 />
            <NM106 />
            <NM107 />
            <!--Identification Code Qualifier-->
            <NM108>XX<!--Centers for Medicare and Medicaid Services Nationa--></NM108>
            <!--Identification Code-->
            <NM109>9876543210</NM109>
          </NM1>
          <N3>
            <!--Address Information-->
            <N301>234 SEAWAY ST</N301>
          </N3>
          <N4>
            <!--City Name-->
            <N401>MIAMI</N401>
            <!--State or Provice Code-->
            <N402>FL</N402>
            <!--Postal Code-->
            <N403>33111</N403>
          </N4>
          <REF>
            <!--Reference Identification Qualifier-->
            <REF01>EI</REF01>
            <!--Reference Identification-->
            <REF02>587654321</REF02>
          </REF>
        </Loop>
        <Loop LoopId="2010AB" Name="PAY-TO ADDRESS NAME">
          <NM1>
            <!--Entity Identifier Code-->
            <NM101>87</NM101>
            <!--Entity Type Qualifier-->
            <NM102>2<!--Non-Person Entity--></NM102>
          </NM1>
          <N3>
            <!--Address Information-->
            <N301>2345 OCEAN BLVD</N301>
          </N3>
          <N4>
            <!--City Name-->
            <N401>MAIMI</N401>
            <!--State or Provice Code-->
            <N402>FL</N402>
            <!--Postal Code-->
            <N403>33111</N403>
          </N4>
        </Loop>
        <HierarchicalLoop LoopId="2000B" LoopName="SUBSCRIBER HIERARCHICAL LOOP" Id="2" ParentId="1">
          <HL>
            <!--Hierarchical ID Number-->
            <HL01>2</HL01>
            <!--Hierarchical Parent ID Number-->
            <HL02>1</HL02>
            <!--Level Code-->
            <HL03>22<!--Subscriber--></HL03>
            <!--Hierarchical Child Code-->
            <HL04>1<!--Additional Subordinate HL Data Segment in This Hierarchical Structure--></HL04>
          </HL>
          <SBR>
            <SBR01>P</SBR01>
            <SBR02 />
            <SBR03>2222-SJ</SBR03>
            <SBR04 />
            <SBR05 />
            <SBR06 />
            <SBR07 />
            <SBR08 />
            <SBR09>CI</SBR09>
          </SBR>
          <Loop LoopId="2010BA" Name="SUBSCRIBER NAME">
            <NM1>
              <!--Entity Identifier Code-->
              <NM101>IL<!--Insured or Subscriber--></NM101>
              <!--Entity Type Qualifier-->
              <NM102>1<!--Person--></NM102>
              <!--Name Last or Organization Name-->
              <NM103>SMITH</NM103>
              <!--Name First-->
              <NM104>JANE</NM104>
              <NM105 />
              <NM106 />
              <NM107 />
              <!--Identification Code Qualifier-->
              <NM108>MI</NM108>
              <!--Identification Code-->
              <NM109>JS00111223333</NM109>
            </NM1>
            <DMG>
              <!--Date Time Period Format Qualifier-->
              <DMG01>D8<!--Date Expressed in Format CCYYMMDD--></DMG01>
              <!--Date Time Period-->
              <DMG02>19430501</DMG02>
              <!--Gender Code-->
              <DMG03>F<!--Female--></DMG03>
            </DMG>
          </Loop>
          <Loop LoopId="2010BC" Name="PAYER NAME">
            <NM1>
              <!--Entity Identifier Code-->
              <NM101>PR<!--Payer--></NM101>
              <!--Entity Type Qualifier-->
              <NM102>2<!--Non-Person Entity--></NM102>
              <!--Name Last or Organization Name-->
              <NM103>KEY INSURANCE COMPANY</NM103>
              <NM104 />
              <NM105 />
              <NM106 />
              <NM107 />
              <!--Identification Code Qualifier-->
              <NM108>PI<!--Payor Identification--></NM108>
              <!--Identification Code-->
              <NM109>999996666</NM109>
            </NM1>
            <REF>
              <!--Reference Identification Qualifier-->
              <REF01>G2</REF01>
              <!--Reference Identification-->
              <REF02>KA6663</REF02>
            </REF>
          </Loop>
          <HierarchicalLoop LoopId="2000C" LoopName="PATIENT HIERARCHICAL LOOP" Id="3" ParentId="2">
            <HL>
              <!--Hierarchical ID Number-->
              <HL01>3</HL01>
              <!--Hierarchical Parent ID Number-->
              <HL02>2</HL02>
              <!--Level Code-->
              <HL03>23<!--Dependent--></HL03>
              <!--Hierarchical Child Code-->
              <HL04>0<!--No Subordinate HL Segment in This Hiearchical Structure--></HL04>
            </HL>
            <PAT>
              <PAT01>19</PAT01>
            </PAT>
            <Loop LoopId="2010CA" Name="PATIENT NAME">
              <NM1>
                <!--Entity Identifier Code-->
                <NM101>QC</NM101>
                <!--Entity Type Qualifier-->
                <NM102>1<!--Person--></NM102>
                <!--Name Last or Organization Name-->
                <NM103>SMITH</NM103>
                <!--Name First-->
                <NM104>TED</NM104>
              </NM1>
              <N3>
                <!--Address Information-->
                <N301>236 N MAIN ST</N301>
              </N3>
              <N4>
                <!--City Name-->
                <N401>MIAMI</N401>
                <!--State or Provice Code-->
                <N402>FL</N402>
                <!--Postal Code-->
                <N403>33413</N403>
              </N4>
              <DMG>
                <!--Date Time Period Format Qualifier-->
                <DMG01>D8<!--Date Expressed in Format CCYYMMDD--></DMG01>
                <!--Date Time Period-->
                <DMG02>19730501</DMG02>
                <!--Gender Code-->
                <DMG03>M<!--Male--></DMG03>
              </DMG>
            </Loop>
            <Loop LoopId="2300" Name="CLAIM INFORMATION">
              <CLM>
                <CLM01>26463774</CLM01>
                <CLM02>100</CLM02>
                <CLM03 />
                <CLM04 />
                <CLM05>
                  <CLM0501>11</CLM0501>
                  <CLM0502>B</CLM0502>
                  <CLM0503>1</CLM0503>
                </CLM05>
                <CLM06>Y</CLM06>
                <CLM07>A</CLM07>
                <CLM08>Y</CLM08>
                <CLM09>I</CLM09>
              </CLM>
              <REF>
                <!--Reference Identification Qualifier-->
                <REF01>D9</REF01>
                <!--Reference Identification-->
                <REF02>17312345600006351</REF02>
              </REF>
              <HI>
                <HI01>
                  <HI0101>BK</HI0101>
                  <HI0102>0340</HI0102>
                </HI01>
                <HI02>
                  <HI0201>BF</HI0201>
                  <HI0202>V7389</HI0202>
                </HI02>
              </HI>
              <Loop LoopId="2400" Name="LINE COUNTER">
                <LX>
                  <LX01>1</LX01>
                </LX>
                <SV1>
                  <SV101>
                    <SV10101>HC</SV10101>
                    <SV10102>99213</SV10102>
                  </SV101>
                  <SV102>40</SV102>
                  <SV103>UN</SV103>
                  <SV104>1</SV104>
                  <SV105 />
                  <SV106 />
                  <SV107>1</SV107>
                </SV1>
                <DTP>
                  <!--Data/Time Qualifier-->
                  <DTP01>472</DTP01>
                  <!--Date Time Period Format Qualifier-->
                  <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
                  <!--Date Time Period-->
                  <DTP03>20061003</DTP03>
                </DTP>
              </Loop>
              <Loop LoopId="2400" Name="LINE COUNTER">
                <LX>
                  <LX01>2</LX01>
                </LX>
                <SV1>
                  <SV101>
                    <SV10101>HC</SV10101>
                    <SV10102>87070</SV10102>
                  </SV101>
                  <SV102>15</SV102>
                  <SV103>UN</SV103>
                  <SV104>1</SV104>
                  <SV105 />
                  <SV106 />
                  <SV107>1</SV107>
                </SV1>
                <DTP>
                  <!--Data/Time Qualifier-->
                  <DTP01>472</DTP01>
                  <!--Date Time Period Format Qualifier-->
                  <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
                  <!--Date Time Period-->
                  <DTP03>20061003</DTP03>
                </DTP>
              </Loop>
              <Loop LoopId="2400" Name="LINE COUNTER">
                <LX>
                  <LX01>3</LX01>
                </LX>
                <SV1>
                  <SV101>
                    <SV10101>HC</SV10101>
                    <SV10102>99214</SV10102>
                  </SV101>
                  <SV102>35</SV102>
                  <SV103>UN</SV103>
                  <SV104>1</SV104>
                  <SV105 />
                  <SV106 />
                  <SV107>2</SV107>
                </SV1>
                <DTP>
                  <!--Data/Time Qualifier-->
                  <DTP01>472</DTP01>
                  <!--Date Time Period Format Qualifier-->
                  <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
                  <!--Date Time Period-->
                  <DTP03>20061010</DTP03>
                </DTP>
              </Loop>
              <Loop LoopId="2400" Name="LINE COUNTER">
                <LX>
                  <LX01>4</LX01>
                </LX>
                <SV1>
                  <SV101>
                    <SV10101>HC</SV10101>
                    <SV10102>86663</SV10102>
                  </SV101>
                  <SV102>10</SV102>
                  <SV103>UN</SV103>
                  <SV104>1</SV104>
                  <SV105 />
                  <SV106 />
                  <SV107>2</SV107>
                </SV1>
                <DTP>
                  <!--Data/Time Qualifier-->
                  <DTP01>472</DTP01>
                  <!--Date Time Period Format Qualifier-->
                  <DTP02>D8<!--Date Expression in Format CCYYMMDD--></DTP02>
                  <!--Date Time Period-->
                  <DTP03>20061010</DTP03>
                </DTP>
              </Loop>
            </Loop>
          </HierarchicalLoop>
        </HierarchicalLoop>
      </HierarchicalLoop>
      <SE>
        <!--Number of Included Segments-->
        <SE01>42</SE01>
        <!--Transaction Set Control Number-->
        <SE02>0021</SE02>
      </SE>
    </Transaction>
    <GE>
      <!--Number of Transaction Sets Included-->
      <GE01>1</GE01>
      <!--Group Control Number-->
      <GE02>1</GE02>
    </GE>
  </FunctionGroup>
  <IEA>
    <!--Number of Included Functional Groups-->
    <IEA01>1</IEA01>
    <!--Interchange Control Number-->
    <IEA02>000000905</IEA02>
  </IEA>
</Interchange>
```