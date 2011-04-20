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

  <xsl:template match="ISA">
    <Header>
      <xsl:attribute name="Date">
        <xsl:value-of select="concat('20',substring(ISA09,1,2),'-',substring(ISA09,3,2),'-',substring(ISA09,5,2),'T',substring(ISA10,1,2),':',substring(ISA10,3,2))"/>
      </xsl:attribute>
      <xsl:attribute name="Version">
        <xsl:value-of select="ISA12"/>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ISA13"/>
      </xsl:attribute>
      <xsl:attribute name="AckRequested">
        <xsl:value-of select="ISA14"/>
      </xsl:attribute>
      <xsl:attribute name="Usage">
        <xsl:value-of select="ISA15"/>
      </xsl:attribute>
      <xsl:comment>
        <xsl:value-of select="ISA01/comment()"/>
      </xsl:comment>
      <Author>
        <xsl:attribute name="Qual">
          <xsl:value-of select="ISA01"/>
        </xsl:attribute>
        <xsl:value-of select="ISA02"/>
      </Author>
      <xsl:comment>
        <xsl:value-of select="ISA03/comment()"/>
      </xsl:comment>
      <Security>
        <xsl:attribute name="Qual">
          <xsl:value-of select="ISA03"/>
        </xsl:attribute>
        <xsl:value-of select="ISA04"/>
      </Security>
      <Sender>
        <xsl:attribute name="Qual">
          <xsl:value-of select="ISA05"/>
        </xsl:attribute>
        <xsl:value-of select="ISA06"/>
      </Sender>
      <Receiver>
        <xsl:attribute name="Qual">
          <xsl:value-of select="ISA07"/>
        </xsl:attribute>
        <xsl:value-of select="ISA08"/>
      </Receiver>
    </Header>
  </xsl:template>

  <xsl:template match="IEA">
    <Trailer>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="IEA02"/>
      </xsl:attribute>
      <xsl:attribute name="FunctionalGroupCount">
        <xsl:value-of select="IEA01"/>
      </xsl:attribute>
    </Trailer>
  </xsl:template>

  <xsl:template match="GS">
    <Header>
      <xsl:attribute name="Date">
        <xsl:value-of select="concat(substring(GS04,1,4),'-',substring(GS04,5,2),'-',substring(GS04,7,2),'T',substring(GS05,1,2),':',substring(GS05,3,2))"/>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="GS06"/>
      </xsl:attribute>
      <xsl:comment>
        <xsl:value-of select="GS01/comment()"/>
      </xsl:comment>
      <FunctionalIdentifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="GS01"/>
        </xsl:attribute>
      </FunctionalIdentifier>
      <Sender>
        <xsl:attribute name="Code">
          <xsl:value-of select="GS02"/>
        </xsl:attribute>
      </Sender>
      <Receiver>
        <xsl:attribute name="Code">
          <xsl:value-of select="GS03"/>
        </xsl:attribute>
      </Receiver>
      <xsl:comment>
        <xsl:value-of select="GS07/comment()"/>
      </xsl:comment>
      <ResponsibleAgency>
        <xsl:attribute name="Code">
          <xsl:value-of select="GS07"/>
        </xsl:attribute>
      </ResponsibleAgency>
      <VersionIdentifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="GS08"/>
        </xsl:attribute>
      </VersionIdentifier>
    </Header>
  </xsl:template>

  <xsl:template match="GE">
    <Trailer>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="GE02"/>
      </xsl:attribute>
      <xsl:attribute name="TransactionCount">
        <xsl:value-of select="GE01"/>
      </xsl:attribute>
    </Trailer>
  </xsl:template>

  <xsl:template match="ST">
    <Header>
      <xsl:attribute name="IdentifierCode">
        <xsl:value-of select="ST01"/>
      </xsl:attribute>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ST02"/>
      </xsl:attribute>
      <xsl:attribute name="Version">
        <xsl:value-of select="ST03"/>
      </xsl:attribute>
    </Header>
  </xsl:template>

  <xsl:template match="SE">
    <Trailer>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="SE02"/>
      </xsl:attribute>
      <xsl:attribute name="SegmentCount">
        <xsl:value-of select="SE01"/>
      </xsl:attribute>
    </Trailer>
  </xsl:template>

  <xsl:template match="HL">
    <xsl:comment>
      <xsl:value-of select="HL03/comment()"/>
    </xsl:comment>
    <Level>
      <xsl:attribute name="Code">
        <xsl:value-of select="HL03"/>
      </xsl:attribute>
      <xsl:if test="HL04='1'">
        <xsl:attribute name="Subordinates">true</xsl:attribute>
      </xsl:if>
    </Level>
  </xsl:template>

  <xsl:template match="BHT">
    <Begin>
      <xsl:attribute name="Date">
        <xsl:value-of select="concat(substring(BHT04,1,4),'-',substring(BHT04,5,2),'-',substring(BHT04,7,2),'T',substring(BHT05,1,2),':',substring(BHT05,3,2))"/>
      </xsl:attribute>
      <xsl:attribute name="Id">
        <xsl:value-of select="BHT03"/>
      </xsl:attribute>
      <xsl:comment>
        <xsl:value-of select="BHT01/comment()"/>
      </xsl:comment>
      <Purpose>
        <xsl:attribute name="Code">
          <xsl:value-of select="BHT01"/>
        </xsl:attribute>
      </Purpose>
      <xsl:comment>
        <xsl:value-of select="BHT06/comment()"/>
      </xsl:comment>
      <Type>
        <xsl:attribute name="Code">
          <xsl:value-of select="BHT06"/>
        </xsl:attribute>
      </Type>

    </Begin>
  </xsl:template>

</xsl:stylesheet>
