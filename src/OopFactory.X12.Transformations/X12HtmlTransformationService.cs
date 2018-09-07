namespace OopFactory.X12.Transformations
{
    using System.Xml;
    using System.Xml.Xsl;

    /// <summary>
    /// Provides a transformation service for converting X12 to HTML 
    /// </summary>
    /// <inheritdoc cref="X12TransformationService"/>
    public class X12HtmlTransformationService : X12TransformationService
    {
        private static XslCompiledTransform transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="X12HtmlTransformationService"/> class
        /// </summary>
        /// <param name="preProcessor">Transformation service preprocessor used prior to transforming</param>
        public X12HtmlTransformationService(ITransformationService preProcessor)
            : base(preProcessor)
        {
        }

        /// <summary>
        /// Returns the XSL transform for X12 to HTML processing
        /// </summary>
        /// <returns>XSL transform object</returns>
        protected override XslCompiledTransform GetTransform()
        {
            if (transform == null)
            {
                transform = new XslCompiledTransform();
                transform.Load(XmlReader.Create(TransformationStreamFactory.GetHtmlTransformationStream()));
            }

            return transform;
        }
    }
}
