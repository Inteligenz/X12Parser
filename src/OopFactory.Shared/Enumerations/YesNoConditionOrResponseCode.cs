namespace OopFactory.X12.Shared.Enumerations
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
