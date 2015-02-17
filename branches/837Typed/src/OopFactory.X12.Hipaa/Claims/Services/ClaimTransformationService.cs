using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class ClaimTransformationService
    {
        private X12Parser _parser;
        private Dictionary<string, string> _revenueCodeToDescriptionMapping; 

        public ClaimTransformationService(X12Parser parser)
        {
            _parser = parser;
        }

        public ClaimTransformationService()
            : this(new X12Parser())
        {
        }
  
        /// <summary>
        /// Reads a claim that has been st
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ClaimDocument Transform837ToClaimDocument(Stream stream)
        {

            var interchanges = _parser.ParseMultiple(stream);
            ClaimDocument doc = new ClaimDocument();
            foreach (var interchange in interchanges)
            {
                var thisDoc = Transform837ToClaimDocument(interchange);
                AddRevenueCodeDescription(thisDoc);
                doc.Claims.AddRange(thisDoc.Claims);
            }
            return doc;
        }

        private void AddRevenueCodeDescription(ClaimDocument claimdoc)
        {
            if (_revenueCodeToDescriptionMapping == null)
            {
                return;
            }
            
             foreach (Claim claim in claimdoc.Claims)
             {
                 foreach (ServiceLine serviceLine in claim.ServiceLines)
                 {
                     if (serviceLine.RevenueCode != null)
                     {
                         if (_revenueCodeToDescriptionMapping.ContainsKey(serviceLine.RevenueCode))
                         {
                             serviceLine.RevenueCodeDescription = _revenueCodeToDescriptionMapping[serviceLine.RevenueCode];
                         }
                     }
                 }
             }
        }

        public void FillRevenueCodeDescriptionMapping(Dictionary<string,string> revCodeDictionary )
        {
            _revenueCodeToDescriptionMapping = revCodeDictionary;
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
