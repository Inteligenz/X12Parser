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
            string xml = parser.Parse(fs).Serialize();
            fs.Close();
            
            FileStream outputFs = new FileStream(outputFilename, FileMode.Create);
            StreamWriter writer = new StreamWriter(outputFs);
            writer.Write(xml);
            writer.Close();
        }
    }
}
