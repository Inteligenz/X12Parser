<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:oop="http://www.OopFactory.com/Form.xsd"
>
  <xsl:decimal-format name="implied" decimal-separator=" " />

  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="claim-image"/>

  <xsl:template match="/Interchange">
    <Interchange>

      <oop:form-master-template name="cms-1500"
        page-width-in="8.5" page-height-in="11"
        margin-top-in="0.0625" margin-left-in="0.625" margin-bottom-in="0" margin-right-in="0.140"
                                x-scale="0.0935" y-scale="0.157" x-offset="-0.21" y-offset="0.1">
        <xsl:attribute name="background-image">
          <xsl:value-of select="$claim-image"/>
        </xsl:attribute>
      </oop:form-master-template>
      <xsl:apply-templates select="@* | node()"/>
    </Interchange>
  </xsl:template>
  
  <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <!-- Follows crosswalk published at http://www.nucc.org/images/stories/PDF/1500_form_map_to_837p_4010a1_v1-0_112008.pdf -->
  <xsl:template match="Claim[@Type='Professional']">
    <xsl:variable name="page-count" select="1 + (count(ServiceLine) - 1 - ((count(ServiceLine) - 1) mod 6)) div 6"/>
    <xsl:for-each select="ServiceLine">
      <xsl:if test="position() mod 6 = 1">
        <xsl:variable name="page-num" select="1 + ((position() - 1) div 6)"/>
        <xsl:variable name="first-line-pos" select="position()"/>
        <xsl:variable name="last-line-pos" select="position() + 5"/>
        <oop:form form-master-template-ref="cms-1500">

          <!-- ABOVE SERVICE LINES -->
          <xsl:call-template name="claim-header">
            <xsl:with-param name="claim" select=".."/>
          </xsl:call-template>

          <xsl:for-each select="../ServiceLine">
            <xsl:if test="position() &gt;= $first-line-pos and position() &lt;= $last-line-pos">
              <xsl:variable name="y" select="42 + 2 * (position() - $first-line-pos)" />
              <!-- SERVICE LINES -->
              <oop:box id="24d-line-notes" x="4" width="64">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y"/>
                </xsl:attribute>
                <!-- CUSTOM SOLUTION HERE -->
              </oop:box>
              <xsl:choose>
                <xsl:when test="count(Provider[Name/@Qual='82'])>0">
                  <xsl:choose>
                    <xsl:when test="Provider/Name/Identification/@Qual != 'XX'">
                      <xsl:call-template name="box-24-i-and-j">
                        <xsl:with-param name="y" select="$y"/>
                        <xsl:with-param name="id" select="Provider/Name/Identification"/>
                      </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:call-template name="box-24-i-and-j">
                        <xsl:with-param name="y" select="$y"/>
                        <xsl:with-param name="id" select="Provider/Reference"/>
                      </xsl:call-template>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:choose>
                    <xsl:when test="../Provider/Name/Identification/@Qual != 'XX'">
                      <xsl:call-template name="box-24-i-and-j">
                        <xsl:with-param name="y" select="$y"/>
                        <xsl:with-param name="id" select="../Provider/Name/Identification"/>
                      </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:call-template name="box-24-i-and-j">
                        <xsl:with-param name="y" select="$y"/>
                        <xsl:with-param name="id" select="../Provider/Reference"/>
                      </xsl:call-template>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:otherwise>
              </xsl:choose>
              <oop:box id="24a-dos-from" x="4" width="8.5" text-align="center">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="concat(substring(DateOfService/@From,6,2),' ',substring(DateOfService/@From,9,2),' ',substring(DateOfService/@From,3,2))"/>
              </oop:box>
              <oop:box id="24a-dos-to" x="12.5" width="9" text-align="center">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="concat(substring(DateOfService/@To,6,2),' ',substring(DateOfService/@To,9,2),' ',substring(DateOfService/@To,3,2))"/>
              </oop:box>
              <oop:box id="24b-place-of-service" x="21.75" width="3" text-align="center">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:choose>
                  <xsl:when test="count(PlaceOfService)>0">
                    <xsl:value-of select="PlaceOfService/@Code"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="../PlaceOfService/@Code"/>
                  </xsl:otherwise>
                </xsl:choose>
              </oop:box>
              <oop:box id="24c-emg" x="25" width="2">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="@EmergencyIndicator"/>
              </oop:box>
              <oop:box id="24d-cpt-hcpcs" x="28.5" width="6">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="Procedure/@Code"/>
              </oop:box>
              <oop:box id="24d-mod-1" x="36" width="3">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="Procedure/Modifier[1]/@Code"/>
              </oop:box>
              <oop:box id="24d-mod-2" x="39" width="3">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="Procedure/Modifier[2]/@Code"/>
              </oop:box>
              <oop:box id="24d-mod-3" x="42" width="3">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="Procedure/Modifier[3]/@Code"/>
              </oop:box>
              <oop:box id="24d-mod-4" x="45" width="3">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="Procedure/Modifier[4]/@Code"/>
              </oop:box>
              <oop:box id="24e-diagnoses" x="48" width="5">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:for-each select="CompositeDiagnosis/Diagnosis">
                  <xsl:value-of select="@Pointer"/>
                </xsl:for-each>
              </oop:box>
              <oop:box id="24f-charges" x="53" width="9" text-align="right">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="format-number(@ChargeAmount,'# 00','implied')"/>
              </oop:box>
              <oop:box id="24g-days-or-units" x="62" width="4" text-align="right">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="@Quantity"/>
              </oop:box>
              <oop:box id="24h-epsdt" x="66" width="2">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y"/>
                </xsl:attribute>
                <xsl:value-of select="@EPSDTIndicator"/>
              </oop:box>
              <oop:box id="24h-family-plan" x="66" width="2">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:value-of select="@FamilyPlanningIndicator"/>
              </oop:box>
              <oop:box id="24j-rendering-provider-npi" x="71" width="12">
                <xsl:attribute name="y">
                  <xsl:value-of select="$y + 1"/>
                </xsl:attribute>
                <xsl:choose>
                  <xsl:when test="count(Provider[Name/@Qual='82'])>0">
                    <xsl:value-of select="Provider/Name[@Qual='82']/Identification[@Qual='24']"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="../Provider/Name[@Qual='82']/Identification[@Qual='24']"/>
                  </xsl:otherwise>
                </xsl:choose>
              </oop:box>
            </xsl:if>
          </xsl:for-each>

          <!-- BELOW SERVICE LINES -->
          <xsl:call-template name="claim-footer">
            <xsl:with-param name="claim" select=".."/>
          </xsl:call-template>
          <oop:box id="page-ref" x="20" y="63" width="40" text-align="center">
            Claim #<xsl:value-of select="../Reference[@Qual='D9']"  />, Page <xsl:value-of select="$page-num"/> of <xsl:value-of select="$page-count"/>
          </oop:box>
        </oop:form>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="claim-header">
    <xsl:param name="claim"/>

    <xsl:variable name="subscriber-loop" select="$claim/ancestor::node()[@LoopId='2000B']" />
    <oop:box id="header" x="33" y="1" width="50">
      <!-- Add stuff that might not have a place on the form like pay-to provider -->
    </oop:box>
    <xsl:call-template name="box-1">
      <xsl:with-param name="filingCode" select="$claim/ancestor::node()[@LoopId='2000B']/ClaimFilingIndicator/@Code"/>
    </xsl:call-template>
    
    <oop:box id="1-a-insured-id-num" x="53" y="7" width="30">
      <xsl:value-of select="$subscriber-loop/Subscriber/Name/Identification"/>
    </oop:box>

    <oop:box id="2" x="4" y="9" width="28.5">
      <xsl:value-of select="concat(../Patient/Name/@Last,', ',../Patient/Name/@First,' ',../Patient/Name/@Middle)"/>
    </oop:box>

    <oop:box id="3-dob" x="34" y="9" width="10" text-align="center">
      <xsl:variable name="dob" select="../Patient/Demographic/@DateOfBirth"/>
      <xsl:value-of select="concat(substring($dob,6,2),' ',substring($dob,9,2),' ',substring($dob,1,4))"/>
    </oop:box>
    <oop:box id="3-sex-m" x="44.5" y="9" width="2.5" text-align="center">
      <xsl:if test="../Patient/Demographic/@Gender='M'">X</xsl:if>
    </oop:box>
    <oop:box id="3-sex-f" x="49.5" y="9" width="2.5" text-align="center">
      <xsl:if test="../Patient/Demographic/@Gender='F'">X</xsl:if>
    </oop:box>

    <oop:box id="4" x="53" y="9" width="30">
      <xsl:value-of select="$subscriber-loop/Subscriber/Name/Full"/>
    </oop:box>

    <oop:box id="5-address" x="4" y="11" width="28.5">
      <xsl:value-of select="../Patient/AddressLine"/>
    </oop:box>
    <oop:box id="5-city" x="4" y="13" width="25">
      <xsl:value-of select="../Patient/Locale/@City"/>
    </oop:box>
    <oop:box id="5-state" x="29" y="13" width="3.5" text-align="center">
      <xsl:value-of select="../Patient/Locale/@State"/>
    </oop:box>
    <oop:box id="5-zip" x="4" y="15" width="13" text-align="center">
      <xsl:value-of select="../Patient/Locale/@PostalAddress"/>
    </oop:box>
    <oop:box id="5-telephone" x="18" y="15" width="14.5">
      234-567-8901
    </oop:box>

    <oop:box id="6-self" x="35.5" y="11" width="2.5" text-align="center">1</oop:box>
    <oop:box id="6-spouse" x="40.5" y="11" width="2.5" text-align="center">2</oop:box>
    <oop:box id="6-child" x="44.5" y="11" width="2.5" text-align="center">3</oop:box>
    <oop:box id="6-other" x="49.5" y="11" width="2.5" text-align="center">4</oop:box>

    <oop:box id="7-insured-address" x="53" y="11" width="30">
      <xsl:value-of select="$subscriber-loop/Subscriber/AddressLine"/>
    </oop:box>
    <oop:box id="7-insured-city" x="53" y="13" width="23">
      <xsl:value-of select="$subscriber-loop/Subscriber/Locale/@City"/>
    </oop:box>
    <oop:box id="7-insured-state" x="77" y="13" width="6">
      <xsl:value-of select="$subscriber-loop/Subscriber/Locale/@State"/>
    </oop:box>
    <oop:box id="7-insured-zip" x="53" y="15" width="12">
      <xsl:value-of select="$subscriber-loop/Subscriber/Locale/@PostalCode"/>
    </oop:box>
    <oop:box id="7-insured-telephone" x="68.5" y="15" width="14.5">
      <xsl:value-of select="$subscriber-loop/Subscriber/Contact/Communication[@Qual='TE']/@Number"/>
    </oop:box>

    <oop:box id="8-single" x="37.5" y="13" width="2.5" text-align="center">5</oop:box>
    <oop:box id="8-married" x="43.5" y="13" width="2.5" text-align="center">6</oop:box>
    <oop:box id="8-other" x="49.5" y="13" width="2.5" text-align="center">7</oop:box>
    <oop:box id="8-employed" x="37.5" y="15" width="2.5" text-align="center">8</oop:box>
    <oop:box id="8-ft-student" x="43.5" y="15" width="2.5" text-align="center">9</oop:box>
    <oop:box id="8-pt-student" x="49.5" y="15" width="2.5" text-align="center">0</oop:box>

    <oop:box id="9-other-insured-name" x="4" y="17" width="28.5">OTHER INSURED NAME</oop:box>
    <oop:box id="9-other-insured-policy" x="4" y="19" width="28.5">OTHER INSURED POLICY</oop:box>
    <oop:box id="9-other-insured-dob" x="5" y="21" width="10" text-align="center">MM DD YYYY</oop:box>
    <oop:box id="9-other-insured-sex-m" x="20.5" y="21" width="2.5" text-align="center">M</oop:box>
    <oop:box id="9-other-insured-sex-f" x="26.5" y="21" width="2.5" text-align="center">F</oop:box>
    <oop:box id="9-other-insured-employer" x="4" y="23" width="28.5">OTHER INSURED EMPLOYER</oop:box>
    <oop:box id="9-other-insured-plan-name" x="4" y="25" width="28.5">OTHER INSURED PLAN NAME</oop:box>

    <oop:box id="10-employment-yes" x="37.5" y="19" width="2.5" text-align="center">Y</oop:box>
    <oop:box id="10-employment-no" x="43.5" y="19" width="2.5" text-align="center">N</oop:box>
    <oop:box id="10-auto-accident-yes" x="37.5" y="21" width="2.5" text-align="center">Y</oop:box>
    <oop:box id="10-auto-accident-no" x="43.5" y="21" width="2.5" text-align="center">N</oop:box>
    <oop:box id="10-auto-accident-state" x="48" y="21" width="2.5" text-align="center">TX</oop:box>
    <oop:box id="10-other-accident-yes" x="37.5" y="23" width="2.5" text-align="center">Y</oop:box>
    <oop:box id="10-other-accident-no" x="43.5" y="23" width="2.5" text-align="center">N</oop:box>
    <oop:box id="10-reserved" x="33" y="25" width="20">RESERVED</oop:box>

    <oop:box id="11-insured-policy-group" x="53" y="17" width="30">
      <xsl:value-of select="$subscriber-loop/GroupOrPolicy/@Number"/>
    </oop:box>
    <xsl:variable name="insured-dob" select="$subscriber-loop/Subscriber/Demographic/@DateOfBirth"/>
    <oop:box id="11-insured-dob" x="56.5" y="19" width="10" text-align="center">
      <xsl:value-of select="concat(substring($insured-dob,6,2),' ',substring($insured-dob,9,2),' ',substring($insured-dob,1,4))"/>
    </oop:box>
    <xsl:variable name="insured-gender" select="$subscriber-loop/Subscriber/Demographic/@Gender"/>
    <oop:box id="11-insured-sex-m" x="70.5" y="19" width="2.5" text-align="center">
      <xsl:if test="$insured-gender='M'">X</xsl:if>
    </oop:box>
    <oop:box id="11-insured-sex-f" x="77.5" y="19" width="2.5" text-align="center">
      <xsl:if test="$insured-gender='F'">X</xsl:if>
    </oop:box>
    <oop:box id="11-insured-employer" x="53" y="21" width="30">
      <!-- according to CMS, this is not in 837P -->
    </oop:box>
    <oop:box id="11-insured-plan-name" x="53" y="23" width="30">
      <xsl:value-of select="$subscriber-loop/GroupOrPolicy"/>
    </oop:box>
    <xsl:choose>
      <xsl:when test="$claim/Loop[@LoopId='2320']">
        <oop:box id="11-insured-another-yes" x="54.5" y="25" width="2.5" text-align="center">X</oop:box>
      </xsl:when>
      <xsl:otherwise>
        <oop:box id="11-insured-another-no" x="59.5" y="25" width="2.5" text-align="center">X</oop:box>
      </xsl:otherwise>
    </xsl:choose>

    <oop:box id="12-patient-signature" x="9" y="28.5" width="25" text-align="center"></oop:box>
    <oop:box id="12-patient-signature-date" x="39" y="28.5" width="14" text-align="center"></oop:box>

    <oop:box id="13-insured-signature" x="59" y="28.5" width="24" text-align="center"></oop:box>

    <oop:box id="14-date-of-current-illness" x="5" y="31" width="10" text-align="center">MM DD YYYY</oop:box>

    <oop:box id="15-illness-first-date" x="40.5" y="31" width="10" text-align="center">MM DD YYYY</oop:box>

    <oop:box id="16-unable-to-work-from" x="57" y="31" width="10" text-align="center">MM DD YYYY</oop:box>
    <oop:box id="16-unable-to-work-to" x="71" y="31" width="10" text-align="center">MM DD YYYY</oop:box>

    <oop:box id="17-referring-provider-name" x="4" y="33" width="26">REFERRING PROVIDER NAME</oop:box>
    <oop:box id="17-referring-provider-id-qual" x="33" y="32" width="3">QL</oop:box>
    <oop:box id="17-referring-provider-id" x="36" y="32" width="16">REFERRING PROVIDER ID</oop:box>
    <oop:box id="17-referring-provider-npi" x="36" y="33" width="16">NPI</oop:box>

    <oop:box id="18-hospitalization-from" x="57" y="33" width="10" text-align="center">MM DD YYYY</oop:box>
    <oop:box id="18-hospitalization-to" x="71" y="33" width="10" text-align="center">MM DD YYYY</oop:box>

    <oop:box id="19-reserved" x="4" y="35" width="49">RESERVED</oop:box>

    <oop:box id="20-outside-lab-yes" x="54.5" y="35" width="2.5" text-align="center">Y</oop:box>
    <oop:box id="20-outside-lab-no" x="59.5" y="35" width="2.5" text-align="center">N</oop:box>
    <oop:box id="20-outside-lab-charges-1" x="65" y="35" width="9" text-align="right">00.00</oop:box>
    <oop:box id="20-outside-lab-charges-2" x="74.5" y="35" width="9" text-align="right">00.00</oop:box>

    <xsl:call-template name="box-21">
      <xsl:with-param name="claim" select="$claim"/>
    </xsl:call-template>

    <oop:box id="22-medicare-resubmission-code" x="53" y="37" width="11">RESUB CODE</oop:box>
    <oop:box id="22-medicare-resubmission-original-ref-number" x="65" y="37" width="18">ORIGINAL REF NUM</oop:box>

    <oop:box id="23-prior-authorization-number" x="53" y="39" width="30">PRIOR AUTH #</oop:box>

  </xsl:template>

  <xsl:template name="claim-footer">
    <xsl:param name="claim"/>

    <xsl:call-template name="box-25">
      <xsl:with-param name="claim" select="$claim"/>
    </xsl:call-template>
    <oop:box id="26-patient-control-number" x="26" y="55" width="14">
      <xsl:value-of select="$claim/@PatientControlNumber"/>
    </oop:box>
    <oop:box id="27-accept-assignment-yes" x="40.5" y="55" width="2.5" text-align="center">
      <xsl:if test="$claim/@BenefitsAssignmentCertificationIndicator = 'Y'">X</xsl:if>
    </oop:box>
    <oop:box id="27-accept-assignment-no" x="45.5" y="55" width="2.5" text-align="center">
      <xsl:if test="$claim/@BenefitsAssignmentCertificationIndicator = 'N'">X</xsl:if>
    </oop:box>

    <oop:box id="28-total-charges" x="55" y="55" width="9" text-align="right">
      <xsl:value-of select="format-number($claim/@TotalCharge,'# 00','implied')"/>
    </oop:box>
    <xsl:choose>
      <xsl:when test="string-length($claim/Amount[@Qual='F5']) > 0">
        <oop:box id="29-amount-paid" x="64.5" y="55" width="9" text-align="right">
          <xsl:value-of select="format-number($claim/Amount[@Qual='F5'],'# 00','implied')"/>
        </oop:box>
        <oop:box id="30-balance-due" x="74" y="55" width="9" text-align="right">
          <xsl:value-of select="format-number($claim/@TotalCharge - $claim/Amount[@Qual='F5'],'# 00','implied')"/>
        </oop:box>
      </xsl:when>
      <xsl:otherwise>
        <oop:box id="29-amount-paid" x="64.5" y="55" width="9" text-align="right">
        </oop:box>
        <oop:box id="30-balance-due" x="74" y="55" width="9" text-align="right">
          <xsl:value-of select="format-number($claim/@TotalCharge,'# 00','implied')"/>
        </oop:box>
      </xsl:otherwise>
    </xsl:choose>

    <xsl:call-template name="box-31">
      <xsl:with-param name="provider" select="$claim/Provider[@LoopId='2310B']" />
    </xsl:call-template>

    <xsl:choose>
      <xsl:when test="count($claim/Provider[@LoopId='2310D']) > 0">
        <xsl:call-template name="box-32">
          <xsl:with-param name="facility" select="$claim/Provider[@LoopId='2310D']"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="box-32">
          <xsl:with-param name="facility" select="$claim/ancestor::node()[@LoopId='2000A']/Provider[Name/@Qual='85']" />
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>

    <xsl:call-template name="box-33">
      <xsl:with-param name="billing-provider" select="$claim/ancestor::node()[@LoopId='2000A']/Provider[Name/@Qual='85']" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="box-1">
    <xsl:param name="filingCode"/>
    <oop:box id="1-medicare" x="3.5" y="7" width="2.5" text-align="center">
      <xsl:if test="$filingCode='MB'">X</xsl:if>
    </oop:box>
    <oop:box id="1-medicaid" x="10.5" y="7" width="2.5" text-align="center">
      <xsl:if test="$filingCode='MC'">X</xsl:if>
    </oop:box>
    <oop:box id="1-tricare-champus" x="17.5" y="7" width="2.5" text-align="center">
      <xsl:if test="$filingCode='CH'">X</xsl:if>
    </oop:box>
    <oop:box id="1-champva" x="26.5" y="7" width="2.5" text-align="center">

    </oop:box>
    <oop:box id="1-group-health-plan" x="33.5" y="7" width="2.5" text-align="center">

    </oop:box>
    <oop:box id="1-feca-blk-lung" x="41.5" y="7" width="2.5" text-align="center">

    </oop:box>
    <oop:box id="1-other" x="47.5" y="7" width="2.6" text-align="center">

    </oop:box>
  </xsl:template>

  <xsl:template name="box-21">
    <xsl:param name="claim"/>
    <xsl:variable name="diag1" select="$claim/InformationCode[@Qual='BK']"/>
    <xsl:variable name="diag2" select="$claim/InformationCode[@Qual='BF'][1]"/>
    <xsl:variable name="diag3" select="$claim/InformationCode[@Qual='BF'][2]"/>
    <xsl:variable name="diag4" select="$claim/InformationCode[@Qual='BF'][3]"/>
    <oop:box id="21-diag-1" x="6" y="37" width="8">
      <xsl:value-of select="concat(substring($diag1,1,3),' ',substring($diag1,4))"/>
    </oop:box>
    <oop:box id="21-diag-2" x="6" y="39" width="8">
      <xsl:value-of select="concat(substring($diag2,1,3),' ',substring($diag2,4))"/>
    </oop:box>
    <oop:box id="21-diag-3" x="33" y="37" width="8">
      <xsl:value-of select="concat(substring($diag3,1,3),' ',substring($diag3,4))"/>
    </oop:box>
    <oop:box id="21-diag-4" x="33" y="39" width="8">
      <xsl:value-of select="concat(substring($diag4,1,3),' ',substring($diag4,4))"/>
    </oop:box>
  </xsl:template>
  
  <xsl:template name="box-24-i-and-j">
    <xsl:param name="y"/>
    <xsl:param name="id"/>
    <oop:box id="24i-id-qual" x="68" width="3">
      <xsl:attribute name="y">
        <xsl:value-of select="$y"/>
      </xsl:attribute>
      <xsl:value-of select="$id/@Qual"/>
    </oop:box>
    <oop:box id="24j-provider-id" x="71" width="12">
      <xsl:attribute name="y">
        <xsl:value-of select="$y"/>
      </xsl:attribute>
      <xsl:value-of select="$id"/>
    </oop:box>
  </xsl:template>

  <xsl:template name="box-25">
    <xsl:param name="claim"/>
    <xsl:variable name="pay-to-provider" select="$claim/ancestor::node()[@LoopId='2000A']/Provider[Name/@Qual='87']" />
    <xsl:variable name="billing-provider" select="$claim/ancestor::node()[@LoopId='2000A']/Provider[Name/@Qual='85']" />
    <xsl:choose>
      <xsl:when test="count($pay-to-provider/Name/Identification[@Qual='24' or @Qual='34']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$pay-to-provider/Name/Identification"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center">
          <xsl:if test="$pay-to-provider/Name/Identification/@Qual='34'">X</xsl:if>
        </oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center">
          <xsl:if test="$pay-to-provider/Name/Identification/@Qual='24'">X</xsl:if>
        </oop:box>
      </xsl:when>
      <xsl:when test="count($pay-to-provider/Reference[@Qual='EI']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$pay-to-provider/Reference[@Qual='EI']"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center"></oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center">X</oop:box>
      </xsl:when>
      <xsl:when test="count($pay-to-provider/Reference[@Qual='SY']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$pay-to-provider/Reference[@Qual='SY']"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center">X</oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center"></oop:box>
      </xsl:when>
      <xsl:when test="count($billing-provider/Name/Identification[@Qual='24' or @Qual='34']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$billing-provider/Name/Identification"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center">
          <xsl:if test="$billing-provider/Name/Identification/@Qual='34'">X</xsl:if>
        </oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center">
          <xsl:if test="$billing-provider/Name/Identification/@Qual='24'">X</xsl:if>
        </oop:box>
      </xsl:when>
      <xsl:when test="count($billing-provider/Reference[@Qual='EI']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$billing-provider/Reference[@Qual='EI']"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center"></oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center">X</oop:box>
      </xsl:when>
      <xsl:when test="count($billing-provider/Reference[@Qual='SY']) > 0">
        <oop:box id="25-tax-id" x="4" y="55" width="15">
          <xsl:value-of select="$billing-provider/Reference[@Qual='SY']"/>
        </oop:box>
        <oop:box id="25-tax-id-is-ssn" x="19.5" y="55" width="2.5" text-align="center">X</oop:box>
        <oop:box id="25-tax-id-is-ein" x="21.5" y="55" width="2.5" text-align="center"></oop:box>
      </xsl:when>  
    </xsl:choose>

  </xsl:template>

  <xsl:template name="box-31">
    <xsl:param name="provider" />
    
    <oop:box id="31-line-1" x="4" y="57" width="21"></oop:box>
    <oop:box id="31-line-2" x="4" y="58" width="21"></oop:box>
    <oop:box id="31-line-3" x="4" y="59" width="21">
      <xsl:value-of select="$provider/Name/Full"/>
    </oop:box>
    <oop:box id="31-line-4" x="4" y="60" width="21"></oop:box>


  </xsl:template>
  
  <xsl:template name="box-32">
    <xsl:param name="facility"/>
    
    <oop:box id="32-service-facility-location-1" x="26" y="56" width="27">
    </oop:box>
    <oop:box id="32-service-facility-location-2" x="26" y="57" width="27">
      <xsl:value-of select="$facility/Name/Full"/>
    </oop:box>
    <oop:box id="32-service-facility-location-3" x="26" y="58" width="27">
      <xsl:for-each select="$facility/AddressLine">
        <xsl:value-of select="."/>
      </xsl:for-each>  
    </oop:box>
    <oop:box id="32-service-facility-location-4" x="26" y="59" width="27">
      <xsl:value-of select="concat($facility/Locale/@City, ', ',$facility/Locale/@State, ' ', $facility/Locale/@PostalCode)"/>
    </oop:box>
    <oop:box id="32a" x="27" y="60" width="10">
      <xsl:value-of select="$facility/Name/Identification[@Qual='XX']"/>

    </oop:box>
    <oop:box id="32b" x="38" y="60" width="15">
      <xsl:value-of select="concat($facility/Reference/@Qual, ' ', $facility/Reference)"/>

    </oop:box>
  </xsl:template>
  
  <xsl:template name="box-33">
    <xsl:param name="billing-provider"/>
    <oop:box id="33-billing-provider-1" x="53" y="56" width="30">
      
    </oop:box>
    <oop:box id="33-billing-provider-2" x="53" y="57" width="30">
      <xsl:value-of select="$billing-provider/Name/Full"/>
    </oop:box>
    <oop:box id="33-billing-provider-3" x="53" y="58" width="30">
      <xsl:for-each select="$billing-provider/AddressLine">
        <xsl:value-of select="."/>
      </xsl:for-each>
    </oop:box>
    <oop:box id="33-billing-provider-4" x="53" y="59" width="30">
      <xsl:value-of select="concat($billing-provider/Locale/@City, ', ',$billing-provider/Locale/@State, ' ', $billing-provider/Locale/@PostalCode)"/>
    </oop:box>
    <oop:box id="33a-npi" x="54" y="60" width="10">
      <xsl:value-of select="$billing-provider/Name/Identification[@Qual='XX']"/>
    </oop:box>
    <oop:box id="33b" x="65" y="60" width="18">
      <xsl:value-of select="concat($billing-provider/Reference/@Qual, ' ', $billing-provider/Reference)"/>
    </oop:box>
  </xsl:template>
    
  
</xsl:stylesheet>
