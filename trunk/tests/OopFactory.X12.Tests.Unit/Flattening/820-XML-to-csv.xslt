<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="yes"/>

  <xsl:template match="Interchange">
Transaction,Creation Date,Payer Name,Payee Name, Payer Tax ID
<xsl:apply-templates select="FunctionGroup/Transaction/Loop[@LoopId='2000']/Loop[@LoopId='2100']"/>
  </xsl:template>
  
    <xsl:template match="Loop[@LoopId='2100']" >
      <xsl:variable name="trans" select="../../."/>
      <xsl:variable name="payer" select="$trans/Loop[@LoopId='1000A']"/>
      <xsl:variable name="payee" select="$trans/Loop[@LoopId='1000B']"/>
      <xsl:variable name="header" select="../."/>
      
      <xsl:value-of select="$trans/ST/ST02"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$trans/DTM[DTM01='097']/DTM02"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$payer/N1/N102"/>      
      <xsl:value-of select="','"/>
      <xsl:value-of select="$payee/N1/N102"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$payee/N1[N103='FI']/N104"/>
      <xsl:value-of select="','"/>
      <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
