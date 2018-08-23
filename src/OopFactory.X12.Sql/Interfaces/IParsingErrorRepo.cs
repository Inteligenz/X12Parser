namespace OopFactory.X12.Sql.Interfaces
{
    /// <summary>
    /// Provides interface for storing error data in a repository
    /// </summary>
    public interface IParsingErrorRepo
    {
        /// <summary>
        /// Stores a parsing error into a repository
        /// </summary>
        /// <param name="interchangeId">Interchange identifier object</param>
        /// <param name="positionInInterchange">Interchange element pointer</param>
        /// <param name="revisionId">Object revision Id, if present</param>
        /// <param name="errorMessage">Message to be stored</param>
        /// <returns>Error identifier object</returns>
        object PersistParsingError(object interchangeId, int positionInInterchange, int? revisionId, string errorMessage);
    }
}
