namespace OopFactory.X12.Transformations
{
    using System.IO;
    using System.Xml;
    using System.Xml.Xsl;

    public abstract class X12TransformationService : ITransformationService
    {
        private readonly ITransformationService preProcessor;

        protected X12TransformationService(ITransformationService preProcessor)
        {
            this.preProcessor = preProcessor;
        }

        protected abstract XslCompiledTransform GetTransform();

        protected virtual XsltArgumentList GetArguments()
        {
            return new XsltArgumentList();
        }

        public virtual string Transform(string x12)
        {
            string xml = this.preProcessor.Transform(x12);
            XslCompiledTransform transform = this.GetTransform();
            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), this.GetArguments(), writer);
            return writer.GetStringBuilder().ToString();
        }
    }
}
