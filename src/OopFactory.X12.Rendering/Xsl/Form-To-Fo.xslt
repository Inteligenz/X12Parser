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
    <fo:simple-page-master margin="0in">
      <xsl:attribute name="master-name"><xsl:value-of select="@name"/></xsl:attribute>
      <xsl:attribute name="page-width"><xsl:value-of select="@page-width-in"/>in</xsl:attribute>
      <xsl:attribute name="page-height"><xsl:value-of select="@page-height-in"/>in</xsl:attribute>
      <fo:region-body>
        <xsl:attribute name="margin-top"><xsl:value-of select="@margin-top-in"/>in</xsl:attribute>
        <xsl:attribute name="margin-left"><xsl:value-of select="@margin-left-in"/>in</xsl:attribute>
        <xsl:attribute name="margin-right"><xsl:value-of select="@margin-right-in"/>in</xsl:attribute>
      </fo:region-body>
    </fo:simple-page-master>
  </xsl:template>
  
  <xsl:template match="oop:form">
    <xsl:variable name="reference" select="../@form-master-template-ref"/>
    <xsl:variable name="master" select="/Interchange/oop:form-master-template[@name=$reference]"/>
    <fo:page-sequence>
      <xsl:attribute name="master-reference">
        <xsl:value-of select="@form-master-template-ref"/>
      </xsl:attribute>
      <fo:flow flow-name="xsl-region-body" font-size="10pt" font-family="Courier">
        <xsl:apply-templates select="oop:box" />
      </fo:flow>
    </fo:page-sequence>
  </xsl:template>

  <xsl:template match="oop:box">
    <xsl:variable name="reference" select="../@form-master-template-ref"/>
    <xsl:variable name="master" select="/Interchange/oop:form-master-template[@name=$reference]"/>
    <xsl:variable name="x-scale" select="$master/@x-scale"/>
    <xsl:variable name="y-scale" select="$master/@y-scale"/>
    <fo:block-container position="absolute">
      <xsl:attribute name="left"><xsl:value-of select="@x * $x-scale"/>in</xsl:attribute>
      <xsl:attribute name="width"><xsl:value-of select="@width * $x-scale"/>in</xsl:attribute>
      <xsl:attribute name="top"><xsl:value-of select="@y * $y-scale"/>in</xsl:attribute>
      <xsl:attribute name="height"><xsl:value-of select="$y-scale * 2"/>in</xsl:attribute>
      <fo:block>
        <xsl:value-of select="."/>
      </fo:block>
    </fo:block-container>
  </xsl:template>

</xsl:stylesheet>
