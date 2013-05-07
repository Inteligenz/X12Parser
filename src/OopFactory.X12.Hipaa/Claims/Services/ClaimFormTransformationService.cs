using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Professional;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Parsing;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class ClaimFormTransformationService : ClaimTransformationService
    {
        IClaimToClaimFormTransfomation _professionalTransformation;
        IClaimToClaimFormTransfomation _institutionalTransformation;
        IClaimToClaimFormTransfomation _dentalTransformation;

        public ClaimFormTransformationService(
            IClaimToClaimFormTransfomation professionalTransformation,
            IClaimToClaimFormTransfomation institutionalTransformation,
            IClaimToClaimFormTransfomation dentalTransformation,
            X12Parser parser
            )
            : base(parser)
        {
            _professionalTransformation = professionalTransformation;
            _institutionalTransformation = institutionalTransformation;
            _dentalTransformation = dentalTransformation;
        }

        public ClaimFormTransformationService(
            IClaimToClaimFormTransfomation professionalTransformation,
            IClaimToClaimFormTransfomation institutionalTransformation,
            IClaimToClaimFormTransfomation dentalTransformation)
            : this(professionalTransformation, institutionalTransformation, dentalTransformation, new X12Parser())
        {
        }

        public string TransformClaimDocumentToFoXml(ClaimDocument document)
        {
            FormDocument form = new FormDocument();

            foreach (var claim in document.Claims)
            {
                if (claim.Type == ClaimTypeEnum.Professional)
                {
                    var pages = _professionalTransformation.TransformClaimToClaimFormFoXml(claim);
                    form.Pages.AddRange(pages);
                }
                else if (claim.Type == ClaimTypeEnum.Institutional)
                {
                    var pages = _institutionalTransformation.TransformClaimToClaimFormFoXml(claim);
                    form.Pages.AddRange(pages);
                }
                else
                    form.Pages.AddRange(_dentalTransformation.TransformClaimToClaimFormFoXml(claim));
            }

            var xml = form.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Services.Xsl.FormDocument-To-FoXml.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            return new StreamReader(outputStream).ReadToEnd();
        }
    }
}
