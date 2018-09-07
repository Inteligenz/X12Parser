namespace OopFactory.X12.Transformations
{
    using System.IO;
    using System.Xml;
    using System.Xml.Xsl;

    /// <summary>
    /// Provides the abstract base class for X12 transformations
    /// </summary>
    public abstract class X12TransformationService : ITransformationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X12TransformationService"/> class
        /// </summary>
        /// <param name="preProcessor">Transformer preprocessor</param>
        protected X12TransformationService(ITransformationService preProcessor)
        {
            this.PreProcessor = preProcessor;
        }

        /// <summary>
        /// Gets the transformation service preprocessor for processing prior to transforming
        /// </summary>
        protected ITransformationService PreProcessor { get; }

        /// <summary>
        /// Transforms the given string into an X12 string
        /// </summary>
        /// <param name="x12">string to transform into X12</param>
        /// <returns>Transformed X12 string</returns>
        public virtual string Transform(string x12)
        {
            string xml = this.PreProcessor.Transform(x12);
            XslCompiledTransform transform = this.GetTransform();
            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), this.GetArguments(), writer);
            return writer.GetStringBuilder().ToString();
        }

        /// <summary>
        /// Gets the XSLT arguments for additional transformation options
        /// </summary>
        /// <returns>XSLT arguments object</returns>
        protected virtual XsltArgumentList GetArguments()
        {
            return new XsltArgumentList();
        }

        /// <summary>
        /// Gets the compiled transform for the transformation service
        /// </summary>
        /// <returns>Compiled transform for the transformation service</returns>
        protected abstract XslCompiledTransform GetTransform();
    }
}
