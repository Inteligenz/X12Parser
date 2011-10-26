using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;
using OopFactory.X12.Hipaa.Claims.Forms.Professional;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class ClaimTransformationService
    {
        /// <summary>
        /// Reads a claim that has been st
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ClaimDocument Transform837ToClaimDocument(Stream stream)
        {

            var parser = new X12Parser();
            var interchange = parser.Parse(stream);
            return Transform837ToClaimDocument(interchange);
        }

        public ClaimDocument Transform837ToClaimDocument(Interchange interchange)
        {
            var xml = interchange.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Services.Xsl.X12-837-To-ClaimDocument.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            xml = new StreamReader(outputStream).ReadToEnd();

            return ClaimDocument.Deserialize(xml);
        }

    }
}
