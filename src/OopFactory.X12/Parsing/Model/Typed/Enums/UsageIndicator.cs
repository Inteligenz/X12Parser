using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum UsageIndicator
    {
        [EDIFieldValue("P")]
        ProductionData,
        [EDIFieldValue("T")]
        TestData
    }
}
