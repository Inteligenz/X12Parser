<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
                xmlns:oop="http://www.OopFactory.com/Form.xsd"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <fo:root>
      <fo:layout-master-set>
        <xsl:apply-templates select="Interchange/oop:form-master-template"/>
      </fo:layout-master-set>
      <xsl:apply-templates select="Interchange/FunctionGroup/node()"/>
    </fo:root>
  </xsl:template>

  <xsl:template match="@* | node()">
    <xsl:apply-templates select="@* | node()"/>
  </xsl:template>

  <xsl:template match="oop:form-master-template">
    <fo:page-sequence-master>
      <xsl:attribute name="master-name">
        <xsl:value-of select="@name"/>
      </xsl:attribute>
    </fo:page-sequence-master>
  </xsl:template>
  
  <xsl:template match="oop:form">
    <fo:page-sequence>
      <xsl:attribute name="master-reference">
        <xsl:value-of select="@form-master-template-ref"/>
      </xsl:attribute>
      <fo:flow>

        <fo:block-container position="absolute">
          <fo:block>
            <xsl:value-of select="box[@id='2']"/>
          </fo:block>
        </fo:block-container>
      </fo:flow>
    </fo:page-sequence>
  </xsl:template>

</xsl:stylesheet>
