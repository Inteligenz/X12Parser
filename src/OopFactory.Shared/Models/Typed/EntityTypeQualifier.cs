namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum EntityTypeQualifier
    {
        [EdiFieldValue("")]
        Undefined = 0,

        [EdiFieldValue("1")]
        Person = 1,

        [EdiFieldValue("2")]
        NonPersonEntity = 2,

        [EdiFieldValue("3")]
        Unknown,

        [EdiFieldValue("4")]
        Corporation,

        [EdiFieldValue("5")]
        Trust,

        [EdiFieldValue("6")]
        Organization,

        [EdiFieldValue("7")]
        LimitedLiabilityCorporation,

        [EdiFieldValue("8")]
        Partnership,

        [EdiFieldValue("9")]
        SCorporation,

        [EdiFieldValue("C")]
        Custodial,

        [EdiFieldValue("D")]
        NonProfitOrganization,

        [EdiFieldValue("E")]
        SoleProprietorship,

        [EdiFieldValue("G")]
        Government,

        [EdiFieldValue("L")]
        LimitedPartnership
    }
}
