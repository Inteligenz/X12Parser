using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum ActionCodes
    {
        [EDIFieldValue("U")]
        Reject,
        [EDIFieldValue("WQ")]
        Accept,
    }
}
