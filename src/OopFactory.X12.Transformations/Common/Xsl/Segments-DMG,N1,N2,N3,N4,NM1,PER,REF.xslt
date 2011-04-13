<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="NM1">
    <xsl:comment>
      <xsl:value-of select="NM101/comment()"/>
    </xsl:comment>
    <Identifier>
      <xsl:attribute name="Code">
        <xsl:value-of select="NM101"/>
      </xsl:attribute>
    </Identifier>
    <Name>
      <xsl:choose>
        <xsl:when test="NM102='1'">
          <xsl:attribute name="Last">
            <xsl:value-of select="NM103"/>
          </xsl:attribute>
          <xsl:attribute name="First">
            <xsl:value-of select="NM104"/>
          </xsl:attribute>
          <xsl:attribute name="Middle">
            <xsl:value-of select="NM105"/>
          </xsl:attribute>
          <xsl:value-of select="concat(NM103,', ',NM104)"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="NM103"/>
        </xsl:otherwise>
      </xsl:choose>
    </Name>
  </xsl:template>
</xsl:stylesheet>
