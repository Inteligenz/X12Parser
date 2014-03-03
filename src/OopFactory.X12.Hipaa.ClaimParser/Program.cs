using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Configuration;
using OopFactory.X12.Parsing;
using OopFactory.X12.Hipaa.Claims.Services;

namespace OopFactory.X12.Hipaa.ClaimParser
{
    class Program
    {
        static void Main(string[] args)
        {
            bool throwException = Convert.ToBoolean(ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"]);

            var opts = new ExecutionOptions(args);
            InstitutionalClaimToUB04ClaimFormTransformation institutionalClaimToUB04ClaimFormTransformation = new InstitutionalClaimToUB04ClaimFormTransformation("UB04_Red.gif");
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"),
                institutionalClaimToUB04ClaimFormTransformation,
                new DentalClaimToJ400FormTransformation("ADAJ400_Red.gif"),
                new X12Parser(throwException));

            foreach (var filename in Directory.GetFiles(opts.Path, opts.SearchPattern, SearchOption.TopDirectoryOnly))
            {
                try
                {
#if DEBUG
                    FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    var parser = new X12.Parsing.X12Parser();
                    var interchange = parser.ParseMultiple(stream).First();
                    File.WriteAllText(filename + ".dat", interchange.SerializeToX12(true));
                    stream.Close();
#endif           
                    DateTime start = DateTime.Now;
                    FileStream inputFilestream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    
                    Dictionary<string,string> revenueDictionary=new Dictionary<string, string>();
                    revenueDictionary["0572"] = "Test Code";
                    service.FillRevenueCodeDescriptionMapping(revenueDictionary);
                    var claimDoc = service.Transform837ToClaimDocument(inputFilestream);
                    institutionalClaimToUB04ClaimFormTransformation.PerPageTotalChargesView = true;
                    FileInfo fi = new FileInfo(filename);
                    DirectoryInfo di = new DirectoryInfo(opts.OutputPath);

                    if (opts.MakeXml)
                    {
                        string outputFilename = string.Format("{0}\\{1}.xml", di.FullName, fi.Name);

                        string xml = claimDoc.Serialize();
                        xml = xml.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                        File.WriteAllText(outputFilename, xml);
                    }

                    if (opts.MakePdf)
                    {
                        string outputFilename = string.Format("{0}\\{1}.pdf", di.FullName, fi.Name);
                        using (FileStream pdfOutput = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                        {
                            XmlDocument foDoc = new XmlDocument();
                            string foXml = service.TransformClaimDocumentToFoXml(claimDoc);
                            foDoc.LoadXml(foXml);

                            var driver = Fonet.FonetDriver.Make();
                            driver.Render(foDoc, pdfOutput);
                            pdfOutput.Close();
                        }
                    }

                    opts.WriteLine(string.Format("{0} parsed in {1}.", filename, DateTime.Now - start));
                }
                catch (Exception exc)
                {
                    opts.WriteLine(string.Format("Exception occurred: {0}.  {1}.  {2}", exc.GetType().FullName, exc.Message, exc.StackTrace));
                }
            }
        }
    }
}
