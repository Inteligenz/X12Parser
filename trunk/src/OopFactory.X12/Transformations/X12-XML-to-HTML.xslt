<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/> 

    <xsl:template match="node()">
      <xsl:if test="*[name()!='Loop']">
        <xsl:call-template name="Segment">
          <xsl:with-param name="seg" select="."/>
        </xsl:call-template>

      </xsl:if>
    </xsl:template>

  <xsl:template name="Component">
    <xsl:param name="element"/>
    <xsl:for-each select="$element/*"><xsl:if test="position() != 1">:</xsl:if>
      <xsl:choose>
        <xsl:when test="string-length(comment()) > 0">
          <span class="component" style="color:blue">
            <xsl:attribute name="title"><xsl:value-of select="comment()"/></xsl:attribute>
            <xsl:value-of select="."/>
          </span>
        </xsl:when>
        <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
      </xsl:choose> 
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template name="Element">
    <xsl:param name="element"/>
    <span class="element">
      <xsl:if test="string-length(preceding-sibling::node()[self::*|self::comment()][1][self::comment()]) > 0">
        <xsl:attribute name="title">
          <xsl:value-of select="preceding-sibling::node()[self::*|self::comment()][1][self::comment()]"/>
        </xsl:attribute>
        <xsl:attribute name="style">background-color: #efefef;</xsl:attribute>
      </xsl:if>
      *<span class="element-value">
        <xsl:if test="string-length(comment()) > 0">
          <xsl:attribute name="title">
            <xsl:value-of select="comment()"/>
          </xsl:attribute>
          <xsl:attribute name="style">color:blue</xsl:attribute>
        </xsl:if>
        <xsl:choose>
          <xsl:when test="count($element/*) = 0">
            <xsl:value-of select="$element"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="Component">
              <xsl:with-param name="element" select="$element"/>
            </xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>
      </span>
    </span>
  </xsl:template>

  <xsl:template name="Segment">
    <xsl:param name="seg"/>
    <xsl:variable name="segId" select="name()" />
    <div>
      <xsl:choose>
        <xsl:when test="position() = 2">
          <xsl:attribute name="class">first-segment</xsl:attribute>
          <xsl:attribute name="style">font-weight: bold;</xsl:attribute>
        </xsl:when>
        <xsl:otherwise>
          <xsl:attribute name="class">segment</xsl:attribute>
          <xsl:attribute name="style">margin-left: 25px;</xsl:attribute>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:value-of select="$segId"/><xsl:for-each select="*">
        <xsl:call-template name="Element">
          <xsl:with-param name="element" select="."/>
        </xsl:call-template>
      </xsl:for-each>~</div>
  </xsl:template>
  
  <xsl:template match="Loop">
    <div class="loop" style="border: 1px dotted gray; margin-left: 20px; margin-bottom: 3px; padding: 5px;">
      <xsl:attribute name="title">Loop ID: <xsl:value-of select="@LoopId"/>, <xsl:value-of select="@Name"/></xsl:attribute>
      <div style="float:right; font-size: smaller; color: lightgrey;"><xsl:value-of select="@Name"/></div>
      
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>

  <xsl:template match="HierarchicalLoop">
    <div class="hierarchical-loop" style="border: 2px solid black; margin-left: 20px; padding: 5px;">
      <xsl:attribute name="title">Loop ID: <xsl:value-of select="@LoopId"/>, <xsl:value-of select="@LoopName"/></xsl:attribute>
      <div style="float:right; font-size: smaller; color: lightgrey;">
        <xsl:value-of select="@LoopName"/>
      </div>
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>
  <xsl:template match="Transaction">
    <div class="transaction" style="border: 3px double black; margin-left: 20px; padding: 5px;">
      <div style="float:right; font-size: smaller; color: lightgrey;">
        TRANSACTION
      </div>
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>

  <xsl:template match="FunctionGroup">
    <div class="function-group" style="margin-left: 20px; border: 1px dotted gray; padding-right: 10px;">
      <div style="float:right; font-size: smaller; color: lightgrey;">
        FUNCTION GROUP
      </div>
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>

  <xsl:template match="Interchange">
    <div class="interchange" style=" border: 1px dotted gray; padding-right: 10px;">
      <div style="float:right; font-size: smaller; color: lightgrey;">
        INTERCHANGE
      </div>
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>

</xsl:stylesheet>
