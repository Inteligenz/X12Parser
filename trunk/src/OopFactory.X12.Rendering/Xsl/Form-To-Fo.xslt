<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="Form[@Number='CMS-1500']">
    <fo:page-sequence>
      <fo:flow>
        
        <fo:block-container position="absolute">
          <fo:block>
            <xsl:value-of select="Box[@Id=2]"/>
          </fo:block>
        </fo:block-container>
      </fo:flow>
    </fo:page-sequence>
  </xsl:template>
  
</xsl:stylesheet>
