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

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Eligibility.Xsl.X12-271-To-BenefitResponse.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            var response = BenefitResponse.DeserializeList(new StreamReader(outputStream).ReadToEnd());
            return response;
        }

        public string TransformBenefitResponseToHtml(BenefitResponse response)
        {
            string xml = response.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Eligibility.Xsl.BenefitResponse-To-Html.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();
            var args = new XsltArgumentList();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            return new StreamReader(outputStream).ReadToEnd();
        }
    }
}
