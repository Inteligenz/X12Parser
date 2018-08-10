namespace OopFactory.X12.Sql.IdentityProviders
{
    internal class Identity<TSize>
    {
        public TSize NextId { get; set; }
        public TSize MaxId { get; set; }
    }
}
