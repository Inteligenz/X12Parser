namespace X12.X12Parser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;

    using X12.Parsing;
    using X12.Shared.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            int maxBatchSize = 10 * 1024 * 1024;
            if (ConfigurationManager.AppSettings["MaxBatchSize"] != null)
            {
                maxBatchSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxBatchSize"]);
            }

            bool throwException = Convert.ToBoolean(ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"]);
            
            string x12Filename = args[0];
            string outputFilename = args.Length > 1 ? args[1] : x12Filename + ".xml";

            var parser = new X12Parser(throwException);
            parser.ParserWarning += Parser_ParserWarning;
            
            var header = new byte[6];
            using (var fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
            {
                // peak at first 6 characters to determine if this is a unicode file
                fs.Read(header, 0, 6);
                fs.Close();
            }

            Encoding encoding = (header[1] == 0 && header[3] == 0 && header[5] == 0) ? Encoding.Unicode : Encoding.UTF8;
                
            if (new FileInfo(x12Filename).Length <= maxBatchSize)
            {
                using (var fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
                {
                    IList<Interchange> interchanges = parser.ParseMultiple(fs, encoding);
                    if (interchanges.Count >= 1)
                    {
                        using (var outputFs = new FileStream(outputFilename, FileMode.Create))
                        {
                            interchanges.First().Serialize(outputFs);
                        }
                    }

                    if (interchanges.Count > 1)
                    {
                        for (int i = 1; i < interchanges.Count; i++)
                        {
                            outputFilename = $"{(args.Length > 1 ? args[1] : x12Filename)}_{i + 1}.xml";
                            using (var outputFs = new FileStream(outputFilename, FileMode.Create))
                            {
                                interchanges[i].Serialize(outputFs);
                            }
                        }
                    }
                }
            }
            else
            {
                using (var fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
                {
                    // Break up output files by batch size
                    var reader = new X12StreamReader(fs, encoding);
                    X12FlatTransaction currentTransactions = reader.ReadNextTransaction();
                    X12FlatTransaction nextTransaction = reader.ReadNextTransaction();
                    int i = 1;
                    while (!string.IsNullOrEmpty(nextTransaction.Transactions.First()))
                    {
                        if (currentTransactions.GetSize() + nextTransaction.GetSize() < maxBatchSize
                            && currentTransactions.IsaSegment == nextTransaction.IsaSegment
                            && currentTransactions.GsSegment == nextTransaction.GsSegment)
                        {
                            currentTransactions.Transactions.AddRange(nextTransaction.Transactions);
                        }
                        else
                        {
                            outputFilename = $"{(args.Length > 1 ? args[1] : x12Filename)}_{i++}.xml";
                            using (var outputFs = new FileStream(outputFilename, FileMode.Create))
                            {
                                parser.ParseMultiple(currentTransactions.ToString()).First().Serialize(outputFs);
                            }

                            currentTransactions = nextTransaction;
                        }

                        nextTransaction = reader.ReadNextTransaction();
                    }

                    outputFilename = $"{(args.Length > 1 ? args[1] : x12Filename)}_{i++}.xml";
                    using (var outputFs = new FileStream(outputFilename, FileMode.Create))
                    {
                        parser.ParseMultiple(currentTransactions.ToString()).First().Serialize(outputFs);
                    }
                }
            }
        }

        static void Parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
