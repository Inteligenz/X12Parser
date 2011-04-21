using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Transformations;

namespace OopFactory.X12.Rendering.Claims
{
    public class ClaimToCms1500FormTransformer : X12TransformationService
    {
        public ClaimToCms1500FormTransformer(ITransformationService preProcessor)
            : base(preProcessor)
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Rendering.Claims.Xsl.Claim-To-Cms-1500.xslt")));
            }
            return _transform;
        }
    }
}
