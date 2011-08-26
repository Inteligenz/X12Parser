<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:apply-templates select="@* | node()"/>
    </xsl:template>
  <!-- INTERCHANGE -->
  <!--
  <xsl:template match="Interchange">
    <ArrayOfUB04Claim>
      <xsl:apply-templates select="@* | node()"/>
    </ArrayOfUB04Claim>
  </xsl:template>
-->

  <!-- 1000A SUBMITTER NAME LOOP -->
  <xsl:template name="SubmitterNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- Last name or organization name -->
  </xsl:template>

  <!-- 2000A BILLING PROVIDER HIERARCHICAL LEVEL -->
  <xsl:template name="BillingProviderHierarchicalLoop">
    <xsl:param name="Loop"></xsl:param>
    <xsl:if test="(PRV01 = 'BI') and (PRV02 = 'PXC')">
      <Field81_CodeCode_ProviderTaxonomyCode>
        <xsl:value-of select="$Loop/PRV/PRV03"/>
      </Field81_CodeCode_ProviderTaxonomyCode>
    </xsl:if>
  </xsl:template>

  <!-- 2000B SUBSCRIBER HIERARCHICAL LOOP -->
  <xsl:template name="SubscriberHierarchicalLoop">
    <xsl:param name="Loop"></xsl:param>
    <Field59a_RelationshipToInsured>
      <xsl:value-of select="$Loop/SBR/SBR02"/>
    </Field59a_RelationshipToInsured>
    <Field61a_InsuredsGroupOrPlanName>
      <xsl:value-of select="$Loop/SBR/SBR04"/>
    </Field61a_InsuredsGroupOrPlanName>
    <Field62a_InsuredsGroupOrPlanNumber>
      <xsl:value-of select="$Loop/SBR/SBR03"/>
    </Field62a_InsuredsGroupOrPlanNumber>
  </xsl:template>


  <!-- 2000C SUBSCRIBER HIERARCHICAL LOOP -->
  <xsl:template name="PatientHierarchicalLoop">
    <!-- We will assume that the subscriber will always be present on the PCM claims.  Therefore, 2000C is not needed. -->
  </xsl:template>



  <!-- 2010AA BILLING PROVIDER NAME -->
  <xsl:template name="BillingProviderNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- Last name or organization name -->
    <Field01_01_BillingProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field01_01_BillingProviderLastName>
    <Field01_04_BillingProviderAddress1>
      <xsl:value-of select="$Loop/NM1/N301"/>
    </Field01_04_BillingProviderAddress1>
    <Field05_FederalTaxId>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field05_FederalTaxId>
    <Field01_06_BillingProviderCity>
      <xsl:value-of select="$Loop/NM1/N401"/>
    </Field01_06_BillingProviderCity>
    <Field01_06_BillingProviderState>
      <xsl:value-of select="$Loop/NM1/N402"/>
    </Field01_06_BillingProviderState>
    <Field01_09_BillingProviderZip>
      <xsl:value-of select="$Loop/NM1/N403"/>
    </Field01_09_BillingProviderZip>
    <Field01_11_BillingProviderPhoneNumber>
      <xsl:value-of select="$Loop/NM1/PER04"/>
    </Field01_11_BillingProviderPhoneNumber>
    <Field01_11_BillingProviderFaxNumber>
      <xsl:value-of select="$Loop/NM1/PER06"/>
    </Field01_11_BillingProviderFaxNumber>
    <Field01_13_BillingProviderCountryCode>
      <xsl:value-of select="$Loop/NM1/N404"/>
    </Field01_13_BillingProviderCountryCode>
    <Field56_NationalProviderIndicator>
      <xsl:if test="(NM108 = 'XX')">
         <xsl:value-of select="$Loop/NM1/NM109"/>
      </xsl:if>
    </Field56_NationalProviderIndicator>
  </xsl:template>

  <!-- 2010AB PAY-TO PROVIDER NAME -->
    <xsl:template name="PayToProviderNameLoop">
      <xsl:param name="Loop"></xsl:param>
      <!-- Last name or organization name -->
      <!--<Field02_01_PayToProviderLastName>
        <xsl:value-of select="$Loop/NM1/NM103"/>
      </Field02_01_PayToProviderLastName>
      <Field02_02_BillingProviderFirstName>
        <xsl:value-of select="$Loop/NM1/NM104"/>
      </Field02_02_BillingProviderFirstName>
      <Field02_02_BillingProviderMiddleName>
        <xsl:value-of select="$Loop/NM1/NM105"/>
      </Field02_02_BillingProviderMiddleName>-->
      <Field02_04_PayToProviderAddress1>
        <xsl:value-of select="$Loop/N3/N301"/>
      </Field02_04_PayToProviderAddress1>
      <Field02_04_PayToProviderAddress2>
        <xsl:value-of select="$Loop/N3/N302"/>
      </Field02_04_PayToProviderAddress2>
      <Field02_06_PayToProviderCity>
        <xsl:value-of select="$Loop/N4/N401"/>
      </Field02_06_PayToProviderCity>
      <Field02_06_PayToProviderState>
        <xsl:value-of select="$Loop/N4/N402"/>
      </Field02_06_PayToProviderState>
      <Field02_09_PayToProviderZip>
        <xsl:value-of select="$Loop/N4/N403"/>
      </Field02_09_PayToProviderZip>
      <Field02_11_PayToProviderCountryCode>
        <xsl:value-of select="$Loop/N4/N404"/>
      </Field02_11_PayToProviderCountryCode>
    </xsl:template>

  <!-- 2010BA SUBSCRIBER NAME LOOP -->
  <xsl:template name="SubscriberNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- first name -->
    <Field02_01_PayToLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field02_01_PayToLastName>
    <!-- middle name -->
    <Field02_02_PayToFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field02_02_PayToFirstName>
    <!-- name last or organization -->
    <Field02_03_PayToMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field02_03_PayToMiddleName>
    <!-- Identification Code -->
    <Field02_04_PayToAddress1>
      <xsl:value-of select="$Loop/N3/N301"/>
    </Field02_04_PayToAddress1>
    <Field02_05_PayToAddress2>
      <xsl:value-of select="$Loop/N3/N302"/>
    </Field02_05_PayToAddress2>
    <Field02_06_PayToCity>
      <xsl:value-of select="$Loop/N4/N401"/>
    </Field02_06_PayToCity>
    <Field02_08_PayToState>
      <xsl:value-of select="$Loop/N4/N402"/>
    </Field02_08_PayToState>
    <Field02_11_PayToCountryCode>
      <xsl:value-of select="$Loop/N4/N304"/>
    </Field02_11_PayToCountryCode>
    <Field08a_PatientIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field08a_PatientIdentifier>
    <Field08b_01_PatientLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field08b_01_PatientLastName>
    <Field08b_02_PatientFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field08b_02_PatientFirstName>
    <Field08b_03_PatientMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field08b_03_PatientMiddleName>
    <Field09a_PatientStreet>
      <xsl:value-of select="$Loop/N4/N303"/>
    </Field09a_PatientStreet>
    <Field09b_PatientCity>
      <xsl:value-of select="$Loop/N4/N303"/>
    </Field09b_PatientCity>
    <Field09b_PatientState>
      <xsl:value-of select="$Loop/N4/N303"/>
    </Field09b_PatientState>
    <Field02_09_PayToZip>
      <xsl:value-of select="$Loop/N4/N303"/>
    </Field02_09_PayToZip>
    <_field09e_PatientCountry>
      <xsl:value-of select="$Loop/N4/N303"/>
    </_field09e_PatientCountry>
    <Field10_PatientDOB>
      <xsl:value-of select="$Loop/DMG/DMG02"/>
    </Field10_PatientDOB>
    <Field11_Sex>
      <xsl:value-of select="$Loop/DMG/DMG03"/>
    </Field11_Sex>
    <Field58a_InsuredsLastName>
      <xsl:if test="(NM101 = 'IL')">
        <xsl:value-of select="$Loop/NM1/NM103"/>
      </xsl:if>
    </Field58a_InsuredsLastName>
    <Field60a_InsuredsIniqueIdentificationNumber>
      <xsl:if test="(NM108 = 'MI')">
        <xsl:value-of select="$Loop/NM1/NM109"/>
      </xsl:if>
    </Field60a_InsuredsIniqueIdentificationNumber>
  </xsl:template>


  <!-- 2010BB PAYER NAME LOOP -->
  <xsl:template name="PayerNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- Payer Name - Field 50 from UB-04 -->
    <Field50_PayerName>
      <xsl:if test="(NM101 = 'PR')">
        <PayerName>
          <xsl:value-of select="$Loop/NM1/NM103"/>
        </PayerName>
      </xsl:if>
    </Field50_PayerName>
    <!--<Field50a_PayerName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field50a_PayerName>-->
    <!-- Identification Code Qualifier -->
    <Field51_NationalProviderIndicator>
      <xsl:value-of select="$Loop/NM1/N109"/>
    </Field51_NationalProviderIndicator>
    <!-- Identification Code -->
    <Field57_OtherProviderIdentifier>
      <xsl:value-of select="$Loop/N3/N302"/>
    </Field57_OtherProviderIdentifier>
</xsl:template>

  
  <!-- 2310A ATTENDING PHYSICIAN NAME LOOP -->
  <xsl:template name="AttendingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->

    <Field76_AttendingProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field76_AttendingProviderLastName>
    <xsl:if test ="(NM102 = '1')">
      <Field76_AttendingProviderFirstName>
        <xsl:value-of select="$Loop/NM1/NM104"/>
      </Field76_AttendingProviderFirstName>
      <Field76_AttendingProviderMiddleName>
        <xsl:value-of select="$Loop/NM1/NM105"/>
      </Field76_AttendingProviderMiddleName>
    </xsl:if>
    <!-- entity identifier code -->
    <xsl:if test ="(NM101 = '71') and (NM108 = 'XX')">
    <Field76_AttendingProviderNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field76_AttendingProviderNationalProviderIdentifier>
  </xsl:if>
    <!-- IDENTIFICATION CODE QUALIFIER -->
    <Field76_AttendingProviderSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field76_AttendingProviderSecondaryQualifier>
    <!-- Identification Code -->
    <Field76_AttendingProviderSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field76_AttendingProviderSecondaryIdentifier>
  </xsl:template>


  <!-- 2310B OPERATING PHYSICIAN NAME LOOP -->
  <xsl:template name="OperatingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field77_OperatingPhysicianLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field77_OperatingPhysicianLastName>
    <Field77_OperatingPhysicianFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field77_OperatingPhysicianFirstName>
    <Field77_OperatingPhysicianMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field77_OperatingPhysicianMiddleName>
    <xsl:if test ="(NM101 = '71') and (NM108 = 'XX')">
      <Field77_OperatingPhysicianNationalProviderIdentifier>
        <xsl:value-of select="$Loop/NM1/NM109"/>
      </Field77_OperatingPhysicianNationalProviderIdentifier>
    </xsl:if>
    <Field77_OperatingPhysicianSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field77_OperatingPhysicianSecondaryQualifier>
    <Field77_OperatingPhysicianSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field77_OperatingPhysicianSecondaryIdentifier>
  </xsl:template>


  <!-- 2310C OTHER OPERATING PHYSICIAN NAME LOOP -->
  <xsl:template name="OtherPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field78_OtherOperatingLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field78_OtherOperatingLastName>
    <Field78_OtherOperatingFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field78_OtherOperatingFirstName>
    <Field78_OtherOperatingMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field78_OtherOperatingMiddleName>
    <Field78_OtherOperatingSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field78_OtherOperatingSuffix>
    <Field78_OtherOperatingNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field78_OtherOperatingNationalProviderIdentifier>
    <Field78_OtherOperatingSecondaryQualifier>
      <xsl:value-of select="$Loop/NM1/NM101"/>
    </Field78_OtherOperatingSecondaryQualifier>
    <Field78_OtherOperatingSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field78_OtherOperatingSecondaryIdentifier>
  
</xsl:template>


  <!-- 2310D RENDERING PROVIDER NAME LOOP -->
  <xsl:template name="RenderingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field79_RenderingProviderNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field79_RenderingProviderNationalProviderIdentifier>
    <Field79_RenderingProviderSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field79_RenderingProviderSecondaryQualifier>
    <Field79_RenderingProviderSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field79_RenderingProviderSecondaryIdentifier>
    <Field79_RenderingProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field79_RenderingProviderLastName>
    <Field79_RenderingProviderFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field79_RenderingProviderFirstName>
    <Field79_RenderingProviderMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field79_RenderingProviderMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field79_RenderingProviderSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field79_RenderingProviderSuffix>
  
</xsl:template>


  <!-- 2310F REFERRING PROVIDER NAME LOOP -->
  <xsl:template name="ReferringOperatingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field79_ReferringProviderNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field79_ReferringProviderNationalProviderIdentifier>
    <Field79_ReferringProviderSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field79_ReferringProviderSecondaryQualifier>
    <Field79_ReferringProviderSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field79_ReferringProviderSecondaryIdentifier>
    <Field79_ReferringProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field79_ReferringProviderLastName>
    <Field79_ReferringProviderFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field79_ReferringProviderFirstName>
    <Field79_ReferringProviderMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field79_ReferringProviderMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field79_ReferringProviderSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field79_ReferringProviderSuffix>
  

</xsl:template>

  <!-- 2320 OTHER SUBSCRIBER INFORMATION -->
  <xsl:template name="OtherSubscriberInfoLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- AMT02, PAYER PAID AMOUNT - Field 54 from UB-04 -->
    <Field54_PriorPayments>
      <xsl:for-each select="AMT">
        <xsl:if test="(AMT01 = 'D')">
          <PriorPayments>
            <xsl:value-of select="$Loop/AMT02"/>
          </PriorPayments>
        </xsl:if>
      </xsl:for-each>
    </Field54_PriorPayments>
    <!-- 2320(OI06) for NON-Destination Payer, Release Of Info Cert Ind - Field 52 from UB-04 -->
      <Field52_ReleaseOfInfoCertIndicator>
        <xsl:for-each select="AMT">
          <ReleaseOfInfoIndicator>
          <xsl:value-of select="$Loop/OI/OI06"/>
        </ReleaseOfInfoIndicator>
        </xsl:for-each>
      </Field52_ReleaseOfInfoCertIndicator>
  </xsl:template>

  
  <!-- 2330 NON-DESTINATION PAYER INFORMATION --><!--
  <xsl:template name="NonDestinationPayersInfoLoop">
 
  </xsl:template>-->


  
  <!-- 2400 SERVICE LINE -->
  <xsl:template name="ServiceLineLoop">

  </xsl:template>
  
  
    <xsl:template match="Loop[@LoopId='2300']">
      <UB04Claim>
        <xsl:call-template name="SubmitterNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='1000A']" />
        </xsl:call-template>
        <xsl:call-template name="BillingProviderNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010AA']" />
        </xsl:call-template>
        <xsl:call-template name="PatientHierarchicalLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']" />
        </xsl:call-template>
        
        
        <!-- LOOP 2300 CLAIM INFORMATION -->
        <!--Patient Account Number-->
       <Field03a_PatientControlNumber>
        <xsl:value-of select="CLM/CLM01"/>
       </Field03a_PatientControlNumber>

        <!-- Medical Record Number-->
        <xsl:for-each select="REF">
          <xsl:if test="(REF01 = 'EA')">
            <Field03b_MedicalHealthRecordNumber>
              <xsl:value-of select="REF02"/>
            </Field03b_MedicalHealthRecordNumber>
          </xsl:if>
          <xsl:if test="(REF01 = 'GI')">
            <Field63a_TreatmentAuthorizationCodes>
              <xsl:value-of select="REF02"/>
            </Field63a_TreatmentAuthorizationCodes>
          </xsl:if>
          <xsl:if test="(REF01 = 'F8')">
            <Field64a_DocumentControlNumber>
              <xsl:value-of select="REF02"/>
            </Field64a_DocumentControlNumber>
          </xsl:if>
        </xsl:for-each>

        <!--Place of Service Code-->
        <Field04_TypeOfBill>
          <xsl:value-of select="CLM/CLM05"/>
        </Field04_TypeOfBill>

        <!--Total Claim Charge Amount-->
        <Field47_SummaryTotalCharges>
          <xsl:value-of select="CLM/CLM02"/>
        </Field47_SummaryTotalCharges>

        <!-- 2300(CLM09) for Destination Payer, 2320(O106) for NON-Destination Payer, Release Of Info Cert Ind - Field 52 from UB-04 -->
        <xsl:for-each select="CLM">
          <Field52_ReleaseOfInfoCertIndicator>
            <ReleaseOfInfoIndicator>
              <xsl:value-of select="CLM_CLM09"/>
            </ReleaseOfInfoIndicator>
          </Field52_ReleaseOfInfoCertIndicator>
        </xsl:for-each>

        <!-- 2300(CLM08) for Destination Payer, 2320(OI03) for NON-Destination Payer, Assignment of Benefits Cert Ind - Field 53 from UB-04 -->
        <xsl:for-each select="CLM">
          <Field53_AssignmentOfBenefitsCertIndicator>
            <AssignmentIndicator>
              <xsl:value-of select="CLM/CLM08"/>
            </AssignmentIndicator>
          </Field53_AssignmentOfBenefitsCertIndicator>
        </xsl:for-each>

        <!-- 2300 AMT02 (WHEN AMT01 is 'F3', Estimated Amount Due - Field 55 from UB-04   Other/Tertiary payers do NOT have an EST AMT DUE-->
        <xsl:for-each select="AMT">
          <Field55EstimatedAmountDue>
            <xsl:if test="(AMT01 = 'F3')">
              <PayerEstimatedAmountDue>
                <xsl:value-of select="AMT/AMT02"/>
              </PayerEstimatedAmountDue>
            </xsl:if>
          </Field55EstimatedAmountDue>
        </xsl:for-each>

        <xsl:for-each select="DTP">
          <xsl:if test="(DTP01 = '096')">
          <Field16_DischargeHour>
            <xsl:value-of select="DTP03"/>
          </Field16_DischargeHour>
          </xsl:if>
          <xsl:if test="(DTP01 = '434')">
            <xsl:if test="(DTP02 = 'D8')">
              <Field06_ServiceFromDate>
                <xsl:value-of select="DTP03"/>
              </Field06_ServiceFromDate>
              <Field06_ServiceToDate>
                <xsl:value-of select="DTP03"/>
              </Field06_ServiceToDate>
            </xsl:if>
            <xsl:if test="(DTP02 = 'RD8')">
              <Field06_ServiceFromDate>
                <xsl:value-of select='substring("DTP03",4,1)'/>
              </Field06_ServiceFromDate>
              <Field06_ServiceToDate>
                <xsl:value-of select='substring("DTP03",6,9)'/>
              </Field06_ServiceToDate>
            </xsl:if>
          </xsl:if>
          <xsl:if test="(DTP01 = '434')">
            <Field13_AdmissionHour>
              <xsl:value-of select="DTP03"/>
            </Field13_AdmissionHour>
          </xsl:if>
          <xsl:if test="(DTP01 = '434')">
            <Field12_AdmissionDate>
              <xsl:value-of select="DTP03"/>
            </Field12_AdmissionDate>
          </xsl:if>
        </xsl:for-each>
        
        <!-- Discharge Hour -->
        <!-- Statement Dates -->
        <!-- Admission Date/Hour -->
        <!-- Date - Repricer Received Date -->

        <Field14_TypeOfVisit>
          <xsl:value-of select="CL1/CL101"/>
        </Field14_TypeOfVisit>
        <Field15_SourceOfAdmission>
          <xsl:value-of select="CL1/CL102"/>
        </Field15_SourceOfAdmission>
        <Field17_PatientDischargeStatus>
          <xsl:value-of select="CL1/CL103"/>
        </Field17_PatientDischargeStatus>

        <xsl:for-each select="HI">
          <xsl:if test="('HI01 - 1' = 'BG')">
            <Field18_28_ConditionCodes>
              <xsl:value-of select="HI/HI01 - 2"/>
            </Field18_28_ConditionCodes>
          </xsl:if>
        </xsl:for-each>

        <xsl:for-each select ="REF">
          <xsl:if test="(REF01 = 'LU')">
            <Field29_AccidentState>
              <xsl:value-of select="REF02"/>
            </Field29_AccidentState>
          </xsl:if>
        </xsl:for-each>

        
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field39a_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field39a_Code>
          <Field39a_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field39a_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field39b_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field39b_Code>
          <Field39b_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field39b_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field39c_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field39c_Code>
          <Field39c_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field39c_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field39d_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field39d_Code>
          <Field39d_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field39d_Value>
        </xsl:if>

        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field40a_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field40a_Code>
          <Field40a_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field40a_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field40b_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field40b_Code>
          <Field40b_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field40b_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field40c_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field40c_Code>
          <Field40c_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field40c_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field40d_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field40d_Code>
          <Field40d_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field40d_Value>
        </xsl:if>

        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field41a_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field41a_Code>
          <Field41a_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field41a_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field41b_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field41b_Code>
          <Field41b_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field41b_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field41c_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field41c_Code>
          <Field41c_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field41c_Value>
        </xsl:if>
        <xsl:if test="('HI01 - 1' = 'BE')">
          <Field41d_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field41d_Code>
          <Field41d_Value>
            <xsl:value-of select="HI/HI01 - 5"/>
          </Field41d_Value>
        </xsl:if>

        <xsl:for-each select ="HI">
          <xsl:if test="(HI101 = 'BH')">
            <UB04OccurrenceCodesAndDates>
              <Field31_34_OccurrenceCodesAndDates>
                <xsl:value-of select="HI/HI01 - 2"/>
              </Field31_34_OccurrenceCodesAndDates>
            </UB04OccurrenceCodesAndDates>
          </xsl:if>
        </xsl:for-each>

        <xsl:for-each select ="HI">
          <xsl:if test="(HI101 = 'BH')">
            <UB04OccurrenceSpanCodesAndDates>
              <Field35_36_OccurrenceSpanCodesAndDates>
                <xsl:value-of select="HI/HI01 - 2"/>
              </Field35_36_OccurrenceSpanCodesAndDates>
            </UB04OccurrenceSpanCodesAndDates>
          </xsl:if>
        </xsl:for-each>

        <!--<xsl:for-each select ="HI">
          <xsl:if test="(HI101 = 'BE')">
            <UB04ValueCodesAndAmounts>
              <ValueCode>
                <xsl:value-of select="HI/HI01 - 2"/>
              </ValueCode>
              <Amount>
                <xsl:value-of select="HI/HI01 - 5"/>
              </Amount>
            </UB04ValueCodesAndAmounts>
          </xsl:if>
        </xsl:for-each>-->

        
        <!--
Come back to the service lines



        UB04ServiceLine               Field42_RevenueCode
        Field43_RevenueDescription
        Field44_HCPCS_Rates
        Field45_ServiceDate
        Field46_UnitsOfService
        Field47_TotalCharges
        Field48_NonCoveredCharges
        Field49_Filler
        
        UB04TotalChargesLine          Field42_49_ServiceLinesTotal


-->



        <!--Health Care Code Information-->
        <Field50_55_PayerInfo>
          <Field55_EstimatedAmountDue>
            <xsl:value-of select="AMT/AMT02"/>
          </Field55_EstimatedAmountDue>
        </Field50_55_PayerInfo>
        
        
        
        <xsl:if test="(HI101 = 'BK') or (HI101 = 'ABK')">
          <Field67_PrincipleDiagCode1_7>
              <xsl:value-of select="HI/HI01 - 2"/>
          </Field67_PrincipleDiagCode1_7>
          <Field67_PrincipleDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67_PrincipleDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 1' = 'BF') or ('HI01 - 1' = 'ABF')">
          <Field67a_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67a_OtherDiagCode1_7>
          <Field67a_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67a_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 2' = 'BF') or ('HI01 - 2' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 3' = 'BF') or ('HI01 - 3' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 4' = 'BF') or ('HI01 - 4' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 5' = 'BF') or ('HI01 - 5' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 6' = 'BF') or ('HI01 - 6' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 7' = 'BF') or ('HI01 - 7' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 8' = 'BF') or ('HI01 - 8 '= 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 9' = 'BF') or ('HI01 - 9' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 10' = 'BF') or ('HI01 - 10' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 11' = 'BF') or ('HI01 - 11' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 12' = 'BF') or ('HI01 - 12' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 13' = 'BF') or ('HI01 - 13' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 14' = 'BF') or ('HI01 - 14' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 15' = 'BF') or ('HI01 - 15' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 16' = 'BF') or ('HI01 - 16' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>

        <xsl:if test="('HI01 - 17' = 'BF') or ('HI01 - 17' = 'ABF')">
          <Field67b_OtherDiagCode1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field67b_OtherDiagCode1_7>
          <Field67b_OtherDiagCode8>
            <xsl:value-of select="HI/HI01 - 9"/>
          </Field67b_OtherDiagCode8>
        </xsl:if>


        <xsl:if test="('HI01 - 1' = 'BJ') or ('HI01 - 1' = 'ABJ')">
          <Field69_AdmittingDiagnosisCode>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field69_AdmittingDiagnosisCode>
        </xsl:if>

        <xsl:if test="('HI01 - 1' = 'PR') or ('HI01 - 1' = 'APR')">
          <Field70a_ReasonForVisit>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field70a_ReasonForVisit>
        </xsl:if>

        <xsl:if test="('HI02 - 1' = 'PR') or ('HI02 - 1' = 'ABAPRJ')">
          <Field70b_ReasonForVisit>
            <xsl:value-of select="HI/HI02 - 2"/>
          </Field70b_ReasonForVisit>
        </xsl:if>

        <xsl:if test="('HI03 - 1' = 'PR') or ('HI03 - 1' = 'APR')">
          <Field70c_ReasonForVisit>
            <xsl:value-of select="HI/HI03 - 2"/>
          </Field70c_ReasonForVisit>
        </xsl:if>

        <xsl:if test="'HI01 - 1' = DR">
          <Field71_ProspectivePaymentSystemCode>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field71_ProspectivePaymentSystemCode>
        </xsl:if>

        
        <xsl:if test="('HI01 - 1' = 'BN') or ('HI01 - 1' = 'ABN')">
          <Field72a_ExternalCauseOfInjuryCode_1_7>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field72a_ExternalCauseOfInjuryCode_1_7>
          <Field72a_ExternalCauseOfInjuryCode_8>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field72a_ExternalCauseOfInjuryCode_8>
        </xsl:if>

        <xsl:if test="('HI02 - 1' = 'BN') or ('HI02 - 1' = 'ABN')">
          <Field72b_ExternalCauseOfInjuryCode_1_7>
            <xsl:value-of select="HI/HI02 - 2"/>
          </Field72b_ExternalCauseOfInjuryCode_1_7>
          <Field72b_ExternalCauseOfInjuryCode_8>
            <xsl:value-of select="HI/HI02 - 9"/>
          </Field72b_ExternalCauseOfInjuryCode_8>
        </xsl:if>

        <xsl:if test="('HI03 - 1' = 'BN') or ('HI03 - 1' = 'ABN')">
          <Field72c_ExternalCauseOfInjuryCode_1_7>
            <xsl:value-of select="HI/HI03 - 2"/>
          </Field72c_ExternalCauseOfInjuryCode_1_7>
          <Field72c_ExternalCauseOfInjuryCode_8>
            <xsl:value-of select="HI/HI03 - 9"/>
          </Field72c_ExternalCauseOfInjuryCode_8>
        </xsl:if>

        <xsl:if test="('HI01 - 1' = 'BR') or ('HI01 - 1' = 'BBR')">
          <Field74_Principal_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field74_Principal_Code>
          <Field74_Principal_Date>
            <xsl:value-of select="HI/HI01 - 4"/>
          </Field74_Principal_Date>
        </xsl:if>
        
        <xsl:if test="('HI01 - 1' = 'BQ') or ('HI01 - 1' = 'BBQ')">
          <Field74a_OtherProcedure_Code>
            <xsl:value-of select="HI/HI01 - 2"/>
          </Field74a_OtherProcedure_Code>
          <Field74a_OtherProcedure_Date>
            <xsl:value-of select="HI/HI01 - 4"/>
          </Field74a_OtherProcedure_Date>
        </xsl:if>

        <xsl:if test="('HI02 - 1' = 'BQ') or ('HI02 - 1' = 'BBQ')">
          <Field74b_OtherProcedure_Code>
            <xsl:value-of select="HI/HI02 - 2"/>
          </Field74b_OtherProcedure_Code>
          <Field74b_OtherProcedure_Date>
            <xsl:value-of select="HI/HI02 - 4"/>
          </Field74b_OtherProcedure_Date>
        </xsl:if>

        <xsl:if test="('HI03 - 1' = 'BQ') or ('HI03 - 1' = 'BBQ')">
          <Field74c_OtherProcedure_Code>
            <xsl:value-of select="HI/HI03 - 2"/>
          </Field74c_OtherProcedure_Code>
          <Field74c_OtherProcedure_Date>
            <xsl:value-of select="HI/HI03 - 4"/>
          </Field74c_OtherProcedure_Date>
        </xsl:if>

        <xsl:if test="('HI04 - 1' = 'BQ') or ('HI04 - 1' = 'BBQ')">
          <Field74e_OtherProcedure_Code>
            <xsl:value-of select="HI/HI04 - 2"/>
          </Field74e_OtherProcedure_Code>
          <Field74e_OtherProcedure_Date>
            <xsl:value-of select="HI/HI04 - 4"/>
          </Field74e_OtherProcedure_Date>
        </xsl:if>

        <xsl:if test="('HI05 - 1' = 'BQ') or ('HI05 - 1' = 'BBQ')">
          <Field74e_OtherProcedure_Code>
            <xsl:value-of select="HI/HI05 - 2"/>
          </Field74e_OtherProcedure_Code>
          <Field74e_OtherProcedure_Date>
            <xsl:value-of select="HI/HI05 - 4"/>
          </Field74e_OtherProcedure_Date>
        </xsl:if>

        <xsl:if test="(NTE01 = 'ADD')">
          <Field80_Remarks>
            <xsl:value-of select="NTE/NTE02"/>
          </Field80_Remarks>
          <!-- Billing Note Text -->
          <Field81_CodeCode_Note>
            <xsl:value-of select="NTE/NTE02"/>
          </Field81_CodeCode_Note>
        </xsl:if>
        
        <xsl:call-template name="AttendingPhysicianNameLoop">
          <xsl:with-param name="Loop" select="Loop[@LoopId='2310A']" />
        </xsl:call-template>
        <xsl:call-template name="SubscriberNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']" />
        </xsl:call-template>
        <xsl:call-template name="PayerNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BB']" />
        </xsl:call-template>
        <!--Benefits Assignment Certification Indicator-->
        <!--
        <xsl:call-template name="PatientHierarchicalLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2000C']" />
        </xsl:call-template>-->

      </UB04Claim>
    </xsl:template>



</xsl:stylesheet>
