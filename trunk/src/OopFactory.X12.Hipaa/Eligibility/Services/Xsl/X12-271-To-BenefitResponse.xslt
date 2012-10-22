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
    <EligibilityBenefitDocument>
      <xsl:apply-templates select="@* | node()"/>
    </EligibilityBenefitDocument>
  </xsl:template>

  <xsl:template match="HierarchicalLoop[@LoopId='2000A']">

    <xsl:for-each select="AAA">
      <RequestValidation>
        <xsl:call-template name="RequestValidation">
          <xsl:with-param name="AAA" select="."/>
        </xsl:call-template>
      </RequestValidation>
    </xsl:for-each>

    <xsl:choose>
      <xsl:when test="count(./HierarchicalLoop[@LoopId='2000B'])=0">
        <EligibilityBenefitResponse>
          <xsl:attribute name="TransactionControlNumber">
            <xsl:value-of select="../ST/ST02"/>
          </xsl:attribute>
          <xsl:call-template name="SourceNameLoop">
            <xsl:with-param name="Loop" select="Loop"/>
          </xsl:call-template>
        </EligibilityBenefitResponse>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="HierarchicalLoop2000B">
          <xsl:with-param name="HLoop" select="./HierarchicalLoop[@LoopId='2000B']"></xsl:with-param>
        </xsl:call-template>
      </xsl:otherwise>

    </xsl:choose>

  </xsl:template>

  <xsl:template name="HierarchicalLoop2000B">
    <xsl:param name="HLoop"/>

    <xsl:choose>
      <xsl:when test="count($HLoop/HierarchicalLoop[@LoopId='2000C'])=0">
        <EligibilityBenefitResponse>

          <xsl:attribute name="TransactionControlNumber">
            <xsl:value-of select="$HLoop/../../ST/ST02"/>
          </xsl:attribute>
          <xsl:call-template name="SourceNameLoop">
            <xsl:with-param name="Loop" select="$HLoop/../Loop"/>
          </xsl:call-template>
          <xsl:call-template name="ReceiverNameLoop">
            <xsl:with-param name="Loop" select="$HLoop/Loop"/>
          </xsl:call-template>
        </EligibilityBenefitResponse>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="HierarchicalLoop2000C">
          <xsl:with-param name="HLoop" select="$HLoop/HierarchicalLoop[@LoopId='2000C']"></xsl:with-param>
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <xsl:template name="HierarchicalLoop2000C">
    <xsl:param name="HLoop"/>

    <xsl:choose>
      <xsl:when test="count($HLoop/HierarchicalLoop[@LoopId='2000D'])=0">
        <EligibilityBenefitResponse>
          <xsl:call-template name="SubscriberHLoop">
            <xsl:with-param name="HLoop" select="$HLoop/."/>
          </xsl:call-template>
          <xsl:apply-templates select="$HLoop/Loop"/>
        </EligibilityBenefitResponse>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="HierarchicalLoop2000D">
          <xsl:with-param name="HLoop" select="$HLoop/HierarchicalLoop[@LoopId='2000D']"></xsl:with-param>
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <xsl:template name="HierarchicalLoop2000D">
    <xsl:param name="HLoop"/>

    <EligibilityBenefitResponse>
      <xsl:call-template name="SubscriberHLoop">
        <xsl:with-param name="HLoop" select="$HLoop/../."/>
      </xsl:call-template>
      <xsl:call-template name="DependentNameLoop">
        <xsl:with-param name="Loop" select="$HLoop/Loop"/>
      </xsl:call-template>
      <xsl:apply-templates select="$HLoop/Loop"/>
    </EligibilityBenefitResponse>

  </xsl:template>

  <xsl:template match="Loop[count(EB)>0]">
    <BenefitInfo>
      <xsl:apply-templates select="EB"/>
      <xsl:for-each select="REF">
        <Identification>
          <xsl:call-template name="Identification">
            <xsl:with-param name="REF" select="."/>
          </xsl:call-template>
        </Identification>
      </xsl:for-each>
      <xsl:for-each select="DTP">
        <xsl:call-template name="DTPSegment">
          <xsl:with-param name="DTP" select="."/>
        </xsl:call-template>
      </xsl:for-each>
      <xsl:for-each select="AAA">
        <RequestValidation>
          <xsl:call-template name="RequestValidation">
            <xsl:with-param name="AAA" select="."/>
          </xsl:call-template>
        </RequestValidation>
      </xsl:for-each>
      <xsl:for-each select="MSG">
        <Message>
          <xsl:value-of select="MSG01"/>
        </Message>
      </xsl:for-each>
      <xsl:apply-templates select="Loop[count(III) > 0]"/>
      <xsl:apply-templates select="Loop/Loop[@LoopId='2120C' or @LoopId='2120D']"/>
    </BenefitInfo>
  </xsl:template>

  <xsl:template match="Loop[count(III) > 0]">
    <AdditionalInfo>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="III/III01"/>
      </xsl:attribute>
      <xsl:attribute name="IndustryCode">
        <xsl:value-of select="III/III02"/>
      </xsl:attribute>
      <xsl:attribute name="CodeCategory">
        <xsl:value-of select="III/III03"/>
      </xsl:attribute>
      <xsl:value-of select="III/III04"/>
    </AdditionalInfo>
  </xsl:template>

  <xsl:template match="Loop[@LoopId='2120C' or @LoopId='2120D']">
    <RelatedEntity>
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
      <xsl:for-each select="PER">
        <Contact>
          <xsl:call-template name="Contact">
            <xsl:with-param name="PER" select="."/>
          </xsl:call-template>
        </Contact>
      </xsl:for-each>
      <xsl:for-each select="PRV">
        <ProviderInfo>
          <xsl:call-template name="ProviderInformation">
            <xsl:with-param name="PRV" select="."/>
          </xsl:call-template>
        </ProviderInfo>
      </xsl:for-each>
    </RelatedEntity>
  </xsl:template>


  <xsl:template match="EB">
    <xsl:if test="string-length(EB07)>0">
      <xsl:attribute name="Amount">
        <xsl:value-of select="EB07"/>
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="string-length(EB08)>0">
      <xsl:attribute name="Percentage">
        <xsl:value-of select="EB08"/>
      </xsl:attribute>
    </xsl:if>

    <InfoType>
      <xsl:attribute name="Code">
        <xsl:value-of select="EB01"/>
      </xsl:attribute>
      <xsl:value-of select="EB01/comment()"/>
    </InfoType>

    <xsl:if test="string-length(EB02)>0">
      <CoverageLevel>
        <xsl:attribute name="Code">
          <xsl:value-of select="EB02"/>
        </xsl:attribute>
        <xsl:value-of select="EB02/comment()"/>
      </CoverageLevel>
    </xsl:if>

    <xsl:choose>
      <xsl:when test="count(EB03/child::*)>0">
        <xsl:for-each select="EB03/child::*">
          <ServiceType>
            <xsl:attribute name="Code">
              <xsl:value-of select="."/>
            </xsl:attribute>
            <xsl:value-of select="./comment()"/>
          </ServiceType>
        </xsl:for-each>
      </xsl:when>
      <xsl:otherwise>
        <xsl:if test="string-length(EB03)>0">
          <ServiceType>
            <xsl:attribute name="Code">
              <xsl:value-of select="EB03"/>
            </xsl:attribute>
            <xsl:value-of select="EB03/comment()"/>
          </ServiceType>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>

    <xsl:if test="string-length(EB04)>0">
      <InsuranceType>
        <xsl:attribute name="Code">
          <xsl:value-of select="EB04"/>
        </xsl:attribute>
        <xsl:value-of select="EB04/comment()"/>
      </InsuranceType>
    </xsl:if>

    <xsl:if test="string-length(EB05)>0">
      <PlanCoverageDescription>
        <xsl:value-of select="EB05"/>
      </PlanCoverageDescription>
    </xsl:if>

    <xsl:if test="string-length(EB06)>0">
      <TimePeriod>
        <xsl:attribute name="Code">
          <xsl:value-of select="EB06"/>
        </xsl:attribute>
        <xsl:value-of select="EB06/comment()"/>
      </TimePeriod>
    </xsl:if>
    <xsl:if test="string-length(EB10)>0">
      <Quantity>
        <xsl:if test="string-length(EB09)>0">
          <xsl:attribute name="Qualifier">
            <xsl:value-of select="EB09"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:attribute name="Amount">
          <xsl:value-of select="EB10"/>
        </xsl:attribute>
        <xsl:value-of select="EB09/comment()"/>
      </Quantity>
    </xsl:if>

    <xsl:if test="string-length(EB11)>0">
      <AuthorizationCertificationRequired>
        <xsl:attribute name="Code">
          <xsl:value-of select="EB11"/>
        </xsl:attribute>
        <xsl:value-of select="EB11/comment()"/>
      </AuthorizationCertificationRequired>
    </xsl:if>

    <xsl:if test="string-length(EB12)>0">
      <InPlanNetwork>
        <xsl:attribute name="Code">
          <xsl:value-of select="EB12"/>
        </xsl:attribute>
        <xsl:value-of select="EB12/comment()"/>
      </InPlanNetwork>
    </xsl:if>


    <xsl:if test="string-length(EB13)>0">
      <Procedure>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="EB13/EB1301"/>
        </xsl:attribute>
        <xsl:attribute name="ProcedureCode">
          <xsl:value-of select="EB13/EB1302"/>
        </xsl:attribute>
        <xsl:if test="string-length(EB13/EB1303)>0">
          <xsl:attribute name="Modifier1">
            <xsl:value-of select="EB13/EB1303"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(EB13/EB1304)>0">
          <xsl:attribute name="Modifier2">
            <xsl:value-of select="EB13/EB1304"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(EB13/EB1305)>0">
          <xsl:attribute name="Modifier3">
            <xsl:value-of select="EB13/EB1305"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(EB13/EB1306)>0">
          <xsl:attribute name="Modifier4">
            <xsl:value-of select="EB13/EB1306"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(EB13/EB1308)>0">
          <xsl:attribute name="ProcedureCodeEnd">
            <xsl:value-of select="EB13/EB1308"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:value-of select="EB13/EB1301/comment()"/>
      </Procedure>
    </xsl:if>
  </xsl:template>

  <xsl:template name="SubscriberHLoop">
    <xsl:param name="HLoop" />
    <xsl:attribute name="TransactionControlNumber">
      <xsl:value-of select="$HLoop/../../.././ST/ST02"/>
    </xsl:attribute>
    <xsl:call-template name="SourceNameLoop">
      <xsl:with-param name="Loop" select="$HLoop/../.././Loop"/>
    </xsl:call-template>
    <xsl:call-template name="ReceiverNameLoop">
      <xsl:with-param name="Loop" select="$HLoop/.././Loop"/>
    </xsl:call-template>
    <xsl:call-template name="SubscriberNameLoop">
      <xsl:with-param name="Loop" select="$HLoop/Loop"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="SourceNameLoop">
    <xsl:param name="Loop"/>
    <Source>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="$Loop"/>
        </xsl:call-template>
      </Name>
      <xsl:for-each select="$Loop/PER">
        <Contact>
          <xsl:call-template name="Contact">
            <xsl:with-param name="PER" select="."/>
          </xsl:call-template>
        </Contact>
      </xsl:for-each>
      <xsl:for-each select="$Loop/AAA">
        <RequestValidation>
          <xsl:call-template name="RequestValidation">
            <xsl:with-param name="AAA" select="."/>
          </xsl:call-template>
        </RequestValidation>
      </xsl:for-each>
    </Source>
  </xsl:template>

  <xsl:template name="ReceiverNameLoop">
    <xsl:param name="Loop"/>
    <Receiver>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="$Loop"/>
        </xsl:call-template>
      </Name>
      <xsl:for-each select="$Loop/AAA">
        <RequestValidation>
          <xsl:call-template name="RequestValidation">
            <xsl:with-param name="AAA" select="."/>
          </xsl:call-template>
        </RequestValidation>
      </xsl:for-each>
    </Receiver>
  </xsl:template>

  <xsl:template name="SubscriberNameLoop">
    <xsl:param name="Loop"/>
    <Subscriber>
      <xsl:call-template name="Member">
        <xsl:with-param name="Loop" select="$Loop"/>
      </xsl:call-template>
    </Subscriber>
  </xsl:template>

  <xsl:template name="DependentNameLoop">
    <xsl:param name="Loop"/>
    <xsl:variable name="RelationshipCode" select="$Loop/INS/INS02"/>
    <Dependent>
      <xsl:call-template name="Member">
        <xsl:with-param name="Loop" select="$Loop"/>
      </xsl:call-template>
      <xsl:if test="string-length($RelationshipCode) > 0">
        <Relationship>
          <xsl:attribute name="Code">
            <xsl:value-of select ="$RelationshipCode"/>
          </xsl:attribute>
          <xsl:choose>
            <xsl:when test="$RelationshipCode = '01'">Spouse</xsl:when>
            <xsl:when test="$RelationshipCode = '19'">Child</xsl:when>
            <xsl:when test="$RelationshipCode = '20'">Employee</xsl:when>
            <xsl:when test="$RelationshipCode = '21'">Unknown</xsl:when>
            <xsl:when test="$RelationshipCode = '39'">Organ Donor</xsl:when>
            <xsl:when test="$RelationshipCode = '40'">Cadaver Donor</xsl:when>
            <xsl:when test="$RelationshipCode = '53'">Life Partner</xsl:when>
            <xsl:when test="$RelationshipCode = 'G8'">Other Relationship</xsl:when>
          </xsl:choose>
        </Relationship>
      </xsl:if>
    </Dependent>
  </xsl:template>

  <!-- Common Templates -->
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
    <xsl:for-each select="$Loop/AAA">
      <RequestValidation>
        <xsl:call-template name="RequestValidation">
          <xsl:with-param name="AAA" select="."/>
        </xsl:call-template>
      </RequestValidation>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="EntityName">
    <xsl:param name="Loop"/>
    <xsl:attribute name="Identifier">
      <xsl:value-of select="$Loop/NM1/NM101"/>
    </xsl:attribute>
    <xsl:attribute name="Qualifier">
      <xsl:choose>
        <xsl:when test="$Loop/NM1/NM102='1'">Person</xsl:when>
        <xsl:otherwise>NonPerson</xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
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
      <xsl:if test="string-length($Loop/NM1/NM106)>0">
        <xsl:attribute name="Prefix">
          <xsl:value-of select="$Loop/NM1/NM106"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length($Loop/NM1/NM107)>0">
        <xsl:attribute name="Suffix">
          <xsl:value-of select="$Loop/NM1/NM107"/>
        </xsl:attribute>
      </xsl:if>
    </xsl:if>
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
    <Line2>
      <xsl:value-of select="$N3/N302"/>
    </Line2>
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

  <xsl:template name="RequestValidation">
    <xsl:param name="AAA"/>
    <xsl:attribute name="ValidRequest">
      <xsl:choose>
        <xsl:when test="AAA01='Y'">
          true
        </xsl:when>
        <xsl:otherwise>
          false
        </xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
    <RejectReason>
      <xsl:attribute name="Code">
        <xsl:value-of select="AAA03"/>
      </xsl:attribute>
      <xsl:value-of select="AAA03/comment()"/>
    </RejectReason>
    <FollupAction>
      <xsl:attribute name="Code">
        <xsl:value-of select="AAA04"/>
      </xsl:attribute>
      <xsl:value-of select="AAA04/comment()"/>
    </FollupAction>
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
    <xsl:attribute name="ProviderDescription">
      <xsl:value-of select="PRV01/comment()"/>
    </xsl:attribute>
    <xsl:attribute name="Qualifier">
      <xsl:value-of select="PRV02"/>
    </xsl:attribute>
    <xsl:attribute name="Id">
      <xsl:value-of select="PRV03"/>
    </xsl:attribute>
    <xsl:value-of select="PRV02/comment()"/>
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
