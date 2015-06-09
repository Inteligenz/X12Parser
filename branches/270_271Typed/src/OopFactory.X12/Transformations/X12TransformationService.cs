using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;
using System.Xml;

namespace OopFactory.X12.Transformations
{
    public abstract class X12TransformationService : ITransformationService
    {
        private ITransformationService _preProcessor;

        public X12TransformationService(ITransformationService preProcessor)
        {
            _preProcessor = preProcessor;
        }

        protected abstract XslCompiledTransform GetTransform();

        protected virtual XsltArgumentList GetArguments()
        {
            return new XsltArgumentList();
        }
        

        #region ITransformationService Members

        public virtual string Transform(string x12)
        {
            string xml = _preProcessor.Transform(x12);
            
            XslCompiledTransform transform = GetTransform();            

            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), GetArguments(), writer);
            return writer.GetStringBuilder().ToString();
        }

        #endregion
    }
}
