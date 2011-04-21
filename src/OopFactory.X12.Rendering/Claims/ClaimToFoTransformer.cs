using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Transformations;
using OopFactory.X12.Transformations.INS;
namespace OopFactory.X12.Rendering.Claims
{
    public class ClaimToFoTransformer: X12TransformationService
    {
        public ClaimToFoTransformer()
            : base(new ClaimToCms1500FormTransformer(new ClaimTransformer()))
        {
        }

        private static XslCompiledTransform _transform;

        protected override XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Rendering.Xsl.Form-To-Fo.xslt")));
            }
            return _transform;
        }
    }
}
