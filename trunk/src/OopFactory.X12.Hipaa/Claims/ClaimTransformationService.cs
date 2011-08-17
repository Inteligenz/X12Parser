using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ClaimTransformationService
    {
        public string TransformX12837ToUB04Model(Stream stream)
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(stream);
            string xml = interchange.Serialize();

            Stream transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Forms.Institutional.X12-837I-To-UB04Model.xslt");
            
            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(transformStream));

            MemoryStream outputStream = new MemoryStream();
            
            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            return new StreamReader(outputStream).ReadToEnd();
        }


        
    }
}
