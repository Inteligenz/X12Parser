namespace X12.UnbundleX12
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using X12.Parsing;
    using X12.Shared.Models;

    /// <summary>
    /// Primary driver for the UnbundleX12 library
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the driver
        /// </summary>
        /// <param name="args">Additional arguments for program options</param>
        public static void Main(string[] args)
        {
            var opts = new ExecutionOptions();
            try
            {
                opts.LoadOptions(args);
            }
            catch (ArgumentException exc)
            {
                Console.Write(exc.Message);
                return;
            }

            var parser = new X12Parser();
            
            foreach (var filename in Directory.GetFiles(opts.InputDirectory, opts.FilenamePattern))
            {
                var inputFile = new FileInfo(filename);
                var list = new List<Interchange>();

                using (var fs = new FileStream(inputFile.FullName, FileMode.Open, FileAccess.Read))
                {
                    var reader = new X12StreamReader(fs, Encoding.UTF8);
                    X12FlatTransaction transaction = reader.ReadNextTransaction();
                    while (!string.IsNullOrEmpty(transaction.Transactions.First()))
                    {
                        string x12 = transaction.ToString();
                        Interchange interchange = parser.ParseMultiple(x12).First();
                        if (opts.LoopId == "ST")
                        {
                            list.Add(interchange);
                        }
                        else
                        {
                            list.AddRange(parser.UnbundleByLoop(interchange, opts.LoopId));
                        }

                        transaction = reader.ReadNextTransaction();
                    }
                }
                
                for (int i = 0; i < list.Count; i++)
                {
                    string outputFilename = string.Format(opts.FormatString, opts.OutputDirectory, inputFile.Name, i + 1, inputFile.Extension);
                    
                    using (var outputFilestream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                    using (var writer = new StreamWriter(outputFilestream))
                    {
                        writer.Write(list[i].SerializeToX12(opts.IncludeWhitespace));
                    }
                }
            }
        }
    }
}
