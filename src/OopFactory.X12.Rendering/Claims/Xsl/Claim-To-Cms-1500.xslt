<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:oop="http://www.OopFactory.com/Form.xsd"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/Interchange">
    <Interchange>

      <oop:form-master-template name="cms-1500"
        page-width-in="8.5" page-height-in="11"
        margin-top-in="0.5" margin-left-in="0.5" margin-bottom-in="0.5" margin-right-in="0.5" />
      <xsl:apply-templates select="@* | node()"/>
    </Interchange>
  </xsl:template>
  
    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="Claim[@Type='Professional']">
    <oop:form form-master-template-ref="cms-1500">
      <oop:box id="2" x="1" y="4">
        <xsl:value-of select="concat(../Patient/Name/@Last,', ',../Patient/Name/@First,' ',../Patient/Name/@Middle)"/>
      </oop:box>
      <oop:box id="3-MM" x="20" y="4">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,6,2)"/>
      </oop:box>
      <oop:box id="3-DD">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,9,2)"/>
      </oop:box>
      <oop:box id="3-YY">
        <xsl:value-of select="substring(../Patient/Demographic/@DateOfBirth,3,2)"/>
      </oop:box>
      <oop:box id="3-Sex-M">
        <xsl:if test="../Patient/Demographic/@Gender='M'">X</xsl:if>
      </oop:box>
      <oop:box id="3-Sex-F">
        <xsl:if test="../Patient/Demographic/@Gender='F'">X</xsl:if>
      </oop:box>
      <oop:box id="5-Address" x="1" y="6">
        <xsl:value-of select="../Patient/AddressLine"/>
      </oop:box>
      <oop:box id="5-City">
        <xsl:value-of select="../Patient/Locale/@City"/>
      </oop:box>
      <oop:box id="5-Zip">
        <xsl:value-of select="../Patient/Locale/@PostalAddress"/>
      </oop:box>
    </oop:form>
  </xsl:template>
</xsl:stylesheet>
