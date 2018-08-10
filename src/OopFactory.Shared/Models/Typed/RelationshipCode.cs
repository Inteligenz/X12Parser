namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum RelationshipCode
    {
        [EdiFieldValue("A")]
        Add,

        [EdiFieldValue("D")]
        Delete,

        [EdiFieldValue("I")]
        Include,

        [EdiFieldValue("O")]
        InformationOnly,

        [EdiFieldValue("S")]
        Substituted
    }
}
