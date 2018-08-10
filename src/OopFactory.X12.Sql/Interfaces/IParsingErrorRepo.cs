namespace OopFactory.X12.Sql.Interfaces
{
    public interface IParsingErrorRepo
    {
        object PersistParsingError(object interchangeId, int positionInInterchange, int? revisionId, string errorMessage);
    }
}
