using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.UnbundleX12
{
    class Program
    {
        static void Main(string[] args)
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

            X12Parser parser = new X12Parser();
            
            foreach (var filename in Directory.GetFiles(opts.InputDirectory, opts.FilenamePattern))
            {
                FileInfo inputFile = new FileInfo(filename);
                List<Interchange> list = new List<Interchange>();
                using (FileStream fs = new FileStream(inputFile.FullName, FileMode.Open, FileAccess.Read))
                {
                    X12StreamReader reader = new X12StreamReader(fs, Encoding.UTF8);
                    X12FlatTransaction transaction = reader.ReadNextTransaction();
                    while (!string.IsNullOrEmpty(transaction.Transactions.First()))
                    {
                        string x12 = transaction.ToString();
                        var interchange = parser.ParseMultiple(x12).First();
                        if (opts.LoopId == "ST")
                            list.Add(interchange);
                        else
                        {
                            list.AddRange(parser.UnbundleByLoop(interchange, opts.LoopId));
                        }
                        transaction = reader.ReadNextTransaction();
                    }
                }
                List<Interchange> interchanges = parser.ParseMultiple(new FileStream(filename, FileMode.Open, FileAccess.Read));
                for (int i = 0; i < list.Count; i++)
                {
                    string outputFilename = String.Format(opts.FormatString, opts.OutputDirectory, inputFile.Name, i + 1, inputFile.Extension);
                    using (FileStream outputFilestream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(outputFilestream))
                        {
                            writer.Write(list[i].SerializeToX12(opts.IncludeWhitespace));
                            writer.Close();
                        }
                        outputFilestream.Close();
                    }
                }
            }
        }
    }
}
