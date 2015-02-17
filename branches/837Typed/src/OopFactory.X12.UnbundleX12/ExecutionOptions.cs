using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OopFactory.X12.UnbundleX12
{
    public class ExecutionOptions
    {
        public ExecutionOptions()
        {
            InputDirectory = Environment.CurrentDirectory;
            FilenamePattern = "*.*";
            LoopId = "ST";
            OutputDirectory = Environment.CurrentDirectory;
            FormatString = "{0}\\{1}_{2:000}{3}";
            IncludeWhitespace = true;
        }

        public void LoadOptions(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException(@"UnbundleX12 expected 3 to 5 arguments: input filename, loop ID (or ST for transaction), output directory and an optional format string argument and include whitespace flag.  
Example1: UnbundleX12 c:\\MyEdiFile.txt 2300 c:\\Output {0}\\{1}_{2:000}{3} false
Example2: UnbundleX12 c:\\Inbound\*.edi ST c:\\Output");
            }

            if (args.Length > 0)
            {
                int endDirectoryIndex = args[0].LastIndexOf('\\');
                int endDirectoryIndex2 = args[0].LastIndexOf('/');
                if (endDirectoryIndex > 0)
                {
                    InputDirectory = args[0].Substring(0, endDirectoryIndex);
                    FilenamePattern = args[0].Substring(endDirectoryIndex + 1);
                }
                else if (endDirectoryIndex2 > 0)
                {
                    InputDirectory = args[0].Substring(0, endDirectoryIndex2);
                    FilenamePattern = args[0].Substring(endDirectoryIndex2 + 1);
                }
                else
                    FilenamePattern = args[0];
            }

            if (Directory.GetFiles(InputDirectory, FilenamePattern).Length == 0)
                throw new ArgumentException("Files do not exist with search criteria " + args[0]);

            if (args.Length > 1)
                LoopId = args[1];

            if (args.Length > 2)
                OutputDirectory = args[2];

            if (!Directory.Exists(OutputDirectory))
                throw new ArgumentException(string.Format("Directory {0} does not exist!", OutputDirectory));

            if (args.Length > 3)
                FormatString = args[3];

            if (args.Length > 4)
                IncludeWhitespace = bool.Parse(args[4]);
        }

        public string InputDirectory { get; private set; }

        public string FilenamePattern { get; private set; }

        public string LoopId { get; private set; }

        public string OutputDirectory { get; private set; }

        public string FormatString { get; private set; }

        public bool IncludeWhitespace { get; private set; }
    }
}
