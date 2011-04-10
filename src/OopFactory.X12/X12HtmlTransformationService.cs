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
    public class X12HtmlTransformationService : ITransformationService
    {
        private ITransformationService _preProcessor;

        public X12HtmlTransformationService(ITransformationService preProcessor)
        {
            _preProcessor = preProcessor;
        }

        #region ITransformationService Members

        public string Transform(string x12)
        {
            string xml = _preProcessor.Transform(x12);

            var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.X12-XML-to-HTML.xslt"));

            XslCompiledTransform transform = new XslCompiledTransform(); 
            transform.Load(xsltReader);

            var writer = new StringWriter();

            transform.Transform(XmlReader.Create(new StringReader(xml)), null, writer);
            return writer.GetStringBuilder().ToString();
        }

        #endregion
    }
}
