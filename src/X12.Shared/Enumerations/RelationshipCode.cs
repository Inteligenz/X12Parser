namespace X12.Shared.Enumerations
{
    using X12.Shared.Attributes;

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
