<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                 xmlns:hip="http://www.oopfactory.com/2011/XSL/Hipaa"
>
    <xsl:output method="html" indent="yes"/>
  
    <xsl:template match="hip:EligibilityBenefitResponse">
      <div>
        <div class="eligibilityTable">
          <h1>
            Payer
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="payerTable">
            <tr>
              <th class="col1">Payer Name</th>
              <td class="col2"><xsl:value-of select="hip:Source/hip:Name/@LastName"/></td>
              <th class="col3">Transaction ID</th>
              <td class="col4"><xsl:value-of select="@TransactionControlNumber"/></td>
            </tr>
          </table>
        </div>
        <div class="eligibilityTable">
          <h1>
            Provider
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="providerTable">
            <tr>
              <th class="col1">Provider</th>
              <td colspan="3">
                <xsl:choose>
                  <xsl:when test="hip:Receiver/hip:Name/@Qualifier='NonPerson'">
                    <xsl:value-of select="hip:Receiver/hip:Name/@LastName"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat(hip:Receiver/hip:Name/@LastName,', ',hip:Receiver/hip:Name/@FirstName,' ',hip:Receiver/hip:Name/@MiddleName)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <div><xsl:value-of select="hip:Receiver/hip:Address/hip:Line1"/></div>
                <div>
                  <xsl:value-of select="hip:Receiver/hip:Address/hip:Line2"/>
                </div>
                <div>
                  <xsl:value-of select="concat(hip:Receiver/hip:Address/@City, ', ', hip:Receiver/hip:Address/@StateCode, ' ', hip:Receiver/hip:Address/@PostalCode)"/>
                </div>
              </td>
            </tr>
            <tr>
              <th class="col1">Provider ID</th>
              <td class="col2"><xsl:value-of select="hip:Receiver/@Npi"/></td>
              <th class="col3">Tax ID</th>
              <td class="col4"><xsl:value-of select="hip:Receiver/@TaxId"/></td>
            </tr>
          </table>
        </div>
        <div class="eligibilityTable">
          <h1>
            Subscriber
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="subscriberTable">
            <tr>
              <th class="col1">Insured Name</th>
              <td colspan="3"><xsl:value-of select="concat(hip:Subscriber/hip:Name/@LastName, ', ', hip:Subscriber/hip:Name/@FirstName, ' ', hip:Subscriber/hip:Name/@MiddleName)"/></td>
            </tr>
            <tr>
              <th class="col1">Member ID</th>
              <td class="col2"><xsl:value-of select="hip:Subscriber/@MemberId"/></td>
              <th class="col3">SSN</th>
              <td class="col4"><xsl:value-of select="hip:Subscriber/@Ssn"/></td>
            </tr>
            <tr>
              <th class="col1">Group Number</th>
              <td class="col2"><xsl:value-of select="hip:Subscriber/@GroupNumber"/></td>
              <th class="col3">Group Name</th>
              <td class="col4"><xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;</td>
            </tr>
            <tr>
              <th class="col1">Date of Birth</th>
              <td class="col2"><xsl:value-of select="hip:Subscriber/@DateOfBirth"/></td>
              <th class="col3">Gender</th>
              <td class="col4"><xsl:value-of select="hip:Subscriber/@Gender"/></td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <xsl:value-of select="hip:Subscriber/hip:Address/hip:Line1"/>
                <xsl:value-of select="hip:Subscriber/hip:Address/hip:Line2"/>
              </td>
            </tr>
            <tr>
              <th class="col1"></th>
              <td colspan="3"><xsl:value-of select="concat(hip:Subscriber/hip:Address/@City, ', ', hip:Subscriber/hip:Address/@StateCode, ' ', hip:Subscriber/hip:Address/@PostalCode)"/></td>
            </tr>
            <tr>
              <th class="col1">Dependent Sequence Number</th>
              <td colspan="3">
                1
              </td>
            </tr>
            <tr>
              <th class="col1">Branch</th>
              <td colspan="3">0002</td>
            </tr>
            <tr>
              <th class="col1">Subdivision</th>
              <td colspan="3">0001</td>
            </tr>
            <tr>
              <th class="col1">Employee ID Number</th>
              <td colspan="3"><xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;</td>
            </tr>
            <tr>
              <th class="col1">Plan Code</th>
              <td colspan="3"><xsl:value-of select="hip:Subscriber/@PlanNumber"/></td>
            </tr>
          </table>
        </div>
        <xsl:if test="count(hip:Dependent)>0">
        <div class="eligibilityTable">
          <h1>
            Dependent
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="dependentTable">
            <tr>
              <th class="col1">Patient Name</th>
              <td colspan="3"><xsl:value-of select="concat(hip:Dependent/hip:Name/@LastName, ', ', hip:Dependent/hip:Name/@FirstName, ' ', hip:Dependent/hip:Name/@MiddleName)"/></td>
            </tr>
            <tr>
              <th class="col1">Relationship</th>
              <td class="col2"><xsl:value-of select="hip:Dependent/hip:Relationship"/></td>
              <th class="col3">SSN</th>
              <td class="col4"><xsl:value-of select="hip:Dependent/@Ssn"/></td>
            </tr>
            <tr>
              <th class="col1">Group Number</th>
              <td class="col2"><xsl:value-of select="hip:Dependent/@GroupNumber"/></td>
              <th class="col3">Group Name</th>
              <td class="col4"><xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;</td>
            </tr>
            <tr>
              <th class="col1">Date of Birth</th>
              <td class="col2"><xsl:value-of select="hip:Dependent/@DateOfBirth" /></td>
              <th class="col3">Gender</th>
              <td class="col4"><xsl:value-of select="hip:Dependent/@Gender"/></td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <xsl:value-of select="hip:Dependent/hip:Address/hip:Line1"/>
                <xsl:value-of select="hip:Dependent/hip:Address/hip:Line2"/>
              </td>
            </tr>
            <tr>
              <th class="col1"></th>
              <td colspan="3"><xsl:value-of select="concat(hip:Dependent/hip:Address/@City, ', ', hip:Dependent/hip:Address/@StateCode, ' ', hip:Dependent/hip:Address/@PostalCode)"/></td>
            </tr>
          </table>
        </div>

        </xsl:if>
        <div class="oneColumnTable">
          <h1>
            Coverage Type
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="coveragesTable">
            <tr>
              <td>
                <xsl:value-of select="concat(hip:Benefit/hip:InsuranceType,': ', hip:Benefit/hip:CoverageLevel, ', ', hip:Benefit/hip:InfoType)"/>
              </td>
            </tr>
          </table>
        </div>
        <div class="oneColumnTable">
          <h1>
            Coverage Dates
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="coverageDatesTable">
            <tr>
              <th>
                <strong>Dependent Coverage Dates</strong>
              </th>
            </tr>
            <tr>
              <td>
                Eligibility Begin <xsl:value-of select="hip:Dependent/@EligibilityBeginDate" />
              </td>
            </tr>
          </table>
        </div>
        <h1>
          Deductibles &amp; Maximums
        </h1>
        <div class="eligibilityGrid">
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="deductiblesTable">
            <tr>
              <th class="description" colspan="3">
                Deductible
              </th>
              <th class="unkNetwork right end">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </th>
            </tr>
            <xsl:for-each select="hip:BenefitInfo[hip:InfoType/@Code='C']">
              <tr>
                <td class="description" colspan="3">
                  <xsl:value-of select="hip:CoverageLevel"/>
                </td>
                <td class="unkNetwork right">
                  <xsl:value-of select="@Amount"/>
                </td>
              </tr>
              <tr>
                <td class="description" colspan="3">
                  Family
                </td>
                <td class="unkNetwork right">
                  $150.00
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </div>
        <div class="eligibilityGrid">
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="maximumsTable">
            <tr>
              <th class="description" colspan="2">
                Maximum
              </th>
              <th class="unkNetwork right end">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </th>
            </tr>
            <tr>
              <td>
                Individual
              </td>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="unkNetwork right">
                $2,000.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Used
              </td>
              <td class="unkNetwork right">
                $103.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Remaining
              </td>
              <td class="unkNetwork right">
                $1,897.00
              </td>
            </tr>
            <tr>
              <td>
                Individual, Dental Care
              </td>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="unkNetwork right">
                $2,000.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Used
              </td>
              <td class="unkNetwork right">
                $103.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Remaining
              </td>
              <td class="unkNetwork right">
                $1,897.00
              </td>
            </tr>
            <tr>
              <td>
                Individual, Periodontics
              </td>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="unkNetwork right">
                $2,000.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Used
              </td>
              <td class="unkNetwork right">
                $103.00
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td>
                Amount Remaining
              </td>
              <td class="unkNetwork right">
                $1,897.00
              </td>
            </tr>
            <tr>
              <td>
                Individual, Orthodontics
              </td>
              <td>
                Lifetime
              </td>
              <td class="unkNetwork right">
                $2,500.00
              </td>
            </tr>
          </table>
        </div>
        <div class="oneColumnTable">
          <h1>
            Plan Provisions
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="planProvisionsTable">
            <tr>
              <td>
                This plan Coordinates Benefits
              </td>
            </tr>
            <tr>
              <td>
                This plan uses Birthday Rule to Coordinate Benefits
              </td>
            </tr>
            <tr>
              <td>
                COB Type - Non-duplication of benefits applies.
              </td>
            </tr>
            <tr>
              <td>
                This plan covers teeth lost prior to the effective date
              </td>
            </tr>
            <tr>
              <td>
                Total ortho charge to be considered as the placement charges 20%
              </td>
            </tr>
            <tr>
              <td>
                Repetitive ortho payments are made QUARTERLY
              </td>
            </tr>
          </table>
        </div>
        <div class="eligibilityGrid">
          <h1>
            Coverage
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="coInsuranceTable">
            <tr>
              <th class="description">
                Description
              </th>
              <th class="unkNetwork right">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </th>
              <th class="messages end">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </th>
              <th class="deductible end">
                Deductible Applies
              </th>
            </tr>
            <xsl:for-each select="hip:BenefitInfo[hip:InfoType/@Code='1']">
              <tr>
                <td class="description">
                  <xsl:value-of select="hip:InsuranceType"/>
                  <xsl:if test="count(hip:Procedure)>0">
                    <xsl:value-of select="concat(', ', hip:Procedure/@ProcedureCode)"/>
                  </xsl:if>
                  <xsl:for-each select="hip:ServiceType">
                    <div>
                      <xsl:value-of select="."/>
                    </div>
                  </xsl:for-each>
                </td>
                <td class="unkNetwork right">
                  <xsl:if test="string-length(@Percentage) > 0">
                    <xsl:value-of select="concat(100 * @Percentage,'%')"/>
                  </xsl:if>
                </td>
                <td class="messages">
                  <xsl:for-each select="Message">
                    <xsl:value-of select="."/>
                  </xsl:for-each>
                </td>
                <td class="deductible">
                  NO
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </div>
        <div class="eligibilityGrid">
          <h1>
            Frequency Limitations
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="frequencyRestrictionsTable">
            <tr>
              <th class="description">
                Procedure
              </th>
              <th class="restriction">
                Restriction
              </th>
              <th class="lastVisit end">
                Last Visit
              </th>
            </tr>
            <xsl:for-each select="hip:BenefitInfo[hip:InfoType/@Code='F']">
              <tr>
                <td class="procedure">
                  <xsl:value-of select="hip:Procedure/@ProcedureCode"/>
                  <xsl:value-of select="hip:PlanCoverageDescription"/>
                </td>
                <td class="restriction">
                  <xsl:value-of select="concat(hip:Quantity/@Amount, ' ', hip:Quantity, ' ', hip:TimePeriod)"/>
                  
                </td>
                <td class="lastVisit">
                  <xsl:value-of select="hip:Date[@Qualifier='304']/@Date"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </div>
        <div class="eligibilityGrid">
          <h1>
            Age Limitations
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="ageLimitationTable">
            <tr>
              <th class="deltaPlan">
                Plan
              </th>
              <th class="description">
                Procedure
              </th>
              <th class="restriction end">
                Restriction
              </th>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="restriction">
                Student To Age 25
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="restriction">
                Dependent To Age 19
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                Orthodontics
              </td>
              <td class="restriction">
                Child To Age 19
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                Orthodontics
              </td>
              <td class="restriction">
                Student To Age 25
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                Orthodontics
              </td>
              <td class="restriction">
                Employee To Age 99
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                D1204
              </td>
              <td class="restriction">
                To Age 19
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                D1510
              </td>
              <td class="restriction">
                To Age 19
              </td>
            </tr>
            <tr>
              <td class="deltaPlan">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td class="procedure">
                D1351
              </td>
              <td class="restriction">
                To Age 19
              </td>
            </tr>
          </table>
        </div>
        <div class="eligibilityTable">
          <h1>
            Other
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="otherDataTable">
            <tr>
              <th class="col1">
                Employer
              </th>
              <td colspan="3">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
            </tr>
            <tr>
              <th class="col1">
                Name
              </th>
              <td colspan="3">
                MY EMPLOYER
              </td>
            </tr>
            <tr>
              <th class="col1">
                Address
              </th>
              <td colspan="3">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
            </tr>
            <tr>
              <td colspan="4">
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
            </tr>
              <tr>
                <th class="col1">
                  Payer
                </th>
                <td colspan="3">
                  <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                </td>
              </tr>
              <tr>
                <th class="col1">
                  Name
                </th>
                <td colspan="3">
                  MetLife
                </td>
              </tr>
              <tr>
                <th class="col1">
                  Address
                </th>
                <td colspan="3">
                  PO BOX 981282<br />
                  EL PASO, TX<xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;<xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;79998
                </td>
              </tr>
              <tr>
                <th class="col1">
                  Contact
                </th>
                <td class="col2">
                  <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                </td>
                <th class="col3">
                  Telephone
                </th>
                <td class="col4">
                  (888) 660-1046
                </td>
              </tr>
            </table>
        </div>
        <div class="disclaimer">
          <strong>Disclaimer</strong>: This eligibility report is for informational purposes
          only. The information is derived directly from the payer indicated on the report
          and is not to be construed as a guarantee of payment.
        </div>
      </div>
      </xsl:template>
</xsl:stylesheet>
