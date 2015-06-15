using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum InsuranceTypeCode
    {
        [EDIFieldValue("AP")]
        AutoInsurancePolicy,
        [EDIFieldValue("C1")]
        Commercial,
        [EDIFieldValue("CO")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA,
        [EDIFieldValue("GP")]
        GroupPolicy,
        [EDIFieldValue("HM")]
        HealthMaintenanceOrganizationHMO,
        [EDIFieldValue("HN")]
        HealthMaintenanceOrganizationHMO_MedicareRisk,
        [EDIFieldValue("IP")]
        IndividualPolicy,
        [EDIFieldValue("MA")]
        MedicarePartA,
        [EDIFieldValue("MB")]
        MedicarePartB,
        [EDIFieldValue("MC")]
        Medicaid,
        [EDIFieldValue("PR")]
        PreferredProviderOrganizationPPO,
        [EDIFieldValue("PS")]
        PointofServicePOS,
        [EDIFieldValue("SP")]
        SupplementalPolicy,
        [EDIFieldValue("WC")]
        WorkersCompensation,
    }
}
