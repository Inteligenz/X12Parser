using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.IO;
using System.Xml;
using System.Reflection;

namespace OopFactory.X12.Transformations
{
    public class X12HtmlTransformationService : X12TransformationService
    {
        private ITransformationService _preProcessor;

        public X12HtmlTransformationService(ITransformationService preProcessor)
            : base(preProcessor)
        {
            _preProcessor = preProcessor;
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.X12-XML-to-HTML.xslt")));
            }
            return _transform;
        }
    }
}
