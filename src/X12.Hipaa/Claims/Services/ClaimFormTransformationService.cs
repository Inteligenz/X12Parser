namespace X12.Hipaa.Claims.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Xsl;

    using X12.Hipaa.Claims.Forms;
    using X12.Hipaa.Enums;
    using X12.Hipaa.Properties;
    using X12.Parsing;

    /// <summary>
    /// Provides <see cref="ClaimDocument"/> transformation to XML capability
    /// </summary>
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
                throw new ArgumentNullException(nameof(document), Resources.InvalidClaimDocumentError);
            }

            var form = new FormDocument();

            foreach (var claim in document.Claims)
            {
                IList<FormPage> pages;

                switch (claim.Type)
                {
                    case ClaimType.Professional:
                        pages = this.professionalTransformation.TransformClaimToClaimFormFoXml(claim);
                        form.Pages.AddRange(pages);
                        break;
                    case ClaimType.Institutional:
                        pages = this.institutionalTransformation.TransformClaimToClaimFormFoXml(claim);
                        form.Pages.AddRange(pages);
                        break;
                    case ClaimType.Dental:
                        pages = this.dentalTransformation.TransformClaimToClaimFormFoXml(claim);
                        form.Pages.AddRange(pages);
                        break;
                    default:
                        // If we get here, then something went extremely wrong
                        throw new InvalidOperationException(Resources.InvalidClaimTypeError);
                }
            }

            string xml = form.Serialize();
            Stream transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Hipaa.Claims.Services.Xsl.FormDocument-To-FoXml.xslt");

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
