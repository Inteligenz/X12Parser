using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OopFactory.X12.X12Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new X12ParsingService(true);

            string x12Filename = args[0];
            string outputFilename = args.Length > 1 ? args[1] : x12Filename + ".xml";

            FileStream fs = new FileStream(x12Filename, FileMode.Open);
            StreamReader reader = new StreamReader(fs);
            string xml = parser.ParseToDomainXml(reader.ReadToEnd());
            reader.Close();

            FileStream outputFs = new FileStream(outputFilename, FileMode.Create);
            StreamWriter writer = new StreamWriter(outputFs);
            writer.Write(xml);
            writer.Close();
        }
    }
}
