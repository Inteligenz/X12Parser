<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="verbose" select="1" />

  <xsl:template name="FormatD8Date">
    <xsl:param name="Date" />
    <xsl:if test="string-length($Date)>0">
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
    </xsl:if>
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
    <xsl:if test="string-length($Date)>0">
      <xsl:value-of select="$year"/>-<xsl:value-of select="$mo"/>-<xsl:value-of select="$day"/>
      <xsl:if test="string-length($Time)>0">T<xsl:value-of select="$hour"/>:<xsl:value-of select="$minute"/>:<xsl:value-of select="$second"/>.<xsl:value-of select="$decimal"/>
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template name="Identification">
    <xsl:param name="Qualifier"/>
    <xsl:param name="Number"/>
    <xsl:if test="string-length($Qualifier)>0">
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="$Qualifier"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="string-length($Number) > 0">
      <xsl:value-of select="normalize-space($Number)"/>
    </xsl:if>
    <xsl:if test="$verbose=1 and string-length($Qualifier)>0">
      <xsl:comment>
        <xsl:choose>
          <!-- Secondary Identification -->
          <xsl:when test="REF01='OB'">State License Number</xsl:when>
          <xsl:when test="REF01='1A'">Blue Cross Provider Number</xsl:when>
          <xsl:when test="REF01='1B'">Blue Shield Provider Number</xsl:when>
          <xsl:when test="REF01='1C'">Medicare Provider Number</xsl:when>
          <xsl:when test="REF01='1D'">Medicaid Provider Number</xsl:when>
          <xsl:when test="REF01='1G'">Provider UPIN Number</xsl:when>
          <xsl:when test="REF01='1H'">CHAMPUS Identification Number</xsl:when>
          <xsl:when test="REF01='1J'">Facility ID Number</xsl:when>
          <xsl:when test="REF01='B3'">Preferred Provider Organization Number</xsl:when>
          <xsl:when test="REF01='BQ'">Health Maintenance Organization Code Number</xsl:when>
          <xsl:when test="REF01='EI'">Employer's Identification Number</xsl:when>
          <xsl:when test="REF01='FH'">Clinic Number</xsl:when>
          <xsl:when test="REF01='G2'">Provider Commercial Number</xsl:when>
          <xsl:when test="REF01='G5'">Provider Site Number</xsl:when>
          <xsl:when test="REF01='LU'">Location Number</xsl:when>
          <xsl:when test="REF01='SY'">Social Security Number</xsl:when>
          <xsl:when test="REF01='U3'">Unique Supplier Identification Number (USIN)</xsl:when>
          <xsl:when test="REF01='X5'">State Industrial Accident Provider Number</xsl:when>
          <!-- Credit/Debit Card Billing Information-->
          <xsl:when test="REF01='06'">System Number</xsl:when>
          <xsl:when test="REF01='8U'">Bank Assigned Security Identifier</xsl:when>
          <xsl:when test="REF01='EM'">Electronic Payment Reference Number</xsl:when>
          <xsl:when test="REF01='IJ'">Standard Industry Classification (SIC) Code</xsl:when>
          <xsl:when test="REF01='LU'">Location Number</xsl:when>
          <xsl:when test="REF01='RB'">Rate code number</xsl:when>
          <xsl:when test="REF01='ST'">Store Number</xsl:when>
          <xsl:when test="REF01='TT'">Terminal Code</xsl:when>

          <!-- Transmission Type Identification-->
          <xsl:when test="$Qualifier='87'">Funtional Category</xsl:when>

          <!-- Provider Secondary Identification -->
          <xsl:when test="$Qualifier='OB'">State License Number</xsl:when>
          <xsl:when test="$Qualifier='1A'">Blue Cross Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1B'">Blue Shield Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1C'">Medicare Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1D'">Medicaid Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1G'">Provider UPIN Number</xsl:when>
          <xsl:when test="$Qualifier='1H'">CHAMPUS Identification Number</xsl:when>
          <xsl:when test="$Qualifier='1J'">Facility ID Number</xsl:when>
          <xsl:when test="$Qualifier='B3'">Preferred Provider Organization Number</xsl:when>
          <xsl:when test="$Qualifier='BQ'">Health Maintenance Organization Code Number</xsl:when>
          <xsl:when test="$Qualifier='EI'">Employer's Identification Number</xsl:when>
          <xsl:when test="$Qualifier='FH'">Clinic Number</xsl:when>
          <xsl:when test="$Qualifier='G2'">Provider Commercial Number</xsl:when>
          <xsl:when test="$Qualifier='G5'">Provider Site Number</xsl:when>
          <xsl:when test="$Qualifier='LU'">Location Number</xsl:when>
          <xsl:when test="$Qualifier='N5'">Provider Plan Network Identification Number</xsl:when>
          <xsl:when test="$Qualifier='SY'">Social Security Number</xsl:when>
          <xsl:when test="$Qualifier='U3'">Unique Supplier Identification Number (USIN)</xsl:when>
          <xsl:when test="$Qualifier='X5'">State Industrial Accident Provider Number</xsl:when>
          <!-- Provider Credit/Debit Card Billing Information-->
          <xsl:when test="$Qualifier='06'">System Number</xsl:when>
          <xsl:when test="$Qualifier='8U'">Bank Assigned Security Identifier</xsl:when>
          <xsl:when test="$Qualifier='EM'">Electronic Payment Reference Number</xsl:when>
          <xsl:when test="$Qualifier='IJ'">Standard Industry Classification (SIC) Code</xsl:when>
          <xsl:when test="$Qualifier='LU'">Location Number</xsl:when>
          <xsl:when test="$Qualifier='RB'">Rate code number</xsl:when>
          <xsl:when test="$Qualifier='ST'">Store Number</xsl:when>
          <xsl:when test="$Qualifier='TT'">Terminal Code</xsl:when>
          <!-- Subscriber/Patient/Other Subscriber Secondary Identification -->
          <xsl:when test="$Qualifier='1W'">Member Identification Number</xsl:when>
          <xsl:when test="$Qualifier='23'">Client Number</xsl:when>
          <xsl:when test="$Qualifier='IG'">Insurance Policy Number</xsl:when>
          <xsl:when test="$Qualifier='SY'">Social Security Number</xsl:when>
          <!-- Property and Casualty Claim Number-->
          <xsl:when test="$Qualifier='Y4'">Agency Claim Number</xsl:when>
          <!-- Credit/Debit Card Information -->
          <xsl:when test="$Qualifier='AB'">Acceptable Source Purchaser ID</xsl:when>
          <xsl:when test="$Qualifier='BB'">Authorization Number</xsl:when>
          <!-- Payer/Other Payer Secondary Identification -->
          <xsl:when test="$Qualifier='2U'">Payer Identification Number</xsl:when>
          <xsl:when test="$Qualifier='F8'">Original Reference Number</xsl:when>
          <xsl:when test="$Qualifier='FY'">Claim Office Number</xsl:when>
          <xsl:when test="$Qualifier='NF'">National Association of Insurance Commissioners (NAIC) Code</xsl:when>
          <xsl:when test="$Qualifier='TJ'">Federal Taxpayer's Identification Number</xsl:when>
          <!-- Claim Identifiers -->
          <xsl:when test="$Qualifier='9C'">Adjusted Repriced Claim Reference Number</xsl:when>
          <xsl:when test="$Qualifier='9A'">Repriced Claim Reference Number</xsl:when>
          <xsl:when test="$Qualifier='D9'">Claim Number</xsl:when>
          <xsl:when test="$Qualifier='DD'">Document Identification Code</xsl:when>
          <xsl:when test="$Qualifier='LX'">Qualified Products List</xsl:when>
          <xsl:when test="$Qualifier='4N'">Special Payment Reference Number</xsl:when>
          <xsl:when test="$Qualifier='G4'">Peer Review Organization (PRO) Approval Number</xsl:when>
          <xsl:when test="$Qualifier='9F'">Referral Number</xsl:when>
          <xsl:when test="$Qualifier='G1'">Prior Authorization Number</xsl:when>
          <xsl:when test="$Qualifier='EA'">Medical Record Identification Number</xsl:when>
          <xsl:when test="$Qualifier='P4'">Project Code</xsl:when>
          <xsl:when test="$Qualifier='XZ'">Pharmacy Prescription Number</xsl:when>

          <xsl:when test="$Qualifier='F5'">Medicare Version Code</xsl:when>
          <xsl:when test="$Qualifier='EW'">Mammography Certification Number</xsl:when>
          <xsl:when test="$Qualifier='F8'">Original Reference Number</xsl:when>
          <xsl:when test="$Qualifier='X4'">Clinical Laboratory Improvement Amendment Number</xsl:when>
          <xsl:when test="$Qualifier='1S'">Ambulatory Patient Group (APG) Number</xsl:when>
          <!-- ISA Sender and Receiver -->
          <xsl:when test="$Qualifier='01'">Duns (Dun &amp; Bradstreet)</xsl:when>
          <xsl:when test="$Qualifier='14'">Duns Plus Suffix</xsl:when>
          <xsl:when test="$Qualifier='20'">Health Industry Number (HIN)</xsl:when>
          <xsl:when test="$Qualifier='27'">Carrier Identification Number as assigned by HCFA</xsl:when>
          <xsl:when test="$Qualifier='28'">Fiscal Intermediary Identification Number as assigned by HCFA</xsl:when>
          <xsl:when test="$Qualifier='29'">Medicare Provider and Supplier Identification Number as assigned by HCFA</xsl:when>
          <xsl:when test="$Qualifier='30'">U.S. Federal Tax Identification Number</xsl:when>
          <xsl:when test="$Qualifier='33'">National Association of Insurance Commissioners Company Code (NAIC)</xsl:when>
          <xsl:when test="$Qualifier='ZZ'">Mutually Defined</xsl:when>
          <!-- Receiver Identification -->
          <xsl:when test="$Qualifier='EV'">Receiver Identification Number</xsl:when>
          <!-- Version Identification -->
          <xsl:when test="$Qualifier='F2'">Version Code - Local</xsl:when>

          <!-- Additional Payer Identification -->
          <xsl:when test="$Qualifier='2U'">Payer Identification Number</xsl:when>
          <xsl:when test="$Qualifier='EO'">Submitter Identification Number</xsl:when>
          <xsl:when test="$Qualifier='HI'">Health Industry Number (HIN)</xsl:when>
          <xsl:when test="$Qualifier='NF'">National Association of Insurance Commissioners (NAIC) Code</xsl:when>
          <!-- Payee Identification -->
          <xsl:when test="$Qualifier='FI'">Federal Taxpayer's Identification Number</xsl:when>

          <!-- Payee Additional Identification -->
          <xsl:when test="$Qualifier='0B'">State License Number</xsl:when>
          <xsl:when test="$Qualifier='1A'">Blue Cross Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1B'">Blue Shield Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1C'">Medicare Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1D'">Medicaid Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1E'">Dentist License Number</xsl:when>
          <xsl:when test="$Qualifier='1F'">Anesthesia License Number</xsl:when>
          <xsl:when test="$Qualifier='1G'">Provider UPIN Number</xsl:when>
          <xsl:when test="$Qualifier='1H'">CHAMPUS Identification Number</xsl:when>
          <xsl:when test="$Qualifier='D3'">National Association of Boards of Pharmacy Number</xsl:when>
          <xsl:when test="$Qualifier='G2'">Provider Commerical Number</xsl:when>
          <xsl:when test="$Qualifier='N5'">Provider Plan Network Identification Number</xsl:when>
          <xsl:when test="$Qualifier='PQ'">Payee Identification</xsl:when>
          <xsl:when test="$Qualifier='TJ'">Federal Taxpayer's Identification Number</xsl:when>
          <!-- Other Claim Related Identfication -->
          <xsl:when test="$Qualifier='1L'">Group or Policy Number</xsl:when>
          <xsl:when test="$Qualifier='1W'">Member Identification Number</xsl:when>
          <xsl:when test="$Qualifier='9A'">Repriced Claim Reference Number</xsl:when>
          <xsl:when test="$Qualifier='9C'">Adjusted Repriced Claim Reference Number</xsl:when>
          <xsl:when test="$Qualifier='A6'">Employee Identification Number</xsl:when>
          <xsl:when test="$Qualifier='BB'">Authorization Number</xsl:when>
          <xsl:when test="$Qualifier='CE'">Class of Contract Code</xsl:when>
          <xsl:when test="$Qualifier='EA'">Medical Record Identification Number</xsl:when>
          <xsl:when test="$Qualifier='F8'">Original Reference Number</xsl:when>
          <xsl:when test="$Qualifier='G1'">Prior Authorization Number</xsl:when>
          <xsl:when test="$Qualifier='G3'">Predetermination of Benefits Identification Number</xsl:when>
          <xsl:when test="$Qualifier='IG'">Insurance Policy Number</xsl:when>
          <xsl:when test="$Qualifier='SY'">Social Security Number</xsl:when>
          <!-- Service Identification -->
          <xsl:when test="$Qualifier='1S'">Ambulatory Patient Group (APG) Number</xsl:when>
          <xsl:when test="$Qualifier='6R'">Provider Control Number</xsl:when>
          <xsl:when test="$Qualifier='BB'">Authorization Number</xsl:when>
          <xsl:when test="$Qualifier='E9'">Attachment Code</xsl:when>
          <xsl:when test="$Qualifier='G1'">Prior Authorization Number</xsl:when>
          <xsl:when test="$Qualifier='G3'">Predetermination of Benefits Identification Number</xsl:when>
          <xsl:when test="$Qualifier='LU'">Location Number</xsl:when>
          <xsl:when test="$Qualifier='RB'">Rate code number</xsl:when>
          <!-- Rendering Provider Identification -->
          <xsl:when test="$Qualifier='1A'">Blue Cross Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1B'">Blue Shield Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1C'">Medicare Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1D'">Medicaid Provider Number</xsl:when>
          <xsl:when test="$Qualifier='1G'">Provider UPIN Number</xsl:when>
          <xsl:when test="$Qualifier='1H'">CHAMPUS Identification Number</xsl:when>
          <xsl:when test="$Qualifier='D3'">National Association of Boards of Pharmacy Number</xsl:when>
          <xsl:when test="$Qualifier='G2'">Provider Commercial Number</xsl:when>
          <xsl:when test="$Qualifier='1J'">Facility ID Number</xsl:when>
          <xsl:when test="$Qualifier='HPI'">Health Care Financing Adminstration National Provider Identification</xsl:when>
          <xsl:when test="$Qualifier='SY'">Social Security Number</xsl:when>
          <xsl:when test="$Qualifier='TJ'">Federal Taxpayer's Identification Number</xsl:when>
          <!-- Patient, Insured -->
          <xsl:when test="$Qualifier='34'">Social Security Number</xsl:when>
          <xsl:when test="$Qualifier='HN'">Health Insurance Claim (HIC) Number</xsl:when>
          <xsl:when test="$Qualifier='II'">United States National Individual Identifier</xsl:when>
          <xsl:when test="$Qualifier='MI'">Member Identification Number</xsl:when>
          <xsl:when test="$Qualifier='MR'">Medicaid Recipient Identification Number</xsl:when>
          <xsl:when test="$Qualifier='C'">Insured's Changed Unique Identification Number</xsl:when>
          <!-- Provider -->
          <xsl:when test="$Qualifier='BD'">Blue Cross Provider Number</xsl:when>
          <xsl:when test="$Qualifier='BS'">Blue Shield Provider Number</xsl:when>
          <xsl:when test="$Qualifier='FI'">Federal Taxpayer's Identification Number</xsl:when>
          <xsl:when test="$Qualifier='MC'">Medical Provider Number</xsl:when>
          <xsl:when test="$Qualifier='PC'">Provider Commerical Number</xsl:when>
          <xsl:when test="$Qualifier='SL'">State License Number</xsl:when>
          <xsl:when test="$Qualifier='UP'">Unique Physician Identification Number</xsl:when>
          <xsl:when test="$Qualifier='XX'">Health Care Financing Administration National Provider Identifier</xsl:when>
          <!-- Crossover Carrier -->
          <xsl:when test="$Qualifier='AD'">Blue Cross Blue Shield Association Plan Code</xsl:when>
          <xsl:when test="$Qualifier='FI'">Federal Taxpayer's Identification Number</xsl:when>
          <xsl:when test="$Qualifier='NI'">National Association of Insurance Commissioners (NAIC) Identification</xsl:when>
          <xsl:when test="$Qualifier='PI'">Payor Identification</xsl:when>
          <xsl:when test="$Qualifier='PP'">Pharmacy Processor Number</xsl:when>
          <xsl:when test="$Qualifier='XV'">Health Care Financing Administration National Provider Identifier</xsl:when>
          
          <!-- Suppliers -->
          <xsl:when test="$Qualifier='1'">DUNS Number</xsl:when>
          <xsl:when test="$Qualifier='92'">Assigned by Buyer or Buyer's Agent</xsl:when>
          <xsl:when test="$Qualifier='BM'">Bill of Lading Number</xsl:when>
          <xsl:when test="$Qualifier='BP'">Buyer's Part Number</xsl:when>
          <xsl:when test="$Qualifier='VN'">Vendor's (Seller's) Part Number</xsl:when>
          <xsl:when test="$Qualifier='EC'">Engineering Change Level</xsl:when>
          <xsl:when test="$Qualifier='VB'">Vendor's Engineering Change Level Number</xsl:when>
          <xsl:when test="$Qualifier='CN'">PRO Number</xsl:when>
          <xsl:when test="$Qualifier='PK'">Packing List/Slip Number</xsl:when>

          <xsl:when test="$Qualifier='CNT'">Container</xsl:when>
          <xsl:when test="$Qualifier='CTN'">Carton</xsl:when>
          <xsl:when test="$Qualifier='PAT'">Pallet - 2 Way</xsl:when>
          <xsl:when test="$Qualifier='PCK'">Packed - not otherwise specified</xsl:when>
          <xsl:when test="$Qualifier='PKG'">Package</xsl:when>
          <xsl:when test="$Qualifier='PLT'">Pallet</xsl:when>
          <xsl:when test="$Qualifier='PAT90'">Standard Pallet</xsl:when>
        </xsl:choose>
      </xsl:comment>
    </xsl:if>
  </xsl:template>

  <!-- Interchange Control Header -->
  <xsl:template match="ISA">
    <InterchangeControlHeader>
      <xsl:attribute name="InterchangeDate">
        <xsl:call-template name="FormatD8DateTime">
          <xsl:with-param name="Date" select="concat('20',ISA09)"/>
          <xsl:with-param name="Time" select="ISA10"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ISA13"/>
      </xsl:attribute>
      <xsl:attribute name="AcknowledgmentRequested">
        <xsl:choose>
          <xsl:when test="ISA14='1'">true</xsl:when>
          <xsl:otherwise>false</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:if test="ISA01!='00'">
        <AuthorizationInformation>
          <Qualifier>
            <xsl:attribute name="Code">
              <xsl:value-of select="ISA01"/>
            </xsl:attribute>
            <xsl:choose>
              <xsl:when test="ISA01='00'">No Authorization Information Present</xsl:when>
              <xsl:when test="ISA01='03'">Additional Data Identification</xsl:when>
            </xsl:choose>
          </Qualifier>
          <Text>
            <xsl:value-of select="ISA02"/>
          </Text>
        </AuthorizationInformation>
      </xsl:if>
      <xsl:if test="ISA03!='00'">
        <SecurityInformation>
          <Qualifier>
            <xsl:attribute name="Code">
              <xsl:value-of select="ISA03"/>
            </xsl:attribute>
            <xsl:choose>
              <xsl:when test="ISA03='00'">No Security Information Present</xsl:when>
              <xsl:when test="ISA03='01'">Password</xsl:when>
            </xsl:choose>
          </Qualifier>
          <Text>
            <xsl:value-of select="ISA04"/>
          </Text>
        </SecurityInformation>
      </xsl:if>
      <SenderId>
        <xsl:call-template name="Identification">
          <xsl:with-param name="Qualifier" select="ISA05"/>
          <xsl:with-param name="Number" select="ISA06"/>
        </xsl:call-template>
      </SenderId>
      <ReceiverId>
        <xsl:call-template name="Identification">
          <xsl:with-param name="Qualifier" select="ISA07"/>
          <xsl:with-param name="Number" select="ISA08"/>
        </xsl:call-template>
      </ReceiverId>
      <UsageIndicator>
        <xsl:attribute name="Code">
          <xsl:value-of select="ISA15"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="ISA15='P'">Production Data</xsl:when>
              <xsl:when test="ISA15='T'">Test Data</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </UsageIndicator>
    </InterchangeControlHeader>
  </xsl:template>

  <!-- Funtional Group Header -->
  <xsl:template match="GS">
    <FunctionGroupHeader>
      <xsl:attribute name="SenderCode">
        <xsl:value-of select="GS02"/>
      </xsl:attribute>
      <xsl:attribute name="ReceiverCode">
        <xsl:value-of select="GS03"/>
      </xsl:attribute>
      <xsl:attribute name="CreationDate">
        <xsl:call-template name="FormatD8DateTime">
          <xsl:with-param name="Date" select="GS04"/>
          <xsl:with-param name="Time" select="GS05"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="GS06"/>
      </xsl:attribute>
      <EdiStandard>
        <xsl:attribute name="ResponsibleAgency">
          <xsl:value-of select="GS07"/>
        </xsl:attribute>
        <xsl:value-of select="GS08"/>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="GS07='X'">Accredited Standards Committee X12</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </EdiStandard>
    </FunctionGroupHeader>
  </xsl:template>

  <!-- Name -->
  <xsl:template match="N1">
    <Name>
      <xsl:value-of select="N102"/>
    </Name>
    <xsl:if test="string-length(N104)>0">
      <Identification>
        <xsl:call-template name="Identification">
          <xsl:with-param name="Qualifier" select="N103"/>
          <xsl:with-param name="Number" select="N104"/>
        </xsl:call-template>
      </Identification>
    </xsl:if>
  </xsl:template>

  <!-- Individual or Organization Name -->
  <xsl:template match="NM1">
    <Name>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="NM101"/>
      </xsl:attribute>
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
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <!-- 837 Entities -->
            <xsl:when test="NM101='71'">Attending Physician</xsl:when>
            <xsl:when test="NM101='72'">Operating Physician</xsl:when>
            <xsl:when test="NM101='73'">Other Physician</xsl:when>
            <xsl:when test="NM101='77'">Service Location</xsl:when>
            <xsl:when test="NM101='82'">Rendering Provider</xsl:when>
            <xsl:when test="NM101='85'">Billing Provider</xsl:when>
            <xsl:when test="NM101='87'">Pay-To Provider</xsl:when>
            <xsl:when test="NM101='DK'">Ordering Physician</xsl:when>
            <xsl:when test="NM101='DN'">Referring Provider</xsl:when>
            <xsl:when test="NM101='DQ'">Supervising Physician</xsl:when>
            <xsl:when test="NM101='FA'">Facility</xsl:when>
            <xsl:when test="NM101='LI'">Indepedent Lab</xsl:when>
            <xsl:when test="NM101='P3'">Primary Care Provider</xsl:when>
            <xsl:when test="NM101='PR'">Payer</xsl:when>
            <xsl:when test="NM101='QB'">Purchase Service Provider</xsl:when>
            <xsl:when test="NM101='TL'">Testing Laboratory</xsl:when>
            <!-- 835 Entities -->
            <xsl:when test="NM101='QC'">Patient Name</xsl:when>
            <xsl:when test="NM101='IL'">Insured Name</xsl:when>
            <xsl:when test="NM101='74'">Corrected Patient/Insured Name</xsl:when>
            <xsl:when test="NM101='82'">Service Provider Name</xsl:when>
            <xsl:when test="NM101='TT'">Crossover Carrier Name</xsl:when>
            <xsl:when test="NM101='PR'">Corrected Priority Payer Name</xsl:when>
          </xsl:choose>
        </xsl:comment>
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
    <Identification>
      <xsl:call-template name="Identification">
        <xsl:with-param name="Qualifier" select="NM108"/>
        <xsl:with-param name="Number" select="NM109"/>
      </xsl:call-template>
    </Identification>
  </xsl:template>

  <!-- Address Segment -->
  <xsl:template match="N3">
    <xsl:attribute name="StateCode">
      <xsl:value-of select="../N4/N402"/>
    </xsl:attribute>
    <xsl:attribute name="PostalCode">
      <xsl:value-of select="../N4/N403"/>
    </xsl:attribute>
    <Line1>
      <xsl:value-of select="N301"/>
    </Line1>
    <xsl:if test="string-length(N302)>0">
      <Line2>
        <xsl:value-of select="N302"/>
      </Line2>
    </xsl:if>
    <City>
      <xsl:value-of select="../N4/N401"/>
    </City>
  </xsl:template>

  <!-- Reference Identification -->
  <xsl:template match="REF">
    <Identification>
      <xsl:call-template name="Identification">
        <xsl:with-param name="Qualifier" select="REF01"/>
        <xsl:with-param name="Number" select="REF02"/>
      </xsl:call-template>
    </Identification>
  </xsl:template>

  <!-- Administrative Communications Contact -->
  <xsl:template match="PER">
    <Contact>
      <Function>
        <xsl:attribute name="Code">
          <xsl:value-of select="PER01"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="PER01='IC'">Information Contact</xsl:when>
              <xsl:when test="PER01='CX'">Payers Claim Office</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </Function>
      <Name>
        <xsl:value-of select="PER02"/>
      </Name>
      <xsl:if test="PER03='EM'">
        <Email>
          <xsl:value-of select="PER04"/>
        </Email>
      </xsl:if>
      <xsl:if test="PER03='FX'">
        <Fax>
          <xsl:value-of select="PER04"/>
        </Fax>
      </xsl:if>
      <xsl:if test="PER03='TE'">
        <Phone>
          <xsl:if test="PER05='EX'">
            <xsl:attribute name="Ext">
              <xsl:value-of select="PER06"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="PER04"/>
        </Phone>
      </xsl:if>
      <xsl:if test="PER05='EM'">
        <Email>
          <xsl:value-of select="PER06"/>
        </Email>
      </xsl:if>
      <xsl:if test="PER05='FX'">
        <Fax>
          <xsl:value-of select="PER06"/>
        </Fax>
      </xsl:if>
      <xsl:if test="PER05='TE'">
        <Phone>
          <xsl:if test="PER07='EX'">
            <xsl:attribute name="Extension">
              <xsl:value-of select="PER08"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="PER06"/>
        </Phone>
      </xsl:if>
    </Contact>
  </xsl:template>

</xsl:stylesheet>
