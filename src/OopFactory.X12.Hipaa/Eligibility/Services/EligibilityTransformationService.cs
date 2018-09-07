namespace OopFactory.X12.Hipaa.Eligibility.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Xsl;

    using OopFactory.X12.Parsing;
    using OopFactory.X12.Shared.Models;

    /// <summary>
    /// Provides methods for transforming data to and from their Eligibility benefit objects
    /// </summary>
    public class EligibilityTransformationService
    {
        /// <summary>
        /// Transforms data from a stream in 271 format into its <see cref="EligibilityBenefitDocument"/> representation
        /// </summary>
        /// <param name="stream">Stream containing 271 data to be transformed</param>
        /// <returns>Object transformed from stream</returns>
        public EligibilityBenefitDocument Transform271ToBenefitResponse(Stream stream)
        {
            EligibilityBenefitDocument fullResponse = new EligibilityBenefitDocument();

            var parser = new X12Parser();
            IList<Interchange> interchanges = parser.ParseMultiple(stream);
            foreach (var interchange in interchanges)
            {
                string xml = interchange.Serialize();
                Stream transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Eligibility.Services.Xsl.X12-271-To-BenefitResponse.xslt");
                var transform = new XslCompiledTransform();

                if (transformStream != null)
                {
                    transform.Load(XmlReader.Create(transformStream));
                }

                var outputStream = new MemoryStream();

                transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
                outputStream.Position = 0;
                string responseXml = new StreamReader(outputStream).ReadToEnd();
                EligibilityBenefitDocument response = EligibilityBenefitDocument.Deserialize(responseXml);
                fullResponse.EligibilityBenefitInquiries.AddRange(response.EligibilityBenefitInquiries);
                fullResponse.EligibilityBenefitResponses.AddRange(response.EligibilityBenefitResponses);
                fullResponse.RequestValidations.AddRange(response.RequestValidations);
            }

            return fullResponse;
        }

        /// <summary>
        /// Transforms a provided <see cref="EligibilityBenefitResponse"/> to its HTML string representation
        /// </summary>
        /// <param name="response">Benefit response to transform</param>
        /// <returns>HTML compliant string representation</returns>
        public string TransformBenefitResponseToHtml(EligibilityBenefitResponse response)
        {
            string xml = response.Serialize();
            Stream transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Eligibility.Services.Xsl.BenefitResponse-To-Html.xslt");
            var transform = new XslCompiledTransform();

            if (transformStream != null)
            {
                transform.Load(XmlReader.Create(transformStream));
            }

            var outputStream = new MemoryStream();
            var args = new XsltArgumentList();

            transform.Transform(XmlReader.Create(new StringReader(xml)), args, outputStream);
            outputStream.Position = 0;
            return new StreamReader(outputStream).ReadToEnd();
        }
    }
}
