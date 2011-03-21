using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;

namespace OopFactory.X12.Presentation
{
    internal class TransformationService
    {
        public byte[] CreatePdf(Model.FoDocument document)
        {
            string xml = document.Serialize();
            
            var transform = GetTransform();
            var writer = new StringWriter();
            XsltArgumentList list = new XsltArgumentList();
#if DEBUG
            list.AddParam("debug", "", "1");
#endif
            transform.Transform(XmlReader.Create(new StringReader(xml)), list, writer);
            var foXml = writer.GetStringBuilder().ToString();

            var driver = Fonet.FonetDriver.Make();
            MemoryStream output = new MemoryStream();
            driver.Render(new StringReader(foXml), output);
            return output.ToArray();
        }

        private static XslCompiledTransform _transform;

        internal static XslCompiledTransform GetTransform()
        {
            if (_transform == null)
            {
                _transform = new XslCompiledTransform();

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Presentation.Xslt.FoDocument-To-Fo.xslt"));

                _transform.Load(xsltReader);

            }
            return _transform;
        }
    }
}
