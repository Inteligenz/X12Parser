<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="yes"/>

  <xsl:template match="Interchange">
Transaction,Creation Date,Submitter Name, Borrower Last Name, Remittance ID
<xsl:apply-templates select="FunctionGroup/Transaction/Loop[@LoopId='ENT']/Loop[@LoopId='RMR']"/>
  </xsl:template>
  
    <xsl:template match="Loop[@LoopId='RMR']" >
      <xsl:variable name="trans" select="../../."/>
      <xsl:variable name="entity" select="../."/>
      <xsl:value-of select="$trans/ST/ST02"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$trans/DTM[DTM01='097']/DTM02"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$trans/Loop[@LoopId='N1']/N1[N101='41']/N102"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="$entity/Loop[@LoopId='NM1']/NM1[NM101='BW']/NM103"/>
      <xsl:value-of select="','"/>
      <xsl:value-of select="RMR/RMR02" />
      <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
</xsl:stylesheet>
