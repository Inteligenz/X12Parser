namespace OopFactory.X12.Transformations
{
    using System.IO;
    using System.Xml.Xsl;
    using System.Xml;

    public abstract class X12TransformationService : ITransformationService
    {
        private readonly ITransformationService preProcessor;

        public X12TransformationService(ITransformationService preProcessor)
        {
            this.preProcessor = preProcessor;
        }

        protected abstract XslCompiledTransform GetTransform();

        protected virtual XsltArgumentList GetArguments()
        {
            return new XsltArgumentList();
        }
        
        #region ITransformationService Members

        public virtual string Transform(string x12)
        {
            string xml = this.preProcessor.Transform(x12);
            
            XslCompiledTransform transform = GetTransform();            

            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), GetArguments(), writer);
            return writer.GetStringBuilder().ToString();
        }

        #endregion
    }
}
