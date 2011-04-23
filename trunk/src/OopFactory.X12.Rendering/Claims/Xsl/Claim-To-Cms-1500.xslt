<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:oop="http://www.OopFactory.com/Form.xsd"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="claim-image"/>

  <xsl:template match="/Interchange">
    <Interchange>

      <oop:form-master-template name="cms-1500"
        page-width-in="8.5" page-height-in="11"
        margin-top-in="0.0625" margin-left-in="0.625" margin-bottom-in="0" margin-right-in="0.140"
                                x-scale="0.0935" y-scale="0.157" x-offset="-0.21" y-offset="0.1">
        <xsl:attribute name="background-image">
          <xsl:value-of select="$claim-image"/>
        </xsl:attribute>
      </oop:form-master-template>
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
      
      <oop:box id="header" x="33" y="1" width="50">
        <xsl:value-of select="@PatientControlNumber"/>
      </oop:box>
      <oop:box id="1-medicare" x="10.5" y="7" width="2.5" text-align="center">X</oop:box>
      <oop:box id="1-tricare" x="17.5" y="7" width="2.5" text-align="center">X</oop:box>
      <oop:box id="1-champ-va" x="26.5" y="7" width="2.5" text-align="center">X</oop:box>
      <oop:box id="1-group-health-plan" x="41.5" y="7" width="2.5" text-align="center">X</oop:box>
      <oop:box id="1-feca-blk-lung" x="41.5" y="7" width="2.5" text-align="center">X</oop:box>
      <oop:box id="1-other" x="47.5" y="7" width="2.6" text-align="center">X</oop:box>
      <oop:box id="2" x="4" y="9" width="28.5">
        <xsl:value-of select="concat(../Patient/Name/@Last,', ',../Patient/Name/@First,' ',../Patient/Name/@Middle)"/>
      </oop:box>
      <oop:box id="3-dob" x="34" y="9" width="9" text-align="center">
        <xsl:variable name="dob" select="../Patient/Demographic/@DateOfBirth"/>
        <xsl:value-of select="concat(substring($dob,6,2),' ',substring($dob,9,2),' ',substring($dob,3,2))"/>
      </oop:box>
      <oop:box id="3-sex-m" x="44.5" y="9" width="2.5" text-align="center">
        <xsl:if test="../Patient/Demographic/@Gender='M'">X</xsl:if>
      </oop:box>
      <oop:box id="3-sex-f" x="49.5" y="9" width="2.5" text-align="center">
        <xsl:if test="../Patient/Demographic/@Gender='F'">X</xsl:if>
      </oop:box>
      <oop:box id="5-address" x="4" y="11" width="28.5">
        <xsl:value-of select="../Patient/AddressLine"/>
      </oop:box>
      <oop:box id="5-city" x="4" y="13" width="25">
        <xsl:value-of select="../Patient/Locale/@City"/>
      </oop:box>
      <oop:box id="5-state" x="29" y="13" width="3.5" text-align="center">
        <xsl:value-of select="../Patient/Locale/@State"/>
      </oop:box>
      <oop:box id="5-zip" x="4" y="15" width="13" text-align="center">
        <xsl:value-of select="../Patient/Locale/@PostalAddress"/>
      </oop:box>
    </oop:form>
  </xsl:template>
</xsl:stylesheet>
