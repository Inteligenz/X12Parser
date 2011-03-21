using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using OopFactory.X12.Presentation.Model;

namespace OopFactory.X12.Presentation.Claims
{
    public class ClaimHcfa1500RenderingService : IRenderingService
    {
        private TransformationService _foService;
        private string _imageFilename;

        public ClaimHcfa1500RenderingService()
        {
            _foService = new TransformationService();
            var name = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            var path = Path.GetDirectoryName(name);
            _imageFilename = String.Format("{0}\\Images\\HCFA1500_Red.gif", path);
        }

        public byte[] CreatePdf(string domainXml)
        {
            return _foService.CreatePdf(this.TransformToFo(domainXml));
        }

        private FoDocument TransformToFo(string domainXml)
        {
            FoDocument foDoc = new FoDocument();
            foDoc.PageMasters.Add(new FoPageMaster
            {
                Name = "HCFA",
                Width = 8.5m,
                Height = 11m,
                MarginLeft = 0.25m,
                MarginTop = 0.75m,
                MarginRight = 0.25m,
                BackgroundImageUri = _imageFilename,
                BackgroundImageScaledToFit = true,
                XPointOffset = -4,
                YPointOffset = -10,
                XPointScale = 7.2m,
                YPointScale = 14.1m,
                FontFamily = "Courier",
                FontSize = 12
            });

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(domainXml);

            foDoc.PageSequences.Add(new FoPageSequence { MasterReference = "HCFA" });
            return foDoc;
        }
    }
}