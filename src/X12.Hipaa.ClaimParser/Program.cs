namespace X12.Hipaa.ClaimParser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    using Fonet;

    using X12.Hipaa.Claims.Services;
    using X12.Parsing;

    /// <summary>
    /// Primary driver for the Hippa.ClaimParser
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the Hippa.ClaimParser driver
        /// </summary>
        /// <param name="args">Additional arguments for driver options</param>
        public static void Main(string[] args)
        {
            bool throwException = Convert.ToBoolean(ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"]);

            var opts = new ExecutionOptions(args);
            var institutionalClaimToUb04ClaimFormTransformation = new InstitutionalClaimToUb04ClaimFormTransformation("UB04_Red.gif");
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"),
                institutionalClaimToUb04ClaimFormTransformation,
                new DentalClaimToJ400FormTransformation("ADAJ400_Red.gif"),
                new X12Parser(throwException));

            foreach (var filename in Directory.GetFiles(opts.Path, opts.SearchPattern, SearchOption.TopDirectoryOnly))
            {
                try
                {
#if DEBUG
                    var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    var parser = new X12Parser();
                    var interchange = parser.ParseMultiple(stream).First();
                    File.WriteAllText(filename + ".dat", interchange.SerializeToX12(true));
                    stream.Close();
#endif           
                    DateTime start = DateTime.Now;
                    var inputFilestream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                    var revenueDictionary = new Dictionary<string, string>
                    {
                        ["0572"] = "Test Code"
                    };
                    service.FillRevenueCodeDescriptionMapping(revenueDictionary);
                    var claimDoc = service.Transform837ToClaimDocument(inputFilestream);
                    institutionalClaimToUb04ClaimFormTransformation.PerPageTotalChargesView = true;
                    var fi = new FileInfo(filename);
                    var di = new DirectoryInfo(opts.OutputPath);

                    if (opts.MakeXml)
                    {
                        string outputFilename = $"{di.FullName}\\{fi.Name}.xml";
                        string xml = claimDoc.Serialize();
                        xml = xml.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                        File.WriteAllText(outputFilename, xml);
                    }

                    if (opts.MakePdf)
                    {
                        string outputFilename = $"{di.FullName}\\{fi.Name}.pdf";
                        using (FileStream pdfOutput = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                        {
                            var foDoc = new XmlDocument();
                            string foXml = service.TransformClaimDocumentToFoXml(claimDoc);
                            foDoc.LoadXml(foXml);

                            FonetDriver driver = FonetDriver.Make();
                            driver.Render(foDoc, pdfOutput);
                            pdfOutput.Close();
                        }
                    }

                    opts.WriteLine($"{filename} parsed in {DateTime.Now - start}.");
                }
                catch (Exception exc)
                {
                    opts.WriteLine($"Exception occurred: {exc.GetType().FullName}.  {exc.Message}.  {exc.StackTrace}");
                }
            }
        }
    }
}
