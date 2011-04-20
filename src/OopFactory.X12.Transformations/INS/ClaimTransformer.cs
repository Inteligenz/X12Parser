using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;

namespace OopFactory.X12.Transformations.INS
{
    public class ClaimTransformer: X12TransformationService
    {
        public ClaimTransformer(ITransformationService preProcessor)
            : base(new Common.CommonSegmentsTransformer(preProcessor))
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.INS.Xsl.X12-837-To-Claim.xslt")));
            }
            return _transform;
        }
        
    }
}
