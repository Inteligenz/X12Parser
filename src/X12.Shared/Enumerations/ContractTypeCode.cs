﻿namespace X12.Shared.Enumerations
{
    using X12.Shared.Attributes;

    public enum ContractTypeCode
    {
        [EdiFieldValue("01")]
        DiagnosisRelatedGroup_DRG,

        [EdiFieldValue("02")]
        PerDiem,

        [EdiFieldValue("03")]
        VariablePerDiem,

        [EdiFieldValue("04")]
        Flat,

        [EdiFieldValue("05")]
        Capitated,

        [EdiFieldValue("06")]
        Percent,

        [EdiFieldValue("09")]
        Other,

        [EdiFieldValue("AB")]
        NegotiatedGrowingEquityMortgage_GEM,

        [EdiFieldValue("AC")]
        AnticipatedContract,

        [EdiFieldValue("AD")]
        FederalHousingAuthorityAdjustableRateMortgage,

        [EdiFieldValue("AE")]
        FederalHousingAuthorityVeteransAffairsFixedRateMortgage,

        [EdiFieldValue("AF")]
        ConventionalSecondMortgages,

        [EdiFieldValue("AG")]
        ConventionalFixedRateMortgages,

        [EdiFieldValue("AH")]
        FederalHousingAuthorityVeteransAffairsGraduatedPaymentMortgage,

        [EdiFieldValue("AI")]
        NegotiatedConventional_GraduatedPayment_Or_StepRateMortgage,

        [EdiFieldValue("AJ")]
        ConventionalAdjustableRateMortgage,

        [EdiFieldValue("CA")]
        CostPlusIncentiveFee_WithPerformanceIncentives,

        [EdiFieldValue("CB")]
        CostPlusIncentiveFee_WithoutPerformanceIncentives,

        [EdiFieldValue("CH")]
        CostSharing,

        [EdiFieldValue("CP")]
        CostPlus,

        [EdiFieldValue("CS")]
        Cost,

        [EdiFieldValue("CW")]
        CostPlusAwardFee,

        [EdiFieldValue("CX")]
        CostPlusFixedFee,

        [EdiFieldValue("CY")]
        CostPlusIncentiveFee,

        [EdiFieldValue("DI")]
        Distributor,

        [EdiFieldValue("EA")]
        ExclusiveAgency,

        [EdiFieldValue("ER")]
        ExclusiveRight,

        [EdiFieldValue("FA")]
        FirmorActualContract,

        [EdiFieldValue("FB")]
        FixedPriceIncentiveFirmTarget_WithPerformanceIncentive,

        [EdiFieldValue("FC")]
        FixedPriceIncentiveFirmTarget_WithoutPerformanceIncentive,

        [EdiFieldValue("FD")]
        FixedPriceRedetermination,

        [EdiFieldValue("FE")]
        FixedPricewithEscalation,

        [EdiFieldValue("FF")]
        FixedPriceIncentiveSuccessiveTarget_WithPerformanceIncentive,

        [EdiFieldValue("FG")]
        FixedPriceIncentiveSuccessiveTarget_WithoutPerformanceIncentive,

        [EdiFieldValue("FH")]
        FixedPriceAwardFee,

        [EdiFieldValue("FI")]
        FixedPriceIncentive,

        [EdiFieldValue("FJ")]
        FixedPriceLevelofEffort,

        [EdiFieldValue("FK")]
        NoCost,

        [EdiFieldValue("FL")]
        FlatAmount,

        [EdiFieldValue("FM")]
        RetroactiveFixedPriceRedetermination,

        [EdiFieldValue("FR")]
        FirmFixedPrice,

        [EdiFieldValue("FX")]
        FixedPricewithEconomicPriceAdjustment,

        [EdiFieldValue("LA")]
        Labor,

        [EdiFieldValue("LE")]
        LevelofEffort,

        [EdiFieldValue("LH")]
        LaborHours,

        [EdiFieldValue("OC")]
        OtherContractType,

        [EdiFieldValue("PR")]
        ProspectReservation,

        [EdiFieldValue("SP")]
        SamePercentageasFilmRentalEarned_SPFRE,

        [EdiFieldValue("TM")]
        TimeandMaterials,

        [EdiFieldValue("ZZ")]
        MutuallyDefined,
    }
}
