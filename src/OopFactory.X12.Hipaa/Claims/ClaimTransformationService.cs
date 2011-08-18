using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ClaimTransformationService
    {
        public UB04Claim TransformX12837ToUB04Model(Stream stream)
        {
            var parser = new X12Parser();
            var interchange = parser.Parse(stream);
            var xml = interchange.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Forms.Institutional.X12-837I-To-UB04Model.xslt");
            
            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();
            
            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            var claim = UB04Claim.Deserialize(new StreamReader(outputStream).ReadToEnd());
            return claim;
        }

    }
}
