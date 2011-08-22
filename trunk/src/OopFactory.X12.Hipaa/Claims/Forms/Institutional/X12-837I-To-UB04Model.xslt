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
  
  <!-- 2010AA BILLING PROVIDER NAME -->
  <xsl:template name="BillingProviderNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- Last name or organization name -->
    <Field76_AttendingProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field76_AttendingProviderLastName>
  </xsl:template>
  
  <!-- 2010BA PATIENT NAME LOOP -->
  <xsl:template name="PatientNameLoop">
    <xsl:param name="Loop"></xsl:param>
      <Field08b_02_PatientFirstName>
        <xsl:value-of select="$Loop/NM1/NM104"/>
      </Field08b_02_PatientFirstName>
      <Field08b_01_PatientLastName>
        <xsl:value-of select="$Loop/NM1/NM103"/>
      </Field08b_01_PatientLastName>
  </xsl:template>

  <!-- 2010BA SUBSCRIBER NAME LOOP -->
  <xsl:template name="SubscriberNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- first name -->
    <Field58a_InsuredsName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </Field58a_InsuredsName>
    <!-- middle name -->
    <Field58b_InsuredsName>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </Field58b_InsuredsName>
    <!-- name last or organization -->
    <Field58c_InsuredsName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field58c_InsuredsName>
    <!-- Identification Code -->
    <Field60a_InsuredsIniqueIdentificationNumber>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field60a_InsuredsIniqueIdentificationNumber>

  </xsl:template>

  <!-- 2010BB PAYER NAME LOOP -->
  <xsl:template name="PayerNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- name last or organization -->
    <Field50a_PayerName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </Field50a_PayerName>
    <!-- Identification Code Qualifier -->
    <Field56_NationalProviderIndicator>
      <xsl:value-of select="$Loop/NM1/NM108"/>
    </Field56_NationalProviderIndicator>
    <!-- Identification Code -->
    <Field57_OtherProviderIdentifier>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </Field57_OtherProviderIdentifier>
    
  
</xsl:template>

  <!-- 2310A ATTENDING PHYSICIAN NAME LOOP -->
  <xsl:template name="AttendingPhysicianNameLoop">
    <xsl:param name="Loop"></xsl:param>
    <!-- entity identifier code -->
    <UNKNOWN_NM101>
      <xsl:value-of select="$Loop/NM1/NM101"/>
    </UNKNOWN_NM101>
    <!-- person or non person entity -->
    <UNKNOWN_NM102>
      <xsl:value-of select="$Loop/NM1/NM102"/>
    </UNKNOWN_NM102>
    <field76_AttendingProviderLastName>
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </field76_AttendingProviderLastName>
    <field76_AttendingProviderFirstName>
      <xsl:value-of select="$Loop/NM1/NM104"/>
    </field76_AttendingProviderFirstName>
    <!-- middle name ??? there is no middle name for Attending Provider in object model. Are these other fields? -->
    <UNKNOWN_NM105>
      <xsl:value-of select="$Loop/NM1/NM105"/>
    </UNKNOWN_NM105>
    <UNKNOWN_NM106>
      <xsl:value-of select="$Loop/NM1/NM106"/>
    </UNKNOWN_NM106>
    <UNKNOWN_NM107>
      <xsl:value-of select="$Loop/NM1/NM107"/>
    </UNKNOWN_NM107>
    <!-- IDENTIFICATION CODE QUALIFIER -->
    <UNKNOWN_NM108>
      <xsl:value-of select="$Loop/NM1/NM108"/>
    </UNKNOWN_NM108>
    <!-- Identification Code -->
    <UNKNOWN_NM109>
      <xsl:value-of select="$Loop/NM1/NM109"/>
    </UNKNOWN_NM109>
    <!-- PROVIDER CODE -->
    <UNKNOWN_PRV01>
      <xsl:value-of select="$Loop/PRV/PVR01"/>
    </UNKNOWN_PRV01>
    <!-- Reference Identification Qualifier -->
    <UNKNOWN_PRV02>
      <xsl:value-of select="$Loop/PRV/PVR02"/>
    </UNKNOWN_PRV02>
    <!-- Reference Identification -->
    <UNKNOWN_PRV03>
      <xsl:value-of select="$Loop/PRV/PVR03"/>
    </UNKNOWN_PRV03>
  </xsl:template>

  <!-- 2320 OTHER SUBSCRIBER INFORMATION -->
  <xsl:template name="OtherSubscriberInfoLoop">

  </xsl:template>

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
        <xsl:call-template name="PatientNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']" />
        </xsl:call-template>
        <!-- LOOP 2300 CLAIM INFORMATION -->
        <!--Patient Account Number-->
       <Field03a_PatientControlNumber>
        <xsl:value-of select="CLM/CLM01"/>
       </Field03a_PatientControlNumber>
        <!--Total Claim Charge Amount-->
        <Field55a_EstimatedAmountDue>
          <xsl:value-of select="CLM/CLM02"/>
        </Field55a_EstimatedAmountDue>
        <UNKNOWN_CLM03>
          <xsl:value-of select="CLM/CLM03"/>
        </UNKNOWN_CLM03>
        <UNKNOWN_CLM04>
          <xsl:value-of select="CLM/CLM04"/>
        </UNKNOWN_CLM04>
        <!--Place of Service Code-->
        <placeOfServiceCode1>
          <xsl:value-of select="CLM/CLM05/CLM0501"/>
        </placeOfServiceCode1>
        <placeOfServiceCode2>
          <xsl:value-of select="CLM/CLM05/CLM0502"/>
        </placeOfServiceCode2>
        <placeOfServiceCode3>
          <xsl:value-of select="CLM/CLM05/CLM0503"/>
        </placeOfServiceCode3>
        <UNKNOWN_CLM06>
          <xsl:value-of select="CLM/CLM06"/>
        </UNKNOWN_CLM06>
        <!--Provider Accept Assignment Code-->
        <UNKNOWN_CLM07>
          <xsl:value-of select="CLM/CLM07"/>
        </UNKNOWN_CLM07>
        <!--Benefits Assignment Certification Indicator-->
        <UNKNOWN_CLM08>
          <xsl:value-of select="CLM/CLM08"/>
        </UNKNOWN_CLM08>
        <!--Release of Information Code-->
        <UNKNOWN_CLM09>
          <xsl:value-of select="CLM/CLM09"/>
        </UNKNOWN_CLM09>
        <!--Date/Time Qualifier-->
        <Field14_TypeOfVisit>
          <xsl:value-of select="DTP/DTP01"/>
        </Field14_TypeOfVisit>
        <!--Date Time Period Format Qualifier-->
        <Field15_SourceOfAdmission>
          <xsl:value-of select="DTP/DTP02"/>
        </Field15_SourceOfAdmission>
        <!--Date Time Period-->
        <Field16_DischargeHour>
          <xsl:value-of select="DTP/DTP03"/>
        </Field16_DischargeHour>
        <UNKNOWN_CL101>
          <xsl:value-of select="CL1/CL101"/>
        </UNKNOWN_CL101>
        <UNKNOWN_CL102>
          <xsl:value-of select="CL1/CL102"/>
        </UNKNOWN_CL102>
        <!--Health Care Code Information-->
        <UNKNOWN_HI0101>
          <xsl:value-of select="HI/HI01/HI0101"/>
        </UNKNOWN_HI0101>
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
