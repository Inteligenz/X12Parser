using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace OopFactory.X12.Hipaa.ClaimParser
{
    public class ExecutionOptions
    {
        private List<string> _options;

        public ExecutionOptions(string[] args)
        {
            if (args.Length > 0)
                Path = args[0];
            else
                Path = System.Environment.CurrentDirectory;
            if (args.Length > 1)
                SearchPattern = args[1];
            else
                SearchPattern = "*.*";
            if (args.Length > 2)
                OutputPath = args[2];
            else
                OutputPath = System.Environment.CurrentDirectory;

            _options = new List<string>();
            for (int i = 3; i < args.Length; i++)
                _options.Add(args[i].ToUpper());
        }

        public string Path { get; private set; }
        public string SearchPattern { get; private set; }
        public string OutputPath { get; private set; }
        public bool MakeXml { get { return !_options.Contains("NOXML"); } }
        public bool MakePdf { get { return !_options.Contains("NOPDF"); } }
        public string LogFile
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFile"];
            }
        }

        public void WriteLine(string content)
        {
            string contents = string.Format("{0}: {1}\r\n", DateTime.Now, content);
            Console.WriteLine(contents);
            File.AppendAllText(LogFile, contents);
        }
    }
}
