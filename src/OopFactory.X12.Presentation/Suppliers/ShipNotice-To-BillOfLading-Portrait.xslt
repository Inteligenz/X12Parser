<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
      <fo:root>
        <fo:layout-master-set>
          <fo:simple-page-master margin-left="0.5in" margin-right="0.5in" margin-top="0.5in" master-name="Portrait" page-height="11in" page-width="8.5in">
            <fo:region-body margin="0in"/>
          </fo:simple-page-master>
        </fo:layout-master-set>
        <fo:page-sequence master-reference="Portrait">
          <fo:flow>
            <fo:block>Bill of Laden</fo:block>
          </fo:flow>
          
        </fo:page-sequence>
      </fo:root>
    </xsl:template>
</xsl:stylesheet>
