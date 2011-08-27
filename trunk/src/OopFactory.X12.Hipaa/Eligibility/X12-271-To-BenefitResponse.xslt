<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="@* | node()">
      <xsl:apply-templates select="@* | node()"/>
  </xsl:template>

  <xsl:template match="Interchange">
    <ArrayOfBenefitResponse>
          <xsl:apply-templates select="@* | node()"/>
    </ArrayOfBenefitResponse>
  </xsl:template>

    <xsl:template match="Loop[@LoopId='2110C' or @LoopId='2110D']">
      <BenefitResponse>
        <xsl:choose>
          <xsl:when test="@LoopId='2110C'">
            <xsl:call-template name="SourceNameLoop">
              <xsl:with-param name="Loop" select="../../../.././Loop"/>
            </xsl:call-template>
            <xsl:call-template name="ReceiverNameLoop">
              <xsl:with-param name="Loop" select="../../.././Loop"/>
            </xsl:call-template>
            <xsl:call-template name="SubscriberNameLoop">
              <xsl:with-param name="Loop" select="../.././Loop"/>
            </xsl:call-template>
          </xsl:when>
          <xsl:when test="@LoopId='2110D'">
            <xsl:call-template name="SourceNameLoop">
              <xsl:with-param name="Loop" select="../../.././Loop"/>
            </xsl:call-template>
            <xsl:call-template name="ReceiverNameLoop">
              <xsl:with-param name="Loop" select="../.././Loop"/>
            </xsl:call-template>
            <xsl:call-template name="SubscriberNameLoop">
              <xsl:with-param name="Loop" select=".././Loop"/>
            </xsl:call-template>
          </xsl:when>
        </xsl:choose>
      </BenefitResponse>
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
    </xsl:if>
    <Identification>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="$Loop/NM1/NM108"/>
      </xsl:attribute>
      <xsl:attribute name="Id">
        <xsl:value-of select="$Loop/NM1/NM109"/>
      </xsl:attribute>
    </Identification>
  </xsl:template>

  <xsl:template name="SourceNameLoop">
    <xsl:param name="Loop"/>
    <Source>
      <Name>
        <xsl:call-template name="EntityName">
          <xsl:with-param name="Loop" select="$Loop"/>
        </xsl:call-template>
      </Name>
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
    </Receiver>
  </xsl:template>

  <xsl:template name="SubscriberNameLoop">
    <xsl:param name="Loop"/>
    <Subscriber>
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
      <xsl:for-each select="$Loop/DTP">
        <xsl:call-template name="DTPSegment">
          <xsl:with-param name="DTP" select="."/>
        </xsl:call-template>
      </xsl:for-each>
    </Subscriber>  
  </xsl:template>

  <xsl:template name="DTPSegment">
    <xsl:param name="DTP"/>
      <xsl:choose>
        <xsl:when test="$DTP/DTP02='D8'">
          <Date>
            <xsl:attribute name="Qualifier">
              <xsl:value-of select="$DTP/DTP01"/>
            </xsl:attribute>
            <xsl:attribute name="Date">
              <xsl:value-of select="concat(substring($DTP/DTP03,1,4),'-',substring($DTP/DTP03,5,2),'-',substring($DTP/DTP03,7,2))"/>
            </xsl:attribute>
          </Date>
        </xsl:when>
        <xsl:otherwise>
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
          </DateRange>
        </xsl:otherwise>
      </xsl:choose>
  </xsl:template>
  
</xsl:stylesheet>
