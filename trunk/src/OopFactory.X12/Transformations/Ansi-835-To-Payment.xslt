<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:import href="Ansi-Common.xslt"/>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template name="AdjustmentLine">
    <xsl:param name="ReasonCode"/>
    <xsl:param name="Amount"/>
    <xsl:param name="Quantity"/>
    <xsl:if test="string-length($Amount)>0">
      <Adjustment>
        <xsl:attribute name="Amount">
          <xsl:value-of select="$Amount"/>
        </xsl:attribute>
        <xsl:attribute name="Quantity">
          <xsl:value-of select="$Quantity"/>
        </xsl:attribute>
        <Reason>
          <xsl:attribute name="Code">
            <xsl:value-of select="$ReasonCode"/>
          </xsl:attribute>
        </Reason>
      </Adjustment>
    </xsl:if>
  </xsl:template>
  
  <xsl:template name="ProviderAdjustmentLine">
    <xsl:param name="ReasonCode"/>
    <xsl:param name="Identifier"/>
    <xsl:param name="Amount"/>
    <xsl:if test="string-length($Amount)>0">
      <Adjustment>
        <xsl:attribute name="ReferenceId">
          <xsl:value-of select="$Identifier"/>
        </xsl:attribute>
        <xsl:attribute name="Amount">
          <xsl:value-of select="$Amount"/>
        </xsl:attribute>
        <Reason>
          <xsl:attribute name="Code">
            <xsl:value-of select="$ReasonCode"/>
          </xsl:attribute>
          <xsl:choose>
            <xsl:when test="$ReasonCode='50'">Late Charge</xsl:when>
            <xsl:when test="$ReasonCode='51'">Interest Penalty Charge</xsl:when>
            <xsl:when test="$ReasonCode='72'">Authorized Return</xsl:when>
            <xsl:when test="$ReasonCode='90'">Early Payment Allowance</xsl:when>
            <xsl:when test="$ReasonCode='AH'">Origination Fee</xsl:when>
            <xsl:when test="$ReasonCode='AM'">Applied To Borrower's Account</xsl:when>
            <xsl:when test="$ReasonCode='AP'">Acceleration of Benefits</xsl:when>
            <xsl:when test="$ReasonCode='B2'">Rebate</xsl:when>
            <xsl:when test="$ReasonCode='B3'">Recovery Allowance</xsl:when>
            <xsl:when test="$ReasonCode='BD'">Bad Dept Adjustment</xsl:when>
            <xsl:when test="$ReasonCode='BN'">Bonus</xsl:when>
            <xsl:when test="$ReasonCode='C5'">Temporary Allowance</xsl:when>
            <xsl:when test="$ReasonCode='CR'">Capitation Interest</xsl:when>
            <xsl:when test="$ReasonCode='CS'">Adjustment</xsl:when>
            <xsl:when test="$ReasonCode='CT'">Capitation Payment</xsl:when>
            <xsl:when test="$ReasonCode='CV'">Capital Passthru</xsl:when>
            <xsl:when test="$ReasonCode='CW'">Certified Registered Nurse Anesthetist Passthru</xsl:when>
            <xsl:when test="$ReasonCode='DM'">Direct Medical Education Passthru</xsl:when>
            <xsl:when test="$ReasonCode='E3'">Withholding</xsl:when>
            <xsl:when test="$ReasonCode='FB'">Forward Balance</xsl:when>
            <xsl:when test="$ReasonCode='FC'">Fund Allocation</xsl:when>
            <xsl:when test="$ReasonCode='GO'">Graduate Medical Education Passthru</xsl:when>
            <xsl:when test="$ReasonCode='IP'">Incentive Premium Payment</xsl:when>
            <xsl:when test="$ReasonCode='IR'">Internal Revenue Service Withholding</xsl:when>
            <xsl:when test="$ReasonCode='IS'">Interim Settlement</xsl:when>
            <xsl:when test="$ReasonCode='J1'">Nonreimbursable</xsl:when>
            <xsl:when test="$ReasonCode='L3'">Penalty</xsl:when>
            <xsl:when test="$ReasonCode='L6'">Interest Owed</xsl:when>
            <xsl:when test="$ReasonCode='LE'">Levy</xsl:when>
            <xsl:when test="$ReasonCode='LS'">Lump Sum</xsl:when>
            <xsl:when test="$ReasonCode='OA'">Organ Acquisition Passthru</xsl:when>
            <xsl:when test="$ReasonCode='OB'">Offset for Affiliated Providers</xsl:when>
            <xsl:when test="$ReasonCode='PI'">Periodic Interim Payment</xsl:when>
            <xsl:when test="$ReasonCode='PL'">Payment Final</xsl:when>
            <xsl:when test="$ReasonCode='RA'">Retro-activity Adjustment</xsl:when>
            <xsl:when test="$ReasonCode='RE'">Return on Equity</xsl:when>
            <xsl:when test="$ReasonCode='SL'">Student Loan Repayment</xsl:when>
            <xsl:when test="$ReasonCode='TL'">Third Party Liability</xsl:when>
            <xsl:when test="$ReasonCode='WO'">Overpayment Recovery</xsl:when>
            <xsl:when test="$ReasonCode='WU'">Unspecified Recovery</xsl:when>
            <xsl:when test="$ReasonCode='ZZ'">Mutually Defined</xsl:when>
          </xsl:choose>
        </Reason>
      </Adjustment>
    </xsl:if>
  </xsl:template>

  <xsl:template match="BPR">
    <FinancialInformation>
      <xsl:attribute name="TotalActualProviderPaymentAmount">
        <xsl:value-of select="BPR02"/>
      </xsl:attribute>
      <xsl:if test="string-length(BPR16)>0">
        <xsl:attribute name="PaymentEffectiveDate">
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="BPR16"/>
          </xsl:call-template>
        </xsl:attribute>
      </xsl:if>
      <Handling>
        <xsl:attribute name="Code">
          <xsl:value-of select="BPR01"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="BPR01='C'">Payment Accompanies Remittance Advice</xsl:when>
              <xsl:when test="BPR01='D'">Make Payment Only</xsl:when>
              <xsl:when test="BPR01='H'">Notification Only</xsl:when>
              <xsl:when test="BPR01='I'">Remittance Information Only</xsl:when>
              <xsl:when test="BPR01='P'">Prenotification of Future Transfers</xsl:when>
              <xsl:when test="BPR01='U'">Split Payment and Remittance</xsl:when>
              <xsl:when test="BPR01='X'">Handling Party's Option to Split Payment and Remittance</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </Handling>
      <TransactionType>
        <xsl:attribute name="Code">
          <xsl:value-of select="BPR03"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="BPR03='C'">Credit</xsl:when>
              <xsl:when test="BPR03='D'">Debit</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </TransactionType>
      <PaymentMethod>
        <xsl:attribute name="Code">
          <xsl:value-of select="BPR04"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:comment>
            <xsl:choose>
              <xsl:when test="BPR04='ACH'">Automated Clearing House (ACH)</xsl:when>
              <xsl:when test="BPR04='BOP'">Financial Institution Option</xsl:when>
              <xsl:when test="BPR04='CHK'">Check</xsl:when>
              <xsl:when test="BPR04='FWT'">Federal Reserve Funds/Wire Transfer - Nonrepetitive</xsl:when>
              <xsl:when test="BPR04='NON'">Non-Payment Data</xsl:when>
            </xsl:choose>
          </xsl:comment>
        </xsl:if>
      </PaymentMethod>
      <xsl:if test="string-length(BPR05)>0">
        <PaymentFormat>
          <xsl:attribute name="Code">
            <xsl:value-of select="BPR05"/>
          </xsl:attribute>
          <xsl:if test="$verbose=1">
            <xsl:comment>
              <xsl:choose>
                <xsl:when test="BPR05='CCP'">Cash Concentration/Disbursement plus Addenda (CCD+)(ACH)</xsl:when>
                <xsl:when test="BPR05='CTX'">Corporate Trade Exchange (CTX)(ACH)</xsl:when>
              </xsl:choose>
            </xsl:comment>
          </xsl:if>
        </PaymentFormat>
      </xsl:if>
      <Sender>
        <xsl:if test="$verbose=1">
          <xsl:comment>aka Originating Company</xsl:comment>
        </xsl:if>        
        <xsl:if test="string-length(BPR10)>0">
          <xsl:attribute name="Identifier">
            <xsl:value-of select="BPR10"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(BPR11)>0">
          <xsl:attribute name="SupplementalCode">
            <xsl:value-of select="BPR11"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(BPR07)>0">
          <DepositoryFinancialInstitution>
            <Qualifier>
              <xsl:attribute name="Code">
                <xsl:value-of select="BPR06"/>
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="BPR06='01'">ABA Transit Routing Number Including Check Digits (9 digits)</xsl:when>
                <xsl:when test="BPR06='04'">Canadian Bank Branch and Institution Number</xsl:when>
              </xsl:choose>
            </Qualifier>
            <Number>
              <xsl:value-of select="BPR07"/>
            </Number>
          </DepositoryFinancialInstitution>
        </xsl:if>
        <xsl:if test="string-length(BPR09)>0">
          <BankAccount>
            <Qualifier>
              <xsl:attribute name="Code">
                <xsl:value-of select="BPR08"/>
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="BPR08='DA'">Demand Deposit</xsl:when>
              </xsl:choose>
            </Qualifier>
            <Number>
              <xsl:value-of select="BPR09"/>
            </Number>
          </BankAccount>
        </xsl:if>
      </Sender>
      <Receiver>
        <xsl:if test="string-length(BPR13)>0">
          <DepositoryFinancialInstitution>
            <Qualifier>
              <xsl:attribute name="Code">
                <xsl:value-of select="BPR12"/>
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="BPR12='01'">ABA Transit Routing Number Including Check Digits (9 digits)</xsl:when>
                <xsl:when test="BPR12='04'">Canadian Bank Branch and Institution Number</xsl:when>
              </xsl:choose>
            </Qualifier>
            <Number>
              <xsl:value-of select="BPR13"/>
            </Number>
          </DepositoryFinancialInstitution>
        </xsl:if>
        <xsl:if test="string-length(BPR15)>0">
          <BankAccount>
            <Qualifier>
              <xsl:attribute name="Code">
                <xsl:value-of select="BPR14"/>
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="BPR14='DA'">Demand Deposit</xsl:when>
                <xsl:when test="BPR14='SG'">Savings</xsl:when>
              </xsl:choose>
            </Qualifier>
            <Number>
              <xsl:value-of select="BPR15"/>
            </Number>
          </BankAccount>
        </xsl:if>
      </Receiver>
    </FinancialInformation>

  </xsl:template>

  <xsl:template match="TRN">
    <Trace>
      <Number>
        <xsl:value-of select="TRN02"/>
      </Number>
      <OriginatingCompany>
        <xsl:attribute name="SupplementalCode">
          <xsl:value-of select="TRN04"/>
        </xsl:attribute>
        <Identifier>
          <xsl:value-of select="TRN03"/>
        </Identifier>
      </OriginatingCompany>
    </Trace>  
  </xsl:template>

  <xsl:template match="CUR">
    <ForeignCurrency>
      <xsl:attribute name="Code">
        <xsl:value-of select="CUR02"/>
      </xsl:attribute>
      <xsl:attribute name="ExchangeRate">
        <xsl:value-of select="CUR03"/>
      </xsl:attribute>
    </ForeignCurrency>
  </xsl:template>
  
  <xsl:template match="DTM">
    <xsl:choose>
      <!-- Claim Payment Date -->
      <xsl:when test="DTM01='036'">
        <ExpirationDate>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ExpirationDate>
      </xsl:when>
      <xsl:when test="DTM01='050'">
        <ReceivedDate>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ReceivedDate>
      </xsl:when>
      <xsl:when test="DTM01='232'">
        <ClaimStatmentPeriodStart>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ClaimStatmentPeriodStart>
      </xsl:when>
      <xsl:when test="DTM01='233'">
        <ClaimStatementPeriodEnd>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ClaimStatementPeriodEnd>
      </xsl:when>


      <!-- Service Payment Date -->
      <xsl:when test="DTM01='150'">
        <ServicePeriodStart>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ServicePeriodStart>
      </xsl:when>
      <xsl:when test="DTM01='151'">
        <ServicePeriodEnd>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ServicePeriodEnd>
      </xsl:when>
      <xsl:when test="DTM01='472'">
        <ServiceDate>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTM02"/>
          </xsl:call-template>
        </ServiceDate>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="CAS">
    <AdjustmentGroup>
      <Qualifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="CAS01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="CAS01='CO'">Contractual Obligations</xsl:when>
          <xsl:when test="CAS01='CR'">Correction and Reversals</xsl:when>
          <xsl:when test="CAS01='OA'">Other adjustments</xsl:when>
          <xsl:when test="CAS01='PI'">Payor Initiated Reductions</xsl:when>
          <xsl:when test="CAS01='PR'">Patient Responsibility</xsl:when>
        </xsl:choose>
      </Qualifier>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS02"/>
        <xsl:with-param name="Amount" select="CAS03"/>
        <xsl:with-param name="Quantity" select="CAS04"/>
      </xsl:call-template>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS05"/>
        <xsl:with-param name="Amount" select="CAS06"/>
        <xsl:with-param name="Quantity" select="CAS07"/>
      </xsl:call-template>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS08"/>
        <xsl:with-param name="Amount" select="CAS09"/>
        <xsl:with-param name="Quantity" select="CAS10"/>
      </xsl:call-template>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS11"/>
        <xsl:with-param name="Amount" select="CAS12"/>
        <xsl:with-param name="Quantity" select="CAS13"/>
      </xsl:call-template>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS14"/>
        <xsl:with-param name="Amount" select="CAS15"/>
        <xsl:with-param name="Quantity" select="CAS16"/>
      </xsl:call-template>
      <xsl:call-template name="AdjustmentLine">
        <xsl:with-param name="ReasonCode" select="CAS17"/>
        <xsl:with-param name="Amount" select="CAS18"/>
        <xsl:with-param name="Quantity" select="CAS19"/>
      </xsl:call-template>
    </AdjustmentGroup>
  </xsl:template>
  
  <xsl:template match="AMT">
    <OtherAmount>
      <Qualifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="AMT01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="AMT01='B6'">Allowed - Actual</xsl:when>
          <xsl:when test="AMT01='KH'">Deduction Amount</xsl:when>
          <xsl:when test="AMT01='NE'">Net Billed</xsl:when>

          <xsl:when test="AMT01='AU'">Coverage Amount</xsl:when>
          <xsl:when test="AMT01='D8'">Discount Amount</xsl:when>
          <xsl:when test="AMT01='DY'">Per Day Limit</xsl:when>
          <xsl:when test="AMT01='F5'">Patient Amount Paid</xsl:when>
          <xsl:when test="AMT01='I'">Interest</xsl:when>
          <xsl:when test="AMT01='NL'">Negative Ledger Balance</xsl:when>
          <xsl:when test="AMT01='T'">Tax</xsl:when>
          <xsl:when test="AMT01='T2'">Total Claim Before Taxes</xsl:when>
          <xsl:when test="AMT01='ZK'">Federal Medicare or Medicaid Payment Mandate - Category 1</xsl:when>
          <xsl:when test="AMT01='ZL'">Federal Medicare or Medicaid Payment Mandate - Category 2</xsl:when>
          <xsl:when test="AMT01='ZM'">Federal Medicare or Medicaid Payment Mandate - Category 3</xsl:when>
          <xsl:when test="AMT01='ZN'">Federal Medicare or Medicaid Payment Mandate - Category 4</xsl:when>
          <xsl:when test="AMT01='ZO'">Federal Medicare or Medicaid Payment Mandate - Category 5</xsl:when>
          <xsl:when test="AMT01='ZZ'">Mutually Defined</xsl:when>
        </xsl:choose>
      </Qualifier>
      <Amount>
        <xsl:value-of select="AMT02"/>
      </Amount>
    </OtherAmount>
  </xsl:template>

  <!-- Claim/Service Supplemental Quantity Segement -->
  <xsl:template match="QTY">
    <OtherQuantity>
      <Qualifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="QTY01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="QTY01='CA'">Covered - Actual</xsl:when>
          <xsl:when test="QTY01='CD'">Co-insured - Actual</xsl:when>
          <xsl:when test="QTY01='LA'">Life-time Reserve - Actual</xsl:when>
          <xsl:when test="QTY01='LE'">Life-time Reserve - Estimated</xsl:when>
          <xsl:when test="QTY01='NA'">Number of Non-covered Days</xsl:when>
          <xsl:when test="QTY01='NE'">Non-Covered - Estimated</xsl:when>
          <xsl:when test="QTY01='NR'">Not Replaced Blood Units</xsl:when>
          <xsl:when test="QTY01='OU'">Outlier Days</xsl:when>
          <xsl:when test="QTY01='PS'">Prescription</xsl:when>
          <xsl:when test="QTY01='VS'">Visits</xsl:when>
          <xsl:when test="QTY01='ZK'">Federal Medicare and Medicaid Payment Mandate - Category 1</xsl:when>
          <xsl:when test="QTY01='ZL'">Federal Medicare and Medicaid Payment Mandate - Category 2</xsl:when>
          <xsl:when test="QTY01='ZM'">Federal Medicare and Medicaid Payment Mandate - Category 3</xsl:when>
          <xsl:when test="QTY01='ZN'">Federal Medicare and Medicaid Payment Mandate - Category 4</xsl:when>
          <xsl:when test="QTY01='ZO'">Federal Medicare and Medicaid Payment Mandate - Category 5</xsl:when>
        </xsl:choose>
      </Qualifier>
      <Number>
        <xsl:value-of select="QTY02"/>
      </Number>
    </OtherQuantity>  
  </xsl:template>
  
  <!-- Health Care Remark Codes -->
  <xsl:template match="LQ">
    <Remarks>
      <Qualifier>
        <xsl:attribute name="Code">
          <xsl:value-of select="LQ01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="LQ01='HE'">Claim Payment Remark Codes</xsl:when>
          <xsl:when test ="LQ01='RX'">National Council for Prescription Drug Programs Reject/Payment Codes</xsl:when>
        </xsl:choose>
      </Qualifier>
      <Text>
        <xsl:value-of select="LQ02"/>
      </Text>
    </Remarks>
  </xsl:template>

  <xsl:template match="CLP">
    <xsl:attribute name="PatientControlNumber">
      <xsl:value-of select="CLP01"/>
    </xsl:attribute>
    <xsl:attribute name="TotalClaimChargeAmount">
      <xsl:value-of select="CLP03"/>
    </xsl:attribute>
    <xsl:attribute name="PaymentAmount">
      <xsl:value-of select="CLP04"/>
    </xsl:attribute>
    <xsl:attribute name="PatientResponsibleAmount">
      <xsl:value-of select="CLP05"/>
    </xsl:attribute>
    <ClaimStatus>
      <xsl:attribute name="Code">
        <xsl:value-of select="CLP02"/>
      </xsl:attribute>
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="CLP02='1'">Processed as Primary</xsl:when>
            <xsl:when test="CLP02='2'">Processed as Secondary</xsl:when>
            <xsl:when test="CLP02='3'">Processed as Tertiary</xsl:when>
            <xsl:when test="CLP02='4'">Denied</xsl:when>
            <xsl:when test="CLP02='5'">Pended</xsl:when>
            <xsl:when test="CLP02='10'">Received, but not in process</xsl:when>
            <xsl:when test="CLP02='13'">Suspended</xsl:when>
            <xsl:when test="CLP02='15'">Suspended - investigation with field</xsl:when>
            <xsl:when test="CLP02='16'">Suspended - return with material</xsl:when>
            <xsl:when test="CLP02='17'">Suspended - review pending</xsl:when>
            <xsl:when test="CLP02='19'">Processed as Primary, Forwarded to Additional Payer(s)</xsl:when>
            <xsl:when test="CLP02='20'">Processed as Secondary, Forwarded to Additional Payer(s)</xsl:when>
            <xsl:when test="CLP02='21'">Processed as Tertiary, Forwarded to Additional Payer(s)</xsl:when>
            <xsl:when test="CLP02='22'">Reversal of Previous Payment</xsl:when>
            <xsl:when test="CLP02='23'">Not Our Claim, Forwarded to Additional Payer(s)</xsl:when>
            <xsl:when test="CLP02='25'">Predetermination Pricing Only - No Payment</xsl:when>
            <xsl:when test="CLP02='27'">Reviewed</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </ClaimStatus>
    <ClaimType>
      <xsl:attribute name="Code">
        <xsl:value-of select="CLP06"/>
      </xsl:attribute>
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="CLP06='12'">Preferred Provider Organization (PPO)</xsl:when>
            <xsl:when test="CLP06='13'">Point of Service (POS)</xsl:when>
            <xsl:when test="CLP06='14'">Exclusive Provider Organization (EPO)</xsl:when>
            <xsl:when test="CLP06='15'">Indemnity Insurance</xsl:when>
            <xsl:when test="CLP06='16'">Health Maintenance Organization (HMO) Medicare Risk</xsl:when>
            <xsl:when test="CLP06='AM'">Automobile Medical</xsl:when>
            <xsl:when test="CLP06='CH'">Champus</xsl:when>
            <xsl:when test="CLP06='DS'">Disability</xsl:when>
            <xsl:when test="CLP06='HM'">Health Maintenance Organization</xsl:when>
            <xsl:when test="CLP06='LM'">Liability Medical</xsl:when>
            <xsl:when test="CLP06='MA'">Medicare Part A</xsl:when>
            <xsl:when test="CLP06='MB'">Medicare Part B</xsl:when>
            <xsl:when test="CLP06='MC'">Medicaid</xsl:when>
            <xsl:when test="CLP06='OF'">Other Federal Program</xsl:when>
            <xsl:when test="CLP06='TV'">Title V</xsl:when>
            <xsl:when test="CLP06='VA'">Veteran Administration Plan</xsl:when>
            <xsl:when test="CLP06='WC'">Workers' Compensation Health Claim</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </ClaimType>
    <PayerClaimControlNumber>
      <xsl:value-of select="CLP07"/>
    </PayerClaimControlNumber>
    <Facility>
      <xsl:attribute name="Code">
        <xsl:value-of select="CLP08"/>
      </xsl:attribute>
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="CLP08='11'">Office</xsl:when>
            <xsl:when test="CLP08='12'">Home</xsl:when>
            <xsl:when test="CLP08='21'">Inpatient Hospital</xsl:when>
            <xsl:when test="CLP08='22'">Outpatient Hospital</xsl:when>
            <xsl:when test="CLP08='23'">Emergency Room - Hospital</xsl:when>
            <xsl:when test="CLP08='24'">Ambulatory Surgical Center</xsl:when>
            <xsl:when test="CLP08='25'">Birthing Center</xsl:when>
            <xsl:when test="CLP08='26'">Military Treatment Facility</xsl:when>
            <xsl:when test="CLP08='31'">Skilled Nursing Facility</xsl:when>
            <xsl:when test="CLP08='32'">Nursing Facility</xsl:when>
            <xsl:when test="CLP08='33'">Custodial Care Facility</xsl:when>
            <xsl:when test="CLP08='34'">Hospice</xsl:when>
            <xsl:when test="CLP08='41'">Ambulance - Land</xsl:when>
            <xsl:when test="CLP08='42'">Ambulance - Air or Water</xsl:when>
            <xsl:when test="CLP08='51'">Inpatient Psychiatric Facility</xsl:when>
            <xsl:when test="CLP08='52'">Psychiatric Facility Partial Hospitalization</xsl:when>
            <xsl:when test="CLP08='53'">Community Mental Health Center</xsl:when>
            <xsl:when test="CLP08='54'">Intermediate Care Facility/Mentally Retarded</xsl:when>
            <xsl:when test="CLP08='55'">Residential Substance Abuse Treatment Facility</xsl:when>
            <xsl:when test="CLP08='56'">Psychiatric Residential Treament Center</xsl:when>
            <xsl:when test="CLP08='50'">Federally Qualified Health Center</xsl:when>
            <xsl:when test="CLP08='60'">Mass Immunization Center</xsl:when>
            <xsl:when test="CLP08='61'">Comprehensive Inpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="CLP08='62'">Comprehensive Outpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="CLP08='65'">End Stage Renal Disease Treatment Facility</xsl:when>
            <xsl:when test="CLP08='71'">State or Local Public Health Clinic</xsl:when>
            <xsl:when test="CLP08='72'">Rural Health Clinic</xsl:when>
            <xsl:when test="CLP08='81'">Independent Laboratory</xsl:when>
            <xsl:when test="CLP08='99'">Other Unlisted Facility</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </Facility>
  </xsl:template>

  <xsl:template match="MIA">
    <MedicareInpatientAdjustment>
      <xsl:attribute name="CoveredDaysOrVisitsCount">
        <xsl:value-of select="MIA01"/>
      </xsl:attribute>
      <xsl:attribute name="PpsOperatingOutlierAmount">
        <xsl:value-of select="MIA02"/>
      </xsl:attribute>
      <xsl:attribute name="LifetimePsychiatricDaysCount">
        <xsl:value-of select="MIA03"/>
      </xsl:attribute>
      <xsl:attribute name="RemarkCode">
        <xsl:value-of select="MIA05"/>
      </xsl:attribute>
      <xsl:if test="string-length(MIA04)>0">
        <MonetaryAmount Qualifier="Claim DRG MonetaryAmount">
          <xsl:value-of select="MIA04"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA06)>0">
        <MonetaryAmount Qualifier="Claim Disproportionate Share Amount">
          <xsl:value-of select="MIA06"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA07)>0">
        <MonetaryAmount Qualifier="Claim MSP Pass-through Amount">
          <xsl:value-of select="MIA07"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA08)>0">
        <MonetaryAmount Qualifier="Claim PPS Capital Amount">
          <xsl:value-of select="MIA08"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA09)>0">
        <MonetaryAmount Qualifier="PPS-Capital FSP DRG Amount">
          <xsl:value-of select="MIA09"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA10)>0">
        <MonetaryAmount Qualifier="PPS-Capital HSP DRG Amount">
          <xsl:value-of select="MIA10"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA11)>0">
        <MonetaryAmount Qualifier="PPS-Capital DSH DRG Amount">
          <xsl:value-of select="MIA11"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA12)>0">
        <MonetaryAmount Qualifier="Old Capital Amount">
          <xsl:value-of select="MIA12"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA13)>0">
        <MonetaryAmount Qualifier="PPS-Capital IME amount">
          <xsl:value-of select="MIA13"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA14)>0">
        <MonetaryAmount Qualifier="PPS-Operating Hospital Specific DRG Amount">
          <xsl:value-of select="MIA14"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA16)>0">
        <MonetaryAmount Qualifier="PPS-Operating Federal Specific DRG Amount">
          <xsl:value-of select="MIA16"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA17)>0">
        <MonetaryAmount Qualifier="Claim PPS Capital Outlier Amount">
          <xsl:value-of select="MIA17"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA18)>0">
        <MonetaryAmount Qualifier="Claim Indirect Teaching Amount">
          <xsl:value-of select="MIA18"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA19)>0">
        <MonetaryAmount Qualifier="Nonpayable Professional Component Amount">
          <xsl:value-of select="MIA19"/>
        </MonetaryAmount>
      </xsl:if>
      <xsl:if test="string-length(MIA24)>0">
        <MonetaryAmount Qualifier="PPS-Capital Exception Amount">
          <xsl:value-of select="MIA24"/>
        </MonetaryAmount>
      </xsl:if>

    </MedicareInpatientAdjustment>
  </xsl:template>

  <!-- Provider Adjustment Segment -->
  <xsl:template match="PLB">
    <ProviderAdjustment>
      <xsl:attribute name="ReferenceId">
        <xsl:value-of select="PLB01"/>
      </xsl:attribute>
      <xsl:attribute name="FiscalPeriodEndDate">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="PLB02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB03/PLB0301"/>
        <xsl:with-param name="Identifier" select="PLB03/PLB0302"/>
        <xsl:with-param name="Amount" select="PLB04"/>
      </xsl:call-template>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB05/PLB0501"/>
        <xsl:with-param name="Identifier" select="PLB05/PLB0502"/>
        <xsl:with-param name="Amount" select="PLB06"/>
      </xsl:call-template>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB07/PLB0701"/>
        <xsl:with-param name="Identifier" select="PLB07/PLB0702"/>
        <xsl:with-param name="Amount" select="PLB08"/>
      </xsl:call-template>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB09/PLB0901"/>
        <xsl:with-param name="Identifier" select="PLB09/PLB0902"/>
        <xsl:with-param name="Amount" select="PLB10"/>
      </xsl:call-template>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB11/PLB1101"/>
        <xsl:with-param name="Identifier" select="PLB11/PLB1102"/>
        <xsl:with-param name="Amount" select="PLB12"/>
      </xsl:call-template>
      <xsl:call-template name="ProviderAdjustmentLine">
        <xsl:with-param name="ReasonCode" select="PLB13/PLB1301"/>
        <xsl:with-param name="Identifier" select="PLB13/PLB1302"/>
        <xsl:with-param name="Amount" select="PLB14"/>
      </xsl:call-template>
    </ProviderAdjustment>
  </xsl:template>

  <!-- Service Payment Information Loop -->
  <xsl:template match="Loop[@LoopId='2110']">
    <ServicePayment>
      <xsl:attribute name="ChargeAmount">
        <xsl:value-of select="SVC/SVC02"/>
      </xsl:attribute>
      <xsl:attribute name="PaymentAmount">
        <xsl:value-of select="SVC/SVC03"/>
      </xsl:attribute>
      <xsl:attribute name="Quantity">
        <xsl:value-of select="SVC/SVC05"/>
      </xsl:attribute>
      <Qualifer>
        <xsl:attribute name="Code">
          <xsl:value-of select="SVC/SVC01/SVC0101"/>
        </xsl:attribute>
      </Qualifer>
      <xsl:apply-templates select="DTM"/>
      <xsl:apply-templates select="CAS"/>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="AMT"/>
      <xsl:apply-templates select="QTY"/>
      <xsl:apply-templates select="LQ"/>
    </ServicePayment>
  </xsl:template>

  <!-- Claim Payment Information Loop -->
  <xsl:template match="Loop[@LoopId='2100']">
    <ClaimPayment>
      <xsl:apply-templates select="CLP"/>
      <xsl:apply-templates select="CAS"/>
      <Patient>
        <xsl:apply-templates select="NM1[NM101='QC']"/>
      </Patient>
      <xsl:for-each select="NM1[NM101!='QC']">
        <OtherParty>
          <xsl:apply-templates select="."/>
        </OtherParty>
      </xsl:for-each>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="PER"/>
      <xsl:apply-templates select="MIA"/>
      <xsl:apply-templates select="DTM"/>
      <xsl:apply-templates select="AMT"/>
      <xsl:apply-templates select="QTY"/>
      <xsl:apply-templates select="Loop[@LoopId='2110']"/>
    </ClaimPayment>
  </xsl:template>

  <!-- Payer Identification -->
  <xsl:template match="Loop[@LoopId='1000A']">
    <Payer>
      <xsl:apply-templates select="N1"/>
      <xsl:apply-templates select="REF"/>
      <Address>
        <xsl:apply-templates select="N3"/>
      </Address>
      <xsl:apply-templates select="PER"/>
    </Payer>
  </xsl:template>

  <!-- Payee Identification -->
  <xsl:template match="Loop[@LoopId='1000B']">
    <Payee>
      <xsl:apply-templates select="N1"/>
      <xsl:apply-templates select="REF"/>
      <Address>
        <xsl:apply-templates select="N3"/>
      </Address>
    </Payee>
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
      <xsl:apply-templates select="BPR"/>
      <xsl:apply-templates select="TRN"/>
      <xsl:apply-templates select="CUR"/>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="Loop[@LoopId='1000A']"/>
      <xsl:apply-templates select="Loop[@LoopId='1000B']"/>
      <xsl:apply-templates select="Loop/Loop[@LoopId='2100']"/>
      <xsl:apply-templates select="PLB"/>
    </Transaction>
  </xsl:template>

  <xsl:template match="/">
    <ArrayOfTransaction>
      <xsl:apply-templates select="/Interchange/FunctionGroup/Transaction"/>
    </ArrayOfTransaction>
  </xsl:template>

</xsl:stylesheet>
