<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:apply-templates select="@* | node()"/>
    </xsl:template>

  <xsl:template match="Loop[@LoopId='2300']">
    <UB04Claim>
      <Field03a_PatientControlNumber>
        <xsl:value-of select="CLM/CLM01"/>
      </Field03a_PatientControlNumber>
    </UB04Claim>
  </xsl:template>
</xsl:stylesheet>
