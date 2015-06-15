using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{    public enum Gender
    {
        [EDIFieldValue("U")]
        Undefined,
        [EDIFieldValue("F")]
        Female,
        [EDIFieldValue("M")]
        Male,
        [EDIFieldValue("U")]
        Unknown
    }
}
