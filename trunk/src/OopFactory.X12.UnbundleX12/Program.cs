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


            foreach (var filename in Directory.GetFiles(opts.InputDirectory, opts.FilenamePattern))
            {
                FileInfo inputFile = new FileInfo(filename);

                X12Parser parser = new X12Parser();
                List<Interchange> interchanges = parser.ParseMultiple(new FileStream(filename, FileMode.Open, FileAccess.Read));
                int offset = 0;
                foreach (var interchange in interchanges)
                {
                    List<Interchange> list;
                    if (opts.LoopId == "ST")
                        list = parser.UnbundleByTransaction(interchange);
                    else
                        list = parser.UnbundleByLoop(interchange, opts.LoopId);

                    for (int i = 0; i < list.Count; i++)
                    {
                        string outputFilename = String.Format(opts.FormatString, opts.OutputDirectory, inputFile.Name, offset + i + 1, inputFile.Extension);
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
                    offset += list.Count;

                }
            }
        }
    }
}
