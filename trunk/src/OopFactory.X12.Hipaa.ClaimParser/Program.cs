using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using OopFactory.X12.Hipaa.Claims.Services;

namespace OopFactory.X12.Hipaa.ClaimParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            string searchPattern = args[1];
            string outputPath = args[2];

            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"),
                new InstitutionalClaimToUB04ClaimFormTransformation("UB04_Red.gif"),
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"));

            foreach (var filename in Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly))
            {
                FileStream inputFilestream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                var claimDoc = service.Transform837ToClaimDocument(inputFilestream);

                FileInfo fi = new FileInfo(filename);
                DirectoryInfo di = new DirectoryInfo(outputPath);

                string outputFilename = string.Format("{0}\\{1}.xml", di.FullName, fi.Name);

                File.WriteAllText(outputFilename, claimDoc.Serialize());

                outputFilename = string.Format("{0}\\{1}.pdf", di.FullName, fi.Name);
                XmlDocument foDoc = new XmlDocument();
                string foXml = service.TransformClaimDocumentToFoXml(claimDoc);
                foDoc.LoadXml(foXml);


                var driver = Fonet.FonetDriver.Make();
                FileStream pdfOutput = new FileStream(outputFilename, FileMode.Create, FileAccess.Write);
                driver.Render(foDoc, pdfOutput);
                
            }
        }
    }
}
