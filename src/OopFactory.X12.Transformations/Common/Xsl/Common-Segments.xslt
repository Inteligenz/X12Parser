<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="AMT">
    <xsl:comment>
      <xsl:value-of select="AMT01/comment()"/>
    </xsl:comment>
    <Amount>
      <xsl:attribute name="Qual">
        <xsl:value-of select="AMT01"/>
      </xsl:attribute>
      <xsl:value-of select="AMT02"/>
    </Amount>
  </xsl:template>

  <xsl:template match="DTP">
    <xsl:comment>
      <xsl:value-of select="DTP01/comment()"/>
    </xsl:comment>
    <Date>
      <xsl:attribute name="Qual">
        <xsl:value-of select="DTP01"/>
      </xsl:attribute>
      <xsl:attribute name="Format">
        <xsl:value-of select="DTP02"/>
      </xsl:attribute>
      <xsl:variable name="from-date" select="concat(substring(DTP03,1,4),'-',substring(DTP03,5,2),'-',substring(DTP03,7,2))"/>
      <xsl:variable name="to-date" select="concat(substring(DTP03,10,4),'-',substring(DTP03,14,2),'-',substring(DTP03,16,2))"/>
      <xsl:choose>
        <xsl:when test="DTP02='D8'">
          <xsl:value-of select="$from-date"/>
        </xsl:when>
        <xsl:when test="DTP02='RD8'">
          <xsl:attribute name="From">
            <xsl:value-of select="$from-date"/>
          </xsl:attribute>
          <xsl:attribute name="To">
            <xsl:value-of select="$to-date"/>
          </xsl:attribute>
          <xsl:value-of select="concat($from-date,' to ',$to-date)"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="DTP03"/>
        </xsl:otherwise>
      </xsl:choose>
    </Date>
  </xsl:template>
  
  <xsl:template match="REF">
    <xsl:comment>
      <xsl:value-of select="REF01/comment()"/>
    </xsl:comment>
    <Reference>
      <xsl:attribute name="Qual">
        <xsl:value-of select="REF01"/>
      </xsl:attribute>
      <xsl:value-of select="REF02"/>
    </Reference>
  </xsl:template>


  <xsl:template match="DMG">
    <Demographic>
      <xsl:attribute name="Gender">
        <xsl:value-of select="DMG03"/>
      </xsl:attribute>
      <xsl:attribute name="DateOfBirth">
        <xsl:value-of select="concat(substring(DMG02,1,4),'-',substring(DMG02,5,2),'-',substring(DMG02,7,2))"/>
      </xsl:attribute>
    </Demographic>
  </xsl:template>

  <xsl:template match="NM1">
    <xsl:comment>
      <xsl:value-of select="NM101/comment()"/>
    </xsl:comment>
    <Name>
      <xsl:attribute name="Qual">
        <xsl:value-of select="NM101"/>
      </xsl:attribute>
      <xsl:attribute name="IsPerson">
        <xsl:choose>
          <xsl:when test="NM102='1'">true</xsl:when>
          <xsl:otherwise>false</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:choose>
        <xsl:when test="NM102='1'">
          <xsl:if test="string-length(NM106) > 0">
            <xsl:attribute name="Prefix">
              <xsl:value-of select="NM106"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:attribute name="First">
            <xsl:value-of select="NM104"/>
          </xsl:attribute>
          <xsl:if test="string-length(NM105)>0">
            <xsl:attribute name="Middle">
              <xsl:value-of select="NM105"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:attribute name="Last">
            <xsl:value-of select="NM103"/>
          </xsl:attribute>
          <xsl:if test="string-length(NM107) > 0">
            <xsl:attribute name="Suffix">
              <xsl:value-of select="NM107"/>
            </xsl:attribute>
          </xsl:if>
          <Full>
            <xsl:value-of select="concat(NM103, ' ', NM107,', ',NM104, ' ', NM105)"/>
          </Full>
        </xsl:when>
        <xsl:otherwise>
          <Full>
            <xsl:value-of select="NM103"/>
          </Full>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="string-length(NM112) > 0">
        <Additional>
          <xsl:value-of select="NM112"/>
        </Additional>
      </xsl:if>
      <xsl:for-each select="following-sibling::N2">
        <Additional>
          <xsl:value-of select="N201"/>
        </Additional>
      </xsl:for-each>
      <xsl:comment>
        <xsl:value-of select="NM108/comment()"/>
      </xsl:comment>
      <Identification>
        <xsl:attribute name="Qual">
          <xsl:value-of select="NM108"/>
        </xsl:attribute>
        <xsl:value-of select="NM109"/>
      </Identification>
    </Name>
  </xsl:template>

  <xsl:template match="N1">
    <Name>
      <Full>
        <xsl:value-of select="N102"/>
      </Full>
      <xsl:comment>
        <xsl:value-of select="N101/comment()"/>
      </xsl:comment>
      <Identifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="N101"/>
        </xsl:attribute>
      </Identifier>
      <xsl:for-each select="following-sibling::N2">
        <Additional>
          <xsl:value-of select="N201"/>
        </Additional>
      </xsl:for-each>
    </Name>
  </xsl:template>

  <xsl:template match="N2"></xsl:template>

  <xsl:template match="N3">
    <AddressLine>
      <xsl:value-of select="N301"/>
    </AddressLine>
    <xsl:if test="count(N302)>0">
      <AddressLine>
        <xsl:value-of select="N302"/>
      </AddressLine>
    </xsl:if>
  </xsl:template>

  <xsl:template match="N4">
    <Locale>
      <xsl:attribute name="City">
        <xsl:value-of select="N401"/>
      </xsl:attribute>
      <xsl:attribute name="State">
        <xsl:value-of select="N402"/>
      </xsl:attribute>
      <xsl:attribute name="PostalCode">
        <xsl:value-of select="N403"/>
      </xsl:attribute>
    </Locale>
  </xsl:template>

  <xsl:template match="PER">
    <Contact>
      <xsl:comment>
        <xsl:value-of select="PER01/comment()"/>
      </xsl:comment>
      <Function>
        <xsl:attribute name="Code">
          <xsl:value-of select="PER01"/>
        </xsl:attribute>
      </Function>
      <Name>
        <xsl:value-of select="PER02"/>
      </Name>
      <xsl:call-template name="communication-number">
        <xsl:with-param name="qual" select="PER03"/>
        <xsl:with-param name="number" select="PER04"/>
      </xsl:call-template>
      <xsl:call-template name="communication-number">
        <xsl:with-param name="qual" select="PER05"/>
        <xsl:with-param name="number" select="PER06"/>
      </xsl:call-template>
      <xsl:call-template name="communication-number">
        <xsl:with-param name="qual" select="PER07"/>
        <xsl:with-param name="number" select="PER08"/>
      </xsl:call-template>
    </Contact>
  </xsl:template>

  <xsl:template name="communication-number">
    <xsl:param name="qual"/>
    <xsl:param name="number"/>
    <xsl:if test="string-length($qual)>0 or string-length($number)>0">
      <Communication>
        <xsl:attribute name="Qual">
          <xsl:value-of select="$qual"/>
        </xsl:attribute>
        <xsl:attribute name="Number">
          <xsl:value-of select="$number"/>
        </xsl:attribute>
      </Communication>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
