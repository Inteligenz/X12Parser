<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="verbose" />

  <xsl:template name="FormatD8Date">
    <xsl:param name="Date" />
    <!-- new date format 1944-05-28 -->
    <xsl:variable name="year">
      <xsl:value-of select="substring($Date,1,4)" />
    </xsl:variable>
    <xsl:variable name="mo">
      <xsl:value-of select="substring($Date,5,2)" />
    </xsl:variable>
    <xsl:variable name="day">
      <xsl:value-of select="substring($Date,7,2)" />
    </xsl:variable>
    <xsl:value-of select="$year"/>-<xsl:value-of select="$mo"/>-<xsl:value-of select="$day"/>
  </xsl:template>

  <xsl:template name="FormatD8DateTime">
    <xsl:param name="Date" />
    <xsl:param name="Time" />
    <!-- new date format 1944-05-28 -->
    <xsl:variable name="year">
      <xsl:value-of select="substring($Date,1,4)" />
    </xsl:variable>
    <xsl:variable name="mo">
      <xsl:value-of select="substring($Date,5,2)" />
    </xsl:variable>
    <xsl:variable name="day">
      <xsl:value-of select="substring($Date,7,2)" />
    </xsl:variable>
    <xsl:variable name="hour">
      <xsl:value-of select="substring($Time,1,2)" />
    </xsl:variable>
    <xsl:variable name="minute">
      <xsl:value-of select="substring($Time,3,2)"/>
    </xsl:variable>
    <xsl:variable name="second">
      <xsl:choose>
        <xsl:when test="string-length($Time) >= 6">
          <xsl:value-of select="substring($Time,5,2)"/>
        </xsl:when>
        <xsl:otherwise>00</xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:variable name="decimal">
      <xsl:choose>
        <xsl:when test="string-length($Time) = 7">
          <xsl:value-of select="substring($Time,7,1)"/>0000
        </xsl:when>
        <xsl:when test="string-length($Time) = 8">
          <xsl:value-of select="substring($Time,7,2)"/>000
        </xsl:when>
        <xsl:otherwise>00000</xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:value-of select="$year"/>-<xsl:value-of select="$mo"/>-<xsl:value-of select="$day"/>T<xsl:value-of select="$hour"/>:<xsl:value-of select="$minute"/>:<xsl:value-of select="$second"/>.<xsl:value-of select="$decimal"/>
  </xsl:template>

  <!-- Interchange Control Header Segment -->
  <xsl:template match="ISA">
    <InterchangeControl>
      <xsl:attribute name="SenderId">
        <xsl:value-of select="normalize-space(ISA06)"/>
      </xsl:attribute>
      <xsl:attribute name="ReceiverId">
        <xsl:value-of select="normalize-space(ISA08)"/>
      </xsl:attribute>
      <xsl:attribute name="Date">
        <xsl:call-template name="FormatD8DateTime">
          <xsl:with-param name="Date" select="concat('20',ISA09)"/>
          <xsl:with-param name="Time" select="ISA10"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ISA13"/>
      </xsl:attribute>
      <xsl:attribute name="UsageIndicator">
        <xsl:value-of select="ISA15"/>
      </xsl:attribute>
    </InterchangeControl>
  </xsl:template>

  <!-- Function Group Header Segment -->
  <xsl:template match="GS">
    <FunctionGroup>
      <xsl:attribute name="SenderCode">
        <xsl:value-of select="normalize-space(GS02)"/>
      </xsl:attribute>
      <xsl:attribute name="ReceiverCode">
        <xsl:value-of select="normalize-space(GS03)"/>
      </xsl:attribute>
      <xsl:attribute name="Date">
        <xsl:call-template name="FormatD8DateTime">
          <xsl:with-param name="Date" select="GS04"/>
          <xsl:with-param name="Time" select="GS05"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="GS06"/>
      </xsl:attribute>
      <xsl:attribute name="Version">
        <xsl:value-of select="GS08"/>
      </xsl:attribute>
    </FunctionGroup>
  </xsl:template>

  <!-- Transaction Set Header Segment -->
  <xsl:template match="ST">
    <TransactionSet>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ST02"/>
      </xsl:attribute>
      <xsl:attribute name="CreationDate">
        <xsl:call-template name="FormatD8DateTime">
          <xsl:with-param name="Date" select="../BHT/BHT04"/>
          <xsl:with-param name="Time" select="../BHT/BHT05"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:if test="$verbose=1">
        <Type>
          <xsl:attribute name="Code">
            <xsl:value-of select="../BHT/BHT06"/>
          </xsl:attribute>
          <xsl:choose>
            <xsl:when test="../BHT/BHT06='CH'">Chargeable</xsl:when>
            <xsl:when test="../BHT/BHT06='RP'">Reporting</xsl:when>
          </xsl:choose>
        </Type>
      </xsl:if>
    </TransactionSet>
  </xsl:template>

  <!-- Entity Name Segment -->
  <xsl:template match="NM1">
    <!--Build Identifier attribute for parent node-->
    <xsl:if test="NM108 = '24'">
      <xsl:attribute name="TaxId">
        <xsl:value-of select="NM109"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="NM108 = '34'">
      <xsl:attribute name="SSN">
        <xsl:value-of select="NM109"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="NM108 = '46'">
      <xsl:attribute name="ETIN">
        <xsl:value-of select="NM109"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="NM108 = 'MI'">
      <xsl:attribute name="MemberId">
        <xsl:value-of select="NM109"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="NM108 = 'XX'">
      <xsl:attribute name="NPI">
        <xsl:value-of select="NM109"/>
      </xsl:attribute>
    </xsl:if>
    <Name>
      <xsl:attribute name="IsPerson">
        <xsl:choose>
          <xsl:when test="NM102='1'">true</xsl:when>
          <xsl:otherwise>false</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:if test="string-length(NM106) > 0">
        <xsl:attribute name="Prefix">
          <xsl:value-of select="NM106"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(NM107) > 0">
        <xsl:attribute name="Suffix">
          <xsl:value-of select="NM107"/>
        </xsl:attribute>
      </xsl:if>
      <Last>
        <xsl:value-of select="NM103"/>
      </Last>
      <xsl:if test="string-length(NM104)>0">
        <First>
          <xsl:value-of select="NM104"/>
        </First>
      </xsl:if>
      <xsl:if test="string-length(NM105)>0">
        <Middle>
          <xsl:value-of select="NM105"/>
        </Middle>
      </xsl:if>
    </Name>
  </xsl:template>

  <!-- Address Segment -->
  <xsl:template match="N3">
    <xsl:attribute name="StateCode">
      <xsl:value-of select="../N4/N402"/>
    </xsl:attribute>
    <xsl:attribute name="PostalCode">
      <xsl:value-of select="../N4/N403"/>
    </xsl:attribute>
    <Address1>
      <xsl:value-of select="N301"/>
    </Address1>
    <xsl:if test="string-length(N302)>0">
      <Address2>
        <xsl:value-of select="N302"/>
      </Address2>
    </xsl:if>
    <City>
      <xsl:value-of select="../N4/N401"/>
    </City>
  </xsl:template>


  <!-- Reference Segment -->
  <xsl:template match="REF">
    <Reference>
      <Qualifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="REF01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="REF01='4N'">Speical Payment Reference Number</xsl:when>
          <xsl:when test="REF01='F5'">Medicare Version Code</xsl:when>
          <xsl:when test="REF01='EW'">Mammography Certification Number</xsl:when>
          <xsl:when test="REF01='9F'">Referral Number</xsl:when>
          <xsl:when test="REF01='G1'">Prior Authorization Number</xsl:when>
          <xsl:when test="REF01='F8'">Original Reference Number</xsl:when>
          <xsl:when test="REF01='X4'">Clinical Laboratory Improvement Amendment Number</xsl:when>
          <xsl:when test="REF01='9A'">Repriced Claim Reference Number</xsl:when>
          <xsl:when test="REF01='9C'">Adjusted Repriced Claim Reference Number</xsl:when>
          <xsl:when test="REF01='LX'">Qualified Products List</xsl:when>
          <xsl:when test="REF01='D9'">Claim Number</xsl:when>
          <xsl:when test="REF01='1S'">Abulatory Patient Group (APG) Number</xsl:when>
          <xsl:when test="REF01='EA'">Medical Record Identification Number</xsl:when>
          <xsl:when test="REF01='P4'">Project Code</xsl:when>
        </xsl:choose>
      </Qualifier>
      <Identification>
        <xsl:value-of select="REF02"/>
      </Identification>
    </Reference>
  </xsl:template>
  <!-- Note/Special Instruction Segment -->
  <xsl:template match="NTE">
    <Note>
      <Reference>
        <xsl:attribute name="Code">
          <xsl:value-of select="NTE01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="NTE01='ADD'">Additional Information</xsl:when>
          <xsl:when test="NTE01='CER'">Certification Narrative</xsl:when>
          <xsl:when test="NTE01='DCP'">Goals, Rehabilitation Potential, or Discharge Plans</xsl:when>
          <xsl:when test="NTE01='DGN'">Diagnosis Description</xsl:when>
          <xsl:when test="NTE01='PMT'">Payment</xsl:when>
          <xsl:when test="NTE01='TPO'">Third Party Organization Notes</xsl:when>
        </xsl:choose>
      </Reference>
      <Description>
        <xsl:value-of select="NTE02"/>
      </Description>
    </Note>
  </xsl:template>

  <!-- Healthcare Pricing Segment -->
  <xsl:template match="HCP">
    <Pricing>
      <xsl:attribute name="AllowedAmount">
        <xsl:value-of select="HCP02"/>
      </xsl:attribute>
      <xsl:attribute name="OrganizationId">
        <xsl:value-of select="HCP04"/>
      </xsl:attribute>
      <Methodology>
        <xsl:attribute name="Code">
          <xsl:value-of select="HCP01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="HCP01='00'">Zero Pricing (Not Covered Under Contract)</xsl:when>
          <xsl:when test="HCP01='01'">Priced as Billed at 100%</xsl:when>
          <xsl:when test="HCP01='02'">Priced at the Standard Fee Schedule</xsl:when>
          <xsl:when test="HCP01='03'">Priced at a Contractual Percentage</xsl:when>
          <xsl:when test="HCP01='04'">Bundled Pricing</xsl:when>
          <xsl:when test="HCP01='05'">Peer Review Pricing</xsl:when>
          <xsl:when test="HCP01='07'">Flat Rate Pricing</xsl:when>
          <xsl:when test="HCP01='08'">Combination Pricing</xsl:when>
          <xsl:when test="HCP01='09'">Maternity Pricing</xsl:when>
          <xsl:when test="HCP01='10'">Other Pricing</xsl:when>
          <xsl:when test="HCP01='11'">Lower of Cost</xsl:when>
          <xsl:when test="HCP01='12'">Ratio of Cost</xsl:when>
          <xsl:when test="HCP01='13'">Cost Reimbursed</xsl:when>
          <xsl:when test="HCP01='14'">Adjustment Pricing</xsl:when>
        </xsl:choose>
      </Methodology>
      <xsl:if test="string-length(HCP03)>0">
        <SavingsAmount>
          <xsl:value-of select="HCP03"/>
        </SavingsAmount>
      </xsl:if>
      <xsl:if test="string-length(HCP04)>0">

      </xsl:if>
    </Pricing>
  </xsl:template>

  <!-- Professional Service Line Segment -->
  <xsl:template match="SV1">
    <xsl:attribute name="Quantity">
      <xsl:choose>
        <xsl:when test="string-length(SV104)>0">
          <xsl:value-of select="SV104"/>
        </xsl:when>
        <xsl:otherwise>1</xsl:otherwise>
      </xsl:choose>

    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV103"/>
    </xsl:attribute>
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV102"/>
    </xsl:attribute>
    <xsl:if test="string-length(SV105)>0">
      <PlaceOfService>
        <xsl:attribute name="Code">
          <xsl:value-of select="SV105"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:choose>
            <xsl:when test="SV105='11'">Office</xsl:when>
            <xsl:when test="SV105='12'">Home</xsl:when>
            <xsl:when test="SV105='21'">Inpatient Hospital</xsl:when>
            <xsl:when test="SV105='22'">Outpatient Hospital</xsl:when>
            <xsl:when test="SV105='23'">Emergency Room - Hospital</xsl:when>
            <xsl:when test="SV105='24'">Ambulatory Surgical Center</xsl:when>
            <xsl:when test="SV105='25'">Birthing Center</xsl:when>
            <xsl:when test="SV105='26'">Military Treatment Facility</xsl:when>
            <xsl:when test="SV105='31'">Skilled Nursing Facility</xsl:when>
            <xsl:when test="SV105='32'">Nursing Facility</xsl:when>
            <xsl:when test="SV105='33'">Custodial Care Facility</xsl:when>
            <xsl:when test="SV105='34'">Hospice</xsl:when>
            <xsl:when test="SV105='41'">Ambulance - Land</xsl:when>
            <xsl:when test="SV105='42'">Ambulance - Air or Water</xsl:when>
            <xsl:when test="SV105='50'">Federally Qualified Health Care</xsl:when>
            <xsl:when test="SV105='51'">Inpatient Psychiatric Facility</xsl:when>
            <xsl:when test="SV105='52'">Psychiatric Facility Partial Hospitalization</xsl:when>
            <xsl:when test="SV105='53'">Community Mental Health Center</xsl:when>
            <xsl:when test="SV105='54'">Intermediate Care Facility/Mentally Retarded</xsl:when>
            <xsl:when test="SV105='55'">Residential Substance Abuse Treatment Facility</xsl:when>
            <xsl:when test="SV105='56'">Psychiatric Residential Treatment Center</xsl:when>
            <xsl:when test="SV105='60'">Mass Immunization Center</xsl:when>
            <xsl:when test="SV105='61'">Comprehensive Inpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="SV105='62'">Comprehensive Outpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="SV105='65'">End Stage Renal Disease Treatment Facility</xsl:when>
            <xsl:when test="SV105='71'">State or Local Public Health Clinic</xsl:when>
            <xsl:when test="SV105='72'">Rural Health Clinic</xsl:when>
            <xsl:when test="SV105='81'">Independent Laboratory</xsl:when>
            <xsl:when test="SV105='99'">Other Unlisted Facility</xsl:when>
          </xsl:choose>
        </xsl:if>
      </PlaceOfService>
    </xsl:if>
    <Procedure>
      <xsl:attribute name="Code">
        <xsl:value-of select="SV101/SV10102"/>
      </xsl:attribute>
      <xsl:if test="string-length(SV101/SV10103) > 0">
        <xsl:attribute name="Mod1">
          <xsl:value-of select="SV101/SV10103"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10104) > 0">
        <xsl:attribute name="Mod2">
          <xsl:value-of select="SV101/SV10104"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10105) > 0">
        <xsl:attribute name="Mod3">
          <xsl:value-of select="SV101/SV10105"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10106) > 0">
        <xsl:attribute name="Mod4">
          <xsl:value-of select="SV101/SV10106"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10107)>0">
        <xsl:value-of select="SV101/SV10107"/>
      </xsl:if>
    </Procedure>
  </xsl:template>

  <!-- Institutional Service Line Segment -->
  <xsl:template match="SV2">
    <xsl:attribute name="Quantity">
      <xsl:choose>
        <xsl:when test="string-length(SV205)>0">
          <xsl:value-of select="SV205"/>
        </xsl:when>
        <xsl:otherwise>1</xsl:otherwise>
      </xsl:choose>

    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV204"/>
    </xsl:attribute>
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV203"/>
    </xsl:attribute>
    <Revenue>
      <xsl:attribute name="Code">
        <xsl:value-of select="SV201"/>
      </xsl:attribute>
    </Revenue>
    <xsl:if test="string-length(SV202/SV20202)>0">

      <Procedure>
        <xsl:attribute name="Code">
          <xsl:value-of select="SV202/SV20202"/>
        </xsl:attribute>
        <xsl:if test="string-length(SV202/SV20203) > 0">
          <xsl:attribute name="Mod1">
            <xsl:value-of select="SV202/SV20203"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20204) > 0">
          <xsl:attribute name="Mod2">
            <xsl:value-of select="SV202/SV20204"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20205) > 0">
          <xsl:attribute name="Mod3">
            <xsl:value-of select="SV202/SV20205"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20206) > 0">
          <xsl:attribute name="Mod4">
            <xsl:value-of select="SV202/SV20206"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20207)>0">
          <xsl:value-of select="SV202/SV20207"/>
        </xsl:if>
      </Procedure>
    </xsl:if>
  </xsl:template>

  <!-- Dental Service Line Segment -->
  <xsl:template match="SV3"></xsl:template>

  <!-- Subscriber Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='2000B']">
    <Subscriber>
      <xsl:attribute name="DateOfBirth">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="Loop[@LoopId='2010BA']/DMG/DMG02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="Gender">
        <xsl:value-of select="Loop[@LoopId='2010BA']/DMG/DMG03"/>
      </xsl:attribute>
      <xsl:apply-templates select="Loop[@LoopId='2010BA']/NM1" />
    </Subscriber>
  </xsl:template>

  <!-- Provider Loop in Billing Loop, Claim Loop or Service Line Loop -->
  <xsl:template match="Loop[substring(@LoopId,1,4)='2010'] | Loop[substring(@LoopId,1,4)='2310'] | Loop[substring(@LoopId,1,4)='2420']">
    <Provider>
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="NM1/NM101='71'">Attending Physician</xsl:when>
          <xsl:when test="NM1/NM101='72'">Operating Physician</xsl:when>
          <xsl:when test="NM1/NM101='73'">Other Physician</xsl:when>
          <xsl:when test="NM1/NM101='77'">Service Location</xsl:when>
          <xsl:when test="NM1/NM101='82'">Rendering Provider</xsl:when>
          <xsl:when test="NM1/NM101='85'">Billing Provider</xsl:when>
          <xsl:when test="NM1/NM101='87'">Pay-To Provider</xsl:when>
          <xsl:when test="NM1/NM101='DK'">Ordering Physician</xsl:when>
          <xsl:when test="NM1/NM101='DN'">Referring Provider</xsl:when>
          <xsl:when test="NM1/NM101='DQ'">Supervising Physician</xsl:when>
          <xsl:when test="NM1/NM101='FA'">Facility</xsl:when>
          <xsl:when test="NM1/NM101='LI'">Indepedent Lab</xsl:when>
          <xsl:when test="NM1/NM101='P3'">Primary Care Provider</xsl:when>
          <xsl:when test="NM1/NM101='PR'">Payer</xsl:when>
          <xsl:when test="NM1/NM101='QB'">Purchase Service Provider</xsl:when>
          <xsl:when test="NM1/NM101='TL'">Testing Laboratory</xsl:when>
        </xsl:choose>
      </xsl:attribute>
      <xsl:apply-templates select="NM1" />
      <Address>
        <xsl:apply-templates select="N3"/>
      </Address>
      <xsl:if test="string-length(PRV/PRV01) > 0">
        <Speciality>
          <xsl:attribute name="Code">
            <xsl:value-of select="PRV/PRV03"/>
          </xsl:attribute>
        </Speciality>
      </xsl:if>
      <xsl:if test="string-length(../PRV/PRV01) > 0 ">
        <xsl:if test="NM1/NM101='85' and ../PRV/PRV01='BI' or NM1/NM101='87' and ../PRV/PRV01='PT'">
          <Speciality>
            <xsl:attribute name="Code">
              <xsl:value-of select="../PRV/PRV03"/>
            </xsl:attribute>
          </Speciality>
        </xsl:if>
      </xsl:if>
    </Provider>
  </xsl:template>

  <!-- Claim Loop -->
  <xsl:template name="ClaimTemplate">
    <xsl:param name="SubscriberLoop" />
    <xsl:param name="ClaimLoop" />
    <xsl:param name="PatientIsSubscriber" />
    <Claim>
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="count($ClaimLoop/Loop[@LoopId='2400']/SV3)>0">Dental</xsl:when>
          <xsl:when test="count($ClaimLoop/Loop[@LoopId='2400']/SV2)>0">Institutional</xsl:when>
          <xsl:otherwise>Professional</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="PatientControlNumber">
        <xsl:value-of select="$ClaimLoop/CLM/CLM01"/>
      </xsl:attribute>
      <xsl:attribute name="TotalCharges">
        <xsl:value-of select="$ClaimLoop/CLM/CLM02"/>
      </xsl:attribute>
      <Header>
        <xsl:if test="$verbose=1">
          <!-- Interchange Control Header-->
          <xsl:apply-templates select="$SubscriberLoop/../../../../ISA" />
          <!-- Function Group Header -->
          <xsl:apply-templates select="$SubscriberLoop/../../../GS" />
        </xsl:if>
        <!-- Transaction Set Header -->
        <xsl:apply-templates select="$SubscriberLoop/../../ST" />
      </Header>
      <!-- Reference Identifications -->
      <xsl:apply-templates select="$ClaimLoop/REF"/>
      <!-- Billing Hierarchical Loop -->
      <xsl:apply-templates select="$SubscriberLoop/../Loop" />
      <!-- Rendering/Facility Providers -->
      <xsl:apply-templates select="$ClaimLoop/Loop[substring(@LoopId,1,4)='2310']"/>
      <!-- Subscriber Hierarchical Loop -->
      <xsl:apply-templates select="$SubscriberLoop" />
      <!-- Note/Special Instruction Segment -->
      <xsl:apply-templates select="$ClaimLoop/NTE"/>
      <!-- Healthcare Pricing Segment -->
      <xsl:apply-templates select="$ClaimLoop/HCP"/>

      <xsl:for-each select="$ClaimLoop/Loop[@LoopId='2400']">
        <ServiceLine>
          <xsl:attribute name="LineNumber">
            <xsl:value-of select="LX/LX01"/>
          </xsl:attribute>
          <xsl:apply-templates select="SV1"/>
          <xsl:apply-templates select="SV2"/>
          <xsl:apply-templates select="SV3"/>
          <xsl:if test="AMT/AMT01='AAE'">
            <ApprovedAmount>
              <xsl:value-of select="AMT/AMT02"/>
            </ApprovedAmount>
          </xsl:if>
          <xsl:apply-templates select="HCP"/>
        </ServiceLine>
      </xsl:for-each>
    </Claim>
  </xsl:template>

  <xsl:template match="/">
    <ArrayOfClaim>
      <!-- Claims without patient loop -->
      <xsl:for-each select="/Interchange/FunctionGroup/Transaction/HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select=".."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="1"/>
        </xsl:call-template>
      </xsl:for-each>
      <!-- Claims with patient loop -->
      <xsl:for-each select="/Interchange/FunctionGroup/Transaction/HierarchicalLoop/HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select="../.."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="0"/>
        </xsl:call-template>
      </xsl:for-each>
    </ArrayOfClaim>
  </xsl:template>

</xsl:stylesheet>
