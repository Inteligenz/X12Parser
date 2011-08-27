<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>
  
    <xsl:template match="BenefitResponse">
      <div>
        <div class="eligibilityTable">
          <h1>
            Payer
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="payerTable">
            <tr>
              <th class="col1">Payer Name</th>
              <td class="col2"><xsl:value-of select="Source/Name/@LastName"/></td>
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
                  <xsl:when test="Receiver/Name/@Qualifier='NonPerson'">
                    <xsl:value-of select="Receiver/Name/@LastName"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="concat(Receiver/Name/@LastName,', ',Receiver/Name/@FirstName,' ',Receiver/Name/@MiddleName)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <div><xsl:value-of select="Receiver/Address/Line1"/></div>
                <div>
                  <xsl:value-of select="Receiver/Address/Line2"/>
                </div>
                <div>
                  <xsl:value-of select="concat(Receiver/Address/@City, ', ', Receiver/Address/@StateCode, ' ', Receiver/Address/@PostalCode)"/>
                </div>
              </td>
            </tr>
            <tr>
              <th class="col1">Provider ID</th>
              <td class="col2"><xsl:value-of select="Receiver/@Npi"/></td>
              <th class="col3">Tax ID</th>
              <td class="col4"><xsl:value-of select="Receiver/@TaxId"/></td>
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
              <td colspan="3"><xsl:value-of select="concat(Subscriber/Name/@LastName, ', ', Subscriber/Name/@FirstName, ' ', Subscriber/Name/@MiddleName)"/></td>
            </tr>
            <tr>
              <th class="col1">Member ID</th>
              <td class="col2"><xsl:value-of select="Subscriber/@MemberId"/></td>
              <th class="col3">SSN</th>
              <td class="col4"><xsl:value-of select="Subscriber/@Ssn"/></td>
            </tr>
            <tr>
              <th class="col1">Group Number</th>
              <td class="col2"><xsl:value-of select="Subscriber/@GroupNumber"/></td>
              <th class="col3">Group Name</th>
              <td class="col4"><xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;</td>
            </tr>
            <tr>
              <th class="col1">Date of Birth</th>
              <td class="col2"><xsl:value-of select="Subscriber/@DateOfBirth"/></td>
              <th class="col3">Gender</th>
              <td class="col4"><xsl:value-of select="Subscriber/@Gender"/></td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <xsl:value-of select="Subscriber/Address/Line1"/>
                <xsl:value-of select="Subscriber/Address/Line2"/>
              </td>
            </tr>
            <tr>
              <th class="col1"></th>
              <td colspan="3"><xsl:value-of select="concat(Subscriber/Address/@City, ', ', Subscriber/Address/@StateCode, ' ', Subscriber/Address/@PostalCode)"/></td>
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
              <td colspan="3"><xsl:value-of select="Subscriber/@PlanNumber"/></td>
            </tr>
          </table>
        </div>
        <xsl:if test="count(Dependent)>0">
        <div class="eligibilityTable">
          <h1>
            Dependent
          </h1>
          <table cellpadding="0" cellspacing="0" border="1" width="100%" id="dependentTable">
            <tr>
              <th class="col1">Patient Name</th>
              <td colspan="3"><xsl:value-of select="concat(Dependent/Name/@LastName, ', ', Dependent/Name/@FirstName, ' ', Dependent/Name/@MiddleName)"/></td>
            </tr>
            <tr>
              <th class="col1">Relationship</th>
              <td class="col2"><xsl:value-of select="Dependent/Relationship"/></td>
              <th class="col3">SSN</th>
              <td class="col4"><xsl:value-of select="Dependent/@Ssn"/></td>
            </tr>
            <tr>
              <th class="col1">Group Number</th>
              <td class="col2"><xsl:value-of select="Dependent/@GroupNumber"/></td>
              <th class="col3">Group Name</th>
              <td class="col4"><xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;</td>
            </tr>
            <tr>
              <th class="col1">Date of Birth</th>
              <td class="col2"><xsl:value-of select="Dependent/@DateOfBirth" /></td>
              <th class="col3">Gender</th>
              <td class="col4"><xsl:value-of select="Dependent/@Gender"/></td>
            </tr>
            <tr>
              <th class="col1">Address</th>
              <td colspan="3">
                <xsl:value-of select="Dependent/Address/Line1"/>
                <xsl:value-of select="Dependent/Address/Line2"/>
              </td>
            </tr>
            <tr>
              <th class="col1"></th>
              <td colspan="3"><xsl:value-of select="concat(Dependent/Address/@City, ', ', Dependent/Address/@StateCode, ' ', Dependent/Address/@PostalCode)"/></td>
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
                Dental: Family, Active Coverage
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
                Eligibility Begin 1/1/2009
              </td>
            </tr>
          </table>
        </div>
      </div>
      </xsl:template>
</xsl:stylesheet>
