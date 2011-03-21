<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="debug" select="0"/>

  <xsl:template match="PageMaster">
    <fo:simple-page-master>
      <xsl:attribute name="master-name"><xsl:value-of select="@Name"/></xsl:attribute>
      <xsl:attribute name="page-height"><xsl:value-of select="@Height"/>in</xsl:attribute>
      <xsl:attribute name="page-width"><xsl:value-of select="@Width"/>in</xsl:attribute>
      <xsl:attribute name="margin-left"><xsl:value-of select="@MarginLeft"/>in</xsl:attribute>
      <xsl:attribute name="margin-top"><xsl:value-of select="@MarginTop"/>in</xsl:attribute>
      <xsl:attribute name="margin-right"><xsl:value-of select="@MarginRight"/>in</xsl:attribute>
      <xsl:attribute name="margin-bottom"><xsl:value-of select="@MarginBottom"/>in</xsl:attribute>
      <fo:region-body margin="0in"/>
    </fo:simple-page-master>
  </xsl:template>

  <xsl:template match="PageSequence">
    <xsl:variable name="MasterReference" select="@MasterReference"/>
    <xsl:variable name="PageMaster" select="../PageMaster[@Name=$MasterReference]"/>
    <fo:page-sequence color="blue" font-size="12pt">
      <xsl:attribute name="master-reference"><xsl:value-of select="$MasterReference"/></xsl:attribute>
      <xsl:attribute name="font-family"><xsl:value-of select="$PageMaster/@FontFamily"/></xsl:attribute>
      <xsl:attribute name="font-size"><xsl:value-of select="$PageMaster/@FontSize"/>pt</xsl:attribute>
      <fo:flow>
        <fo:block>
          <fo:external-graphic>
            <xsl:attribute name="src">
              <xsl:value-of select="$PageMaster/@BackgroundImageUri"/>
            </xsl:attribute>
          </fo:external-graphic>
        </fo:block>
        <xsl:apply-templates select="Field"/>
      </fo:flow>
    </fo:page-sequence>
  </xsl:template>

  <xsl:template match="Field">
    <xsl:variable name="MasterReference" select="../@MasterReference"/>
    <xsl:variable name="PageMaster" select="../../PageMaster[@Name=$MasterReference]"/>
    <fo:block-container padding="2pt" position="absolute">
      <xsl:if test="$debug = 1">
        <xsl:attribute name="border-color">red</xsl:attribute>
        <xsl:attribute name="border-style">solid</xsl:attribute>
        <xsl:attribute name="border-width">medium</xsl:attribute>
        <xsl:attribute name="border-bottom-style">none</xsl:attribute>
      </xsl:if>
      <xsl:attribute name="top"><xsl:value-of select="$PageMaster/@YPointOffset + @Top * $PageMaster/@YPointScale"/>pt</xsl:attribute>
      <xsl:attribute name="left"><xsl:value-of select="$PageMaster/@XPointOffset + @Left * $PageMaster/@XPointScale"/>pt</xsl:attribute>
      <xsl:attribute name="height"><xsl:value-of select="$PageMaster/@YPointScale * 1.25"/>pt</xsl:attribute>
      <xsl:attribute name="width"><xsl:value-of select="@Width * $PageMaster/@XPointScale"/>pt</xsl:attribute>
      <xsl:if test="string-length(@Align)>0">
        <xsl:attribute name="text-align">
          <xsl:value-of select="@Align"/>
        </xsl:attribute>
      </xsl:if>
      <fo:block>
        <xsl:value-of select="."/>
      </fo:block>
    </fo:block-container>
  </xsl:template>
  
  <xsl:template match="/FoDocument">
    <fo:root>
      <fo:layout-master-set>
        <xsl:apply-templates select="PageMaster"/>
      </fo:layout-master-set>
      <xsl:apply-templates select="PageSequence"/>
    </fo:root>
  </xsl:template>
</xsl:stylesheet>
