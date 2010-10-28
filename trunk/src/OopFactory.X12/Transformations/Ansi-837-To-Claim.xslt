<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:import href="Ansi-Common.xslt"/>
  <xsl:output method="xml" indent="yes"/>
   
  <!-- Note/Special Instruction Segment -->
  <xsl:template match="NTE">
    <Note>
      <Reference>
        <xsl:attribute name="Code">
          <xsl:value-of select="NTE01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="NTE01='ADD'">Additional Information</xsl:when>
          <xsl:when test="NTE01='CER'">Certification Narrative</xsl:when>
          <xsl:when test="NTE01='DCP'">Goals, Rehabilitation Potential, or Discharge Plans</xsl:when>
          <xsl:when test="NTE01='DGN'">Diagnosis Description</xsl:when>
          <xsl:when test="NTE01='PMT'">Payment</xsl:when>
          <xsl:when test="NTE01='TPO'">Third Party Organization Notes</xsl:when>
        </xsl:choose>
      </Reference>
      <Description>
        <xsl:value-of select="NTE02"/>
      </Description>
    </Note>
  </xsl:template>

  <!-- Healthcare Pricing Segment -->
  <xsl:template match="HCP">
    <Pricing>
      <xsl:attribute name="AllowedAmount">
        <xsl:value-of select="HCP02"/>
      </xsl:attribute>
      <xsl:attribute name="OrganizationId">
        <xsl:value-of select="HCP04"/>
      </xsl:attribute>
      <Methodology>
        <xsl:attribute name="Code">
          <xsl:value-of select="HCP01"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="HCP01='00'">Zero Pricing (Not Covered Under Contract)</xsl:when>
          <xsl:when test="HCP01='01'">Priced as Billed at 100%</xsl:when>
          <xsl:when test="HCP01='02'">Priced at the Standard Fee Schedule</xsl:when>
          <xsl:when test="HCP01='03'">Priced at a Contractual Percentage</xsl:when>
          <xsl:when test="HCP01='04'">Bundled Pricing</xsl:when>
          <xsl:when test="HCP01='05'">Peer Review Pricing</xsl:when>
          <xsl:when test="HCP01='07'">Flat Rate Pricing</xsl:when>
          <xsl:when test="HCP01='08'">Combination Pricing</xsl:when>
          <xsl:when test="HCP01='09'">Maternity Pricing</xsl:when>
          <xsl:when test="HCP01='10'">Other Pricing</xsl:when>
          <xsl:when test="HCP01='11'">Lower of Cost</xsl:when>
          <xsl:when test="HCP01='12'">Ratio of Cost</xsl:when>
          <xsl:when test="HCP01='13'">Cost Reimbursed</xsl:when>
          <xsl:when test="HCP01='14'">Adjustment Pricing</xsl:when>
        </xsl:choose>
      </Methodology>
      <xsl:if test="string-length(HCP03)>0">
        <SavingsAmount>
          <xsl:value-of select="HCP03"/>
        </SavingsAmount>
      </xsl:if>
      <xsl:if test="string-length(HCP04)>0">

      </xsl:if>
    </Pricing>
  </xsl:template>

  <xsl:template name="HealthcareInformation">
    <xsl:param name="Qualifier"/>
    <xsl:param name="Code"/>
    <xsl:param name="DateFormat"/>
    <xsl:param name="Date"/>
    <xsl:param name="Amount"/>
    <xsl:param name="Quantity"/>
    <xsl:param name="Version"/>
    <xsl:param name="POA"/>
    <xsl:choose>
      <xsl:when test="$Qualifier='BH'">
        <Occurrence>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:attribute name="Date">
            <xsl:call-template name="FormatD8Date">
              <xsl:with-param name="Date" select="$Date"/>
            </xsl:call-template>
          </xsl:attribute>
          <xsl:if test="$verbose=1">
            <xsl:comment>
              <xsl:choose>
                <xsl:when test="$Code='01'">AUTO ACCIDENT</xsl:when>
                <xsl:when test="$Code='02'">NO FAULT INSURANCE INVOLVED-INCLUDING AUTO ACCIDENT/OTHER</xsl:when>
                <xsl:when test="$Code='03'">ACCIDENT/TORT LIABILITY</xsl:when>
                <xsl:when test="$Code='04'">ACCIDENT-EMPLOYMENT-RELATED</xsl:when>
                <xsl:when test="$Code='05'">ACCIDENT/NO MEDICAL OR LIABILITY COVERAGE</xsl:when>
                <xsl:when test="$Code='06'">CRIME VICTIM</xsl:when>
                <xsl:when test="$Code='09'">START OF INFERTILITY TREATMENT CYCLE</xsl:when>
                <xsl:when test="$Code='10'">LAST MENSTRUAL PERIOD</xsl:when>
                <xsl:when test="$Code='11'">ONSET OF SYMPTOMS/ILLNESS</xsl:when>
                <xsl:when test="$Code='12'">DATE OF ONSET FOR A CHRONICALLY DEPENDENT INDIVIDUAL (CDI) (HHA ONLY)</xsl:when>
                <xsl:when test="$Code='16'">DATE OF LAST THERAPY</xsl:when>
                <xsl:when test="$Code='17'">DATE OUTPATIENT OCCUPATIONAL THERAPY PLAN ESTABLISHED OR LAST REVIEWED</xsl:when>
                <xsl:when test="$Code='18'">DATE OF RETIREMENT OF PATIENT/BENEFICIARY</xsl:when>
                <xsl:when test="$Code='19'">DATE OF RETIREMENT OF SPOUSE</xsl:when>
                <xsl:when test="$Code='20'">DATE GUARANTEE OF PAYMENT BEGAN</xsl:when>
                <xsl:when test="$Code='21'">DATE UR NOTICE RECEIVED</xsl:when>
                <xsl:when test="$Code='22'">DATE ACTIVE CARE ENDED</xsl:when>
                <xsl:when test="$Code='23'">PAYER USE ONLY</xsl:when>
                <xsl:when test="$Code='24'">DATE INSURANCE DENIED</xsl:when>
                <xsl:when test="$Code='25'">DATE BENEFITS TERMINATED BY PRIMARY PAYER</xsl:when>
                <xsl:when test="$Code='26'">DATE SNF BED BECAME AVAILABLE</xsl:when>
                <xsl:when test="$Code='27'">DATE OF HOSPICE CERTIFICATION OR RECERTIFICATION</xsl:when>
                <xsl:when test="$Code='28'">DATE COMPREHENSIVE OUTPATIENT REHABILITATION PLAN ESTABLISHED OR LAST REVIEWED</xsl:when>
                <xsl:when test="$Code='29'">DATE OUTPATIENT PHYSICAL THERAPY PLAN ESTABLISHED OR LAST REVIEWED</xsl:when>
                <xsl:when test="$Code='30'">DATE OUTPATIENT SPEECH PATHOLOGY PLAN ESTABLISHED OR LAST REVIEWED</xsl:when>
                <xsl:when test="$Code='31'">DATE BENEFICIARY NOTIFIED OF INTENT TO BILL (ACCOMMODATIONS)</xsl:when>
                <xsl:when test="$Code='32'">DATE BENEFICIARY NOTIFIED OF INTENT TO BILL (PROCEDURES OR TREATMENTS)</xsl:when>
                <xsl:when test="$Code='33'">FIRST DAY OF THE COORDINATION PERIOD FOR ESRD BENEFICIARIES COVERED BY EGHP</xsl:when>
                <xsl:when test="$Code='34'">DATE OF ELECTION OF EXTENDED CARE SERVICES (RNHCI ONLY)</xsl:when>
                <xsl:when test="$Code='35'">DATE TREATMENT STARTED FOR PHYSICAL THERAPY</xsl:when>
                <xsl:when test="$Code='36'">DATE OF INPATIENT HOSPITAL DISCHARGE FOR COVERED TRANSPLANT PATIENT</xsl:when>
                <xsl:when test="$Code='37'">DATE OF INPATIENT HOSPITAL DISCHARGE FOR NONCOVERED TRANSPLANT PATIENT</xsl:when>
                <xsl:when test="$Code='38'">DATE TREATMENT STARTED FOR HOME IV THERAPY</xsl:when>
                <xsl:when test="$Code='39'">DATE DISCHARGED ON A CONTINUOUS COURSE OF IV THERAPY</xsl:when>
                <xsl:when test="$Code='40'">SCHEDULED DATE OF ADMISSION</xsl:when>
                <xsl:when test="$Code='41'">DATE OF FIRST TEST FOR PREADMISSION TESTING</xsl:when>
                <xsl:when test="$Code='42'">DATE OF DISCHARGE</xsl:when>
                <xsl:when test="$Code='43'">SCHEDULED DATE OF CANCELLED SURGERY</xsl:when>
                <xsl:when test="$Code='44'">DATE TREATMENT STARTED FOR OCCUPATIONAL THERAPY</xsl:when>
                <xsl:when test="$Code='45'">DATE TREATMENT STARTED FOR SPEECH THERAPY</xsl:when>
                <xsl:when test="$Code='46'">DATE TREATMENT STARTED FOR CARDIAC REHABILITATION</xsl:when>
                <xsl:when test="$Code='47'">DATE COST OUTLIER STATUS BEGINS</xsl:when>
                <xsl:when test="$Code='50'">ASSESSMENT DATE (EFFECTIVE 01/01/2011)</xsl:when>
                <xsl:when test="$Code='51'">DATE OF LAST KT/V READING (EFFECTIVE 07/01/2010)</xsl:when>
                <xsl:when test="$Code='A1'">BIRTH DATE-INSURED A</xsl:when>
                <xsl:when test="$Code='A2'">EFFECTIVE DATE-INSURED A POLICY</xsl:when>
                <xsl:when test="$Code='A3'">BENEFITS EXHAUSTED</xsl:when>
                <xsl:when test="$Code='A4'">SPLIT BILL DATE</xsl:when>
                <xsl:when test="$Code='B1'">BIRTH DATE-INSURED B</xsl:when>
                <xsl:when test="$Code='B2'">EFFECTIVE DATE-INSURED B POLICY</xsl:when>
                <xsl:when test="$Code='B3'">BENEFITS EXHAUSTED</xsl:when>
                <xsl:when test="$Code='C1'">BIRTH DATE-INSURED C</xsl:when>
                <xsl:when test="$Code='C2'">EFFECTIVE DATE-INSURED C POLICY</xsl:when>
                <xsl:when test="$Code='C3'">BENEFITS EXHAUSTED</xsl:when>
                <xsl:when test="$Code='70'">QUALIFYING STAY DATES (SNF ONLY)</xsl:when>
                <xsl:when test="$Code='71'">PRIOR STAY DATES</xsl:when>
                <xsl:when test="$Code='72'">FIRST/LAST VISIT</xsl:when>
                <xsl:when test="$Code='73'">BENEFIT ELIGIBILITY PERIOD</xsl:when>
                <xsl:when test="$Code='74'">NONCOVERED LEVEL OF CARE/LEAVE OF ABSENCE DATES</xsl:when>
                <xsl:when test="$Code='75'">SNF LEVEL OF CARE</xsl:when>
                <xsl:when test="$Code='76'">PATIENT LIABILITY</xsl:when>
                <xsl:when test="$Code='77'">PROVIDER LIABILITY PERIOD</xsl:when>
                <xsl:when test="$Code='78'">SNF PRIOR STAY DATES</xsl:when>
                <xsl:when test="$Code='79'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='80'">PRIOR SAME-SNF STAY DATES FOR PAYMENT BAN PURPOSES</xsl:when>
                <xsl:when test="$Code='M0'">QIO/UR APPROVED STAY DATES</xsl:when>
                <xsl:when test="$Code='M1'">PROVIDER LIABILITY-NO UTILIZATION</xsl:when>
                <xsl:when test="$Code='M2'">INPATIENT RESPITE DATES</xsl:when>
                <xsl:when test="$Code='M3'">ICF LEVEL OF CARE</xsl:when>
                <xsl:when test="$Code='M4'">RESIDENTIAL LEVEL OF CARE</xsl:when>
                <xsl:when test="$Code='07'">RESERVED FOR NATIONAL ASSIGNMENT</xsl:when>
                <xsl:when test="$Code='08'">RESERVED FOR NATIONAL ASSIGNMENT</xsl:when>
              </xsl:choose>
            </xsl:comment>
          </xsl:if>
        </Occurrence>
      </xsl:when>
      <xsl:when test="$Qualifier='BI'">
        <OccurrenceSpan>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:attribute name="FromDate">
            <xsl:call-template name="FormatD8Date">
              <xsl:with-param name="Date" select="$Date"/>
            </xsl:call-template>
          </xsl:attribute>
          <xsl:attribute name="ThroughDate">
            <xsl:call-template name="FormatD8Date">
              <xsl:with-param name="Date" select="substring($Date,10,8)"/>
            </xsl:call-template>
          </xsl:attribute>
        </OccurrenceSpan>
      </xsl:when>
      <xsl:when test="$Qualifier='BE'">
        <Value>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:if test="string-length($Amount)>0">
            <xsl:attribute name="Amount">
              <xsl:value-of select="$Amount"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="$verbose=1">
            <xsl:comment>
              <xsl:choose>
                <xsl:when test="$Code='01'">MOST COMMON SEMIPRIVATE ROOM RATE</xsl:when>
                <xsl:when test="$Code='02'">HOSPITAL HAS NO SEMIPRIVATE ROOMS</xsl:when>
                <xsl:when test="$Code='04'">INPATIENT PROFESSIONAL COMPONENT CHARGES WHICH ARE COMBINED BILLED</xsl:when>
                <xsl:when test="$Code='05'">PROFESSIONAL COMPONENT INCLUDED IN CHARGES AND ALSO BILLED SEPARATELY TO CARRIER</xsl:when>
                <xsl:when test="$Code='06'">MEDICARE BLOOD DEDUCTIBLE</xsl:when>
                <xsl:when test="$Code='08'">MEDICARE LIFETIME RESERVE AMOUNT IN THE FIRST CALENDAR YEAR</xsl:when>
                <xsl:when test="$Code='09'">MEDICARE COINSURANCE AMOUNT IN THE FIRST CALENDAR YEAR</xsl:when>
                <xsl:when test="$Code='10'">MEDICARE LIFETIME RESERVE AMOUNT IN THE SECOND CALENDAR YEAR</xsl:when>
                <xsl:when test="$Code='11'">MEDICARE COINSURANCE AMOUNT FOR SECOND CALENDAR YEAR</xsl:when>
                <xsl:when test="$Code='12'">WORKING AGED BENEFICIARY/SPOUSE WITH EGHP</xsl:when>
                <xsl:when test="$Code='13'">ESRD BENEFICIARY IN A MEDICARE COORDINATION PERIOD WITH AN EGHP</xsl:when>
                <xsl:when test="$Code='14'">NO-FAULT, INCLUDING AUTO/OTHER</xsl:when>
                <xsl:when test="$Code='15'">WORKERS COMPENSATION</xsl:when>
                <xsl:when test="$Code='16'">PUBLIC HEALTH SERVICE (PHS) OR OTHER FEDERAL AGENCY</xsl:when>
                <xsl:when test="$Code='21'">CATASTROPHIC</xsl:when>
                <xsl:when test="$Code='22'">SURPLUS</xsl:when>
                <xsl:when test="$Code='23'">RECURRING MONTHLY INCOME</xsl:when>
                <xsl:when test="$Code='24'">MEDICAID RATE CODE</xsl:when>
                <xsl:when test="$Code='25'">OFFSET TO PATIENT PAYMENT AMOUNT - PRESCRIPTION DRUGS</xsl:when>
                <xsl:when test="$Code='26'">OFFSET TO PATIENT PAYMENT AMOUNT - HEARING AND EAR SERVICES</xsl:when>
                <xsl:when test="$Code='27'">OFFSET TO PATIENT PAYMENT AMOUNT - VISION AND EYE SERVICES</xsl:when>
                <xsl:when test="$Code='28'">OFFSET TO PATIENT PAYMENT AMOUNT - DENTAL SERVICES</xsl:when>
                <xsl:when test="$Code='29'">OFFSET TO PATIENT PAYMENT AMOUNT - CHIROPRACTIC SERVICES</xsl:when>
                <xsl:when test="$Code='30'">PREADMISSION TESTING</xsl:when>
                <xsl:when test="$Code='31'">PATIENT LIABILITY AMOUNT</xsl:when>
                <xsl:when test="$Code='32'">MULTIPLE PATIENT AMBULANCE TRANSPORT</xsl:when>
                <xsl:when test="$Code='33'">OFFSET TO PATIENT PAYMENT AMOUNT - PODIATRIC SERVICES</xsl:when>
                <xsl:when test="$Code='34'">OFFSET TO PATIENT PAYMENT AMOUNT - OTHER MEDICAL SERVICES</xsl:when>
                <xsl:when test="$Code='35'">OFFSET TO PATIENT PAYMENT AMOUNT - HEALTH INSURANCE PREMIUMS</xsl:when>
                <xsl:when test="$Code='37'">PINTS OF BLOOD FURNISHED</xsl:when>
                <xsl:when test="$Code='38'">BLOOD DEDUCTIBLE PINTS</xsl:when>
                <xsl:when test="$Code='39'">PINTS OF BLOOD REPLACED</xsl:when>
                <xsl:when test="$Code='40'">NEW COVERAGE NOT IMPLEMENTED BY HMO (FOR INPATIENT CLAIMS ONLY)</xsl:when>
                <xsl:when test="$Code='41'">BLACK LUNG</xsl:when>
                <xsl:when test="$Code='42'">VA</xsl:when>
                <xsl:when test="$Code='43'">DISABLED BENEFICIARY UNDER AGE 65 WITH LGHP</xsl:when>
                <xsl:when test="$Code='44'">AMOUNT PROVIDER AGREED TO ACCEPT FROM THE PRIMARY INSURER WHEN THIS AMOUNT IS LESS THAN TOTAL CHARGES, BUT HIGHER THAN THE PAYMENTS RECEIVED</xsl:when>
                <xsl:when test="$Code='45'">ACCIDENT HOUR</xsl:when>
                <xsl:when test="$Code='46'">NUMBER OF GRACE DAYS</xsl:when>
                <xsl:when test="$Code='47'">ANY LIABILITY INSURANCE</xsl:when>
                <xsl:when test="$Code='48'">HEMOGLOBIN READING</xsl:when>
                <xsl:when test="$Code='49'">HEMATOCRIT READING</xsl:when>
                <xsl:when test="$Code='50'">PHYSICAL THERAPY VISITS</xsl:when>
                <xsl:when test="$Code='51'">OCCUPATIONAL THERAPY VISITS</xsl:when>
                <xsl:when test="$Code='52'">SPEECH THERAPY VISITS</xsl:when>
                <xsl:when test="$Code='53'">CARDIAC REHABILITATION VISITS</xsl:when>
                <xsl:when test="$Code='54'">NEWBORN BIRTH WEIGHT IN GRAMS</xsl:when>
                <xsl:when test="$Code='55'">ELIGIBILITY THRESHOLD FOR CHARITY CARE</xsl:when>
                <xsl:when test="$Code='56'">SKILLED NURSE-HOME VISIT HOURS (HHA ONLY)</xsl:when>
                <xsl:when test="$Code='57'">HOME HEALTH AIDE-HOME VISIT HOURS (HHA ONLY)</xsl:when>
                <xsl:when test="$Code='58'">ARTERIAL BLOOD GAS (PO2/PA2)</xsl:when>
                <xsl:when test="$Code='59'">OXYGEN SATURATION (O2 SAT/OXIMETRY)</xsl:when>
                <xsl:when test="$Code='60'">HHA BRANCH MSA</xsl:when>
                <xsl:when test="$Code='61'">PLACE OF RESIDENCE WHERE SERVICE IS FURNISHED (HHA AND HOSPICE)</xsl:when>
                <xsl:when test="$Code='62'">HOME HEALTH AGENCY VISITS-PART A</xsl:when>
                <xsl:when test="$Code='63'">HOME HEALTH AGENCY VISITS-PART B</xsl:when>
                <xsl:when test="$Code='64'">HOME HEALTH AGENCY REIMBURSEMENT-PART A</xsl:when>
                <xsl:when test="$Code='65'">HOME HEALTH AGENCY REIMBURSEMENT-PART B</xsl:when>
                <xsl:when test="$Code='66'">MEDICAID SPENDDOWN AMOUNT</xsl:when>
                <xsl:when test="$Code='67'">PERITONEAL DIALYSIS</xsl:when>
                <xsl:when test="$Code='68'">EPO-DRUG</xsl:when>
                <xsl:when test="$Code='69'">STATE CHARITY CARE PERCENT</xsl:when>
                <xsl:when test="$Code='71'">FUNDING OF ESRD NETWORKS</xsl:when>
                <xsl:when test="$Code='72'">FLAT RATE SURGERY CHARGE</xsl:when>
                <xsl:when test="$Code='73'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='74'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='76'">PROVIDERS INTERIM RATE</xsl:when>
                <xsl:when test="$Code='77'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='80'">COVERED DAYS</xsl:when>
                <xsl:when test="$Code='81'">NONCOVERED DAYS</xsl:when>
                <xsl:when test="$Code='82'">COINSURANCE DAYS</xsl:when>
                <xsl:when test="$Code='83'">LIFETIME RESERVE DAYS</xsl:when>
                <xsl:when test="$Code='A0'">SPECIAL ZIP CODE REPORTING</xsl:when>
                <xsl:when test="$Code='A1'">DEDUCTIBLE PAYER A</xsl:when>
                <xsl:when test="$Code='A2'">COINSURANCE PAYER A</xsl:when>
                <xsl:when test="$Code='A3'">ESTIMATED RESPONSIBILITY PAYER A</xsl:when>
                <xsl:when test="$Code='A4'">COVERED SELF-ADMINISTRABLE DRUGS-EMERGENCY</xsl:when>
                <xsl:when test="$Code='A5'">COVERED SELF-ADMINISTRABLE DRUGS-NOT SELF-ADMINISTRABLE IN FORM AND SITUATION FURNISHED TO PATIENT</xsl:when>
                <xsl:when test="$Code='A6'">COVERED SELF-ADMINISTRABLE DRUGS-DIAGNOSTIC STUDY AND OTHER</xsl:when>
                <xsl:when test="$Code='A7'">COPAYMENT PAYER A</xsl:when>
                <xsl:when test="$Code='A8'">PATIENT WEIGHT</xsl:when>
                <xsl:when test="$Code='A9'">PATIENT HEIGHT</xsl:when>
                <xsl:when test="$Code='AA'">REGULATORY SURCHARGES, ASSESSMENTS, ALLOWANCES OR HEALTH CARE RELATED TAXES PAYER A</xsl:when>
                <xsl:when test="$Code='AB'">OTHER ASSESSMENTS OR ALLOWANCES (E.G., MEDICAL EDUCATION) PAYER A</xsl:when>
                <xsl:when test="$Code='B1'">DEDUCTIBLE PAYER B</xsl:when>
                <xsl:when test="$Code='B2'">COINSURANCE PAYER B</xsl:when>
                <xsl:when test="$Code='B3'">ESTIMATED RESPONSIBILITY PAYER B</xsl:when>
                <xsl:when test="$Code='B7'">COPAYMENT PAYER B</xsl:when>
                <xsl:when test="$Code='BA'">REGULATORY SURCHARGES, ASSESSMENTS, ALLOWANCES OR HEALTH CARE RELATED TAXES PAYER B</xsl:when>
                <xsl:when test="$Code='BB'">OTHER ASSESSMENTS OR ALLOWANCES (E.G., MEDICAL EDUCATION) PAYER B</xsl:when>
                <xsl:when test="$Code='C1'">DEDUCTIBLE PAYER C</xsl:when>
                <xsl:when test="$Code='C2'">COINSURANCE PAYER C</xsl:when>
                <xsl:when test="$Code='C3'">ESTIMATED RESPONSIBILITY PAYER C</xsl:when>
                <xsl:when test="$Code='C7'">COPAYMENT PAYER C</xsl:when>
                <xsl:when test="$Code='CA'">REGULATORY SURCHARGES, ASSESSMENTS, ALLOWANCES OR HEALTH CARE RELATED TAXES PAYER C</xsl:when>
                <xsl:when test="$Code='CB'">OTHER ASSESSMENTS OR ALLOWANCES (E.G., MEDICAL EDUCATION) PAYER C</xsl:when>
                <xsl:when test="$Code='D3'">ESTIMATED RESPONSIBILITY PATIENT</xsl:when>
                <xsl:when test="$Code='D4'">CLINICAL TRIAL NUMBER ASSIGNED BY NLM/NIH</xsl:when>
                <xsl:when test="$Code='D5'">LAST KT/V READING (EFFECTIVE 7/1/2010)</xsl:when>
                <xsl:when test="$Code='FC'">PATIENT PAID AMOUNT</xsl:when>
                <xsl:when test="$Code='FD'">CREDIT RECEIVED FROM THE MANUFACTURER FOR A REPLACED MEDICAL DEVICE</xsl:when>
                <xsl:when test="$Code='G8'">FACILITY WHERE INPATIENT HOSPICE SERVICE IS DELIVERED</xsl:when>
                <xsl:when test="$Code='Y1'">PART A DEMONSTRATION PAYMENT</xsl:when>
                <xsl:when test="$Code='Y2'">PART B DEMONSTRATION PAYMENT</xsl:when>
                <xsl:when test="$Code='Y3'">PART B COINSURANCE</xsl:when>
                <xsl:when test="$Code='Y4'">CONVENTIONAL PROVIDER PAYMENT AMOUNT FOR NON-DEMONSTRATION CLAIMS</xsl:when>
              </xsl:choose>
            </xsl:comment>
          </xsl:if>
        </Value>
      </xsl:when>
      <xsl:when test="$Qualifier='BG'">
        <Condition>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:if test="$verbose=1">
            <xsl:comment>
              <xsl:choose>
                <xsl:when test="$Code='01'">MILITARY SERVICE RELATED</xsl:when>
                <xsl:when test="$Code='02'">CONDITION IS EMPLOYMENT RELATED</xsl:when>
                <xsl:when test="$Code='03'">PATIENT COVERED BY INSURANCE NOT REFLECTED HERE</xsl:when>
                <xsl:when test="$Code='04'">INFORMATION ONLY BILL</xsl:when>
                <xsl:when test="$Code='05'">LIEN HAS BEEN FILED</xsl:when>
                <xsl:when test="$Code='06'">ESRD PATIENT IN FIRST 30 MONTHS OF ENTITLEMENT COVERED BY EMPLOYER GROUP HEALTH INSURANCE</xsl:when>
                <xsl:when test="$Code='07'">TREATMENT OF NONTERMINAL CONDITION FOR HOSPICE PATIENT</xsl:when>
                <xsl:when test="$Code='08'">BENEFICIARY WOULD NOT PROVIDE INFORMATION CONCERNING OTHER INSURANCE COVERAGE</xsl:when>
                <xsl:when test="$Code='09'">NEITHER PATIENT NOR SPOUSE IS EMPLOYED</xsl:when>
                <xsl:when test="$Code='10'">PATIENT AND/OR SPOUSE IS EMPLOYED BUT NO EGHP EXISTS</xsl:when>
                <xsl:when test="$Code='11'">DISABLED BENEFICIARY, BUT NO LARGE GROUP HEALTH PLAN (LGHP)</xsl:when>
                <xsl:when test="$Code='15'">CLEAN CLAIM DELAYED IN CMS' PROCESSING SYSTEM</xsl:when>
                <xsl:when test="$Code='16'">SNF TRANSITION EXEMPTION</xsl:when>
                <xsl:when test="$Code='17'">PATIENT IS HOMELESS</xsl:when>
                <xsl:when test="$Code='18'">MAIDEN NAME RETAINED</xsl:when>
                <xsl:when test="$Code='19'">CHILD RETAINS MOTHER'S NAME</xsl:when>
                <xsl:when test="$Code='20'">BENEFICIARY REQUESTED BILLING</xsl:when>
                <xsl:when test="$Code='21'">BILLING FOR DENIAL NOTICE</xsl:when>
                <xsl:when test="$Code='22'">PATIENT ON MULTIPLE DRUG REGIMEN</xsl:when>
                <xsl:when test="$Code='23'">HOME CARE GIVER AVAILABLE</xsl:when>
                <xsl:when test="$Code='24'">HOME IV PATIENT ALSO RECEIVING HHA SERVICES</xsl:when>
                <xsl:when test="$Code='25'">PATIENT IS A NON-US RESIDENT</xsl:when>
                <xsl:when test="$Code='26'">VETERANS AFFAIRS-ELIGIBLE PATIENT CHOOSES TO RECEIVE SERVICES IN MEDICARE-CERTIFIED FACILITY</xsl:when>
                <xsl:when test="$Code='27'">PATIENT REFERRED TO A SOLE COMMUNITY HOSPITAL FOR A DIAGNOSTIC LABORATORY TEST</xsl:when>
                <xsl:when test="$Code='28'">PATIENT AND/OR SPOUSE'S EGHP IS SECONDARY TO MEDICARE</xsl:when>
                <xsl:when test="$Code='29'">DISABLED BENEFICIARY AND/OR FAMILY MEMBER'S LGHP IS SECONDARY TO MEDICARE</xsl:when>
                <xsl:when test="$Code='30'">QUALIFYING CLINICAL TRIALS</xsl:when>
                <xsl:when test="$Code='31'">PATIENT IS STUDENT (FULL-TIME DAY)</xsl:when>
                <xsl:when test="$Code='32'">PATIENT IS STUDENT (COOPERATIVE/WORK STUDY PROGRAM)</xsl:when>
                <xsl:when test="$Code='33'">PATIENT IS A STUDENT (FULL-TIME NIGHT)</xsl:when>
                <xsl:when test="$Code='34'">PATIENT IS STUDENT (PART-TIME)</xsl:when>
                <xsl:when test="$Code='36'">GENERAL CARE PATIENT IN A SPECIAL UNIT</xsl:when>
                <xsl:when test="$Code='37'">WARD ACCOMMODATION AT PATIENT'S REQUEST</xsl:when>
                <xsl:when test="$Code='38'">SEMIPRIVATE ROOM NOT AVAILABLE</xsl:when>
                <xsl:when test="$Code='39'">PRIVATE ROOM MEDICALLY NECESSARY</xsl:when>
                <xsl:when test="$Code='40'">SAME-DAY TRANSFER</xsl:when>
                <xsl:when test="$Code='41'">PARTIAL HOSPITALIZATION</xsl:when>
                <xsl:when test="$Code='42'">CONTINUING CARE NOT RELATED TO INPATIENT HOSPITALIZATION</xsl:when>
                <xsl:when test="$Code='43'">CONTINUING CARE NOT PROVIDED WITHIN PRESCRIBED POSTDISCHARGE WINDOW</xsl:when>
                <xsl:when test="$Code='44'">INPATIENT ADMISSION CHANGED TO OUTPATIENT</xsl:when>
                <xsl:when test="$Code='45'">AMBIGUOUS GENDER CATEGORY</xsl:when>
                <xsl:when test="$Code='46'">NONAVAILABILITY STATEMENT ON FILE</xsl:when>
                <xsl:when test="$Code='48'">PSYCHIATRIC RESIDENTIAL TREATMENT CENTERS (RTCS) FOR CHILDREN AND ADOLESCENTS</xsl:when>
                <xsl:when test="$Code='49'">PRODUCT REPLACEMENT WITHIN PRODUCT LIFECYCLE</xsl:when>
                <xsl:when test="$Code='50'">PRODUCT REPLACEMENT FOR KNOWN RECALL OF A PRODUCT</xsl:when>
                <xsl:when test="$Code='55'">SNF BED NOT AVAILABLE</xsl:when>
                <xsl:when test="$Code='56'">MEDICAL APPROPRIATENESS</xsl:when>
                <xsl:when test="$Code='57'">SNF READMISSION</xsl:when>
                <xsl:when test="$Code='58'">TERMINATED MEDICARE ADVANTAGE ENROLLEE</xsl:when>
                <xsl:when test="$Code='59'">NON-PRIMARY ESRD FACILITY</xsl:when>
                <xsl:when test="$Code='60'">DAY OUTLIER</xsl:when>
                <xsl:when test="$Code='61'">COST OUTLIER</xsl:when>
                <xsl:when test="$Code='62'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='63'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='64'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='65'">PAYER CODE</xsl:when>
                <xsl:when test="$Code='63'">INCARCERATED BENEFICIARIES</xsl:when>
                <xsl:when test="$Code='64'">PAYER ONLY CODE</xsl:when>
                <xsl:when test="$Code='65'">PAYER ONLY CODE</xsl:when>
                <xsl:when test="$Code='66'">PROVIDER DOES NOT WISH COST OUTLIER PAYMENT</xsl:when>
                <xsl:when test="$Code='67'">BENEFICIARY ELECTS NOT TO USE LIFETIME RESERVE (LTR) DAYS</xsl:when>
                <xsl:when test="$Code='68'">BENEFICIARY ELECTS TO USE LTR DAYS</xsl:when>
                <xsl:when test="$Code='69'">IME/DGME/N&amp;AH PAYMENT ONLY BILL</xsl:when>
                <xsl:when test="$Code='70'">SELF ADMINISTERED ANEMIA MANAGEMENT DRUG</xsl:when>
                <xsl:when test="$Code='71'">FULL CARE IN UNIT</xsl:when>
                <xsl:when test="$Code='72'">SELF-CARE IN UNIT</xsl:when>
                <xsl:when test="$Code='73'">SELF-CARE TRAINING</xsl:when>
                <xsl:when test="$Code='74'">HOME</xsl:when>
                <xsl:when test="$Code='75'">HOME-100 PERCENT REIMBURSEMENT</xsl:when>
                <xsl:when test="$Code='76'">BACKUP IN-FACILITY DIALYSIS</xsl:when>
                <xsl:when test="$Code='77'">PROVIDER ACCEPTS OR IS OBLIGATED/REQUIRED DUE TO A CONTRACTUAL ARRANGEMENT OR LAW TO ACCEPT PAYMENT </xsl:when>
                <xsl:when test="$Code='78'">NEW COVERAGE NOT IMPLEMENTED BY MANAGED CARE PLAN</xsl:when>
                <xsl:when test="$Code='79'">CORF SERVICES PROVIDED OFF-SITE</xsl:when>
                <xsl:when test="$Code='80'">HOME DIALYSIS-NURSING FACILITY</xsl:when>
                <xsl:when test="$Code='A0'">TRICARE EXTERNAL PARTNERSHIP PROGRAM</xsl:when>
                <xsl:when test="$Code='A1'">EPSDT/CHAP</xsl:when>
                <xsl:when test="$Code='A2'">PHYSICALLY HANDICAPPED CHILDREN'S PROGRAM</xsl:when>
                <xsl:when test="$Code='A3'">SPECIAL FEDERAL FUNDING</xsl:when>
                <xsl:when test="$Code='A4'">FAMILY PLANNING</xsl:when>
                <xsl:when test="$Code='A5'">DISABILITY</xsl:when>
                <xsl:when test="$Code='A6'">VACCINES/MEDICARE 100% PAYMENT</xsl:when>
                <xsl:when test="$Code='A9'">SECOND OPINION SURGERY</xsl:when>
                <xsl:when test="$Code='AA'">ABORTION PERFORMED DUE TO RAPE</xsl:when>
                <xsl:when test="$Code='AB'">ABORTION PERFORMED DUE TO INCEST</xsl:when>
                <xsl:when test="$Code='AC'">ABORTION PERFORMED DUE TO SERIOUS FETAL GENETIC DEFECT, DEFORMITY, OR ABNORMALITY</xsl:when>
                <xsl:when test="$Code='AD'">ABORTION PERFORMED DUE TO A LIFE ENDANGERING PHYSICAL CONDITION</xsl:when>
                <xsl:when test="$Code='AE'">ABORTION PERFORMED DUE TO PHYSICAL HEALTH OF MOTHER THAT IS NOT LIFE ENDANGERING</xsl:when>
                <xsl:when test="$Code='AF'">ABORTION PERFORMED DUE TO EMOTIONAL/PSYCHOLOGICAL HEALTH OF THE MOTHER</xsl:when>
                <xsl:when test="$Code='AG'">ABORTION PERFORMED DUE TO SOCIAL OR ECONOMIC REASONS</xsl:when>
                <xsl:when test="$Code='AH'">ELECTIVE ABORTION</xsl:when>
                <xsl:when test="$Code='AI'">STERILIZATION</xsl:when>
                <xsl:when test="$Code='AJ'">PAYER RESPONSIBLE FOR COPAYMENT</xsl:when>
                <xsl:when test="$Code='AK'">AIR AMBULANCE REQUIRED</xsl:when>
                <xsl:when test="$Code='AL'">SPECIALIZED TREATMENT/BED UNAVAILABLE-ALTERNATE FACILITY TRANSPORT</xsl:when>
                <xsl:when test="$Code='AM'">NONEMERGENCY MEDICALLY NECESSARY STRETCHER TRANSPORT REQUIRED</xsl:when>
                <xsl:when test="$Code='AN'">PREADMISSION SCREENING NOT REQUIRED</xsl:when>
                <xsl:when test="$Code='B0'">MEDICARE COORDINATED CARE DEMONSTRATION CLAIM</xsl:when>
                <xsl:when test="$Code='B1'">BENEFICIARY IS INELIGIBLE FOR DEMONSTRATION PROGRAM</xsl:when>
                <xsl:when test="$Code='B2'">CRITICAL ACCESS HOSPITAL AMBULANCE ATTESTATION</xsl:when>
                <xsl:when test="$Code='B3'">PREGNANCY INDICATOR</xsl:when>
                <xsl:when test="$Code='B4'">ADMISSION UNRELATED TO DISCHARGE ON SAME DAY</xsl:when>
                <xsl:when test="$Code='C1'">APPROVED AS BILLED</xsl:when>
                <xsl:when test="$Code='C2'">AUTOMATIC APPROVAL AS BILLED BASED ON FOCUSED REVIEW</xsl:when>
                <xsl:when test="$Code='C3'">PARTIAL APPROVAL</xsl:when>
                <xsl:when test="$Code='C4'">ADMISSION/SERVICES DENIED</xsl:when>
                <xsl:when test="$Code='C5'">POSTPAYMENT REVIEW APPLICABLE</xsl:when>
                <xsl:when test="$Code='C6'">ADMISSION PREAUTHORIZATION</xsl:when>
                <xsl:when test="$Code='C7'">EXTENDED AUTHORIZATION</xsl:when>
                <xsl:when test="$Code='D0'">CHANGES TO SERVICES DATES</xsl:when>
                <xsl:when test="$Code='D1'">CHANGES TO CHARGES</xsl:when>
                <xsl:when test="$Code='D2'">CHANGES IN REVENUE CODES/HCPCS/HIPPS RATE CODES</xsl:when>
                <xsl:when test="$Code='D3'">SECOND OR SUBSEQUENT INTERIM PPS BILL</xsl:when>
                <xsl:when test="$Code='D4'">CHANGE IN CLINICAL CODES (ICD) FOR DIAGNOSIS AND/OR PROCEDURE CODES</xsl:when>
                <xsl:when test="$Code='D5'">CANCEL TO CORRECT INSURED ID OR PROVIDER ID</xsl:when>
                <xsl:when test="$Code='D6'">CANCEL ONLY TO REPAY A DUPLICATE OR OIG OVERPAYMENT</xsl:when>
                <xsl:when test="$Code='D7'">CHANGE TO MAKE MEDICARE THE SECONDARY PAYER</xsl:when>
                <xsl:when test="$Code='D8'">CHANGE TO MAKE MEDICARE THE PRIMARY PAYER</xsl:when>
                <xsl:when test="$Code='D9'">ANY OTHER CHANGE</xsl:when>
                <xsl:when test="$Code='DR'">DISASTER RELATED</xsl:when>
                <xsl:when test="$Code='E0'">CHANGE IN PATIENT STATUS</xsl:when>
                <xsl:when test="$Code='G0'">DISTINCT MEDICAL VISIT</xsl:when>
                <xsl:when test="$Code='H0'">DELAYED FILING, STATEMENT OF INTENT SUBMITTED</xsl:when>
                <xsl:when test="$Code='H2'">DISCHARGE BY A HOSPICE PROVIDER FOR CAUSE</xsl:when>
                <xsl:when test="$Code='M0'">ALL-INCLUSIVE RATE FOR OUTPATIENT (MEDICARE CODE)</xsl:when>
                <xsl:when test="$Code='M1'">ROSTER BILLED INFLUENZA VIRUS VACCINE OR PPV (MEDICARE CODE)</xsl:when>
                <xsl:when test="$Code='M2'">HHA PAYMENT SIGNIFICANTLY EXCEEDS TOTAL CHARGES  (MEDICARE CODE)</xsl:when>
                <xsl:when test="$Code='P1'">DO NOT RESUSCITATE ORDER (DNR). FOR PUBLIC HEALTH REPORTING ONLY</xsl:when>
                <xsl:when test="$Code='P7'">DIRECT INPATIENT ADMISSION FROM EMERGENCY ROOM</xsl:when>
                <xsl:when test="$Code='W0'">UNITED MINE WORKERS OF AMERICA (UMWA) DEMONSTRATION INDICATOR</xsl:when>
                <xsl:when test="$Code='W2'">DUPLICATE OF ORIGINAL BILL</xsl:when>
                <xsl:when test="$Code='W3'">LEVEL I APPEAL</xsl:when>
                <xsl:when test="$Code='W4'">LEVEL II APPEAL</xsl:when>
                <xsl:when test="$Code='W5'">LEVEL III APPEAL</xsl:when>              </xsl:choose>
            </xsl:comment>
          </xsl:if>
        </Condition>
      </xsl:when>

      <xsl:when test="$Qualifier='BI' or $Qualifier='BJ' or $Qualifier='ZZ' or $Qualifier='BN' or $Qualifier='BF'">
        <Diagnosis>
          <xsl:attribute name="Type">
            <xsl:choose>
              <xsl:when test="$Qualifier='BK'">Principal</xsl:when>
              <xsl:when test="$Qualifier='BJ'">Admitting</xsl:when>
              <xsl:when test="$Qualifier='ZZ'">Patient Reason For Visit</xsl:when>
              <xsl:when test="$Qualifier='BN'">External Cause Of Injury E-Code</xsl:when>
              <xsl:when test="$Qualifier='BF'">Other</xsl:when>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:if test="string-length($POA)>0">
            <xsl:attribute name="PresentOnAdmission">
              <xsl:value-of select="$POA"/>
            </xsl:attribute>
          </xsl:if>
        </Diagnosis>
      </xsl:when>
      <xsl:when test="$Qualifier='DR'">
        <DiagnosisRelatedGroup>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
        </DiagnosisRelatedGroup>
      </xsl:when>

      <xsl:when test="$Qualifier='BP' or $Qualifier='BR' or $Qualifier='BO' or $Qualifier='BQ'">
        <Procedure>
          <xsl:attribute name="IsPrincipal">
            <xsl:choose>
              <xsl:when test="$Qualifier='BP' or $Qualifier='BR'">true</xsl:when>
              <xsl:otherwise>false</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
          <xsl:attribute name="Date">
            <xsl:call-template name="FormatD8Date">
              <xsl:with-param name="Date" select="$Date"/>
            </xsl:call-template>
          </xsl:attribute>
        </Procedure>
      </xsl:when>
      <xsl:when test="$Qualifier='TC'">
        <Treatment>
          <xsl:attribute name="Code">
            <xsl:value-of select="$Code"/>
          </xsl:attribute>
        </Treatment>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="HI">
    <xsl:if test="count(HI01)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI01/HI0101"/>
        <xsl:with-param name="Code" select="HI01/HI0102"/>
        <xsl:with-param name="DateFormat" select="HI01/HI0103"/>
        <xsl:with-param name="Date" select="HI01/HI0104"/>
        <xsl:with-param name="Amount" select="HI01/HI0105"/>
        <xsl:with-param name="Quantity" select="HI01/HI0106"/>
        <xsl:with-param name="Version" select="HI01/HI0107"/>
        <xsl:with-param name="POA" select="HI01/HI0109"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI02)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI02/HI0201"/>
        <xsl:with-param name="Code" select="HI02/HI0202"/>
        <xsl:with-param name="DateFormat" select="HI02/HI0203"/>
        <xsl:with-param name="Date" select="HI02/HI0204"/>
        <xsl:with-param name="Amount" select="HI02/HI0205"/>
        <xsl:with-param name="Quantity" select="HI02/HI0206"/>
        <xsl:with-param name="Version" select="HI02/HI0207"/>
        <xsl:with-param name="POA" select="HI02/HI0209"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI03)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI03/HI0301"/>
        <xsl:with-param name="Code" select="HI03/HI0302"/>
        <xsl:with-param name="DateFormat" select="HI03/HI0303"/>
        <xsl:with-param name="Date" select="HI03/HI0304"/>
        <xsl:with-param name="Amount" select="HI03/HI0305"/>
        <xsl:with-param name="Quantity" select="HI03/HI0306"/>
        <xsl:with-param name="Version" select="HI03/HI0307"/>
        <xsl:with-param name="POA" select="HI03/HI0309"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI04)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI04/HI0401"/>
        <xsl:with-param name="Code" select="HI04/HI0402"/>
        <xsl:with-param name="DateFormat" select="HI04/HI0403"/>
        <xsl:with-param name="Date" select="HI04/HI0404"/>
        <xsl:with-param name="Amount" select="HI04/HI0405"/>
        <xsl:with-param name="Quantity" select="HI04/HI0406"/>
        <xsl:with-param name="Version" select="HI04/HI0407"/>
        <xsl:with-param name="POA" select="HI04/HI0409"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI05)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI05/HI0501"/>
        <xsl:with-param name="Code" select="HI05/HI0502"/>
        <xsl:with-param name="DateFormat" select="HI05/HI0503"/>
        <xsl:with-param name="Date" select="HI05/HI0504"/>
        <xsl:with-param name="Amount" select="HI05/HI0505"/>
        <xsl:with-param name="Quantity" select="HI05/HI0506"/>
        <xsl:with-param name="Version" select="HI05/HI0507"/>
        <xsl:with-param name="POA" select="HI05/HI0509"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI06)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI06/HI0601"/>
        <xsl:with-param name="Code" select="HI06/HI0602"/>
        <xsl:with-param name="DateFormat" select="HI06/HI0603"/>
        <xsl:with-param name="Date" select="HI06/HI0604"/>
        <xsl:with-param name="Amount" select="HI06/HI0605"/>
        <xsl:with-param name="Quantity" select="HI06/HI0606"/>
        <xsl:with-param name="Version" select="HI06/HI0607"/>
        <xsl:with-param name="POA" select="HI06/HI0609"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI07)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI07/HI0701"/>
        <xsl:with-param name="Code" select="HI07/HI0702"/>
        <xsl:with-param name="DateFormat" select="HI07/HI0703"/>
        <xsl:with-param name="Date" select="HI07/HI0704"/>
        <xsl:with-param name="Amount" select="HI07/HI0705"/>
        <xsl:with-param name="Quantity" select="HI07/HI0706"/>
        <xsl:with-param name="Version" select="HI07/HI0707"/>
        <xsl:with-param name="POA" select="HI07/HI0709"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI08)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI08/HI0801"/>
        <xsl:with-param name="Code" select="HI08/HI0802"/>
        <xsl:with-param name="DateFormat" select="HI08/HI0803"/>
        <xsl:with-param name="Date" select="HI08/HI0804"/>
        <xsl:with-param name="Amount" select="HI08/HI0805"/>
        <xsl:with-param name="Quantity" select="HI08/HI0806"/>
        <xsl:with-param name="Version" select="HI08/HI0807"/>
        <xsl:with-param name="POA" select="HI08/HI0809"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI09)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI09/HI0901"/>
        <xsl:with-param name="Code" select="HI09/HI0902"/>
        <xsl:with-param name="DateFormat" select="HI09/HI0903"/>
        <xsl:with-param name="Date" select="HI09/HI0904"/>
        <xsl:with-param name="Amount" select="HI09/HI0905"/>
        <xsl:with-param name="Quantity" select="HI09/HI0906"/>
        <xsl:with-param name="Version" select="HI09/HI0907"/>
        <xsl:with-param name="POA" select="HI09/HI0909"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI10)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI10/HI1001"/>
        <xsl:with-param name="Code" select="HI10/HI1002"/>
        <xsl:with-param name="DateFormat" select="HI10/HI1003"/>
        <xsl:with-param name="Date" select="HI10/HI1004"/>
        <xsl:with-param name="Amount" select="HI10/HI1005"/>
        <xsl:with-param name="Quantity" select="HI10/HI1006"/>
        <xsl:with-param name="Version" select="HI10/HI1007"/>
        <xsl:with-param name="POA" select="HI10/HI1009"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI11)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI11/HI1101"/>
        <xsl:with-param name="Code" select="HI11/HI1102"/>
        <xsl:with-param name="DateFormat" select="HI11/HI1103"/>
        <xsl:with-param name="Date" select="HI11/HI1104"/>
        <xsl:with-param name="Amount" select="HI11/HI1105"/>
        <xsl:with-param name="Quantity" select="HI11/HI0116"/>
        <xsl:with-param name="Version" select="HI11/HI1107"/>
        <xsl:with-param name="POA" select="HI11/HI1109"/>
      </xsl:call-template>
    </xsl:if>
    <xsl:if test="count(HI12)>0">
      <xsl:call-template name="HealthcareInformation">
        <xsl:with-param name="Qualifier" select="HI12/HI1201"/>
        <xsl:with-param name="Code" select="HI12/HI1202"/>
        <xsl:with-param name="DateFormat" select="HI12/HI1203"/>
        <xsl:with-param name="Date" select="HI12/HI1204"/>
        <xsl:with-param name="Amount" select="HI12/HI1205"/>
        <xsl:with-param name="Quantity" select="HI12/HI1206"/>
        <xsl:with-param name="Version" select="HI12/HI1207"/>
        <xsl:with-param name="POA" select="HI12/HI1209"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

  <!-- Professional Service Line Segment -->
  <xsl:template match="SV1">
    <xsl:attribute name="Quantity">
      <xsl:choose>
        <xsl:when test="string-length(SV104)>0">
          <xsl:value-of select="SV104"/>
        </xsl:when>
        <xsl:otherwise>1</xsl:otherwise>
      </xsl:choose>

    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV103"/>
    </xsl:attribute>
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV102"/>
    </xsl:attribute>
    <xsl:if test="string-length(SV105)>0">
      <PlaceOfService>
        <xsl:attribute name="Code">
          <xsl:value-of select="SV105"/>
        </xsl:attribute>
        <xsl:if test="$verbose=1">
          <xsl:choose>
            <xsl:when test="SV105='11'">Office</xsl:when>
            <xsl:when test="SV105='12'">Home</xsl:when>
            <xsl:when test="SV105='21'">Inpatient Hospital</xsl:when>
            <xsl:when test="SV105='22'">Outpatient Hospital</xsl:when>
            <xsl:when test="SV105='23'">Emergency Room - Hospital</xsl:when>
            <xsl:when test="SV105='24'">Ambulatory Surgical Center</xsl:when>
            <xsl:when test="SV105='25'">Birthing Center</xsl:when>
            <xsl:when test="SV105='26'">Military Treatment Facility</xsl:when>
            <xsl:when test="SV105='31'">Skilled Nursing Facility</xsl:when>
            <xsl:when test="SV105='32'">Nursing Facility</xsl:when>
            <xsl:when test="SV105='33'">Custodial Care Facility</xsl:when>
            <xsl:when test="SV105='34'">Hospice</xsl:when>
            <xsl:when test="SV105='41'">Ambulance - Land</xsl:when>
            <xsl:when test="SV105='42'">Ambulance - Air or Water</xsl:when>
            <xsl:when test="SV105='50'">Federally Qualified Health Care</xsl:when>
            <xsl:when test="SV105='51'">Inpatient Psychiatric Facility</xsl:when>
            <xsl:when test="SV105='52'">Psychiatric Facility Partial Hospitalization</xsl:when>
            <xsl:when test="SV105='53'">Community Mental Health Center</xsl:when>
            <xsl:when test="SV105='54'">Intermediate Care Facility/Mentally Retarded</xsl:when>
            <xsl:when test="SV105='55'">Residential Substance Abuse Treatment Facility</xsl:when>
            <xsl:when test="SV105='56'">Psychiatric Residential Treatment Center</xsl:when>
            <xsl:when test="SV105='60'">Mass Immunization Center</xsl:when>
            <xsl:when test="SV105='61'">Comprehensive Inpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="SV105='62'">Comprehensive Outpatient Rehabilitation Facility</xsl:when>
            <xsl:when test="SV105='65'">End Stage Renal Disease Treatment Facility</xsl:when>
            <xsl:when test="SV105='71'">State or Local Public Health Clinic</xsl:when>
            <xsl:when test="SV105='72'">Rural Health Clinic</xsl:when>
            <xsl:when test="SV105='81'">Independent Laboratory</xsl:when>
            <xsl:when test="SV105='99'">Other Unlisted Facility</xsl:when>
          </xsl:choose>
        </xsl:if>
      </PlaceOfService>
    </xsl:if>
    <Procedure>
      <xsl:attribute name="Code">
        <xsl:value-of select="SV101/SV10102"/>
      </xsl:attribute>
      <xsl:if test="string-length(SV101/SV10103) > 0">
        <xsl:attribute name="Mod1">
          <xsl:value-of select="SV101/SV10103"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10104) > 0">
        <xsl:attribute name="Mod2">
          <xsl:value-of select="SV101/SV10104"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10105) > 0">
        <xsl:attribute name="Mod3">
          <xsl:value-of select="SV101/SV10105"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10106) > 0">
        <xsl:attribute name="Mod4">
          <xsl:value-of select="SV101/SV10106"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="string-length(SV101/SV10107)>0">
        <xsl:value-of select="SV101/SV10107"/>
      </xsl:if>
    </Procedure>
  </xsl:template>

  <!-- Institutional Service Line Segment -->
  <xsl:template match="SV2">
    <xsl:attribute name="Quantity">
      <xsl:choose>
        <xsl:when test="string-length(SV205)>0">
          <xsl:value-of select="SV205"/>
        </xsl:when>
        <xsl:otherwise>1</xsl:otherwise>
      </xsl:choose>

    </xsl:attribute>
    <xsl:attribute name="Unit">
      <xsl:value-of select="SV204"/>
    </xsl:attribute>
    <xsl:attribute name="ChargeAmount">
      <xsl:value-of select="SV203"/>
    </xsl:attribute>
    <Revenue>
      <xsl:attribute name="Code">
        <xsl:value-of select="SV201"/>
      </xsl:attribute>
      <xsl:if test="$verbose=1">
        <xsl:comment>
          <xsl:choose>
            <xsl:when test="SV201='0270'">Medical/Surgical Supplies</xsl:when>
            <xsl:when test="SV201='0300'">Laboratory-Clinical Diagnostic</xsl:when>
            <xsl:when test="SV201='0320'">Radiology-Diagnostic</xsl:when>
          </xsl:choose>
        </xsl:comment>
      </xsl:if>
    </Revenue>
    <xsl:if test="string-length(SV202/SV20202)>0">
      <Procedure>
        <xsl:attribute name="Qualifier">
          <xsl:value-of select="SV202/SV20201"/>
        </xsl:attribute>
        <xsl:attribute name="Code">          
          <xsl:value-of select="SV202/SV20202"/>
        </xsl:attribute>
        <xsl:if test="string-length(SV202/SV20203) > 0">
          <xsl:attribute name="Mod1">
            <xsl:value-of select="SV202/SV20203"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20204) > 0">
          <xsl:attribute name="Mod2">
            <xsl:value-of select="SV202/SV20204"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20205) > 0">
          <xsl:attribute name="Mod3">
            <xsl:value-of select="SV202/SV20205"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:if test="string-length(SV202/SV20206) > 0">
          <xsl:attribute name="Mod4">
            <xsl:value-of select="SV202/SV20206"/>
          </xsl:attribute>
        </xsl:if>
      </Procedure>
    </xsl:if>
  </xsl:template>

  <!-- Dental Service Line Segment -->
  <xsl:template match="SV3"></xsl:template>

  <!-- Subscriber Loop -->
  <xsl:template match="HierarchicalLoop[@LoopId='2000B']">
    <Subscriber>
      <xsl:if test="Loop[@LoopId='2010BA']/NM1/NM108 = 'MI'">
        <xsl:attribute name="MemberId">
          <xsl:value-of select="Loop[@LoopId='2010BA']/NM1/NM109"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="DateOfBirth">
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="Loop[@LoopId='2010BA']/DMG/DMG02"/>
        </xsl:call-template>
      </xsl:attribute>
      <xsl:attribute name="Gender">
        <xsl:choose>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'F'">Female</xsl:when>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'M'">Male</xsl:when>
          <xsl:when test="Loop[@LoopId='2010BA']/DMG/DMG03 = 'U'">Unknown</xsl:when>
          <xsl:otherwise>Unknown</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:apply-templates select="Loop[@LoopId='2010BA']/NM1" />
      <Address>
        <xsl:apply-templates select="Loop[@LoopId='2010BA']/N3" />
      </Address>
    </Subscriber>
  </xsl:template>

  <!-- Provider Loop in Billing Loop, Claim Loop or Service Line Loop -->
  <xsl:template match="Loop[substring(@LoopId,1,4)='2010'] | Loop[substring(@LoopId,1,4)='2310'] | Loop[substring(@LoopId,1,4)='2420']">
    <Provider>
      <xsl:choose>
        <xsl:when test="NM1/NM108='24'">
          <xsl:attribute name="EmployerId">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="count(REF[REF01='EI'])>0">
          <xsl:attribute name="EmployerId">
            <xsl:value-of select="REF[REF01='EI']/REF02"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="NM1/NM108='34'">
          <xsl:attribute name="Ssn">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="count(REF[REF01='SY'])>0">
          <xsl:attribute name="Ssn">
            <xsl:value-of select="REF[REF01='SY']/REF02"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="NM1/NM108='XX'">
          <xsl:attribute name="Npi">
            <xsl:value-of select="NM1/NM109"/>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
      <xsl:apply-templates select="NM1"/>
      
      <Address>
        <xsl:apply-templates select="N3"/>
      </Address>
      <xsl:apply-templates select="REF"/>
      <xsl:apply-templates select="PER"/>
      <xsl:if test="string-length(PRV/PRV01) > 0">
        <Speciality>
          <xsl:attribute name="Code">
            <xsl:value-of select="PRV/PRV03"/>
          </xsl:attribute>
        </Speciality>
      </xsl:if>
      <xsl:if test="string-length(../PRV/PRV01) > 0 ">
        <xsl:if test="NM1/NM101='85' and ../PRV/PRV01='BI' or NM1/NM101='87' and ../PRV/PRV01='PT'">
          <Speciality>
            <xsl:attribute name="Code">
              <xsl:value-of select="../PRV/PRV03"/>
            </xsl:attribute>
          </Speciality>
        </xsl:if>
      </xsl:if>
    </Provider>
  </xsl:template>

  <!-- Service Line Loop -->
  <xsl:template match="Loop[@LoopId='2400']">
    <ServiceLine>
      <xsl:attribute name="AssignedNumber">
        <xsl:value-of select="LX/LX01"/>
      </xsl:attribute>
      <xsl:apply-templates select="SV1"/>
      <xsl:apply-templates select="SV2"/>
      <xsl:apply-templates select="SV3"/>
      <DateOfServiceFrom>
        <xsl:call-template name="FormatD8Date">
          <xsl:with-param name="Date" select="substring(DTP[DTP01='472']/DTP03,1,8)"/>
        </xsl:call-template>
      </DateOfServiceFrom>
      <xsl:if test="DTP[DTP01='472']/DTP02='RD8'">
        <DateOfServiceTo>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="substring(DTP[DTP01='472']/DTP03,9,8)"/>
          </xsl:call-template>
        </DateOfServiceTo>
      </xsl:if>
      <xsl:if test="count(DTP[DTP01='866'])>0">
        <AssessmentDate>
          <xsl:call-template name="FormatD8Date">
            <xsl:with-param name="Date" select="DTP[DTP01='866']/DTP03"/>
          </xsl:call-template>
        </AssessmentDate>
      </xsl:if>
      <xsl:if test="AMT/AMT01='AAE'">
        <ApprovedAmount>
          <xsl:value-of select="AMT/AMT02"/>
        </ApprovedAmount>
      </xsl:if>
      <xsl:apply-templates select="HCP"/>
    </ServiceLine>
  </xsl:template>
  
  <!-- Claim Loop -->
  <xsl:template name="ClaimTemplate">
    <xsl:param name="SubscriberLoop" />
    <xsl:param name="ClaimLoop" />
    <xsl:param name="PatientIsSubscriber" />
    <Claim>
      <xsl:attribute name="Type">
        <xsl:choose>
          <xsl:when test="count($ClaimLoop/Loop[@LoopId='2400']/SV3)>0">Dental</xsl:when>
          <xsl:when test="count($ClaimLoop/Loop[@LoopId='2400']/SV2)>0">Institutional</xsl:when>
          <xsl:otherwise>Professional</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="PatientControlNumber">
        <xsl:value-of select="$ClaimLoop/CLM/CLM01"/>
      </xsl:attribute>
      <xsl:attribute name="TotalCharges">
        <xsl:value-of select="$ClaimLoop/CLM/CLM02"/>
      </xsl:attribute>
      <!-- Reference Identifications -->
      <xsl:apply-templates select="$ClaimLoop/REF"/>
      <!-- Billing Hierarchical Loop -->
      <xsl:apply-templates select="$SubscriberLoop/../Loop" />
      <!-- Rendering/Facility Providers -->
      <xsl:apply-templates select="$ClaimLoop/Loop[substring(@LoopId,1,4)='2310']"/>
      <!-- Subscriber Hierarchical Loop -->
      <xsl:apply-templates select="$SubscriberLoop" />
      <!-- Note/Special Instruction Segment -->
      <xsl:apply-templates select="$ClaimLoop/NTE"/>
      <!-- Healthcare Pricing Segment -->
      <xsl:apply-templates select="$ClaimLoop/HCP"/>
      <xsl:apply-templates select="$ClaimLoop/HI"/>
      <xsl:apply-templates select="$ClaimLoop/Loop[@LoopId='2400']"/>
    </Claim>
  </xsl:template>



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
      <!-- Claims without patient loop -->
      <xsl:for-each select="HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select=".."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="1"/>
        </xsl:call-template>
      </xsl:for-each>
      <!-- Claims with patient loop -->
      <xsl:for-each select="HierarchicalLoop/HierarchicalLoop/HierarchicalLoop/Loop[@LoopId='2300']">
        <xsl:call-template name="ClaimTemplate">
          <xsl:with-param name="SubscriberLoop" select="../.."/>
          <xsl:with-param name="ClaimLoop" select="."/>
          <xsl:with-param name="PatientIsSubscriber" select="0"/>
        </xsl:call-template>
      </xsl:for-each>
    </Transaction>
  </xsl:template>

  <xsl:template match="/">
    <ArrayOfTransaction>
      <xsl:apply-templates select="/Interchange/FunctionGroup/Transaction"/>
    </ArrayOfTransaction>
  </xsl:template>


</xsl:stylesheet>
