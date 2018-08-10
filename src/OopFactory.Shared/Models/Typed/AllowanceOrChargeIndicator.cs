namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

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
