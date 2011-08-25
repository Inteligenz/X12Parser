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
    <Field01_01_ProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field01_01_ProviderLastName>
  </xsl:template>
  
  <!-- 2000B SUBSCRIBER HIERARCHICAL LOOP -->
  <xsl:template name="SubscriberHierarchicalLoop">
  <!-- do any fields in the UB04 correlate to items in here? -->

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
    <Field02_09_PayToZip>
      <xsl:value-of select="$Loop/N4/N303"/>
    </Field02_09_PayToZip>
    <Field02_11_PayToCountryCode>
      <xsl:value-of select="$Loop/N4/N304"/>
    </Field02_11_PayToCountryCode>
  </xsl:template>


  <!-- 2010BB PAYER NAME LOOP -->
  <xsl:template name="PayerNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- name last or organization -->
    <Field50a_PayerName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field50a_PayerName>
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
    <Field76_AttendingProviderFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field76_AttendingProviderFirstName>
    <Field76_AttendingProviderMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field76_AttendingProviderMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field76_AttendingProviderNameSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field76_AttendingProviderNameSuffix>
    <!-- entity identifier code -->
    <Field76_AttendingProviderNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field76_AttendingProviderNationalProviderIdentifier>
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
    <Field76_OperatingPhysicianMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field76_OperatingPhysicianMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field76_OperatingPhysicianNameSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field76_OperatingPhysicianNameSuffix>
    <Field77_OperatingPhysicianNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field77_OperatingPhysicianNationalProviderIdentifier>
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
    <Field77_OtherOperatingPhysicianLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field77_OtherOperatingPhysicianLastName>
    <Field77_OtherOperatingPhysicianFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field77_OtherOperatingPhysicianFirstName>
    <Field76_OtherOperatingPhysicianMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field76_OtherOperatingPhysicianMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field76_OtherOperatingPhysicianNameSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field76_OtherOperatingPhysicianNameSuffix>
    <Field77_OtherOperatingPhysicianNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field77_OtherOperatingPhysicianNationalProviderIdentifier>
    <Field77_OtherOperatingPhysicianSecondaryQualifier>
      <xsl:value-of select="$Loop/NM1/NM101"/>
    </Field77_OtherOperatingPhysicianSecondaryQualifier>
    <Field77_OtherOperatingPhysicianSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field77_OtherOperatingPhysicianSecondaryIdentifier>
  </xsl:template>


  <!-- 2310D OPERATING PHYSICIAN NAME LOOP -->
  <xsl:template name="RenderingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field77_RenderingPhysicianLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field77_RenderingPhysicianLastName>
    <Field77_RenderingPhysicianFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field77_RenderingPhysicianFirstName>
    <Field77_RenderingPhysicianMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field77_RenderingPhysicianMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field77_RenderingPhysicianNameSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field77_RenderingPhysicianNameSuffix>
    <Field77_RenderingPhysicianNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field77_RenderingPhysicianNationalProviderIdentifier>
    <Field77_RenderingPhysicianSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field77_RenderingPhysicianSecondaryQualifier>
    <Field77_RenderingPhysicianSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field77_RenderingPhysicianSecondaryIdentifier>
  </xsl:template>


  <!-- 2310F OPERATING PHYSICIAN NAME LOOP -->
  <xsl:template name="ReferringOperatingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- person or non person entity    NOT ON UB-04 FORM -->
    <Field77_ReferringPhysicianNationalProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field77_ReferringPhysicianNationalProviderIdentifier>
    <Field77_ReferringPhysicianSecondaryQualifier>
      <xsl:value-of select="$Loop/REF/REF01"/>
    </Field77_ReferringPhysicianSecondaryQualifier>
    <Field77_ReferringPhysicianSecondaryIdentifier>
      <xsl:value-of select="$Loop/REF/REF02"/>
    </Field77_ReferringPhysicianSecondaryIdentifier>
    <Field77_ReferringPhysicianLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field77_ReferringPhysicianLastName>
    <Field77_ReferringPhysicianFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field77_ReferringPhysicianFirstName>
    <Field76_ReferringPhysicianMiddleName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field76_ReferringPhysicianMiddleName>
    <!-- Attending Physician Name Suffix -->
    <Field76_ReferringPhysicianNameSuffix>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </Field76_ReferringPhysicianNameSuffix>
  </xsl:template>

  
  
  <!-- 2320 OTHER SUBSCRIBER INFORMATION --><!--
  <xsl:template name="OtherSubscriberInfoLoop">

  </xsl:template>

  

  --><!-- 2330 NON-DESTINATION PAYER INFORMATION --><!--
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
        <!--Total Claim Charge Amount-->
        <Field47_SummaryTotalCharges>
          <xsl:value-of select="CLM/CLM02"/>
        </Field47_SummaryTotalCharges>

        <!--Place of Service Code-->
        <Field04_TypeOfBill>
          <xsl:value-of select="CLM/CLM05"/>
        </Field04_TypeOfBill>
        <!--Provider Accept Assignment Code-->
        <UNKNOWN_CLM07>
          <xsl:value-of select="CLM/CLM07"/>
        </UNKNOWN_CLM07>
        <!--Benefits Assignment Certification Indicator-->

        <xsl:call-template name="PatientHierarchicalLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BB']" />
        </xsl:call-template>

        <!-- 2010BB NM103, Payer Name - Field 50 from UB-04 -->
        <xsl:for-each select="NM1">
          <Field50_PayerName>
            <xsl:if test="NM101 = PR">
              <PayerName>
                <xsl:value-of select="NM1/NM103"/>
              </PayerName>
            </xsl:if>
          </Field50_PayerName>
        </xsl:for-each>

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

        <!-- 2320 AMT02, PAYER PAID AMOUNT - Field 54 from UB-04 -->
        <xsl:for-each select="AMT">
          <Field54_PriorPayments>
            <xsl:if test="AMT01 = D">
              <PriorPayments>
                <xsl:value-of select="AMT02"/>
              </PriorPayments>
            </xsl:if>
          </Field54_PriorPayments>
        </xsl:for-each>

        <!-- 2300 AMT02 (WHEN AMT01 is 'F3', Estimated Amount Due - Field 55 from UB-04   Other/Tertiary payers do NOT have an EST AMT DUE-->
        <xsl:for-each select="AMT">
          <Field55EstimatedAmountDue>
            <xsl:if test="AMT01 = F3">
              <PayerEstimatedAmountDue>
                <xsl:value-of select="AMT/AMT02"/>
              </PayerEstimatedAmountDue>
            </xsl:if>
          </Field55EstimatedAmountDue>
        </xsl:for-each>

        <xsl:for-each select="DTP">
          <xsl:if test="DTP01 = 096">
          <Field16_DischargeHour>
            <xsl:value-of select="DTP03"/>
          </Field16_DischargeHour>
          </xsl:if>
          <xsl:if test="DTP01 = 434">
            <xsl:if test="DTP02 = D8">
              <Field06_ServiceFromDate>
                <xsl:value-of select="DTP03"/>
              </Field06_ServiceFromDate>
              <Field06_ServiceToDate>
                <xsl:value-of select="DTP03"/>
              </Field06_ServiceToDate>
            </xsl:if>
            <xsl:if test="DTP02 = RD8">
              <Field06_ServiceFromDate>
                <xsl:value-of select='substring("DTP03",4,1)'/>
              </Field06_ServiceFromDate>
              <Field06_ServiceToDate>
                <xsl:value-of select='substring("DTP03",6,9)'/>
              </Field06_ServiceToDate>
            </xsl:if>
          </xsl:if>
          <xsl:if test="DTP01 = 434">
            <Field13_AdmissionHour>
              <xsl:value-of select="DTP03"/>
            </Field13_AdmissionHour>
          </xsl:if>
          <xsl:if test="DTP01 = 434">
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

        <!--Health Care Code Information-->
        <Field50_55_PayerInfo>
          <Field55_EstimatedAmountDue>
            <xsl:value-of select="AMT/AMT02"/>  
          </Field55_EstimatedAmountDue>
        </Field50_55_PayerInfo>

        <xsl:for-each select="REF">
        <xsl:if test="REF01 = EA">
          <ield03b_MedicalHealthRecordNumber>
            <xsl:value-of select="REF02"/>
          </ield03b_MedicalHealthRecordNumber>
        </xsl:if>
      </xsl:for-each>


          <UNKNOWN_HI0102>
          <xsl:value-of select="HI/HI01/HI0102"/>
        </UNKNOWN_HI0102>
        <xsl:call-template name="AttendingPhysicianNameLoop">
          <xsl:with-param name="Loop" select="Loop[@LoopId='2310A']" />
        </xsl:call-template>
        <xsl:call-template name="SubscriberNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']" />
        </xsl:call-template>
        <xsl:call-template name="PayerNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BB']" />
        </xsl:call-template>
    
    </UB04Claim>
    </xsl:template>



</xsl:stylesheet>
