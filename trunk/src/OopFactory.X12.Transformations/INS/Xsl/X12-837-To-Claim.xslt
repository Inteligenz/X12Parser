<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="Loop[substring(@LoopId,1,5)='2010A' or substring(@LoopId,1,4)='2310' or substring(@LoopId,1,4)='2420']">
    <Provider>
      <xsl:copy-of select="@* | node()"/>
    </Provider>
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2010BA']">
    <Subscriber>
      <xsl:copy-of select="@* | node()"/>
    </Subscriber>
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2010BB' or @LoopId='2010BC']">
    <Payer>
      <xsl:copy-of select="@* | node()"/>
    </Payer>
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2010CA']">
    <Patient>
      <xsl:copy-of select="@* | node()"/>
    </Patient>
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2300']">
    <Claim>
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="count(Loop/SV3) > 0">Dental</xsl:when>
          <xsl:when test="count(Loop/SV2) > 0">Institutional</xsl:when>
          <xsl:when test="count(Loop/SV1) > 0">Professional</xsl:when>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="PatientControlNumber">
        <xsl:value-of select="CLM/CLM01"/>
      </xsl:attribute>
      <xsl:attribute name="TotalCharge">
        <xsl:value-of select="CLM/CLM02"/>
      </xsl:attribute>
      <xsl:attribute name="BenefitsAssignmentCertificationIndicator">
        <xsl:value-of select="CLM/CLM08"/>
      </xsl:attribute>
      <PlaceOfService>
        <xsl:attribute name="Code">
          <xsl:value-of select="CLM/CLM05/CLM0501"/>
        </xsl:attribute>
        <xsl:value-of select="CLM/CLM05/CLM0501/comment()"/>
      </PlaceOfService>
      <PatientSignatureSource>
        <xsl:attribute name="Code">
          <xsl:value-of select="CLM/CLM10"/>
        </xsl:attribute>
        <xsl:value-of select="CLM/CLM10/comment()"/>
      </PatientSignatureSource>
      <RelatedCauses>
        <xsl:if test="string-length(CLM/CLM11/CLM1104)>0">
          <xsl:attribute name="State">
            <xsl:value-of select="CLM/CLM11/CLM1104"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(CLM/CLM11/CLM1101)>0">
          <Cause>
            <xsl:attribute name="Code">
              <xsl:value-of select="CLM/CLM11/CLM1101"/>
            </xsl:attribute>
          </Cause>
        </xsl:if>
        <xsl:if test="string-length(CLM/CLM11/CLM1102)>0">
          <Cause>
            <xsl:attribute name="Code">
              <xsl:value-of select="CLM/CLM11/CLM1102"/>
            </xsl:attribute>
          </Cause>
        </xsl:if>
        <xsl:if test="string-length(CLM/CLM11/CLM1103)>0">
          <Cause>
            <xsl:attribute name="Code">
              <xsl:value-of select="CLM/CLM11/CLM1103"/>
            </xsl:attribute>
          </Cause>
        </xsl:if>
      </RelatedCauses>
      <xsl:apply-templates select="node()"/>
    </Claim>
  </xsl:template>
  
  <xsl:template match="Loop[@LoopId='2400']">
    <ServiceLine>
      <xsl:attribute name="Number">
        <xsl:value-of select="LX/LX01"/>
      </xsl:attribute>
      <xsl:apply-templates select="*[not(self::LX)]"/>
    </ServiceLine>    
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2330A']">
    <Subscriber Type="Other">
      <xsl:copy-of select="@* | node()"/>
    </Subscriber>
  </xsl:template>

  <xsl:template match="Date[@Qual='472']">
    <DateOfService>
      <xsl:copy-of select="@*"/>
      <xsl:if test="@Format='D8'">
        <xsl:attribute name="From">
          <xsl:value-of select="."/>
        </xsl:attribute>
        <xsl:attribute name="To">
          <xsl:value-of select="."/>
        </xsl:attribute>
      </xsl:if>
      <xsl:copy-of select="node()"/>
    </DateOfService>
  </xsl:template>

  <xsl:template match="SBR">
    <xsl:comment>
      <xsl:value-of select="SBR01/comment()"/>
    </xsl:comment>
    <PayerResponsibilitySequence>
        <xsl:attribute name="NumberCode">
          <xsl:value-of select="SBR01"/>
        </xsl:attribute>
    </PayerResponsibilitySequence>
    <xsl:comment>
      <xsl:value-of select="SBR02/comment()"/>
    </xsl:comment>
    <IndividualRelationship>
      <xsl:attribute name="Code">
        <xsl:value-of select="SBR02"/>
      </xsl:attribute>
    </IndividualRelationship>
    <xsl:comment>
      <xsl:value-of select="SBR05/comment()"/>
    </xsl:comment>
    <GroupOrPolicy>
      <xsl:attribute name="Number">
        <xsl:value-of select="SBR03"/>
      </xsl:attribute>
      <xsl:attribute name="InsuranceType">
        <xsl:value-of select="SBR05"/>
      </xsl:attribute>
      <xsl:value-of select="SBR04"/>
    </GroupOrPolicy>
    <xsl:comment>
      <xsl:value-of select="SBR09/comment()"/>
    </xsl:comment>
    <ClaimFilingIndicator>
      <xsl:attribute name="Code">
        <xsl:value-of select="SBR09"/>
      </xsl:attribute>
    </ClaimFilingIndicator>
  </xsl:template>

  <xsl:template match="PAT">
    <IndividualRelationship>
      <xsl:attribute name="Code">
        <xsl:value-of select="PAT01"/>
      </xsl:attribute>
    </IndividualRelationship>
    <xsl:if test="string-length(PAT06)>0">
      <DateOfDeath>
        <xsl:value-of select="concat(substring(PAT06,1,4),'-',substring(PAT06,5,2),'-',substring(PAT06,7,2))"/>
      </DateOfDeath>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="HI">
    <xsl:for-each select="child::*">
      <xsl:variable name="qual" select="./child::*[1]"/>
      <xsl:variable name="code" select="./child::*[2]"/>
      <xsl:variable name="date-qual" select="./child::*[3]"/>
      <xsl:variable name="date-val" select="./child::*[4]"/>
      <xsl:comment>
        <xsl:value-of select="$qual/comment()"/>
      </xsl:comment>
      <InformationCode>
        <xsl:attribute name="CodeType">
          <xsl:choose>
            <xsl:when test="$qual='BK' or $qual='BF' or $qual='BJ' or $qual='ZZ' or $qual='BN'">Diagnosis</xsl:when>
            <xsl:when test="$qual='DR'">DiagnosisRelatedGroup</xsl:when>
            <xsl:when test="$qual='BP' or $qual='BR' or $qual='BO' or $qual='BQ'">Procedure</xsl:when>
            <xsl:when test="$qual='BI'">OccurrenceSpan</xsl:when>
            <xsl:when test="$qual='BH'">Occurrence</xsl:when>
            <xsl:when test="$qual='BE'">Value</xsl:when>
            <xsl:when test="$qual='BG'">Condition</xsl:when>
          </xsl:choose>
        </xsl:attribute>
        <xsl:attribute name="Qual">
          <xsl:value-of select="$qual"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="$date-qual='D8'">
            <xsl:attribute name="Date">
              <xsl:value-of select="concat(substring($date-val,1,4),'-',substring($date-val,5,2),'-',substring($date-val,7,2))"/>
            </xsl:attribute>
          </xsl:when>
          <xsl:when test="$date-qual='RD8'">
            <xsl:attribute name="From">
              <xsl:value-of select="concat(substring($date-val,1,4),'-',substring($date-val,5,2),'-',substring($date-val,7,2))"/>
            </xsl:attribute>
            <xsl:attribute name="To">
              <xsl:value-of select="concat(substring($date-val,10,4),'-',substring($date-val,14,2),'-',substring($date-val,16,2))"/>
            </xsl:attribute>
          </xsl:when>
        </xsl:choose>
        <xsl:value-of select="$code"/>
      </InformationCode>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="SV1">
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV102"/>
    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV103"/>
    </xsl:attribute>
    <xsl:attribute name="Quantity">
      <xsl:value-of select="SV104"/>
    </xsl:attribute>
    <xsl:attribute name="EmergencyIndicator">
      <xsl:value-of select="SV109"/>
    </xsl:attribute>
    <xsl:attribute name="EPSDTIndicator">
      <xsl:value-of select="SV111"/>
    </xsl:attribute>
    <xsl:attribute name="FamilyPlanningIndicator">
      <xsl:value-of select="SV112"/>
    </xsl:attribute>
    <Procedure>
      <xsl:attribute name="Qual">
        <xsl:value-of select="SV101/SV10101"/>
      </xsl:attribute>
      <xsl:attribute name="Code">
        <xsl:value-of select="SV101/SV10102"/>
      </xsl:attribute>
      <xsl:if test="string-length(SV101/SV10103)>0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV101/SV10103"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10104)>0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV101/SV10104"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10105)>0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV101/SV10105"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10106)>0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV101/SV10106"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
    </Procedure>
    <CompositeDiagnosis>
      <xsl:choose>
        <xsl:when test="string-length(SV107/SV10701)> 0">
          <Diagnosis>
            <xsl:attribute name="Pointer">
              <xsl:value-of select="SV107/SV10701"/>
            </xsl:attribute>
          </Diagnosis>
        </xsl:when>
        <xsl:otherwise>
          <Diagnosis>
            <xsl:attribute name="Pointer">
              <xsl:value-of select="SV107"/>
            </xsl:attribute>
          </Diagnosis>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="string-length(SV107/SV10702) > 0">
        <Diagnosis>
          <xsl:attribute name="Pointer">
            <xsl:value-of select="SV107/SV10702"/>
          </xsl:attribute>
        </Diagnosis>
      </xsl:if>
      <xsl:if test="string-length(SV107/SV10703) > 0">
        <Diagnosis>
          <xsl:attribute name="Pointer">
            <xsl:value-of select="SV107/SV10703"/>
          </xsl:attribute>
        </Diagnosis>
      </xsl:if>
      <xsl:if test="string-length(SV107/SV10704) > 0">
        <Diagnosis>
          <xsl:attribute name="Pointer">
            <xsl:value-of select="SV107/SV10704"/>
          </xsl:attribute>
        </Diagnosis>
      </xsl:if>
    </CompositeDiagnosis>
    <xsl:if test="string-length(SV105)>0">
      <PlaceOfService>
        <xsl:attribute name="Code">
          <xsl:value-of select="SV105"/>
        </xsl:attribute>
        <xsl:value-of select="SV105/comment()"/>
      </PlaceOfService>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="SV2">
    <xsl:attribute name="RevenueCode">
      <xsl:value-of select="SV201"/>
    </xsl:attribute>
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV203"/>
    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV204"/>
    </xsl:attribute>
    <xsl:attribute name="Quantity">
      <xsl:value-of select="SV205"/>
    </xsl:attribute>
    <xsl:if test="string-length(SV207)>0">
      <xsl:attribute name="NonCoveredServiceAmount">
        <xsl:value-of select="SV207"/>
      </xsl:attribute>
    </xsl:if>
    <Procedure>
      <xsl:attribute name="Qual">
        <xsl:value-of select="SV202/SV20201"/>
      </xsl:attribute>
      <xsl:attribute name="Code">
      <xsl:value-of select="SV202/SV20202"/>
      </xsl:attribute>
      <xsl:if test="count(SV202/SV20203) > 0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV202/SV20203"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="count(SV202/SV20204) > 0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV202/SV20204"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="count(SV202/SV20205) > 0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV202/SV20205"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="count(SV202/SV20206) > 0">
        <Modifier>
          <xsl:attribute name="Code">
            <xsl:value-of select="SV202/SV20206"/>
          </xsl:attribute>
        </Modifier>
      </xsl:if>
      <xsl:if test="count(SV202/SV20207) > 0">
        <Description>
            <xsl:value-of select="SV202/SV20207"/>
        </Description>
      </xsl:if>

    </Procedure>
  </xsl:template>
  
</xsl:stylesheet>
