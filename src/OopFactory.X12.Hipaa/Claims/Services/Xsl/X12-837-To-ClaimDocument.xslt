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
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="count(Loop/SV1) > 0">Professional</xsl:when>
          <xsl:when test="count(Loop/SV2) > 0">Institutional</xsl:when>
          <xsl:when test="count(Loop/SV3) > 0">Dental</xsl:when>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="PatientControlNumber">
        <xsl:value-of select="CLM/CLM01"/>
      </xsl:attribute>
      <xsl:attribute name="TotalClaimChargeAmount">
        <xsl:value-of select="CLM/CLM02"/>
      </xsl:attribute>
      <xsl:attribute name="MedicalRecordNumber">
        <xsl:value-of select="REF[REF01='EA']/REF02"/>
      </xsl:attribute>
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

      <xsl:variable name="ParentLoopId" select="../@LoopId"/>
      <xsl:choose>
        
        <xsl:when test="$ParentLoopId = '2000B'"> <!-- Parent is Subscriber Loop -->
          <xsl:call-template name="BillingProviderHLoop">
            <xsl:with-param name="HLoop" select="../../."/>
          </xsl:call-template>
          <xsl:call-template name="SubscriberHLoop">
            <xsl:with-param name="HLoop" select="../."/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="$ParentLoopId = '2000C'"> <!-- Parent is Patient Loop -->
          <xsl:call-template name="BillingProviderHLoop">
            <xsl:with-param name="HLoop" select="../../../."/>
          </xsl:call-template>
          <xsl:call-template name="SubscriberHLoop">
            <xsl:with-param name="HLoop" select="../../."/>
          </xsl:call-template>
        </xsl:when>
      </xsl:choose>

      <xsl:for-each select="DTP">
        <xsl:call-template name="DTPSegment">
          <xsl:with-param name="DTP" select="."/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:apply-templates select="NTE"/>
      <xsl:apply-templates select="HI"/>
      <xsl:for-each select="Loop[count(NM1)>0]">
        <xsl:call-template name="Provider">
          <xsl:with-param name="Loop" select="."/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:apply-templates select="Loop[@LoopId='2400']"/>
    </Claim>
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
        <xsl:attribute name="Amount">
          <xsl:value-of select="$amount"/>
        </xsl:attribute>
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

  <xsl:template match="HI[HI01/HI0101='ABK' or HI01/HI0101='BK' or HI01/HI0101='ABJ' or HI01/HI0101='BJ' or HI01/HI0101='APR' or HI01/HI0101='PR' or HI01/HI0101='ABN' or HI01/HI0101='BN' or HI01/HI0101='ABF' or HI01/HI0101='BF']">
    <xsl:for-each select="child::*">
      <xsl:variable name="qualifier" select="*[1]"/>
      <xsl:variable name="code" select="*[2]"/>
      <Diagnosis>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="$qualifier"/>
        </xsl:attribute>
        <xsl:attribute name="Code">
          <xsl:value-of select="$code"/>
        </xsl:attribute>
      </Diagnosis>
    </xsl:for-each>
  </xsl:template>

  <!-- Service Line Loop 2400 -->
  <xsl:template match="Loop[@LoopId='2400']">
    <ServiceLine>
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
        <Procedure>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="SV101/SV10101"/>
          </xsl:attribute>
          <xsl:attribute name="ProcedureCode">
            <xsl:value-of select="SV101/SV10102"/>
          </xsl:attribute>
          <xsl:if test="string-length(SV101/SV10103)>0">
            <xsl:attribute name="Modifier1">
              <xsl:value-of select="SV101/SV10103"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV101/SV10104)>0">
            <xsl:attribute name="Modifier2">
              <xsl:value-of select="SV101/SV10104"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV101/SV10105)>0">
            <xsl:attribute name="Modifier3">
              <xsl:value-of select="SV101/SV10105"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV101/SV10106)>0">
            <xsl:attribute name="Modifier4">
              <xsl:value-of select="SV101/SV10106"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="SV101/SV10107"/>
        </Procedure>
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
        <Procedure>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="SV202/SV20201"/>
          </xsl:attribute>
          <xsl:attribute name="ProcedureCode">
            <xsl:value-of select="SV202/SV20202"/>
          </xsl:attribute>
          <xsl:if test="string-length(SV202/SV20203)>0">
            <xsl:attribute name="Modifier1">
              <xsl:value-of select="SV202/SV20203"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV202/SV20204)>0">
            <xsl:attribute name="Modifier2">
              <xsl:value-of select="SV202/SV20204"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV202/SV20205)>0">
            <xsl:attribute name="Modifier3">
              <xsl:value-of select="SV202/SV20205"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="string-length(SV202/SV20206)>0">
            <xsl:attribute name="Modifier4">
              <xsl:value-of select="SV202/SV20206"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="SV202/SV20207"/>
        </Procedure>
      </xsl:for-each>
      <xsl:for-each select="DTP">
        <xsl:call-template name="DTPSegment">
          <xsl:with-param name="DTP" select="."/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:for-each select="REF">
        <Identification>
        <xsl:call-template name="Identification">
          <xsl:with-param name="REF" select="."/>
        </xsl:call-template>
        </Identification>
      </xsl:for-each>
      <xsl:apply-templates select="NTE"/>
      <xsl:for-each select="Loop">
        <xsl:call-template name="Provider">
          <xsl:with-param name="Loop" select="."/>
        </xsl:call-template>
      </xsl:for-each>
    </ServiceLine>
  </xsl:template>

  <!-- Hierarchical Loops -->
  <xsl:template name="BillingProviderHLoop">
    <xsl:param name="HLoop"/>
    <BillingInfo>
      <xsl:for-each select="$HLoop/Loop[count(NM1)>0]">
        <xsl:call-template name="Provider">
          <xsl:with-param name="Loop" select="."/>
        </xsl:call-template>
      </xsl:for-each>
    </BillingInfo>
  </xsl:template>  

  <xsl:template name="SubscriberHLoop">
    <xsl:param name="HLoop"/>
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
  </xsl:template>

  <!-- Common Templates -->

  <xsl:template match="NTE">
    <Note>
      <xsl:attribute name="Code">
        <xsl:value-of select="NTE01"/>
      </xsl:attribute>
      <xsl:value-of select="NTE02"/>
    </Note>
  </xsl:template>

  <xsl:template name="Provider">
    <xsl:param name="Loop"/>
    <Provider>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="$Loop"/>
        </xsl:call-template>
      </Name>
      <xsl:for-each select="$Loop/N3">
        <Address>
          <xsl:call-template name="PostalAddress">
            <xsl:with-param name="N3" select="."/>
          </xsl:call-template>
        </Address>
      </xsl:for-each>
      <xsl:for-each select="REF">
        <Identification>
          <xsl:call-template name="Identification">
            <xsl:with-param name="REF" select="."/>
          </xsl:call-template>
        </Identification>
      </xsl:for-each>
      <xsl:for-each select="$Loop/PER">
        <Contact>
          <xsl:call-template name="Contact">
            <xsl:with-param name="PER" select="."/>
          </xsl:call-template>
        </Contact>
      </xsl:for-each>
      <xsl:for-each select="$Loop/PRV">
        <ProviderInfo>
        <xsl:call-template name="ProviderInformation">
          <xsl:with-param name="PRV" select="."/>
        </xsl:call-template>
        </ProviderInfo>
      </xsl:for-each>
    </Provider>
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
    <xsl:for-each select="$Loop/DTP">
      <xsl:call-template name="DTPSegment">
        <xsl:with-param name="DTP" select="."/>
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="EntityName">
    <xsl:param name="Loop"/>
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

  <xsl:template name="Identification">
    <xsl:param name="REF"/>
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

  <xsl:template name="ProviderInformation">
    <xsl:param name="PRV"/>
    <xsl:attribute name="ProviderCode">
      <xsl:value-of select="PRV01"/>
    </xsl:attribute>
    <xsl:attribute name="Qualifier">
      <xsl:value-of select="PRV02"/>
    </xsl:attribute>
    <xsl:attribute name="Id">
      <xsl:value-of select="PRV03"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template name="DTPSegment">
    <xsl:param name="DTP"/>
    <xsl:choose>
      <xsl:when test="$DTP/DTP02='RD8'">
        <DateRange>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="$DTP/DTP01"/>
          </xsl:attribute>
          <xsl:attribute name="BeginDate">
            <xsl:value-of select="concat(substring($DTP/DTP03,1,4),'-',substring($DTP/DTP03,5,2),'-',substring($DTP/DTP03,7,2))"/>
          </xsl:attribute>
          <xsl:attribute name="EndDate">
            <xsl:value-of select="concat(substring($DTP/DTP03,10,4),'-',substring($DTP/DTP03,14,2),'-',substring($DTP/DTP03,16,2))"/>
          </xsl:attribute>
          <xsl:value-of select="$DTP/DTP01/comment()"/>
        </DateRange>
      </xsl:when>
      <xsl:otherwise>
        <Date>
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="$DTP/DTP01"/>
          </xsl:attribute>
          <xsl:attribute name="Date">
            <xsl:choose>
              <xsl:when test="$DTP/DTP02='DT'">
                <xsl:value-of select="concat(substring($DTP/DTP03,1,4),'-',substring($DTP/DTP03,5,2),'-',substring($DTP/DTP03,7,2),'T',substring($DTP/DTP03,9,2),':',substring($DTP/DTP03,11,2),':00')"/>
              </xsl:when>
              <xsl:when test="$DTP/DTP02='TM'">
                <xsl:value-of select="concat('0001-01-01T', substring($DTP/DTP03,1,2), substring($DTP/DTP03,3,2))"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="concat(substring($DTP/DTP03,1,4),'-',substring($DTP/DTP03,5,2),'-',substring($DTP/DTP03,7,2))"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:value-of select="$DTP/DTP01/comment()"/>
        </Date>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  

</xsl:stylesheet>
