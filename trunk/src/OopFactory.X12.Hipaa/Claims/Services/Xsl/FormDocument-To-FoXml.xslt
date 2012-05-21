<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="FormDocument">
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:frm="http://www.fsc.va.gov/XSL/Usva.SharedServices.xsd">
      <fo:layout-master-set>
        <fo:simple-page-master page-width="8.5in" page-height="11in" margin="0.0in" master-name="hcfa1500">
          <fo:region-body margin-top="0.0625in" margin-left="0.0625in" margin-right="0.140in" />
        </fo:simple-page-master>
        <fo:simple-page-master page-width="8.5in" page-height="11in" margin="0.0in" master-name="ub04">
          <fo:region-body margin-top="0.0in" margin-left="0.0in" margin-right="0.0in" />
        </fo:simple-page-master>
        <fo:simple-page-master page-width="8.5in" page-height="11in" margin="0.0in" master-name="j400">
          <fo:region-body margin-top="0.0in" margin-left="0.0in" margin-right="0.0in" />
        </fo:simple-page-master>
      </fo:layout-master-set>
      <xsl:for-each select="Page">
        <fo:page-sequence>
          <xsl:attribute name="master-reference">
            <xsl:value-of select="MasterReference"/>
          </xsl:attribute>
          <fo:flow flow-name="xsl-region-body" font-size="10pt" font-family="Courier">
            <xsl:if test="string-length(ImagePath)>0">
              <fo:block>
                <fo:external-graphic>
                  <xsl:attribute name="src">
                    <xsl:value-of select="ImagePath"/>
                  </xsl:attribute>
                </fo:external-graphic>
              </fo:block>
            </xsl:if>
            <xsl:for-each select="Block">
              <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="4px">
                <xsl:if test="string-length(LetterSpacing)>0">
                  <xsl:attribute name="letter-spacing">
                    <xsl:value-of select="LetterSpacing"/>
                  </xsl:attribute>
                </xsl:if>
                <xsl:attribute name="text-align"><xsl:value-of select="TextAlign"/></xsl:attribute>
                <xsl:attribute name="left"><xsl:value-of select="Left"/>in</xsl:attribute>
                <xsl:attribute name="top"><xsl:value-of select="Top"/>in</xsl:attribute>
                <xsl:attribute name="width"><xsl:value-of select="Width"/>in</xsl:attribute>
                <xsl:attribute name="height"><xsl:value-of select="Height"/>in</xsl:attribute>
                <fo:block><xsl:value-of select="Text"/></fo:block>
              </fo:block-container>
            </xsl:for-each>
          </fo:flow>
        </fo:page-sequence>
      </xsl:for-each>
    </fo:root>
  </xsl:template>
  
</xsl:stylesheet>
