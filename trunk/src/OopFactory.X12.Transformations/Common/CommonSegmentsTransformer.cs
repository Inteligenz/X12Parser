using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml;
using System.Reflection;
using OopFactory.X12.Transformations.Common.Xsl;

namespace OopFactory.X12.Transformations.Common
{
    public class CommonSegmentsTransformer : X12TransformationService
    {
        public CommonSegmentsTransformer(ITransformationService preProcessor)
            : base(
                new ControlSegmentsTransformer(preProcessor))
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Common.Xsl.Common-Segments.xslt")));
            }
            return _transform;
        }
        
    }
}
