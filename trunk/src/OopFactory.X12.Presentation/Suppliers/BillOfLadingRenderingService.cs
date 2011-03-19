using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;
using Fonet.Render;
using System.IO;

namespace OopFactory.X12.Presentation.Suppliers
{
    public class BillOfLadingRenderingService
    {
        public byte[] CreatePdf(string domainXml)
        {
            var transform = GetPortraitTransform();
            var writer = new StringWriter();
            XsltArgumentList list = new XsltArgumentList();
            list.AddParam("portrait", "", "1");
            transform.Transform(XmlReader.Create(new StringReader(domainXml)), list, writer);
            var foXml = writer.GetStringBuilder().ToString();
            
            var driver = Fonet.FonetDriver.Make();
            MemoryStream output = new MemoryStream();
            driver.Render(new StringReader(foXml), output);
            return output.ToArray();
        }

        private static XslCompiledTransform _portraitTransform;

        internal static XslCompiledTransform GetPortraitTransform()
        {
            if (_portraitTransform == null)
            {
                _portraitTransform = new XslCompiledTransform();

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Presentation.Suppliers.ShipNotice-To-BillOfLading-Portrait.xslt"));

                _portraitTransform.Load(xsltReader);

            }
            return _portraitTransform;
        }

    }
}
