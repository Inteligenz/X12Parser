<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns="http://www.oopfactory.com/2011/XSL/Hipaa"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="@* | node()">
    <xsl:apply-templates select="@* | node()"/>
  </xsl:template>

  <xsl:template match="Interchange">
    <ClaimDocument>
      <xsl:apply-templates select="@* | node()"/>
    </ClaimDocument>
  </xsl:template>

  <!-- Claim Loop 2300 -->
  <xsl:template match="Loop[@LoopId='2300']">
    <Claim>
      <xsl:variable name="ParentLoopId" select="../@LoopId"/>
      <xsl:attribute name="Version">
        <xsl:value-of select="/Interchange/FunctionGroup/GS/GS08"/>
      </xsl:attribute>
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="count(Loop/SV1) > 0">Professional</xsl:when>
          <xsl:when test="count(Loop/SV2) > 0">Institutional</xsl:when>
          <xsl:when test="count(Loop/SV3) > 0">Dental</xsl:when>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="TransactionCode">
        <xsl:choose>
          <xsl:when test="$ParentLoopId = '2000B'">
            <xsl:value-of select="../../../ST/ST02"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="../../../../ST/ST02"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="ClaimNumber">
        <xsl:value-of select="REF[REF01='D9']/REF02"/>
      </xsl:attribute>
      <xsl:attribute name="BillTypeCode">
        <xsl:value-of select="concat(CLM/CLM05/CLM0501,CLM/CLM05/CLM0503)"/>
      </xsl:attribute>
      <xsl:attribute name="PatientControlNumber">
        <xsl:value-of select="CLM/CLM01"/>
      </xsl:attribute>
      <xsl:attribute name="TotalClaimChargeAmount">
        <xsl:choose>
          <xsl:when test="string-length(CLM/CLM02) > 0">
            <xsl:value-of select="CLM/CLM02"/>  
          </xsl:when>
          <xsl:otherwise>0</xsl:otherwise><!-- this only happens when a files is not following the 837 implementation guides which requires this field -->
        </xsl:choose>        
      </xsl:attribute>
      <xsl:if test="string-length(CLM/CLM06)>0">
        <xsl:attribute name="ProviderSignatureOnFile">
          <xsl:value-of select="CLM/CLM06"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(CLM/CLM07)>0">
        <xsl:attribute name="ProviderAcceptAssignmentCode">
          <xsl:value-of select="CLM/CLM07"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(CLM/CLM08)>0">
        <xsl:attribute name="BenefitsAssignmentCertificationIndicator">
          <xsl:value-of select="CLM/CLM08"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(CLM/CLM09)>0">
        <xsl:attribute name="ReleaseOfInformationCode">
          <xsl:value-of select="CLM/CLM09"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(CLM/CLM11)>0">
        <xsl:if test="CLM/CLM11">
          <xsl:attribute name="RelatedCauseCode1">
            <xsl:value-of select="CLM/CLM11"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="CLM/CLM11/CLM1101">
          <xsl:attribute name="RelatedCauseCode1">
            <xsl:value-of select="CLM/CLM11/CLM1101"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="CLM/CLM11/CLM1102">
          <xsl:attribute name="RelatedCauseCode2">
            <xsl:value-of select="CLM/CLM11/CLM1102"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="CLM/CLM11/CLM1103">
          <xsl:attribute name="RelatedCauseCode3">
            <xsl:value-of select="CLM/CLM11/CLM1103"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="CLM/CLM11/CLM1104">
          <xsl:attribute name="AutoAccidentState">
            <xsl:value-of select="CLM/CLM11/CLM1104"/>
          </xsl:attribute>
        </xsl:if>
      </xsl:if>
      <xsl:if test="CLM/CLM10">
        <xsl:attribute name="PatientSignatureSourceCode">
          <xsl:value-of select="CLM/CLM10"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="MedicalRecordNumber">
        <xsl:value-of select="REF[REF01='EA']/REF02"/>
      </xsl:attribute>
      <xsl:if test="REF[REF01='G1']">
        <xsl:attribute name="PriorAuthorizationNumber">
          <xsl:value-of select="REF[REF01='G1']/REF02"/>
        </xsl:attribute>
      </xsl:if>
      <ServiceLocationInfo>
        <xsl:attribute name="FacilityCode">
          <xsl:value-of select="CLM/CLM05/CLM0501"/>
        </xsl:attribute>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="CLM/CLM05/CLM0502"/>
        </xsl:attribute>
        <xsl:attribute name="FrequencyTypeCode">
          <xsl:value-of select="CLM/CLM05/CLM0503"/>
        </xsl:attribute>
      </ServiceLocationInfo>
      <xsl:if test="count(CL1)>0">
        <AdmissionType>
          <xsl:attribute name="Code">
            <xsl:value-of select="CL1/CL101"/>
          </xsl:attribute>
          <xsl:value-of select="CL1/CL101/comment()"/>
        </AdmissionType>
        <AdmissionSource>
          <xsl:attribute name="Code">
            <xsl:value-of select="CL1/CL102"/>
          </xsl:attribute>
          <xsl:value-of select="CL1/CL102/comment()"/>
        </AdmissionSource>
        <PatientStatus>
          <xsl:attribute name="Code">
            <xsl:value-of select="CL1/CL103"/>
          </xsl:attribute>
          <xsl:value-of select="CL1/CL103/comment()"/>
        </PatientStatus>
      </xsl:if>

      <xsl:choose>
        <xsl:when test="$ParentLoopId = '2000B'">
          <xsl:call-template name="ProviderInfoLoop">
            <xsl:with-param name="HLoop" select="../../."/>
          </xsl:call-template>
           <xsl:call-template name="SubmitterLoop">
            <xsl:with-param name="HLoop" select="../../../."/>
          </xsl:call-template>
          <xsl:call-template name="BillingProviderHLoop">
            <xsl:with-param name="HLoop" select="../../."/>
          </xsl:call-template>
          <xsl:call-template name="SubscriberHLoop">
            <xsl:with-param name="HLoop" select="../."/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="$ParentLoopId = '2000C'">
          <!-- Parent is Patient Loop -->
          <xsl:call-template name="BillingProviderHLoop">
            <xsl:with-param name="HLoop" select="../../../."/>
          </xsl:call-template>
          <xsl:call-template name="SubscriberHLoop">
            <xsl:with-param name="HLoop" select="../../."/>
          </xsl:call-template>
          <xsl:call-template name="PatientHLoop">
            <xsl:with-param name="HLoop" select="../." />
          </xsl:call-template>
        </xsl:when>
      </xsl:choose>
      <xsl:apply-templates select="PWK"/>
      <xsl:apply-templates select="DTP"/>
      <xsl:apply-templates select="AMT"/>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="NTE"/>
      <xsl:apply-templates select="HI"/>
      <xsl:apply-templates select="Loop"/>
    </Claim>
  </xsl:template>

  <!-- Other subscriber information loop -->
  <xsl:template match="Loop[@LoopId='2320']">
    <OtherSubscriberInformation>
      <xsl:if test="count(DMG) > 0">
        <xsl:attribute name="Gender">
          <xsl:choose>
            <xsl:when test="DMG/DMG03='F'">Female</xsl:when>
            <xsl:when test="DMG/DMG03='M'">Male</xsl:when>
            <xsl:otherwise>Unknown</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(DMG/DMG02)>0">
          <xsl:attribute name="DateOfBirth">
            <xsl:value-of select="concat(substring(DMG/DMG02,1,4),'-',substring(DMG/DMG02,5,2),'-',substring(DMG/DMG02,7,2))"/>
          </xsl:attribute>
        </xsl:if>
      </xsl:if>
      <xsl:if test="string-length(OI/OI03)>0">
        <xsl:attribute name="BenefitsAssignmentCertificationIndicator">
          <xsl:value-of select="OI/OI03"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(OI/OI06)>0">
        <xsl:attribute name="ReleaseOfInformationCode">
          <xsl:value-of select="OI/OI06"/>
        </xsl:attribute>
      </xsl:if>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="Loop[@LoopId='2330A']"/>
        </xsl:call-template>
      </Name>
      <OtherPayer>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="Loop[@LoopId='2330B']"/>
        </xsl:call-template>
      </OtherPayer>
      <xsl:apply-templates select="SBR"/>
      <xsl:apply-templates select="CAS"/>
      <xsl:apply-templates select="AMT"/>
    </OtherSubscriberInformation>
  </xsl:template>

  <!-- Health Care Information Codes -->
  <xsl:template match="HI[HI01/HI0101='BG']">
    <xsl:for-each select="child::*">
      <xsl:variable name="code" select="*[2]"/>
      <Condition>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
      </Condition>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="HI[HI01/HI0101='BH']">
    <xsl:for-each select="child::*">
      <xsl:variable name="code" select="*[2]"/>
      <xsl:variable name="date" select="*[4]"/>
      <Occurrence>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
        <xsl:attribute name="Date">
          <xsl:value-of select="concat(substring($date,1,4),'-',substring($date,5,2),'-',substring($date,7,2))"/>
        </xsl:attribute>
      </Occurrence>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="HI[HI01/HI0101='BI']">
    <xsl:for-each select="child::*">
      <xsl:variable name="code" select="*[2]"/>
      <xsl:variable name="daterange" select="*[4]"/>
      <OccurrenceSpan>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
        <xsl:attribute name="FromDate">
          <xsl:value-of select="concat(substring($daterange,1,4),'-',substring($daterange,5,2),'-',substring($daterange,7,2))"/>
        </xsl:attribute>
        <xsl:attribute name="ThroughDate">
          <xsl:value-of select="concat(substring($daterange,10,4),'-',substring($daterange,14,2),'-',substring($daterange,16,2))"/>
        </xsl:attribute>
      </OccurrenceSpan>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="HI[HI01/HI0101='BE']">
    <xsl:for-each select="child::*">
      <xsl:variable name="code" select="*[2]"/>
      <xsl:variable name="amount" select="*[5]"/>
      <Value>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
        <xsl:if test="string-length($amount) > 0">
          <xsl:attribute name="Amount">
            <xsl:value-of select="$amount"/>
          </xsl:attribute>
        </xsl:if>
      </Value>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="HI[HI01/HI0101='BBR' or HI01/HI0101='BR' or HI01/HI0101='CAH' or HI01/HI0101='BBQ' or HI01/HI0101='BQ']">
    <xsl:for-each select="child::*">
      <xsl:variable name="qualifier" select="*[1]"/>
      <xsl:variable name="code" select="*[2]"/>
      <xsl:variable name="date" select="*[4]"/>
      <Procedure>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="$qualifier"/>
        </xsl:attribute>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
        <xsl:attribute name="Date">
          <xsl:value-of select="concat(substring($date,1,4),'-',substring($date,5,2),'-',substring($date,7,2))"/>
        </xsl:attribute>
      </Procedure>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="HI[HI01/HI0101='DR']">
      <DiagnosisRelatedGroup>
        <xsl:attribute name="Code">
          <xsl:value-of select="HI01/HI0102"/>
        </xsl:attribute>
      </DiagnosisRelatedGroup>
  </xsl:template>
  
  <xsl:template match="HI[HI01/HI0101='ABK' or HI01/HI0101='BK' or HI01/HI0101='ABJ' or HI01/HI0101='BJ' or HI01/HI0101='APR' or HI01/HI0101='PR' or HI01/HI0101='ABN' or HI01/HI0101='BN' or HI01/HI0101='ABF' or HI01/HI0101='BF']">
    <xsl:for-each select="child::*">
      <xsl:variable name="qualifier" select="*[1]"/>
      <xsl:variable name="code" select="*[2]"/>
      <xsl:variable name="poiIndicator" select="*[9]"/>
      <Diagnosis>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="$qualifier"/>
        </xsl:attribute>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
        <xsl:attribute name="PoiIndicator">
          <xsl:value-of select="$poiIndicator"/>
        </xsl:attribute>
      </Diagnosis>
    </xsl:for-each>
  </xsl:template>

  <!-- Service Line Loop 2400 -->
  <xsl:template match="Loop[@LoopId='2400']">
    <ServiceLine>
      <xsl:if test="PS1">
        <xsl:attribute name="PurchasedServiceIdentifier">
          <xsl:value-of select="PS1/PS101"/>
        </xsl:attribute>
        <xsl:attribute name="PurchasedServiceAmount">
          <xsl:value-of select="PS1/PS102"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="LineNumber">
        <xsl:value-of select="LX/LX01"/>
      </xsl:attribute>
      <xsl:for-each select="SV1">
        <xsl:attribute name="ChargeAmount">
          <xsl:value-of select="SV102"/>
        </xsl:attribute>
        <xsl:attribute name="Quantity">
          <xsl:value-of select="SV104"/>
        </xsl:attribute>
        <xsl:if test="string-length(SV103)>0">
          <xsl:attribute name="Unit">
            <xsl:value-of select="SV103"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV109)>0">
          <xsl:attribute name="EmergencyIndicator">
            <xsl:value-of select="SV109"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV111)>0">
          <xsl:attribute name="EpsdtIndicator">
            <xsl:value-of select="SV111"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:choose>
          <xsl:when test="string-length(SV107/SV10701)>0">
            <xsl:attribute name="DiagnosisCodePointer1">
              <xsl:value-of select="SV107/SV10701"/>
            </xsl:attribute>
            <xsl:if test="string-length(SV107/SV10702)>0">
              <xsl:attribute name="DiagnosisCodePointer2">
                <xsl:value-of select="SV107/SV10702"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="string-length(SV107/SV10703)>0">
              <xsl:attribute name="DiagnosisCodePointer3">
                <xsl:value-of select="SV107/SV10703"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="string-length(SV107/SV10704)>0">
              <xsl:attribute name="DiagnosisCodePointer4">
                <xsl:value-of select="SV107/SV10704"/>
              </xsl:attribute>
            </xsl:if>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="DiagnosisCodePointer1">
              <xsl:value-of select="SV107"/>
            </xsl:attribute>
          </xsl:otherwise>
        </xsl:choose>
        <PlaceOfService>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV105"/>
          </xsl:attribute>
          <xsl:value-of select="SV105/comment()"/>
        </PlaceOfService>
        <xsl:call-template name="MedicalProcedure">
          <xsl:with-param name="element" select="SV101"/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:for-each select="SV2">
        <xsl:attribute name="RevenueCode">
          <xsl:value-of select="SV201"/>
        </xsl:attribute>
        <xsl:attribute name="ChargeAmount">
          <xsl:value-of select="SV203"/>
        </xsl:attribute>
        <xsl:attribute name="Quantity">
          <xsl:value-of select="SV205"/>
        </xsl:attribute>
        <xsl:if test="string-length(SV204)>0">
          <xsl:attribute name="Unit">
            <xsl:value-of select="SV204"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV207)>0">
          <xsl:attribute name="NonCoveredChargeAmount">
            <xsl:value-of select="SV207"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:call-template name="MedicalProcedure">
          <xsl:with-param name="element" select="SV202"/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:for-each select="SV3">
        <xsl:attribute name="ChargeAmount">
          <xsl:value-of select="SV302"/>
        </xsl:attribute>
        <xsl:attribute name="Quantity">
          <xsl:value-of select="SV306"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="string-length(SV311/SV31101)>0">
            <xsl:attribute name="DiagnosisCodePointer1">
              <xsl:value-of select="SV311/SV31101"/>
            </xsl:attribute>
            <xsl:if test="string-length(SV311/SV31102)>0">
              <xsl:attribute name="DiagnosisCodePointer2">
                <xsl:value-of select="SV311/SV31102"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="string-length(SV311/SV31103)>0">
              <xsl:attribute name="DiagnosisCodePointer3">
                <xsl:value-of select="SV311/SV31103"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="string-length(SV311/SV31104)>0">
              <xsl:attribute name="DiagnosisCodePointer4">
                <xsl:value-of select="SV311/SV31104"/>
              </xsl:attribute>
            </xsl:if>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="DiagnosisCodePointer1">
              <xsl:value-of select="SV311"/>
            </xsl:attribute>
          </xsl:otherwise>
        </xsl:choose>
        <PlaceOfService>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV303"/>
          </xsl:attribute>
          <xsl:value-of select="SV303/comment()"/>
        </PlaceOfService>
        <xsl:call-template name="MedicalProcedure">
          <xsl:with-param name="element" select="SV301"/>
        </xsl:call-template>
        <xsl:if test="string-length(SV304/SV30401)>0">
          <OralCavityDesignation>
            <xsl:attribute name="Code">
              <xsl:value-of select="SV304/SV30401"/>
            </xsl:attribute>
          </OralCavityDesignation>
        </xsl:if>
        <xsl:if test="string-length(SV304/SV30402)>0">
          <OralCavityDesignation>
            <xsl:attribute name="Code">
              <xsl:value-of select="SV304/SV30402"/>
            </xsl:attribute>
          </OralCavityDesignation>
        </xsl:if>
        <xsl:if test="string-length(SV304/SV30403)>0">
          <OralCavityDesignation>
            <xsl:attribute name="Code">
              <xsl:value-of select="SV304/SV30403"/>
            </xsl:attribute>
          </OralCavityDesignation>
        </xsl:if>
        <xsl:if test="string-length(SV304/SV30404)>0">
          <OralCavityDesignation>
            <xsl:attribute name="Code">
              <xsl:value-of select="SV304/SV30404"/>
            </xsl:attribute>
          </OralCavityDesignation>
        </xsl:if>
        <xsl:if test="string-length(SV304/SV30405)>0">
          <OralCavityDesignation>
            <xsl:attribute name="Code">
              <xsl:value-of select="SV304/SV30405"/>
            </xsl:attribute>
          </OralCavityDesignation>
        </xsl:if>
      </xsl:for-each>
      <xsl:apply-templates select="TOO"/>
      <xsl:apply-templates select="PWK"/>
      <xsl:apply-templates select="DTP"/>
      <!-- Grab service date from claim level if not specified at the line level -->
      <xsl:if test="count(DTP[DTP01='472'])=0">
        <xsl:apply-templates select="../DTP[DTP01='472']"/>
      </xsl:if>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="AMT"/>
      <xsl:apply-templates select="NTE"/>
      <xsl:apply-templates select="Loop"/>
    </ServiceLine>
  </xsl:template>

  <!-- Hierarchical Loops -->
  <xsl:template name="BillingProviderHLoop">
    <xsl:param name="HLoop"/>
    <BillingInfo>
      <xsl:apply-templates select="$HLoop/Loop"/>
    </BillingInfo>
  </xsl:template>
  
  <!-- Hierarchical Loops -->
  <xsl:template name="SubmitterLoop">
    <xsl:param name="HLoop"/>
    <SubmitterInfo>
      <xsl:apply-templates select="$HLoop/Loop"/>
    </SubmitterInfo>
  </xsl:template>
  
  <!-- Hierarchical Loops -->
  <xsl:template name="ProviderInfoLoop">
    <xsl:param name="HLoop"/>
      <xsl:apply-templates select="$HLoop/PRV"/>
  </xsl:template>

  <!-- XXX: This loop is not completely filled out! -->
  <xsl:template name="PatientHLoop">
    <xsl:param name="HLoop"/>

    <xsl:for-each select="$HLoop/Loop[@LoopId='2010CA']">
      <Patient>
        <xsl:if test="count(DMG) > 0">
          <xsl:attribute name="Gender">
            <xsl:choose>
              <xsl:when test="DMG/DMG03='F'">Female</xsl:when>
              <xsl:when test="DMG/DMG03='M'">Male</xsl:when>
              <xsl:otherwise>Unknown</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="DateOfBirth">
            <xsl:value-of select="concat(substring(DMG/DMG02,1,4),'-',substring(DMG/DMG02,5,2),'-',substring(DMG/DMG02,7,2))"/>
          </xsl:attribute>
        </xsl:if>
        <Relationship>
          <xsl:attribute name="Code">
            <xsl:value-of select="../PAT/PAT01"/>
          </xsl:attribute>
        </Relationship>
        <Name>
          <xsl:call-template name="EntityName">
            <xsl:with-param name="Loop" select="."/>
          </xsl:call-template>
        </Name>
        <xsl:for-each select="N3">
          <Address>
            <xsl:call-template name="PostalAddress">
              <xsl:with-param name="N3" select="."/>
            </xsl:call-template>
          </Address>
        </xsl:for-each>
      </Patient>
    </xsl:for-each>
  </xsl:template>

  
  <xsl:template match="SBR">
    <SubscriberInformation>
      <xsl:attribute name="PayerResponsibilitySequenceNumberCode">
        <xsl:value-of select="SBR01"/>
      </xsl:attribute>
      <xsl:attribute name="IndividualRelationshipCode">
        <xsl:value-of select="SBR02"/>
      </xsl:attribute>
      <xsl:attribute name="ReferenceIdentification">
        <xsl:value-of select="SBR03"/>
      </xsl:attribute>
      <xsl:attribute name="Name">
        <xsl:value-of select="SBR04"/>
      </xsl:attribute>
      <xsl:attribute name="InsuranceTypeCode">
        <xsl:value-of select="SBR05"/>
      </xsl:attribute>
      <xsl:attribute name="CoordinationOfBenefitsCode">
        <xsl:value-of select="SBR06"/>
      </xsl:attribute>
      <xsl:attribute name="YesNoConditionResponseCode">
        <xsl:value-of select="SBR07"/>
      </xsl:attribute>
      <xsl:attribute name="EmploymentStatusCode">
        <xsl:value-of select="SBR08"/>
      </xsl:attribute>
      <xsl:attribute name="ClaimFilingIndicatorCode">
        <xsl:value-of select="SBR09"/>
      </xsl:attribute>
    </SubscriberInformation>
  </xsl:template>

  <xsl:template name="SubscriberHLoop">
    <xsl:param name="HLoop"/>
    <!-- Parent is Subscriber Loop -->
    <!-- Capture the subscriber information(specifically the claim file indicator) for filling in CMS-1500, if it exists -->
    <xsl:apply-templates select="$HLoop/SBR"/>
    <xsl:for-each select="$HLoop/Loop[@LoopId='2010BA']">
      <Subscriber>
        <xsl:if test="count(DMG) > 0">
          <xsl:attribute name="Gender">
            <xsl:choose>
              <xsl:when test="DMG/DMG03='F'">Female</xsl:when>
              <xsl:when test="DMG/DMG03='M'">Male</xsl:when>
              <xsl:otherwise>Unknown</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="DateOfBirth">
            <xsl:value-of select="concat(substring(DMG/DMG02,1,4),'-',substring(DMG/DMG02,5,2),'-',substring(DMG/DMG02,7,2))"/>
          </xsl:attribute>
        </xsl:if>
        <Relationship>
          <xsl:attribute name="Code">
            <xsl:value-of select="../SBR/SBR02"/>
          </xsl:attribute>
        </Relationship>
        <Name>
          <xsl:call-template name="EntityName">
            <xsl:with-param name="Loop" select="."/>
          </xsl:call-template>
        </Name>
        <xsl:for-each select="N3">
          <Address>
            <xsl:call-template name="PostalAddress">
              <xsl:with-param name="N3" select="."/>
            </xsl:call-template>
          </Address>
        </xsl:for-each>
      </Subscriber>
    </xsl:for-each>
    <xsl:for-each select="$HLoop/Loop[@LoopId='2010BB']">
      <Payer>
        <Name>
          <xsl:call-template name="EntityName">
            <xsl:with-param name="Loop" select="."/>
          </xsl:call-template>
        </Name>
        <xsl:for-each select="N3">
          <Address>
            <xsl:call-template name="PostalAddress">
              <xsl:with-param name="N3" select="."/>
            </xsl:call-template>
          </Address>
        </xsl:for-each>
        <xsl:apply-templates select="REF"/>
      </Payer>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="TOO">
    <ToothInformation>
        <xsl:attribute name="ToothCode">
          <xsl:value-of select="TOO02"/>
        </xsl:attribute>
      <xsl:if test="string-length(TOO03)>0 and string-length(TOO03/TOO0301)=0">
        <ToothSurface>
          <xsl:attribute name="Code">
            <xsl:value-of select="TOO03"/>
          </xsl:attribute>
        </ToothSurface>
      </xsl:if>
      <xsl:if test="string-length(TOO03/TOO0301)>0">
        <ToothSurface>
          <xsl:attribute name="Code">
            <xsl:value-of select="TOO03/TOO0301"/>
          </xsl:attribute>
        </ToothSurface>
      </xsl:if>
      <xsl:if test="string-length(TOO03/TOO0302)>0">
        <ToothSurface>
          <xsl:attribute name="Code">
            <xsl:value-of select="TOO03/TOO0302"/>
          </xsl:attribute>
        </ToothSurface>
      </xsl:if>
      <xsl:if test="string-length(TOO03/TOO0303)>0">
        <ToothSurface>
          <xsl:attribute name="Code">
            <xsl:value-of select="TOO03/TOO0303"/>
          </xsl:attribute>
        </ToothSurface>
      </xsl:if>
      <xsl:if test="string-length(TOO03/TOO0304)>0">
        <ToothSurface>
          <xsl:attribute name="Code">
            <xsl:value-of select="TOO03/TOO0304"/>
          </xsl:attribute>
        </ToothSurface>
      </xsl:if>
    </ToothInformation>
  </xsl:template>

  <!-- Common Templates -->
  <xsl:template match="AMT">
    <Amount>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="AMT01"/>
      </xsl:attribute>
      <xsl:attribute name="Amount">
        <xsl:value-of select="AMT02"/>
      </xsl:attribute>
    </Amount>
  </xsl:template>

  <xsl:template match="CAS">
    <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
      <xsl:attribute name="ReasonCode">
        <xsl:value-of select="CAS02"/>
      </xsl:attribute>
      <xsl:attribute name="Amount">
        <xsl:choose>
          <xsl:when test="string-length(CAS03)>0">
            <xsl:value-of select="CAS03"/>
          </xsl:when>
          <xsl:otherwise>0</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:if test="string-length(CAS04)>0">
        <xsl:attribute name="Quantity">
          <xsl:value-of select="CAS04"/>
        </xsl:attribute>
      </xsl:if>
    </Adjustment>
    <xsl:if test="string-length(CAS05)>0">
      <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:attribute name="ReasonCode">
          <xsl:value-of select="CAS05"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:choose>
            <xsl:when test="string-length(CAS06)>0">
              <xsl:value-of select="CAS06"/>
            </xsl:when>
            <xsl:otherwise>0</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(CAS07)>0">
          <xsl:attribute name="Quantity">
            <xsl:value-of select="CAS07"/>
          </xsl:attribute>
        </xsl:if>
      </Adjustment>
    </xsl:if>
    <xsl:if test="string-length(CAS08)>0">
      <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:attribute name="ReasonCode">
          <xsl:value-of select="CAS08"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:choose>
            <xsl:when test="string-length(CAS09)>0">
              <xsl:value-of select="CAS09"/>
            </xsl:when>
            <xsl:otherwise>0</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(CAS10)>0">
          <xsl:attribute name="Quantity">
            <xsl:value-of select="CAS10"/>
          </xsl:attribute>
        </xsl:if>
      </Adjustment>
    </xsl:if>
    <xsl:if test="string-length(CAS11)>0">
      <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:attribute name="ReasonCode">
          <xsl:value-of select="CAS11"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:choose>
            <xsl:when test="string-length(CAS12)>0">
              <xsl:value-of select="CAS12"/>
            </xsl:when>
            <xsl:otherwise>0</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(CAS13)>0">
          <xsl:attribute name="Quantity">
            <xsl:value-of select="CAS13"/>
          </xsl:attribute>
        </xsl:if>
      </Adjustment>
    </xsl:if>
    <xsl:if test="string-length(CAS14)>0">
      <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:attribute name="ReasonCode">
          <xsl:value-of select="CAS14"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:choose>
            <xsl:when test="string-length(CAS15)>0">
              <xsl:value-of select="CAS15"/>
            </xsl:when>
            <xsl:otherwise>0</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(CAS16)>0">
          <xsl:attribute name="Quantity">
            <xsl:value-of select="CAS16"/>
          </xsl:attribute>
        </xsl:if>
      </Adjustment>
    </xsl:if>
    <xsl:if test="string-length(CAS17)>0">
      <Adjustment>
        <xsl:attribute name="GroupCode">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:attribute name="ReasonCode">
          <xsl:value-of select="CAS17"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:choose>
            <xsl:when test="string-length(CAS18)>0">
              <xsl:value-of select="CAS18"/>
            </xsl:when>
            <xsl:otherwise>0</xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
        <xsl:if test="string-length(CAS19)>0">
          <xsl:attribute name="Quantity">
            <xsl:value-of select="CAS19"/>
          </xsl:attribute>
        </xsl:if>
      </Adjustment>
    </xsl:if>

  </xsl:template>

  <xsl:template match="DTP">
    <xsl:choose>
      <xsl:when test="DTP02='RD8'">
        <DateRange>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="DTP01"/>
          </xsl:attribute>
          <xsl:attribute name="BeginDate">
            <xsl:value-of select="concat(substring(DTP03,1,4),'-',substring(DTP03,5,2),'-',substring(DTP03,7,2))"/>
          </xsl:attribute>
          <xsl:attribute name="EndDate">
            <xsl:value-of select="concat(substring(DTP03,10,4),'-',substring(DTP03,14,2),'-',substring(DTP03,16,2))"/>
          </xsl:attribute>
          <xsl:value-of select="DTP01/comment()"/>
        </DateRange>
      </xsl:when>
      <xsl:otherwise>
        <Date>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="DTP01"/>
          </xsl:attribute>
          <xsl:attribute name="Date">
            <xsl:variable name="year" select="substring(DTP03,1,4)"/>
            <xsl:variable name="month" select="substring(DTP03,5,2)"/>
            <xsl:variable name="day" select="substring(DTP03,7,2)"/>
            <xsl:choose>
              <xsl:when test="DTP02='DT'">
                <xsl:choose>
                  <xsl:when test="$year='9999'">
                    <!-- This is usually de-identified data that may contain an invalid date value-->
                    <xsl:value-of select="'9999-12-31'"/>
                  </xsl:when>
                  <xsl:when test="$month='00' and $day='00'">
                    <xsl:value-of select="concat($year,'-01-01T',substring(DTP03,9,2),':',substring(DTP03,11,2),':00')"/>
                  </xsl:when>
                  <xsl:when test="$month='00'">
                    <xsl:value-of select="concat($year,'-01-',$day,'T',substring(DTP03,9,2),':',substring(DTP03,11,2),':00')"/>
                  </xsl:when>
                  <xsl:when test="$day='00'">
                    <xsl:value-of select="concat($year,'-',$month,'-01T',substring(DTP03,9,2),':',substring(DTP03,11,2),':00')"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat($year,'-',$month,'-',$day,'T',substring(DTP03,9,2),':',substring(DTP03,11,2),':00')"/>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:when test="DTP02='TM'">
                <xsl:choose>
                  <xsl:when test="substring(DTP03,1,2)='99'">
                    <!-- This is usually de-identified data that may contain an invalid date value-->
                    <xsl:value-of select="'0001-01-01T00:00:00'"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat('0001-01-01T', substring(DTP03,1,2),':',substring(DTP03,3,2),':00')"/>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="concat(substring(DTP03,1,4),'-',substring(DTP03,5,2),'-',substring(DTP03,7,2))"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:value-of select="DTP01/comment()"/>
        </Date>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="NTE">
    <Note>
      <xsl:attribute name="Code">
        <xsl:value-of select="NTE01"/>
      </xsl:attribute>
      <xsl:value-of select="NTE02"/>
    </Note>
  </xsl:template>

  <xsl:template match="PRV">
    <ProviderInfo>
      <xsl:attribute name="ProviderCode">
        <xsl:value-of select="PRV01"/>
      </xsl:attribute>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="PRV02"/>
      </xsl:attribute>
      <xsl:attribute name="Id">
        <xsl:value-of select="PRV03"/>
      </xsl:attribute>
    </ProviderInfo>
  </xsl:template>

  <xsl:template match="PWK">
    <SupplementalInformation>
      <ReportType>
        <xsl:attribute name="Code">
          <xsl:value-of select="PWK01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="PWK01='03'">Report Justifying Treatment Beyond Utilization Guidelines</xsl:when>
          <xsl:when test="PWK01='04'">Drugs Administered</xsl:when>
          <xsl:when test="PWK01='05'">Treatment Diagnosis</xsl:when>
          <xsl:when test="PWK01='06'">Initial Assessment</xsl:when>
          <xsl:when test="PWK01='07'">Functional Goals</xsl:when>
          <xsl:when test="PWK01='08'">Plan of Treatment</xsl:when>
          <xsl:when test="PWK01='09'">Progress Report</xsl:when>
          <xsl:when test="PWK01='10'">Continued Treatment</xsl:when>
          <xsl:when test="PWK01='11'">Chemical Analysis</xsl:when>
          <xsl:when test="PWK01='13'">Certified Test Report</xsl:when>
          <xsl:when test="PWK01='15'">Justification for Admission</xsl:when>
          <xsl:when test="PWK01='21'">Recovery Plan</xsl:when>
          <xsl:when test="PWK01='A3'">Allergies/Sensitivities Document</xsl:when>
          <xsl:when test="PWK01='A4'">Autopsy Report</xsl:when>
          <xsl:when test="PWK01='AM'">Ambulance Certification</xsl:when>
          <xsl:when test="PWK01='AS'">Admission Summary</xsl:when>
          <xsl:when test="PWK01='B2'">Prescription</xsl:when>
          <xsl:when test="PWK01='B3'">Physician Order</xsl:when>
          <xsl:when test="PWK01='B4'">Referral Form</xsl:when>
          <xsl:when test="PWK01='BR'">Benchmark Testing Results</xsl:when>
          <xsl:when test="PWK01='BS'">Baseline</xsl:when>
          <xsl:when test="PWK01='BT'">Blanket Test Results</xsl:when>
          <xsl:when test="PWK01='CB'">Chiropractic Justification</xsl:when>
          <xsl:when test="PWK01='CK'">Consent Form(s)</xsl:when>
          <xsl:when test="PWK01='CT'">Certification</xsl:when>
          <xsl:when test="PWK01='D2'">Drug Profile Document</xsl:when>
          <xsl:when test="PWK01='DA'">Dental Models</xsl:when>
          <xsl:when test="PWK01='DB'">Durable Medical Equipment Prescription</xsl:when>
          <xsl:when test="PWK01='DG'">Diagnostic Report</xsl:when>
          <xsl:when test="PWK01='DJ'">Discharge Monitoring Report</xsl:when>
          <xsl:when test="PWK01='DS'">Discharge Summary</xsl:when>
          <xsl:when test="PWK01='EB'">Explanation of Benefits (Coordination of Benefits or Medicare Secondary Payor)</xsl:when>
          <xsl:when test="PWK01='HC'">Health Certificate</xsl:when>
          <xsl:when test="PWK01='HR'">Health Clinic Records</xsl:when>
          <xsl:when test="PWK01='I5'">Immunization Record</xsl:when>
          <xsl:when test="PWK01='IR'">State School Immunization Records</xsl:when>
          <xsl:when test="PWK01='LA'">Laboratory Results</xsl:when>
          <xsl:when test="PWK01='M1'">Medical Record Attachment</xsl:when>
          <xsl:when test="PWK01='MT'">Models</xsl:when>
          <xsl:when test="PWK01='NN'">Nursing Notes</xsl:when>
          <xsl:when test="PWK01='OB'">Operative Note</xsl:when>
          <xsl:when test="PWK01='OC'">Oxygen Content Averaging Report</xsl:when>
          <xsl:when test="PWK01='OD'">Orders and Treatments Document</xsl:when>
          <xsl:when test="PWK01='OE'">Objective Physical Examination (including vitalsigns) Document</xsl:when>
          <xsl:when test="PWK01='OX'">Oxygen Therapy Certification</xsl:when>
          <xsl:when test="PWK01='OZ'">Support Data for Claim</xsl:when>
          <xsl:when test="PWK01='P4'">Pathology Report</xsl:when>
          <xsl:when test="PWK01='P5'">Patient Medical History Document</xsl:when>
          <xsl:when test="PWK01='PE'">Parenteral or Enteral Certification</xsl:when>
          <xsl:when test="PWK01='PN'">Physical Therapy Notes</xsl:when>
          <xsl:when test="PWK01='PO'">Prosthetics or Orthotic Certification</xsl:when>
          <xsl:when test="PWK01='PQ'">Paramedical Results</xsl:when>
          <xsl:when test="PWK01='PY'">Physician’s Report</xsl:when>
          <xsl:when test="PWK01='PZ'">Physical Therapy Certification</xsl:when>
          <xsl:when test="PWK01='RB'">Radiology Films</xsl:when>
          <xsl:when test="PWK01='RR'">Radiology Reports</xsl:when>
          <xsl:when test="PWK01='RT'">Report of Tests and Analysis Report</xsl:when>
          <xsl:when test="PWK01='RX'">Renewable Oxygen Content Averaging Report</xsl:when>
          <xsl:when test="PWK01='SG'">Symptoms Document</xsl:when>
          <xsl:when test="PWK01='V5'">Death Notification</xsl:when>
          <xsl:when test="PWK01='XP'">Photographs</xsl:when>
        </xsl:choose>
      </ReportType>
      <ReportTransmission>
        <xsl:attribute name="Code">
          <xsl:value-of select="PWK02"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="PWK02='AA'">Available on Request at Provider Site</xsl:when>
          <xsl:when test="PWK02='BM'">By Mail</xsl:when>
          <xsl:when test="PWK02='EL'">Electronically Only</xsl:when>
          <xsl:when test="PWK02='EM'">E-Mail</xsl:when>
          <xsl:when test="PWK02='FT'">File Transfer</xsl:when>
          <xsl:when test="PWK02='FX'">By Fax</xsl:when>
        </xsl:choose>
      </ReportTransmission>
      <Identification>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="PWK05"/>
        </xsl:attribute>
        <xsl:attribute name="Id">
          <xsl:value-of select="PWK06"/>
        </xsl:attribute>
      </Identification>
    </SupplementalInformation>
  </xsl:template>

  <xsl:template match="REF">
    <Identification>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="REF01"/>
      </xsl:attribute>
      <xsl:attribute name="Id">
        <xsl:value-of select="REF02"/>
      </xsl:attribute>
      <xsl:choose>
        <xsl:when test="string-length(REF03) > 0">
          <xsl:value-of select="REF03"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="REF01/comment()"/>
        </xsl:otherwise>
      </xsl:choose>
    </Identification>
  </xsl:template>

  <!-- Provider -->
  <xsl:template match="Loop[count(NM1[NM101='85' or NM101='41' or NM101='87' or NM101='PE' or NM101='PR' or NM101='71' or NM101='72' or NM101='ZZ' or NM101='82' or NM101='77' or NM101='DN'])>0]">
    <Provider>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="."/>
        </xsl:call-template>
      </Name>
      <xsl:for-each select="N3">
        <Address>
          <xsl:call-template name="PostalAddress">
            <xsl:with-param name="N3" select="."/>
          </xsl:call-template>
        </Address>
      </xsl:for-each>
      <xsl:apply-templates select="REF"/>
      <xsl:for-each select="PER">
        <Contact>
          <xsl:call-template name="Contact">
            <xsl:with-param name="PER" select="."/>
          </xsl:call-template>
        </Contact>
      </xsl:for-each>
      <xsl:apply-templates select="PRV"/>
    </Provider>
  </xsl:template>

  <!-- Drug Identification -->
  <xsl:template match="Loop[@LoopId='2410']">
    <Drug>
      <xsl:attribute name="Ndc">
        <xsl:value-of select="LIN/LIN03"/>
      </xsl:attribute>
      <xsl:attribute name="Quantity">
        <xsl:choose>
          <xsl:when test="string-length(CTP/CTP04)>0">
            <xsl:value-of select="CTP/CTP04"/>
          </xsl:when>
          <xsl:otherwise>0</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <UnitOfMeasure>
        <xsl:attribute name="Code">
          <xsl:value-of select="CTP/CTP05"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="CTP/CTP05='F2'">International Unit</xsl:when>
          <xsl:when test="CTP/CTP05='GR'">Gram</xsl:when>
          <xsl:when test="CTP/CTP05='ME'">Milligram</xsl:when>
          <xsl:when test="CTP/CTP05='ML'">Milliliter</xsl:when>
          <xsl:when test="CTP/CTP05='UN'">Unit</xsl:when>
        </xsl:choose>
      </UnitOfMeasure>
      <xsl:apply-templates select="REF"/>
    </Drug>
  </xsl:template>
  
  <!-- Line Adjustment Information-->
  <xsl:template match="Loop[@LoopId='2430']">
    <LineAdjustmentInformation>
      <xsl:attribute name="OtherPayerPrimaryIdentifier">
        <xsl:value-of select="SVD/SVD01"/>
      </xsl:attribute>
      <xsl:if test="string-length(SVD/SVD02)>0">
        <xsl:attribute name="ServiceLinePaidAmount">
          <xsl:value-of select="SVD/SVD02"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SVD/SVD05)>0">
        <xsl:attribute name="PaidServiceUnitCount">
          <xsl:value-of select="SVD/SVD05"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SVD/SVD06)>0">
        <xsl:attribute name="BundledLineNumber">
          <xsl:value-of select="SVD/SVD06"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:call-template name="MedicalProcedure">
        <xsl:with-param name="element" select="SVD/SVD03"/>
      </xsl:call-template>
      <xsl:apply-templates select="CAS"/>
      <xsl:apply-templates select="DTP"/>
      <xsl:apply-templates select="AMT"/>
    </LineAdjustmentInformation>
  </xsl:template>

  <xsl:template name="Member">
    <xsl:param name="Loop"/>
    <xsl:if test="count($Loop/DMG)>0">
      <xsl:attribute name="Gender">
        <xsl:choose>
          <xsl:when test="$Loop/DMG/DMG03='M'">Male</xsl:when>
          <xsl:when test="$Loop/DMG/DMG03='F'">Female</xsl:when>
          <xsl:otherwise>Unknown</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="DateOfBirth">
        <xsl:value-of select="concat(substring($Loop/DMG/DMG02,1,4),'-',substring($Loop/DMG/DMG02,5,2),'-',substring($Loop/DMG/DMG02,7,2))"/>
      </xsl:attribute>
    </xsl:if>
    <Name>
      <xsl:call-template name="EntityName">
        <xsl:with-param name="Loop" select="$Loop"/>
      </xsl:call-template>
    </Name>
    <xsl:for-each select="$Loop/N3">
      <Address>
        <xsl:call-template name="PostalAddress">
          <xsl:with-param name="N3" select="." />
        </xsl:call-template>
      </Address>
    </xsl:for-each>
    <xsl:apply-templates select="DTP"/>
  </xsl:template>

  <xsl:template name="EntityName">
    <xsl:param name="Loop"/>
    <xsl:if test="$Loop/REF[REF01='G1']">
      <xsl:attribute name="PriorAuthorizationNumber">
        <xsl:value-of select="$Loop/REF[REF01='G1']/REF02"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:attribute name="LastName">
      <xsl:value-of select="$Loop/NM1/NM103"/>
    </xsl:attribute>
    <xsl:if test="$Loop/NM1/NM102='1'">
      <xsl:attribute name="FirstName">
        <xsl:value-of select="$Loop/NM1/NM104"/>
      </xsl:attribute>
      <xsl:if test="string-length($Loop/NM1/NM105)>0">
        <xsl:attribute name="MiddleName">
          <xsl:value-of select="$Loop/NM1/NM105"/>
        </xsl:attribute>
      </xsl:if>
    </xsl:if>
    <Type>
      <xsl:attribute name="Identifier">
        <xsl:value-of select="$Loop/NM1/NM101"/>
      </xsl:attribute>
      <xsl:attribute name="Qualifier">
        <xsl:choose>
          <xsl:when test="$Loop/NM1/NM102='1'">Person</xsl:when>
          <xsl:otherwise>NonPerson</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:value-of select="$Loop/NM1/NM101/comment()"/>
    </Type>
    <Identification>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="$Loop/NM1/NM108" />
      </xsl:attribute>
      <xsl:attribute name="Id">
        <xsl:value-of select="$Loop/NM1/NM109"/>
      </xsl:attribute>
      <xsl:value-of select="$Loop/NM1/NM108/comment()"/>
    </Identification>
  </xsl:template>

  <xsl:template name="PostalAddress">
    <xsl:param name="N3"/>
    <xsl:attribute name="City">
      <xsl:value-of select="$N3/.././N4/N401"/>
    </xsl:attribute>
    <xsl:attribute name="StateCode">
      <xsl:value-of select="$N3/.././N4/N402"/>
    </xsl:attribute>
    <xsl:attribute name="PostalCode">
      <xsl:value-of select="$N3/.././N4/N403"/>
    </xsl:attribute>
    <Line1>
      <xsl:value-of select="$N3/N301"/>
    </Line1>
    <xsl:if test="string-length($N3/N302)>0">
      <Line2>
        <xsl:value-of select="$N3/N302"/>
      </Line2>
    </xsl:if>
  </xsl:template>

  <xsl:template name="Contact">
    <xsl:param name="PER"/>
    <xsl:attribute name="FunctionCode">
      <xsl:value-of select="PER01"/>
    </xsl:attribute>
    <xsl:if test="string-length(PER02)>0">
      <Name>
        <xsl:value-of select="PER02"/>
      </Name>
    </xsl:if>
    <xsl:if test="string-length(PER04)>0">
      <Number>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="PER03"/>
        </xsl:attribute>
        <xsl:value-of select="PER04"/>
      </Number>
    </xsl:if>
    <xsl:if test="string-length(PER06)>0">
      <Number>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="PER05"/>
        </xsl:attribute>
        <xsl:value-of select="PER06"/>
      </Number>
    </xsl:if>
    <xsl:if test="string-length(PER08)>0">
      <Number>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="PER07"/>
        </xsl:attribute>
        <xsl:value-of select="PER08"/>
      </Number>
    </xsl:if>
  </xsl:template>

  <xsl:template name="MedicalProcedure">
    <xsl:param name="element"/>
    <Procedure>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="$element/*[1]"/>
      </xsl:attribute>
      <xsl:attribute name="ProcedureCode">
        <xsl:value-of select="$element/*[2]"/>
      </xsl:attribute>
      <xsl:if test="string-length($element/*[3])>0">
        <xsl:attribute name="Modifier1">
          <xsl:value-of select="$element/*[3]"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length($element/*[4])>0">
        <xsl:attribute name="Modifier2">
          <xsl:value-of select="$element/*[4]"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length($element/*[5])>0">
        <xsl:attribute name="Modifier3">
          <xsl:value-of select="$element/*[5]"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length($element/*[6])>0">
        <xsl:attribute name="Modifier4">
          <xsl:value-of select="$element/*[6]"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:value-of select="$element/*[7]"/>
    </Procedure>
  </xsl:template>

</xsl:stylesheet>
