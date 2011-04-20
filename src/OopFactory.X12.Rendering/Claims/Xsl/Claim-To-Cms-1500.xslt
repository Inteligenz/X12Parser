<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns="http://www.OopFactory.com/Form.xsd"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="Claim[@Type='Professional']">
    <form form-master-template-ref="cms-1500">
      <box id="2" x="1" y="4">
        <xsl:value-of select="concat(../Patient/Name/@Last,', ',../Patient/Name/@First,' ',../Patient/Name/@Middle)"/>
      </box>
        <box id="3MM">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,6,2)"/>
      </box>
      <box id="3DD">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,9,2)"/>
      </box>
      <box id="3YY">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,3,2)"/>
      </box>
      <box id="3SexM">
        <xsl:if test="../Patient/Demographic/@Gender='M'">X</xsl:if>
      </box>
      <box id="3SexF">
        <xsl:if test="../Patient/Demographic/@Gender='F'">X</xsl:if>
      </box>
      <box id="5" x="1" y="6">
        <xsl:value-of select="../Patient/AddressLine"/>
      </box>
      <box id="5City">
        <xsl:value-of select="../Patient/Locale/@City"/>
      </box>
      <box id="5Zip">
        <xsl:value-of select="../Patient/Locale/@PostalAddress"/>
      </box>
    </form>
  </xsl:template>
</xsl:stylesheet>
