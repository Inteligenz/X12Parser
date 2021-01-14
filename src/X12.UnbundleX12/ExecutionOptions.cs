namespace X12.UnbundleX12
{
    using System;
    using System.IO;

    public class ExecutionOptions
    {
        public ExecutionOptions()
        {
            this.InputDirectory = Environment.CurrentDirectory;
            this.FilenamePattern = "*.*";
            this.LoopId = "ST";
            this.OutputDirectory = Environment.CurrentDirectory;
            this.FormatString = "{0}\\{1}_{2:000}{3}";
            this.IncludeWhitespace = true;
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
                    this.InputDirectory = args[0].Substring(0, endDirectoryIndex);
                    this.FilenamePattern = args[0].Substring(endDirectoryIndex + 1);
                }
                else if (endDirectoryIndex2 > 0)
                {
                    this.InputDirectory = args[0].Substring(0, endDirectoryIndex2);
                    this.FilenamePattern = args[0].Substring(endDirectoryIndex2 + 1);
                }
                else
                {
                    this.FilenamePattern = args[0];
                }
            }

            if (Directory.GetFiles(this.InputDirectory, this.FilenamePattern).Length == 0)
            {
                throw new ArgumentException("Files do not exist with search criteria " + args[0]);
            }

            if (args.Length > 1)
            {
                this.LoopId = args[1];
            }

            if (args.Length > 2)
            {
                this.OutputDirectory = args[2];
            }

            if (!Directory.Exists(this.OutputDirectory))
            {
                throw new ArgumentException($"Directory {this.OutputDirectory} does not exist!");
            }

            if (args.Length > 3)
            {
                this.FormatString = args[3];
            }

            if (args.Length > 4)
            {
                this.IncludeWhitespace = bool.Parse(args[4]);
            }
        }

        public string InputDirectory { get; private set; }

        public string FilenamePattern { get; private set; }

        public string LoopId { get; private set; }

        public string OutputDirectory { get; private set; }

        public string FormatString { get; private set; }

        public bool IncludeWhitespace { get; private set; }
    }
}
