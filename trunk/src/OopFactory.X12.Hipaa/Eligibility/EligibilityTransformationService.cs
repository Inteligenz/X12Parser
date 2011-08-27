using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public class EligibilityTransformationService
    {
        public List<BenefitResponse> Transform271ToBenefitResponse(Stream stream)
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
            var response = BenefitResponse.DeserializeList(new StreamReader(outputStream).ReadToEnd());
            return response;
        }
    }
}
