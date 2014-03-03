using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using OopFactory.X12.Parsing;

namespace OopFactory.X12.X12Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxBatchSize = 10 * 1012 * 1012; // 10 Mbytes
            if (ConfigurationManager.AppSettings["MaxBatchSize"] != null)
                maxBatchSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxBatchSize"]);

            bool throwException = Convert.ToBoolean(ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"]);
            
            string x12Filename = args[0];
            string outputFilename = args.Length > 1 ? args[1] : x12Filename + ".xml";

            OopFactory.X12.Parsing.X12Parser parser = new Parsing.X12Parser(throwException);
            parser.ParserWarning += new Parsing.X12Parser.X12ParserWarningEventHandler(parser_ParserWarning);
            
            byte[] header = new byte[6];
            using (FileStream fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
            {
                // peak at first 6 characters to determine if this is a unicode file
                fs.Read(header, 0, 6);
                fs.Close();
            }
            Encoding encoding = (header[1] == 0 && header[3] == 0 && header[5] == 0) ? Encoding.Unicode : Encoding.UTF8;
                
            if (new FileInfo(x12Filename).Length <= maxBatchSize)
            {
                using (FileStream fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
                {
                    var interchanges = parser.ParseMultiple(fs, encoding);
                    if (interchanges.Count >= 1)
                    {
                        using (FileStream outputFs = new FileStream(outputFilename, FileMode.Create))
                        {
                            interchanges.First().Serialize(outputFs);
                        }
                    }
                    if (interchanges.Count > 1)
                    {
                        for (int i = 1; i < interchanges.Count; i++)
                        {
                            outputFilename = string.Format("{0}_{1}.xml", args.Length > 1 ? args[1] : x12Filename, i + 1);
                            using (FileStream outputFs = new FileStream(outputFilename, FileMode.Create))
                            {
                                interchanges[i].Serialize(outputFs);
                            }
                        }
                    }
                }
            }
            else
            {
                using (FileStream fs = new FileStream(x12Filename, FileMode.Open, FileAccess.Read))
                {
                    // Break up output files by batch size
                    X12StreamReader reader = new X12StreamReader(fs, encoding);
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
                            outputFilename = string.Format("{0}_{1}.xml", args.Length > 1 ? args[1] : x12Filename, i++);
                            using (FileStream outputFs = new FileStream(outputFilename, FileMode.Create))
                            {
                                parser.ParseMultiple(currentTransactions.ToString()).First().Serialize(outputFs);
                            }
                            currentTransactions = nextTransaction;

                        }

                        nextTransaction = reader.ReadNextTransaction();
                    }

                    outputFilename = string.Format("{0}_{1}.xml", args.Length > 1 ? args[1] : x12Filename, i++);
                    using (FileStream outputFs = new FileStream(outputFilename, FileMode.Create))
                    {
                        parser.ParseMultiple(currentTransactions.ToString()).First().Serialize(outputFs);
                    }
                }
            }
        }

        static void parser_ParserWarning(object sender, X12ParserWarningEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
