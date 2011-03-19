<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:import href="Ansi-Common.xslt"/>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template name="Product">
    <xsl:param name="QualifierCode"/>
    <xsl:param name="ProductId"/>
    <xsl:if test="string-length($ProductId) > 0">
      <Product>
        <xsl:call-template name="Identification">
          <xsl:with-param name="Qualifier" select="$QualifierCode"/>
          <xsl:with-param name="Number" select="$ProductId"/>
        </xsl:call-template>
      </Product>
    </xsl:if>
  </xsl:template>

  <xsl:template match="DTM">
    <DateReference>
      <xsl:attribute name="Qualifier">
        <xsl:value-of select="DTM01"/>
      </xsl:attribute>
      <xsl:call-template name="FormatD8DateTime">
        <xsl:with-param name="Date" select="DTM02"/>
        <xsl:with-param name="Time" select="DTM03"/>
      </xsl:call-template>
      <xsl:if test="$verbose = 1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="DTM01='011'">Shipped</xsl:when>
            <xsl:when test="DTM01='017'">Estimated Delivery</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </DateReference>
  </xsl:template>

  <xsl:template match="PID">
    <Description>
      <xsl:attribute name="Type">
        <xsl:value-of select="PID01"/>
      </xsl:attribute>
      <xsl:value-of select="PID05"/>
    </Description>
  </xsl:template>

  <xsl:template match="TD1">
    <Packaging>
      <xsl:attribute name="Code">
        <xsl:value-of select="TD101"/>
      </xsl:attribute>
      <xsl:attribute name="LadingQuantity">
        <xsl:value-of select="TD102"/>
      </xsl:attribute>
      <xsl:if test="$verbose = 1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="TD101='BOX'">Box</xsl:when>
            <xsl:when test="TD101='ROL'">Roll</xsl:when>
            <xsl:when test="TD101='SKD'">Skid</xsl:when>
            <xsl:when test="TD101='TBN'">Tote Bin</xsl:when>
            <xsl:when test="TD101='CNT'">Container</xsl:when>
            <xsl:when test="TD101='CTN'">Carton</xsl:when>
            <xsl:when test="TD101='PAT'">Pallet - 2 Way</xsl:when>
            <xsl:when test="TD101='PCK'">Packed - not otherwise specified</xsl:when>
            <xsl:when test="TD101='PKG'">Package</xsl:when>
            <xsl:when test="TD101='PLT'">Pallet</xsl:when>
            <xsl:when test="TD101='PAT90'">Standard Pallet</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </Packaging>
  </xsl:template>

  <xsl:template match="TD5">
    <Routing>
      <Sequence>
        <xsl:attribute name="Code">
          <xsl:value-of select="TD501"/>
        </xsl:attribute>
      </Sequence>
      <Identification>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="TD502"/>
        </xsl:attribute>
        <xsl:value-of select="TD503"/>
        <xsl:if test="$verbose = 1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="TD502='2'">Standard Carrier Alpha Code (SCAC)</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </Identification>
      <TransportationMethod>
        <xsl:attribute name="Code">
          <xsl:value-of select="TD504"/>
        </xsl:attribute>
        <xsl:if test="$verbose = 1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="TD504='A'">Air</xsl:when>
              <xsl:when test="TD504='AC'">Air Charter</xsl:when>
              <xsl:when test="TD504='AE'">Air Express</xsl:when>
              <xsl:when test="TD504='C'">Consolidation</xsl:when>
              <xsl:when test="TD504='D'">Parcel Post</xsl:when>
              <xsl:when test="TD504='E'">Expedited Truck</xsl:when>
              <xsl:when test="TD504='H'">Customer Pickup</xsl:when>
              <xsl:when test="TD504='M'">Motor (Common Carrier)</xsl:when>
              <xsl:when test="TD504='P'">Private Carrier</xsl:when>
              <xsl:when test="TD504='PC'">Private Carrier</xsl:when>
              <xsl:when test="TD504='S'">Ocean</xsl:when>
              <xsl:when test="TD504='U'">Private Parcel Service</xsl:when>
              <xsl:when test="TD504='LT'">Less Than Trailer Load (LTL)</xsl:when>
              <xsl:when test="TD504='ZZ'">Mutually defined</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </TransportationMethod>
      <Description>
        <xsl:value-of select="TD505"/>
      </Description>
    </Routing>
  </xsl:template>

  <xsl:template match="TD3">
    <Equipment>
      <xsl:attribute name="Code">
        <xsl:value-of select="TD301"/>
      </xsl:attribute>
      <xsl:if test="string-length(TD302)>0">
        <xsl:attribute name="Initial">
          <xsl:value-of select="TD302"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="Number">
        <xsl:value-of select="TD303"/>
      </xsl:attribute>
      <xsl:if test="$verbose = 1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="TD301='TL'">Trailer Load</xsl:when>
            <xsl:when test="TD301='LT'">Less than Trailer Load</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </Equipment>
  </xsl:template>
                
  <xsl:template match="SN1">
    <Detail>
      <xsl:if test="string-length(SN101)>0">
        <xsl:attribute name="AssignedId">
          <xsl:value-of select="SN101"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SN108)>0">
        <xsl:attribute name="StatusCode">
          <xsl:value-of select="SN108"/>
        </xsl:attribute>
      </xsl:if>
      <Quantity>
        <xsl:attribute name="Shipped">
          <xsl:value-of select="SN102"/>
        </xsl:attribute>
        <xsl:if test="string-length(SN104) > 0">
          <xsl:attribute name="ShippedToDate">
            <xsl:value-of select="SN104"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SN105) > 0">
          <xsl:attribute name="Ordered">
            <xsl:value-of select="SN105"/>
          </xsl:attribute>
        </xsl:if>
      </Quantity>
      <UnitOfMeasure>
        <xsl:attribute name="Code">
          <xsl:value-of select="SN103"/>
        </xsl:attribute>
        <xsl:if test="string-length(SN106) >0">
          <xsl:value-of select="SN106"/>
        </xsl:if>
      </UnitOfMeasure>
    </Detail>
  </xsl:template>

  <xsl:template match="PRF">
    <PurchaseOrderReference>
      <xsl:if test="string-length(PRF04) > 0">
        <xsl:attribute name="Date">
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="PRF04"/>
          </xsl:call-template>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(PRF05) > 0">
        <xsl:attribute name="AssignedId">
          <xsl:value-of select="PRF05"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:call-template name="Identification">
        <xsl:with-param name="Qualifier" select="PRF02"/>
        <xsl:with-param name="Number" select="PRF01"/>
      </xsl:call-template>
    </PurchaseOrderReference>
  </xsl:template>

  <xsl:template match="CLD">
    <LoadDetail>
      <xsl:attribute name="NumberOfLoads">
        <xsl:value-of select="CLD01"/>
      </xsl:attribute>
      <xsl:attribute name="UnitsShipped">
        <xsl:value-of select="CLD02"/>
      </xsl:attribute>
      <xsl:call-template name="Identification">
        <xsl:with-param name="Qualifier" select="CLD03"/>
      </xsl:call-template>
    </LoadDetail>
  </xsl:template>
  
  <!-- Item Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='ITEM']">
    <Item>
      <xsl:attribute name="Number">
        <xsl:value-of select="LIN/LIN01"/>
      </xsl:attribute>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN02"/>
        <xsl:with-param name="ProductId" select="LIN/LIN03"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN04"/>
        <xsl:with-param name="ProductId" select="LIN/LIN05"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN06"/>
        <xsl:with-param name="ProductId" select="LIN/LIN07"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN08"/>
        <xsl:with-param name="ProductId" select="LIN/LIN09"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN10"/>
        <xsl:with-param name="ProductId" select="LIN/LIN11"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN12"/>
        <xsl:with-param name="ProductId" select="LIN/LIN13"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN14"/>
        <xsl:with-param name="ProductId" select="LIN/LIN15"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN16"/>
        <xsl:with-param name="ProductId" select="LIN/LIN17"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN18"/>
        <xsl:with-param name="ProductId" select="LIN/LIN19"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN20"/>
        <xsl:with-param name="ProductId" select="LIN/LIN21"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN22"/>
        <xsl:with-param name="ProductId" select="LIN/LIN23"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN24"/>
        <xsl:with-param name="ProductId" select="LIN/LIN25"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN26"/>
        <xsl:with-param name="ProductId" select="LIN/LIN27"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN28"/>
        <xsl:with-param name="ProductId" select="LIN/LIN29"/>
      </xsl:call-template>
      <xsl:call-template name="Product">
        <xsl:with-param name="QualifierCode" select="LIN/LIN30"/>
        <xsl:with-param name="ProductId" select="LIN/LIN31"/>
      </xsl:call-template>
      <xsl:apply-templates select="PRF"/>
      <xsl:apply-templates select="MEA"/>
      <xsl:apply-templates select="PID"/>
      <xsl:apply-templates select="TD1"/>
      <xsl:apply-templates select="TD5"/>
      <xsl:apply-templates select="TD3"/>
      <xsl:apply-templates select="SN1"/>
      <xsl:apply-templates select="Loop/CLD"/>
    </Item>
  </xsl:template>
  
  <!-- Order Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='ORDER']">
    <Order>
      <xsl:apply-templates select="PRF"/>
      <xsl:apply-templates select="HierarchicalLoop"/>                           
    </Order>
  </xsl:template>
  <!-- Shipment Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='SHIPMENT']">
    <Shipment>
      <xsl:apply-templates select="REF"/>
      <xsl:for-each select="Loop">
        <Party>
          <Type>
            <xsl:attribute name="Code">
              <xsl:value-of select="N1/N101"/>
            </xsl:attribute>
            <xsl:if test="$verbose=1">
              <xsl:comment>
                <xsl:choose>
                  <xsl:when test="N1/N101='IC'">Intermediate Consignee</xsl:when>
                  <xsl:when test="N1/N101='SF'">Ship From</xsl:when>
                  <xsl:when test="N1/N101='ST'">Ship To</xsl:when>
                  <xsl:when test="N1/N101='SU'">Supplier</xsl:when>
                </xsl:choose>
              </xsl:comment>
            </xsl:if>
          </Type>
          <xsl:apply-templates select="N1"/>
          <xsl:apply-templates select="N3"/>
        </Party>
      </xsl:for-each>      
      <xsl:apply-templates select="MEA"/>
      <xsl:apply-templates select="PID"/>
      <xsl:apply-templates select="TD1"/>
      <xsl:apply-templates select="TD5"/>
      <xsl:apply-templates select="TD3"/>
      <xsl:apply-templates select="HierarchicalLoop"/>
    </Shipment>
  </xsl:template>  
  <!-- Transaction Loop -->
  <xsl:template match="Transaction">
    <Transaction>
      <xsl:attribute name="ControlNumber">
        <xsl:value-of select="ST/ST02"/>
      </xsl:attribute>
      <xsl:attribute name="ProductionDate">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="DTM[DTM01='405']/DTM02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:apply-templates select="../../ISA"/>
      <xsl:apply-templates select="../GS"/>
      <xsl:apply-templates select="DTM"/>
      <xsl:apply-templates select="HierarchicalLoop"/>
    </Transaction>
  </xsl:template>
  
  <xsl:template match="/">
    <ArrayOfTransaction>
      <xsl:apply-templates select="/Interchange/FunctionGroup/Transaction"/>
    </ArrayOfTransaction>
  </xsl:template>
</xsl:stylesheet>
