<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" indent="yes"/>
  <xsl:param name="segment-terminator" select="/Interchange/@segment-terminator"/>
  <xsl:param name="element-separator" select="/Interchange/@element-separator"/>
  <xsl:param name="sub-element-separator" select="/Interchange/@sub-element-separator"/>

  <xsl:template match="@* | node()">
    <xsl:if test="name(.) != 'Interchange' and name(.) != 'FunctionGroup' and name(.) != 'Transaction' and name(.) != 'HierarchicalLoop' and name(.) != 'Loop'">
      <xsl:value-of select="name(.)"/>
      <xsl:value-of select="$element-separator"/>
      <xsl:for-each select="child::*">
        <xsl:choose>
          <xsl:when test="count(child::*)>0">
            <xsl:for-each select="child::*">
              <xsl:value-of select="."/>
              <xsl:if test="position() &lt; count(../*)">
                <xsl:value-of select="$sub-element-separator"/>
              </xsl:if>
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="."/>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:if test="position() &lt; count(../*)">
          <xsl:value-of select="$element-separator"/>
        </xsl:if>
      </xsl:for-each>
      <xsl:choose>
        <xsl:when test="$segment-terminator='HQ=='">
          <xsl:value-of select="'~'"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$segment-terminator"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:if>
  </xsl:template>

  <xsl:template match="Interchange">
    <xsl:apply-templates select="child::*"/>
  </xsl:template>

  <xsl:template match="FunctionGroup">
    <xsl:apply-templates select="child::*"/>
  </xsl:template>

  <xsl:template match="Transaction">
    <xsl:apply-templates select="child::*"/>
  </xsl:template>

  <xsl:template match="HierarchicalLoop">
    <xsl:apply-templates select="child::*"/>  
  </xsl:template>
  
  <xsl:template match="Loop">
    <xsl:apply-templates select="child::*"/>
  </xsl:template>
</xsl:stylesheet>
