namespace X12.Shared.Enumerations
{
    using X12.Shared.Attributes;

    public enum AllowanceOrChargeIndicator
    {
        [EdiFieldValue("A")]
        Allowance,

        [EdiFieldValue("C")]
        Charge,

        [EdiFieldValue("N")]
        NoAllowanceOrCharge,

        [EdiFieldValue("P")]
        Promotion,

        [EdiFieldValue("Q")]
        ChargeRequest,

        [EdiFieldValue("R")]
        AllowanceRequest,

        [EdiFieldValue("S")]
        Service
    }
}
