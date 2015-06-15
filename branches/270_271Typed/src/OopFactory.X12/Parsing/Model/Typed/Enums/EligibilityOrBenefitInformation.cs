using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum EligibilityOrBenefitInformation
    {
        [EDIFieldValue("1")]
        ActiveCoverage,
        [EDIFieldValue("2")]
        Active_FullRiskCapitation,
        [EDIFieldValue("3")]
        Active_ServicesCapitated,
        [EDIFieldValue("4")]
        Active_ServicesCapitatedtoPrimaryCarePhysician,
        [EDIFieldValue("5")]
        Active_PendingInvestigation,
        [EDIFieldValue("6")]
        Inactive,
        [EDIFieldValue("7")]
        Inactive_PendingEligibilityUpdate,
        [EDIFieldValue("8")]
        Inactive_PendingInvestigation,
        [EDIFieldValue("A")]
        Co_Insurance,
        [EDIFieldValue("B")]
        Co_Payment,
        [EDIFieldValue("C")]
        Deductible,
        [EDIFieldValue("CB")]
        CoverageBasis,
        [EDIFieldValue("D")]
        BenefitDescription,
        [EDIFieldValue("E")]
        Exclusions,
        [EDIFieldValue("F")]
        Limitations,
        [EDIFieldValue("G")]
        OutofPocket_StopLoss,
        [EDIFieldValue("H")]
        Unlimited,
        [EDIFieldValue("I")]
        Non_Covered,
        [EDIFieldValue("J")]
        CostContainment,
        [EDIFieldValue("K")]
        Reserve,
        [EDIFieldValue("L")]
        PrimaryCareProvider,
        [EDIFieldValue("M")]
        Pre_existingCondition,
        [EDIFieldValue("MC")]
        ManagedCareCoordinator,
        [EDIFieldValue("N")]
        ServicesRestrictedtoFollowingProvider,
        [EDIFieldValue("O")]
        NotDeemedaMedicalNecessity,
        [EDIFieldValue("P")]
        BenefitDisclaimer,
        [EDIFieldValue("Q")]
        SecondSurgicalOpinionRequired,
        [EDIFieldValue("R")]
        OtherorAdditionalPayor,
        [EDIFieldValue("S")]
        PriorYearHistory,
        [EDIFieldValue("T")]
        CardReportedLostStolen,
        [EDIFieldValue("U")]
        ContactFollowingEntityforEligibilityorBenefitInformation,
        [EDIFieldValue("V")]
        CannotProcess,
        [EDIFieldValue("W")]
        OtherSourceOfData,
        [EDIFieldValue("X")]
        HealthCareFacility,
        [EDIFieldValue("Y")]
        SpendDown,
    }
}
