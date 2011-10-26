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
            IClaimToClaimFormTransfomation dentalTransformation
            )
        {
            _professionalTransformation = professionalTransformation;
            _institutionalTransformation = institutionalTransformation;
            _dentalTransformation = dentalTransformation;
        }


        public string TransformClaimDocumentToFoXml(ClaimDocument document)
        {
            FormDocument form = new FormDocument();

            foreach (var claim in document.Claims)
            {
                if (claim.Type == ClaimTypeEnum.Professional)
                    form.Pages.AddRange(_professionalTransformation.TransformClaimToClaimFormFoXml(claim));
                else if (claim.Type == ClaimTypeEnum.Institutional)
                    form.Pages.AddRange(_institutionalTransformation.TransformClaimToClaimFormFoXml(claim));
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
