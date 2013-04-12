using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;
using System.IO;
using System.Diagnostics;
namespace OopFactory.X12.ImportX12
{
    class Program
    {
        static void Main(string[] args)
        {
            string dsn = ConfigurationManager.ConnectionStrings["X12"].ConnectionString;
            
            bool throwExceptionOnSyntaxErrors = ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"] == "true";
            string[] segments = ConfigurationManager.AppSettings["IndexedSegments"].Split(',');
            string parseDirectory = ConfigurationManager.AppSettings["ParseDirectory"];
            string parseSearchPattern = ConfigurationManager.AppSettings["ParseSearchPattern"];
            string archiveDirectory = ConfigurationManager.AppSettings["ArchiveDirectory"];

            var specFinder = new SpecificationFinder();
            var parser = new X12Parser(throwExceptionOnSyntaxErrors);
            parser.ParserWarning += new X12Parser.X12ParserWarningEventHandler(parser_ParserWarning);
            var repo = new SqlTransactionRepository(dsn, specFinder, segments, ConfigurationManager.AppSettings["schema"]);

            foreach (var filename in Directory.GetFiles(parseDirectory, parseSearchPattern, SearchOption.AllDirectories))
            {
                var fi = new FileInfo(filename);
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        var interchanges = parser.ParseMultiple(fs);
                
                        foreach (var interchange in interchanges)
                        {
                            repo.Save(interchange, filename, Environment.UserName);
                        }
                        if (!string.IsNullOrWhiteSpace(archiveDirectory))
                            fi.MoveTo(archiveDirectory);
                    }
                    catch (Exception exc)
                    {
                        Trace.TraceError("Error parsing {0}: {1}\n{2}", fi.FullName, exc.Message, exc.StackTrace);

                    }
                }
            }

        }

        static void parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            Trace.TraceWarning("Error parsing interchange {0} at position {1}: {2}", args.InterchangeControlNumber, args.SegmentPositionInInterchange, args.Message);
        }
    }
}
