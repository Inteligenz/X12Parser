namespace OopFactory.X12.Hipaa.Claims.Services
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Xsl;

    using OopFactory.X12.Hipaa.Claims.Forms;
    using OopFactory.X12.Parsing;

    public class ClaimFormTransformationService : ClaimTransformationService
    {
        private readonly IClaimToClaimFormTransfomation professionalTransformation;
        private readonly IClaimToClaimFormTransfomation institutionalTransformation;
        private readonly IClaimToClaimFormTransfomation dentalTransformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimFormTransformationService"/> class
        /// </summary>
        /// <param name="professionalTransformation">Transformer for professional claims</param>
        /// <param name="institutionalTransformation">Transformer for institutional claims</param>
        /// <param name="dentalTransformation">transformer for dental claims</param>
        /// <param name="parser">X12 document parser</param>
        public ClaimFormTransformationService(
            IClaimToClaimFormTransfomation professionalTransformation,
            IClaimToClaimFormTransfomation institutionalTransformation,
            IClaimToClaimFormTransfomation dentalTransformation,
            X12Parser parser)
            : base(parser)
        {
            this.professionalTransformation = professionalTransformation;
            this.institutionalTransformation = institutionalTransformation;
            this.dentalTransformation = dentalTransformation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimFormTransformationService"/> class
        /// </summary>
        /// <param name="professionalTransformation">Transformer for professional claims</param>
        /// <param name="institutionalTransformation">Transformer for institutional claims</param>
        /// <param name="dentalTransformation">transformer for dental claims</param>
        public ClaimFormTransformationService(
            IClaimToClaimFormTransfomation professionalTransformation,
            IClaimToClaimFormTransfomation institutionalTransformation,
            IClaimToClaimFormTransfomation dentalTransformation)
            : this(professionalTransformation, institutionalTransformation, dentalTransformation, new X12Parser())
        {
        }

        /// <summary>
        /// Transforms document to XML
        /// </summary>
        /// <param name="document">Document to be transformed</param>
        /// <returns>String XML representation of XML document</returns>
        /// <exception cref="ArgumentNullException">Thrown if document passed is null</exception>
        public string TransformClaimDocumentToFoXml(ClaimDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), "Invalid claim document provided");
            }

            var form = new FormDocument();

            foreach (var claim in document.Claims)
            {
                if (claim.Type == ClaimTypeEnum.Professional)
                {
                    var pages = this.professionalTransformation.TransformClaimToClaimFormFoXml(claim);
                    form.Pages.AddRange(pages);
                }
                else if (claim.Type == ClaimTypeEnum.Institutional)
                {
                    var pages = this.institutionalTransformation.TransformClaimToClaimFormFoXml(claim);
                    form.Pages.AddRange(pages);
                }
                else
                {
                    form.Pages.AddRange(this.dentalTransformation.TransformClaimToClaimFormFoXml(claim));
                }
            }

            string xml = form.Serialize();
            Stream transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Services.Xsl.FormDocument-To-FoXml.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null)
            {
                transform.Load(XmlReader.Create(transformStream));
            }

            var outputStream = new MemoryStream();
            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            return new StreamReader(outputStream).ReadToEnd();
        }
    }
}
