namespace OopFactory.X12.Transformations
{
    /// <summary>
    /// Interface for transforming X12 into a different structure
    /// </summary>
    public interface ITransformationService
    {
        /// <summary>
        /// Transforms an X12 string into a different structure
        /// </summary>
        /// <param name="x12">X12 to be transformed</param>
        /// <returns>Transformed data in desired structure</returns>
        string Transform(string x12);
    }
}
