namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum YesNoConditionOrResponseCode
    {
        [EdiFieldValue("N")]
        No,

        [EdiFieldValue("U")]
        Unknown,

        [EdiFieldValue("W")]
        NotApplicable,

        [EdiFieldValue("Y")]
        Yes
    }
}
