using System;
using System.Collections.Generic;
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
            
            string x12Filename = args[0];
            string outputFilename = args.Length > 1 ? args[1] : x12Filename + ".xml";

            FileStream fs = new FileStream(x12Filename, FileMode.Open);
            OopFactory.X12.Parsing.X12Parser parser = new Parsing.X12Parser();
            var interchanges = parser.ParseMultiple(fs);
            fs.Close();
            if (interchanges.Count >= 1)
            {
                string xml = interchanges.First().Serialize();

                FileStream outputFs = new FileStream(outputFilename, FileMode.Create);
                StreamWriter writer = new StreamWriter(outputFs);
                writer.Write(xml);
                writer.Close();
            }
            if (interchanges.Count > 1)
            {
                for (int i = 1; i < interchanges.Count; i++)
                {
                    string xml = interchanges[i].Serialize();

                    outputFilename = string.Format("{0}_{1}.xml", args.Length > 1 ? args[1] : x12Filename, i + 1);
                    FileStream outputFs = new FileStream(outputFilename, FileMode.Create);
                    StreamWriter writer = new StreamWriter(outputFs);
                    writer.Write(xml);
                    writer.Close();

                }
            }
        }
    }
}
