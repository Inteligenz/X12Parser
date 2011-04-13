using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml;
using System.Reflection;

namespace OopFactory.X12.Transformations.Common
{
    public class EntitySegmentsTransformer : X12TransformationService
    {
        public EntitySegmentsTransformer(ITransformationService preProcessor)
            : base(preProcessor)
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Common.Xsl.Segments-DMG,N1,N2,N3,N4,NM1,PER,REF.xslt")));
            }
            return _transform;
        }
    }
}
