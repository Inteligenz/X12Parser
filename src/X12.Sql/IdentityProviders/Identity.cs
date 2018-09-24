namespace X12.Sql.IdentityProviders
{
    /// <summary>
    /// Represents a single object identifier
    /// </summary>
    /// <typeparam name="TSize">Data type size of identifier</typeparam>
    internal class Identity<TSize>
    {
        /// <summary>
        /// Gets or sets the next object identifier
        /// </summary>
        public TSize NextId { get; set; }

        /// <summary>
        /// Gets or sets the object's max identifier
        /// </summary>
        public TSize MaxId { get; set; }
    }
}
