<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:apply-templates select="@* | node()"/>
    </xsl:template>
<!--
  <xsl:template match="Interchange">
    <ArrayOfUB04Claim>
      <xsl:apply-templates select="@* | node()"/>
    </ArrayOfUB04Claim>
  </xsl:template>
-->
  <xsl:template name="PatientNameLoop">
    <xsl:param name="Loop"></xsl:param>
      <Field08b_02_PatientFirstName>
        <xsl:value-of select="$Loop/NM1/NM104"/>
      </Field08b_02_PatientFirstName>
      <Field08b_01_PatientLastName>
        <xsl:value-of select="$Loop/NM1/NM103"/>
      </Field08b_01_PatientLastName>
  </xsl:template>
 
    <xsl:template match="Loop[@LoopId='2300']">
      <UB04Claim>
        <xsl:call-template name="PatientNameLoop">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']" />
        </xsl:call-template>
        <!--Patient Account Number-->
       <Field03a_PatientControlNumber>
        <xsl:value-of select="CLM/CLM01"/>
       </Field03a_PatientControlNumber>
        <!--Total Claim Charge Amount-->
        <Field55a_EstimatedAmountDue>
          <xsl:value-of select="CLM/CLM02"/>
        </Field55a_EstimatedAmountDue>
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
        <!--Provider Accept Assignment Code-->
        <ProviderAcceptAssignmentCode>
          <xsl:value-of select="CLM/CLM07"/>
        </ProviderAcceptAssignmentCode>
        <!--Benefits Assignment Certification Indicator-->
        <BenefitsAssignmentCert>
          <xsl:value-of select="CLM/CLM08"/>
        </BenefitsAssignmentCert>
        <!--Release of Information Code-->
        <ReleaseInfoCode>
          <xsl:value-of select="CLM/CLM09"/>
        </ReleaseInfoCode>
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
        <CL1element1>
          <xsl:value-of select="CL1/CL101"/>
        </CL1element1>
        <CL1_element2>
          <xsl:value-of select="CL1/CL102"/>
        </CL1_element2>
      </UB04Claim>
    </xsl:template>



</xsl:stylesheet>
