using System;
using System.IO;

namespace OopFactory.X12.TransformToX12
{
    class Program
    {
        static void Main(string[] args)
        {
            string x12Filename = args[0];
            string outputFilename = args.Length > 1 ? args[1] : x12Filename + ".edi";
            if (x12Filename.Length == 0)
            {
                Console.WriteLine("There was an error reading the input file argument. Please check the path and filename.");
                return;
            }
            var fs = new StreamReader(x12Filename);
            string xmltext = fs.ReadToEnd();
            var parser = new Parsing.X12Parser();
            string x12 = string.Empty;
            try
            {
                x12 = parser.TransformToX12(xmltext);            
            }
            catch (Exception ex)
            {
                Console.WriteLine("The transformation encountered a problem: " + ex.Message + " -> " +ex.InnerException);
            }
            finally
            {
                fs.Close();
            }

            var outputFs = new FileStream(outputFilename, FileMode.Create);
            var writer = new StreamWriter(outputFs);
            writer.Write(x12);
            writer.Close();
        }
    }
}
