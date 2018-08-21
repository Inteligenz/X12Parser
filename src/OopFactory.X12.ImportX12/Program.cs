namespace OopFactory.X12.ImportX12
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using OopFactory.X12.Parsing;
    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Sql;

    /// <summary>
    /// Primary driver for the ImportX12 application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the ImportX12 driver
        /// </summary>
        public static void Main()
        {
            string dsn = ConfigurationManager.ConnectionStrings["X12"].ConnectionString;
            
            bool throwExceptionOnSyntaxErrors = ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"] == "true";
            string[] segments = ConfigurationManager.AppSettings["IndexedSegments"].Split(',');
            string parseDirectory = ConfigurationManager.AppSettings["ParseDirectory"];
            string parseSearchPattern = ConfigurationManager.AppSettings["ParseSearchPattern"];
            string archiveDirectory = ConfigurationManager.AppSettings["ArchiveDirectory"];
            string failureDirectory = ConfigurationManager.AppSettings["FailureDirectory"];
            string sqlDateType = ConfigurationManager.AppSettings["SqlDateType"];
            int segmentBatchSize = Convert.ToInt32(ConfigurationManager.AppSettings["SqlSegmentBatchSize"]);

            var specFinder = new SpecificationFinder();
            var parser = new X12Parser(throwExceptionOnSyntaxErrors);
            parser.ParserWarning += Parser_ParserWarning;
            var repo = new SqlTransactionRepository(
                dsn,
                specFinder,
                segments,
                typeof(int),
                ConfigurationManager.AppSettings["schema"],
                ConfigurationManager.AppSettings["containerSchema"],
                segmentBatchSize,
                sqlDateType);

            foreach (var filename in Directory.GetFiles(parseDirectory, parseSearchPattern, SearchOption.AllDirectories))
            {
                var header = new byte[6];
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    // peak at first 6 characters to determine if this is a unicode file
                    fs.Read(header, 0, 6);
                    fs.Close();
                }

                Encoding encoding = (header[1] == 0 && header[3] == 0 && header[5] == 0) ? Encoding.Unicode : Encoding.UTF8;
                
                var fi = new FileInfo(filename);
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        var interchanges = parser.ParseMultiple(fs, encoding);
                
                        foreach (var interchange in interchanges)
                        {
                            repo.Save(interchange, filename, Environment.UserName);
                        }

                        if (!string.IsNullOrWhiteSpace(archiveDirectory))
                        {
                            MoveTo(fi, parseDirectory, archiveDirectory);
                        }
                    }
                    catch (Exception exc)
                    {
                        Trace.TraceError($"Error parsing {fi.FullName}: {exc.Message}\n{exc.StackTrace}");
                        if (!string.IsNullOrEmpty(failureDirectory))
                        {
                            MoveTo(fi, parseDirectory, failureDirectory);
                        }
                    }
                }
            }
        }

        private static void MoveTo(FileInfo fi, string sourceDirectory, string targetDirectory)
        {
            string targetFilename = string.Format("{0}{1}", targetDirectory, fi.FullName.Replace(sourceDirectory, ""));
            var targetFile = new FileInfo(targetFilename);
            try
            {
                if (!targetFile.Directory.Exists)
                {
                    targetFile.Directory.Create();
                }

                fi.MoveTo(targetFilename);
            }
            catch (Exception exc2)
            {
                Trace.TraceError($"Error moving {fi.FullName} to {targetFilename}: {exc2.Message}\n{exc2.StackTrace}");
            }
        }

        private static void Parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            Trace.TraceWarning($"Error parsing interchange {args.InterchangeControlNumber} at position {args.SegmentPositionInInterchange}: {args.Message}");
        }
    }
}
