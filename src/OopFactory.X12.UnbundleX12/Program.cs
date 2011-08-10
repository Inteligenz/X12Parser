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
            if (args.Length < 3)
            {
                Console.WriteLine("UnbundleX12 expected 3 to 5 arguments: input filename, loop ID, output directory and an optional format string argument and include whitespace flag");
                Console.WriteLine("Example: UnbundleX12 c:\\MyEdiFile.txt 2300 c:\\Output {0}\\{1}_{2:000}{3} false");
                return;
            }
            string filename = args[0];
            string loopId = args[1];
            string outputDirectory = args[2];
            string formatString = "{0}\\{1}_{2:000}{3}";
            bool includeWhitespace = true;

            if (!File.Exists(filename))
            {
                Console.WriteLine("Filename {0} does not exist!", filename);
                return;
            }
            if (loopId.Length < 3)
            {
                Console.WriteLine("Loop IDs are expected to be at least 3 characters.");
                return;
            }
            if (args.Length >= 5)
            {
                includeWhitespace = bool.Parse(args[4]);
            }
            if (!Directory.Exists(outputDirectory))
            {
                Console.WriteLine("Directory {0} does not exist!", outputDirectory);
                return;
            }
            if (args.Length >= 4)
                formatString = args[3];

            FileInfo inputFile = new FileInfo(filename);
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.Parse(new FileStream(filename, FileMode.Open, FileAccess.Read));

            var list = parser.UnbundleByLoop(interchange, loopId);
            for (int i=0; i<list.Count; i++)
            {
                string outputFilename = String.Format(formatString, outputDirectory, inputFile.Name, i + 1, inputFile.Extension);
                using (FileStream outputFilestream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(outputFilestream))
                    {
                        writer.Write(list[i].SerializeToX12(includeWhitespace));
                        writer.Close();
                    }
                    outputFilestream.Close();
                }
            }
        }
    }
}
