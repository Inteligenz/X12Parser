<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
    <xsl:param name="claim-image"/>

    <xsl:template match="UB04Claim">
      <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:frm="http://www.fsc.va.gov/XSL/Usva.SharedServices.xsd">
        <fo:layout-master-set>
          <fo:simple-page-master page-width="8.5in" page-height="11in" margin="0.0in" master-name="letter-ub04">
            <fo:region-body margin-top="0.1in" margin-left="0.1in" margin-right="0.1in" />
          </fo:simple-page-master>
        </fo:layout-master-set>
        <fo:page-sequence master-reference="letter-ub04">
          <fo:flow flow-name="xsl-region-body" font-size="10pt" font-family="Courier">
            <fo:block>
              <fo:external-graphic>
                <xsl:attribute name="src">
                  <xsl:value-of select="$claim-image"/>
                </xsl:attribute>
              </fo:external-graphic>
            </fo:block>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="0.0474in" width="8.0066in" height="0.1789in">
              <fo:block>DLN: , Clearinghouse Number: 031109000132382</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="0.2100in" width="2.3285in" height="0.1787in">
              <fo:block>
                <xsl:value-of select="Field01_04_BillingProviderAddress1"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="0.3726in" width="2.4102in" height="0.17892in">
              <fo:block>
                <xsl:value-of select="Field01_06_BillingProviderCity"/>
                <xsl:text>&#x0020;</xsl:text>
                <xsl:value-of select="Field01_09_BillingProviderZip"/>
                <xsl:text>&#x0020;</xsl:text>
                <xsl:value-of select="Field01_11_BillingProviderPhoneNumber "/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="0.5352in" width="2.4102in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field01_11_BillingProviderPhoneNumber "/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="0.6978in" width="2.41025in" height="0.1789in">
              <fo:block color="turquoise">1LastLine</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="2.5849in" top="0.2100in" width="2.3489in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field02_01_PayToLastName"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="2.5849in" top="0.3726in" width="2.4306in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field02_04_PayToAddress1"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="2.5849in" top="0.5352in" width="2.4306in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field02_06_PayToCity"/>
                <xsl:text>&#x0020;</xsl:text>
                <xsl:value-of select="Field02_08_PayToState"/>
                <xsl:text>&#x0020;</xsl:text>
                <xsl:value-of select="Field02_09_PayToZip"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="2.5849in" top="0.6978in" width="2.4306in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field02_11_PayToCountryCode"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.3219in" top="0.2100in" width="2.3285in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field03a_PatientControlNumber"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.3218in" top="0.3726in" width="2.3284in" height="0.1789in">
              <fo:block>3b</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.6911in" top="0.3726in" width="0.4493in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field04_TypeOfBill"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.4767in" top="0.5352in" width="0.8987in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field05_FederalTaxId"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.0767in" top="0.6978in" width="0.8987in" height="0.1789in">
              <fo:block>208418853</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.0163in" top="0.6978in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field06_ServiceFromDate"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.7107in" top="0.6978in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field06_ServiceToDate"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.4052in" top="0.5352in" width="0.7353in" height="0.1789in">
              <fo:block color="turquoise">7line1</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.4052in" top="0.6978in" width="0.7353in" height="0.1789in">
              <fo:block color="turquoise">7line2</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="1.2368in" top="0.8604in" width="1.8178in" height="0.1789in">
              <fo:block color="turquoise">8a</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.2564in" top="1.023in" width="2.7982in" height="0.1789in">
              <fo:block color="turquoise">8b</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.1780in" top="0.8604in" width="3.9624in" height="0.1789in">
              <fo:block color="turquoise">9a</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="3.1976in" top="1.023in" width="3.1046in" height="0.1789in">
              <fo:block color="turquoise">9b</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.42485in" top="1.023in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">9c</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.7924in" top="1.023in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">9d</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8545in" top="1.023in" width="0.2860in" height="0.1789in">
              <fo:block color="turquoise">9e</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.1747in" top="1.3483in" width="0.817in" height="0.1789in">
              <fo:block color="turquoise">10</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.0326in" top="1.3482in" width="0.2860in" height="0.1789in">
              <fo:block color="turquoise">11</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.318525in" top="1.3482in" width="0.5719in" height="0.17892in">
              <fo:block>
                <xsl:value-of select="Field12_AdmissionDate"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.9313in" top="1.3482in" width="0.2451in" height="0.17892in">
              <fo:block color="turquoise">
                13
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.2172in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field14_TypeOfVisit"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.5032in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field15_SourceOfAdmission"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.8300in" top="1.3482in" width="0.2043in" height="0.1789in">
              <fo:block color="turquoise">
                16
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.1159in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                17
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.4019in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                18
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.6878in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                19
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.9738in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                20
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.2597in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                21
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5457in" top="1.3482in" width="0.2452in" height="0.1789in">
              <fo:block color="turquoise">
                22
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.8725in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                23
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.1584in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                24
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.4444in" top="1.3482in" width="0.2451in" height="0.1786in">
              <fo:block color="turquoise">
                25
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.7303in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                26
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.0163in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                27
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.3431in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                28
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.6290in" top="1.3482in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                29
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.9150in" top="1.3482in" width="1.2255in" height="0.1789in">
              <fo:block color="turquoise">
                30
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.4607in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field31_OccurrenceCodeDate_a"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.4607in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field31_OccurrenceCodeDate_b"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="1.4411in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field32_OccurrenceCodeDate_a"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="1.4411in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field32_OccurrenceCodeDate_b"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4215in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field33_OccurrenceCodeDate_a"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4215in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field33_OccurrenceCodeDate_b"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.4019in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field34_OccurrenceCodeDate_a"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.4019in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field34_OccurrenceCodeDate_b"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.382275in" top="1.6734in" width="0.6127in" height="0.1789in">
              <fo:block color="turquoise">
                35froma
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.0359in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                35thrua
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.382275in" top="1.8360in" width="0.6127in" height="0.1789in">
              <fo:block color="turquoise">
                35fromb
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.0359in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                35tob
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.0163in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                36froma
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.7107in" top="1.6734in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                36thrua
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.0163in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                36fromb
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.7107in" top="1.8360in" width="0.6536in" height="0.1789in">
              <fo:block color="turquoise">
                36thrub
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.3847in" top="1.6734in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                37line1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.3847in" top="1.8360in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                37line2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.2564in" top="1.9986in" width="3.9829in" height="0.1789in">
              <fo:block color="turquoise">
                38line1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="2.1612in" width="4.085in" height="0.1789in">
              <fo:block color="turquoise">
                38line2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="2.3238in" width="4.085in" height="0.1789in">
              <fo:block color="turquoise">
                38line3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="2.4864in" width="4.085in" height="0.1789in">
              <fo:block color="turquoise">
                38line4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="2.6490in" width="4.085in" height="0.1789in">
              <fo:block color="turquoise">
                38line5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.36185in" top="2.1612in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field39a_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="4.627375in" top="2.1612in" width="1.0008in" height="0.1789in">
              <fo:block color="turquoise">
                39aCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.36185in" top="2.3238in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field39b_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="4.627375in" top="2.3238in" width="1.0008in" height="0.1789in">
              <fo:block color="turquoise">
                39bcode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.36185in" top="2.4864in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                39cAmount 
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="4.627375in" top="2.4864in" width="1.0008in" height="0.1789in">
              <fo:block color="turquoise">
                39cCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.36185in" top="2.6490in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field39d_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="4.627375in" top="2.6490in" width="1.0008in" height="0.1789in">
              <fo:block color="turquoise">
                39dCode 
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6282in" top="2.1612in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field40a_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9141in" top="2.1612in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                40aCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6282in" top="2.3238in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field40b_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9141in" top="2.3238in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                40bCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6282in" top="2.4864in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                40cAmount
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9141in" top="2.4864in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                40cCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6282in" top="2.6490in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field40d_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9141in" top="2.6490in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                40dCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.89455in" top="2.1612in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field41a_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="7.1805in" top="2.1612in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                41aCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.89455in" top="2.3238in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field41b_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="7.1805in" top="2.3238in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                41bCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.89455in" top="2.4864in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                41cAmount
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="7.1805in" top="2.4864in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                41cCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.89455in" top="2.6490in" width="0.2656in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field41d_Amount"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="7.1805in" top="2.6490in" width="0.9804in" height="0.1789in">
              <fo:block color="turquoise">
                41dCode
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="2.9742in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="2.9742in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="2.9742in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="2.9742in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="2.9742in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="2.9742in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="2.9742in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.1368in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.1368in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.1368in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.1368in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.1368in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.1368in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.1368in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.2994in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.2994in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.2994in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.2994in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.2994in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.2994in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.2994in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.4620in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.4620in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.4620in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.4620in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.4620in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.4620in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.4620in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.6246in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.6246in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.6246in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.6246in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.6246in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.6246in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.6246in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.7872in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.7872in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Descrition6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.7872in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.7872in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.7872in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.7872in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.7872in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="3.9498in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                42R7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="3.9498in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                43Description7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="3.9498in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                45ServD7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="3.9498in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                46ServU7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="3.9498in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                47TCharg7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="3.9498in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                48NCCharg7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="3.9498in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                49.7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.1124in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.1124in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.1124in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.1124in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.1124in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.1124in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.1124in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.2750in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.2750in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.2750in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.2750in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.2750in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.2750in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.2750in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.4376in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.4376in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.4376in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.4376in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.4376in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.4376in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.4376in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.6002in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.6002in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.6002in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.6002in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.6002in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.6002in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.6002in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.7628in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.7628in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.7628in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.7628in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.7628in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.7628in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.7628in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="4.9254in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="4.9254in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="4.9254in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="4.9254in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="4.9254in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="4.9254in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="4.9254in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.088in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.088in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.088in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.088in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.088in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.088in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.088in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.2506in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.2506in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.2506in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.2506in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.2506in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.2506in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.2506in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.4132in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.4132in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.4132in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.4132in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.4132in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.4132in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.4132in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.5758in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.5758in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.5758in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.5758in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.5758in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.5758in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.5758in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.7384in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.7384in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.7384in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.7384in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.7384in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.7384in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.7384in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="5.901in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="5.901in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="5.901in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="5.901in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="5.901in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="5.901in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="5.901in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="6.0636in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="6.0636in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="6.0636in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="6.0636in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="6.0636in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="6.0636in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="6.0636in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="6.2263in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="6.2263in" width="2.4102in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="6.2263in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="6.2263in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="6.2263in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="6.2263in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="6.2263in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="0.1543in" top="6.3888in" width="0.4290in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.6037in" top="6.3888in" width="2.4111in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="6.3888in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.1993in" top="6.3888in" width="0.7557in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="6.3888in" width="0.9600in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="6.3888in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.8954in" top="6.3888in" width="0.2656in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="1.0734in" top="6.5514in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                ?
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="1.645325in" top="6.5514in" width="0.2451in" height="0.1789in">
              <fo:block color="turquoise">
                ?
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.5048in" top="6.5514in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                CreatDate
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="5.9755in" top="6.5514in" width="0.9600in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field47_SummaryTotalCharges"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.9558in" top="6.5514in" width="0.9395in" height="0.1789in">
              <fo:block color="turquoise">
                <xsl:value-of select="Field48_SummaryTotalNonCoveredCharges"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="right" left="6.6903in" top="6.7140in" width="1.4706in" height="0.1789in">
              <xsl:value-of select="Field56_NationalProviderIndicators"/>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.6903in" top="6.8766in" width="1.4706in" height="0.1789in">
              <xsl:value-of select="Field57_OtherProviderIdentifier"/>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.6903in" top="7.0392in" width="1.4706in" height="0.1789in">
              <fo:block color="turquoise">
                57Other
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.6903in" top="7.2019in" width="1.4706in" height="0.1789in">
              <fo:block color="turquoise">
                57PrvID
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="7.5270in" width="2.5327in" height="0.1789in">
              <fo:block color="turquoise">SBR LAST NAME , SBR FIRST NAME</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.6870in" top="7.5270in" width="0.2860in" height="0.1789in">
              <fo:block color="turquoise">
                59PR
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="2.9730in" top="7.5270in" width="1.9404in" height="0.1789in">
              <xsl:value-of select="Field60a_InsuredsIniqueIdentificationNumber"/>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.9338in" top="7.5270in" width="1.4503in" height="0.1789in">
              <fo:block color="turquoise">
                61GroupName
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="6.4043in" top="7.5270in" width="1.7565in" height="0.1789in">
              <fo:block color="turquoise">
                62InsuranceGroupNo
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="8.1774in" width="3.0025in" height="0.1789in">
              <fo:block color="turquoise">
                63ATreatAuthCodeA
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="3.1976in" top="8.1774in" width="2.5123in" height="0.1789in">
              <fo:block color="turquoise">
                64DocControlNumA
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.7303in" top="8.1774in" width="2.43057in" height="0.1789in">
              <fo:block color="turquoise">
                65EmployerNameA
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="8.34in" width="3.0025in" height="0.1789in">
              <fo:block color="turquoise">
                63TreatAuthCodeB
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="3.1976in" top="8.34in" width="2.5123in" height="0.1789in">
              <fo:block color="turquoise">
                64DocControlNumB
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.7303in" top="8.34in" width="2.43057in" height="0.1789in">
              <fo:block color="turquoise">
                65EmployerNameB
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.2564in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                1
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.9305in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                2
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.0326in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                3
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.7066in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                4
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.8087in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                5
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4828in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                6
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.6051in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                7
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.2793in" top="8.6652in" width="0.2793in" height="0.1789in">
              <fo:block color="turquoise">
                8
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.3815in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                9
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.0555in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                10
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.1576in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                11
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.8316in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                12
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.9338in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                13
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6079in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                14
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.7303in" top="8.6652in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                15
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.4043in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                16
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.5065in" top="8.6652in" width="0.7761in" height="0.1789in">
              <fo:block color="turquoise">
                17
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.1805in" top="8.6652in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                18
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.3847in" top="8.6652in" width="0.7761in" height="0.1789in">
              <fo:block color="turquoise">
                68A
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.1543in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block>19</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.2564in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                20
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.9305in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.0326in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.7066in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.8087in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4828in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.6051in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.2793in" top="8.8278in" width="0.2793in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.3815in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.0555in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.1576in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.8316in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.9338in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block color="turquoise">
                .
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6079in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block>O</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.7303in" top="8.8278in" width="0.6740in" height="0.1789in">
              <fo:block>7262</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.4043in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block>P</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.5065in" top="8.8278in" width="0.7761in" height="0.1789in">
              <fo:block>7262</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.1805in" top="8.8278in" width="0.1021in" height="0.1789in">
              <fo:block>Q</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.5219in" top="8.9904in" width="0.6945in" height="0.1789in">
              <fo:block>8404</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.7270in" top="8.9904in" width="0.6536in" height="0.1789in">
              <fo:block>131 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4215in" top="8.9904in" width="0.6536in" height="0.1789in">
              <fo:block>132 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.0955in" top="8.9904in" width="0.6740in" height="0.1789in">
              <fo:block>133 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.1576in" top="8.9904in" width="0.4901in" height="0.1789in">
              <fo:block>502</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="4.8316in" top="8.9904in" width="0.6945in" height="0.1789in">
              <fo:block>E9421</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.5262in" top="8.9904in" width="0.1021in" height="0.1789in">
              <fo:block>R</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="5.6282in" top="8.9904in" width="0.6536in" height="0.1789in">
              <fo:block>E9413</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.3022in" top="8.9904in" width="0.1021in" height="0.1789in">
              <fo:block>S</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="6.4043in" top="8.9904in" width="0.6536in" height="0.1789in">
              <fo:block>E8497</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.0784in" top="8.9904in" width="0.1021in" height="0.1789in">
              <fo:block>T</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.2826in" top="8.9904in" width="0.8579in" height="0.1789in">
              <fo:block>134 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.1543in" top="9.3156in" width="0.7761in" height="0.1789in">
              <fo:block>8377</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.9509in" top="9.3156in" width="0.6536in" height="0.1789in">
              <fo:block>101508</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.6249in" top="9.3156in" width="0.7557in" height="0.1789in">
              <fo:block>8363</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4010in" top="9.3156in" width="0.6740in" height="0.1789in">
              <fo:block>101508</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.0955in" top="9.3156in" width="0.7476in" height="0.1789in">
              <fo:block>135 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.8717in" top="9.3156in" width="0.6536in" height="0.1789in">
              <fo:block>136 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.1543in" top="9.6408in" width="0.7761in" height="0.1789in">
              <fo:block>137 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="0.9509in" top="9.6408in" width="0.6536in" height="0.1789in">
              <fo:block>138 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="1.6249in" top="9.6408in" width="0.7557in" height="0.1789in">
              <fo:block>139 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="2.4010in" top="9.6408in" width="0.6740in" height="0.1789in">
              <fo:block>140 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.0955in" top="9.6408in" width="0.7476in" height="0.1789in">
              <fo:block>141 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="3.8717in" top="9.6408in" width="0.6536in" height="0.1789in">
              <fo:block>142 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.5457in" top="9.153in" width="0.4698in" height="0.1789in">
              <fo:block>143 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.5457in" top="9.3156in" width="0.4698in" height="0.1789in">
              <fo:block>144 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.5457in" top="9.4782in" width="0.4698in" height="0.1789in">
              <fo:block>145 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="4.5457in" top="9.6408in" width="0.4698in" height="0.1789in">
              <fo:block>146 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.8529in" top="9.153in" width="1.0213in" height="0.1789in">
              <fo:block>
                <xsl:value-of select="Field76_AttendingProviderNationalProviderIdentifier"/>
              </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.0784in" top="9.153in" width="0.2043in" height="0.1789in">
              <fo:block>1G</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.2826in" top="9.153in" width="0.8783in" height="0.1789in">
              <fo:block>G43918</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.2810in" top="9.3156in" width="1.5115in" height="0.1789in">
              <fo:block>DOC LAST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.0375in" top="9.3156in" width="1.1235in" height="0.1789in">
              <fo:block>DOC FIRST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.8529in" top="9.4782in" width="1.0213in" height="0.1789in">
              <fo:block>1740295484</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.0784in" top="9.4782in" width="0.2043in" height="0.1789in">
              <fo:block>1G</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.2826in" top="9.4782in" width="0.8783in" height="0.1789in">
              <fo:block>OP G43918</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.2810in" top="9.6408in" width="1.5115in" height="0.1789in">
              <fo:block>OP DOC LAST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.0375in" top="9.6408in" width="1.1235in" height="0.1789in">
              <fo:block>OP DOC FIRST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.8529in" top="9.8034in" width="1.0213in" height="0.1789in">
              <fo:block>OT10295484</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.0784in" top="9.8034in" width="0.2043in" height="0.1789in">
              <fo:block>1G</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.2826in" top="9.8034in" width="0.8783in" height="0.1789in">
              <fo:block>OT1 G43918</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.2810in" top="9.9660in" width="1.5115in" height="0.1789in">
              <fo:block>OT1 DOC LAST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.0375in" top="9.9660in" width="1.1235in" height="0.1789in">
              <fo:block>OT1 DOC FIRST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.8529in" top="10.1286in" width="1.0213in" height="0.1789in">
              <fo:block>OT20295484</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="center" left="7.0784in" top="10.1286in" width="0.2043in" height="0.1789in">
              <fo:block>1G</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.2826in" top="10.1286in" width="0.8783in" height="0.1789in">
              <fo:block>OT2 G43918</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="5.2810in" top="10.2912in" width="1.5115in" height="0.1789in">
              <fo:block>OT2 DOC LAST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="7.0375in" top="10.2912in" width="1.1235in" height="0.1789in">
              <fo:block>OT2 DOC FIRST</fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="9.8034in" width="2.3284in" height="0.1789in">
              <fo:block>147 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="9.9660in" width="2.3284in" height="0.1789in">
              <fo:block>148 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="10.1286in" width="2.3284in" height="0.1789in">
              <fo:block>149 </fo:block>
            </fo:block-container>
            <fo:block-container position="absolute" margin-left="2px" wrap-option="no-wrap" margin-right="2px" text-align="left" left="0.1543in" top="10.2912in" width="2.3284in" height="0.1789in">
              <fo:block>150 </fo:block>
            </fo:block-container>
          </fo:flow>
        </fo:page-sequence>
        </fo:root>
    </xsl:template>
</xsl:stylesheet>
