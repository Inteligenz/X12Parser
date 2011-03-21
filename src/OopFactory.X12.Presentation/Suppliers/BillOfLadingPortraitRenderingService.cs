using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;
using Fonet.Render;
using System.IO;
using OopFactory.X12.Presentation.Model;

namespace OopFactory.X12.Presentation.Suppliers
{
    public class BillOfLadingPortraitRenderingService : IRenderingService
    {
        private TransformationService _foService;
        private string _bolPortraitImageFilename;

        public BillOfLadingPortraitRenderingService()
        {
            _foService = new TransformationService();
            var name =  Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            var path = Path.GetDirectoryName(name);
            _bolPortraitImageFilename = String.Format("{0}\\Images\\VISC_BOL_Portrait.gif", path);
        }

        public byte[] CreatePdf(string domainXml)
        {
            return _foService.CreatePdf(this.TransformToFo(domainXml));
        }

        private FoDocument TransformToFo(string domainXml)
        {
            var foDoc = new FoDocument();
            foDoc.PageMasters.Add(new FoPageMaster
            {
                Name = "Portrait",
                Width = 8.5m,
                Height = 11m,
                MarginLeft = 0.25m,
                MarginTop = 0.75m,
                MarginRight = 0.25m,
                BackgroundImageUri = _bolPortraitImageFilename,
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
            foreach (XmlElement transactionElement in doc.SelectNodes("/ArrayOfTransaction/Transaction"))
            {
                DateTime shippedDate;
                DateTime.TryParse(transactionElement.ValueOf("DateReference[@Qualifier='011']"), out shippedDate);

                foreach (XmlElement shipmentElement in transactionElement.SelectNodes("Shipment"))
                {

                    TransformShipment(shippedDate, shipmentElement, foDoc);
                }
            }
            return foDoc;
        }

        private void TransformShipment(DateTime shippedDate, XmlElement element, FoDocument foDoc)
        {
            FoPageSequence firstPage = new FoPageSequence { MasterReference = "Portrait" };

            firstPage.AddField(shippedDate.ToShortDateString(), 1, 6, 11);
            firstPage.AddField("1", 1, 72, 5).WithAlign("right");
            TransformAddress((XmlElement)element.SelectSingleNode("Party[Type/@Code='SF']"), firstPage, 3.3m, 11);
            TransformAddress((XmlElement)element.SelectSingleNode("Party[Type/@Code='ST']"), firstPage, 8.4m, 11);



            foDoc.PageSequences.Add(firstPage);
            
        }

        private void TransformAddress(XmlElement element, FoPageSequence page, decimal top, decimal left)
        {
            page.AddField(element.ValueOf("Name"),
                top, left, 33.5m);
            page.AddField((element.ValueOf("Address/Line1") + " " + element.ValueOf("Address/Line2")).TrimEnd(),
                top + 1, left, 33.5m);
            page.AddField(String.Format("{0} {1}, {2}", element.ValueOf("Address/City"), element.ValueOf("Address/@StateCode"), element.ValueOf("Address/@PostalCode")),
                top + 2, left, 33.5m);
            page.AddField(element.ValueOf("Identification"),
                top + 3, left, 33.5m);

        }

    }
}
