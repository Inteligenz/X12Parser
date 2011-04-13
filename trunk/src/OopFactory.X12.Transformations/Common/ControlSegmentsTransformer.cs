using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;

namespace OopFactory.X12.Transformations.Common
{
    public class ControlSegmentsTransformer : X12TransformationService
    {
        public ControlSegmentsTransformer(ITransformationService preProcessor)
            : base(preProcessor)
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Common.Xsl.Control-Segments.xslt")));
            }
            return _transform;
        }
    }
}
