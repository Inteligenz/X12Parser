namespace OopFactory.X12.Sql.Interfaces
{
    public interface IIdentityProvider
    {
        void EnsureSchema();
        object NextId(string schema, string table);
    }
}
