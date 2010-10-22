<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:import href="Ansi-Common.xslt"/>
  <xsl:output method="xml" indent="yes"/>
   
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
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="SV201='0270'">Medical/Surgical Supplies</xsl:when>
            <xsl:when test="SV201='0300'">Laboratory-Clinical Diagnostic</xsl:when>
            <xsl:when test="SV201='0320'">Radiology-Diagnostic</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </Revenue>
    <xsl:if test="string-length(SV202/SV20202)>0">
      <Procedure>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="SV202/SV20201"/>
        </xsl:attribute>
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
      </Procedure>
    </xsl:if>
  </xsl:template>

  <!-- Dental Service Line Segment -->
  <xsl:template match="SV3"></xsl:template>

  <!-- Subscriber Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='2000B']">
    <Subscriber>
      <xsl:if test="Loop[@LoopId='2010BA']/NM1/NM108 = 'MI'">
        <xsl:attribute name="MemberId">
          <xsl:value-of select="Loop[@LoopId='2010BA']/NM1/NM109"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="DateOfBirth">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="Loop[@LoopId='2010BA']/DMG/DMG02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="Gender">
        <xsl:choose>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'F'">Female</xsl:when>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'M'">Male</xsl:when>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'U'">Unknown</xsl:when>
          <xsl:otherwise>Unknown</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:apply-templates select="Loop[@LoopId='2010BA']/NM1" />
      <Address>
        <xsl:apply-templates select="Loop[@LoopId='2010BA']/N3" />
      </Address>
    </Subscriber>
  </xsl:template>

  <!-- Provider Loop in Billing Loop, Claim Loop or Service Line Loop -->
  <xsl:template match="Loop[substring(@LoopId,1,4)='2010'] | Loop[substring(@LoopId,1,4)='2310'] | Loop[substring(@LoopId,1,4)='2420']">
    <Provider>
      <xsl:choose>
        <xsl:when test="NM1/NM108='24'">
          <xsl:attribute name="EmployerId">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="count(REF[REF01='EI'])>0">
          <xsl:attribute name="EmployerId">
            <xsl:value-of select="REF[REF01='EI']/REF02"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="NM1/NM108='34'">
          <xsl:attribute name="Ssn">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="count(REF[REF01='SY'])>0">
          <xsl:attribute name="Ssn">
            <xsl:value-of select="REF[REF01='SY']/REF02"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="NM1/NM108='XX'">
          <xsl:attribute name="Npi">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:apply-templates select="NM1"/>
      
      <Address>
        <xsl:apply-templates select="N3"/>
      </Address>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="PER"/>
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

  <!-- Service Line Loop -->
  <xsl:template match="Loop[@LoopId='2400']">
    <ServiceLine>
      <xsl:attribute name="AssignedNumber">
        <xsl:value-of select="LX/LX01"/>
      </xsl:attribute>
      <xsl:apply-templates select="SV1"/>
      <xsl:apply-templates select="SV2"/>
      <xsl:apply-templates select="SV3"/>
      <DateOfServiceFrom>
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="substring(DTP[DTP01='472']/DTP03,1,8)"/>
        </xsl:call-template>
      </DateOfServiceFrom>
      <xsl:if test="DTP[DTP01='472']/DTP02='RD8'">
        <DateOfServiceTo>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="substring(DTP[DTP01='472']/DTP03,9,8)"/>
          </xsl:call-template>
        </DateOfServiceTo>
      </xsl:if>
      <xsl:if test="count(DTP[DTP01='866'])>0">
        <AssessmentDate>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTP[DTP01='866']/DTP03"/>
          </xsl:call-template>
        </AssessmentDate>
      </xsl:if>
      <xsl:if test="AMT/AMT01='AAE'">
        <ApprovedAmount>
          <xsl:value-of select="AMT/AMT02"/>
        </ApprovedAmount>
      </xsl:if>
      <xsl:apply-templates select="HCP"/>
    </ServiceLine>
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

      <xsl:apply-templates select="$ClaimLoop/Loop[@LoopId='2400']"/>
    </Claim>
  </xsl:template>



  <xsl:template match="Transaction">
    <Transaction>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ST/ST02"/>
      </xsl:attribute>
      <xsl:attribute name="ProductionDate">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="DTM[DTM01='405']/DTM02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:apply-templates select="../../ISA"/>
      <xsl:apply-templates select="../GS"/>
      <!-- Claims without patient loop -->
      <xsl:for-each select="HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select=".."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="1"/>
        </xsl:call-template>
      </xsl:for-each>
      <!-- Claims with patient loop -->
      <xsl:for-each select="HierarchicalLoop/HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select="../.."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="0"/>
        </xsl:call-template>
      </xsl:for-each>
    </Transaction>
  </xsl:template>

  <xsl:template match="/">
    <ArrayOfTransaction>
      <xsl:apply-templates select="/Interchange/FunctionGroup/Transaction"/>
    </ArrayOfTransaction>
  </xsl:template>


</xsl:stylesheet>
