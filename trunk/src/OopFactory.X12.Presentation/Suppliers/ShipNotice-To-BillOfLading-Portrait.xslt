<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
>
  <xsl:param name="debug" select="0"/>
  <xsl:param name="xOffset" select="-4"/>
  <xsl:param name="yOffset" select="-10"/>
  <xsl:param name="xScale" select="7.2"/>
  <xsl:param name="yScale" select="14.1"/>
  <xsl:param name="bolPortrait"/>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template name="field">
    <xsl:param name="value"/>
    <xsl:param name="top"/>
    <xsl:param name="left"/>
    <xsl:param name="width"/>
    <xsl:param name="align"/>
    <fo:block-container padding="2pt" position="absolute">
      <xsl:if test="$debug = 1">
        <xsl:attribute name="border-color">red</xsl:attribute>
        <xsl:attribute name="border-style">solid</xsl:attribute>
        <xsl:attribute name="border-width">medium</xsl:attribute>
        <xsl:attribute name="border-bottom-style">none</xsl:attribute>
      </xsl:if>        
      <xsl:attribute name="top"><xsl:value-of select="$yOffset + $top * $yScale"/>pt</xsl:attribute>
      <xsl:attribute name="left"><xsl:value-of select="$xOffset + $left * $xScale"/>pt</xsl:attribute>
      <xsl:attribute name="height"><xsl:value-of select="$yScale * 1.25"/>pt</xsl:attribute>
      <xsl:attribute name="width"><xsl:value-of select="$width * $xScale"/>pt</xsl:attribute>
      <xsl:if test="string-length($align)>0">
        <xsl:attribute name="text-align">
          <xsl:value-of select="$align"/>
        </xsl:attribute>        
      </xsl:if>
      <fo:block>
        <xsl:value-of select="$value"/>
      </fo:block>
    </fo:block-container>
  </xsl:template>

  <xsl:template match="Party">
    <xsl:variable name="top">
      <xsl:choose>
        <xsl:when test="Type/@Code = 'SF'">3.3</xsl:when>
        <xsl:when test="Type/@Code = 'ST'">8.4</xsl:when>
        <xsl:otherwise>0</xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:call-template name="field">
      <xsl:with-param name="value" select="Name"/>
      <xsl:with-param name="top" select="$top"/>
      <xsl:with-param name="left" select="11"/>
      <xsl:with-param name="width" select="33.5"/>
    </xsl:call-template>
    <xsl:variable name="ShipAddress">
      <xsl:value-of select="Address/Line1"/>
      <xsl:value-of select="Address/Line2"/>
    </xsl:variable>
    <xsl:call-template name="field">
      <xsl:with-param name="value" select="$ShipAddress"/>
      <xsl:with-param name="top" select="$top + 1"/>
      <xsl:with-param name="left" select="11"/>
      <xsl:with-param name="width" select="33.5"/>
    </xsl:call-template>
    <xsl:variable name="Location">
      <xsl:value-of select="Address/City"/>&#60;<xsl:value-of select="Address/@StateCode"/>, <xsl:value-of select="Address/@PostalCode"/>
    </xsl:variable>
    <xsl:call-template name="field">
      <xsl:with-param name="value" select="$Location"/>
      <xsl:with-param name="top" select="$top + 2"/>
      <xsl:with-param name="left" select="11"/>
      <xsl:with-param name="width" select="33.5"/>
    </xsl:call-template>
    <xsl:call-template name="field">
      <xsl:with-param name="value" select="Identification"/>
      <xsl:with-param name="top" select="$top + 3"/>
      <xsl:with-param name="left" select="11"/>
      <xsl:with-param name="width" select="24"/>
    </xsl:call-template>
    
  </xsl:template>
  <xsl:template match="Shipment">
    <fo:page-sequence master-reference="Portrait" font-family="Courier" color="blue" font-size="12pt">
      <fo:flow>
        <fo:block>
          <fo:external-graphic>
            <xsl:attribute name="src">
              <xsl:value-of select="$bolPortrait"/>
            </xsl:attribute>
          </fo:external-graphic>
        </fo:block>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="substring(../DateReference[@Qualifier='011'],1,10)"/>
          <xsl:with-param name="top" select="1"/>
          <xsl:with-param name="left" select="6"/>
          <xsl:with-param name="width" select="12"/>
        </xsl:call-template>
        <xsl:apply-templates select="Party[Type/@Code='SF']"/>
        <xsl:apply-templates select="Party[Type/@Code='ST']"/>

        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'Line 1'"/>
          <xsl:with-param name="top" select="23"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="22"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'Line 2'"/>
          <xsl:with-param name="top" select="24"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="22"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'Line 3'"/>
          <xsl:with-param name="top" select="25"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="22"/>
        </xsl:call-template>

        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'Line 4'"/>
          <xsl:with-param name="top" select="26"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="22"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'Line 5'"/>
          <xsl:with-param name="top" select="27"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="22"/>
        </xsl:call-template>

        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'100'"/>
          <xsl:with-param name="top" select="32.4"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="4.5"/>
          <xsl:with-param name="align" select="'right'"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'100'"/>
          <xsl:with-param name="top" select="33.4"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="4.5"/>
          <xsl:with-param name="align" select="'right'"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'5'"/>
          <xsl:with-param name="top" select="34.4"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="4.5"/>
          <xsl:with-param name="align" select="'right'"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'100'"/>
          <xsl:with-param name="top" select="35.4"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="4.5"/>
          <xsl:with-param name="align" select="'right'"/>
        </xsl:call-template>
        <xsl:call-template name="field">
          <xsl:with-param name="value" select="'50'"/>
          <xsl:with-param name="top" select="36.4"/>
          <xsl:with-param name="left" select="1"/>
          <xsl:with-param name="width" select="4.5"/>
          <xsl:with-param name="align" select="'right'"/>
        </xsl:call-template>      
      </fo:flow>

    </fo:page-sequence>
  </xsl:template>

  <xsl:template match="/ArrayOfTransaction">
    <fo:root>
      <fo:layout-master-set>
        <fo:simple-page-master master-name="Portrait" page-height="11in" page-width="8.5in"
                               margin-left="0.25in" margin-right="0.25in" margin-top="0.75in">
          <fo:region-body margin="0in"/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      <xsl:apply-templates select="Transaction/Shipment"/>
    </fo:root>
    </xsl:template>
</xsl:stylesheet>
