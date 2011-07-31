<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="yes"/>

  <xsl:template match="@* | node()">
      <xsl:apply-templates select="@* | node()"/>
  </xsl:template>
  
  <xsl:template match="ISA">
    <xsl:text>Billing Provider Name, Billing Provider ID, Subscriber Name, Patient Name, Patient Control Number, Total Claim Charge Amount&#x0A;</xsl:text>
  </xsl:template>
  
  <xsl:template match="Loop[@LoopId='2300']">
    <xsl:choose>
      <xsl:when test="../@LoopId = '2000B'">
        <xsl:call-template name="BillingProvider">
          <xsl:with-param name="HLoop" select="../../."/>
        </xsl:call-template>
        <xsl:call-template name="Subscriber">
          <xsl:with-param name="HLoop" select="../."/>
        </xsl:call-template>
        <xsl:call-template name="PatientName">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010BA']"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="BillingProvider">
          <xsl:with-param name="HLoop" select="../../../."/>
        </xsl:call-template>
        <xsl:call-template name="Subscriber">
          <xsl:with-param name="HLoop" select="../../."/>
        </xsl:call-template>
        <xsl:call-template name="PatientName">
          <xsl:with-param name="Loop" select="../Loop[@LoopId='2010CA']"/>
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:value-of select="CLM/CLM01"/>
    <xsl:text>,</xsl:text>
    <xsl:value-of select="CLM/CLM02"/>
    <xsl:text>&#x0A;</xsl:text>
  </xsl:template>

  <xsl:template name="BillingProvider">
    <xsl:param name="HLoop"/>
    <xsl:value-of select="$HLoop/Loop[@LoopId='2010AA']/NM1/NM103"/>
    <xsl:text>,</xsl:text>
    <xsl:value-of select="$HLoop/Loop[@LoopId='2010AA']/NM1/NM109"/>
    <xsl:text>,</xsl:text>
  </xsl:template>

  <xsl:template name="Subscriber">
    <xsl:param name="HLoop"/>
    <xsl:text>"</xsl:text>
    <xsl:value-of select="$HLoop/Loop[@LoopId='2010BA']/NM1/NM104"/>
    <xsl:text>, </xsl:text>
    <xsl:value-of select="$HLoop/Loop[@LoopId='2010BA']/NM1/NM103"/>
    <xsl:text>",</xsl:text>
  </xsl:template>
  <xsl:template name="PatientName">
    <xsl:param name="Loop"/>
    <xsl:text>"</xsl:text>
    <xsl:value-of select="$Loop/NM1/NM104"/>
    <xsl:text>, </xsl:text>
    <xsl:value-of select="$Loop/NM1/NM103"/>
    <xsl:text>",</xsl:text>
  </xsl:template>
</xsl:stylesheet>
