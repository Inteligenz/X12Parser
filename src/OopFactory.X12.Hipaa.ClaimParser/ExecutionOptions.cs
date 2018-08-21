namespace OopFactory.X12.Hipaa.ClaimParser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    /// <summary>
    /// Represents a collection of additional application options
    /// </summary>
    public class ExecutionOptions
    {
        private readonly List<string> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionOptions"/> class
        /// </summary>
        /// <param name="args">Additional commandline arguments</param>
        public ExecutionOptions(string[] args)
        {
            this.Path = args.Length > 0 ? args[0] : Environment.CurrentDirectory;
            this.SearchPattern = args.Length > 1 ? args[1] : "*.*";
            this.OutputPath = args.Length > 2 ? args[2] : Environment.CurrentDirectory;
            this.options = new List<string>();

            for (int i = 3; i < args.Length; i++)
            {
                this.options.Add(args[i].ToUpper());
            }
        }

        /// <summary>
        /// Gets the input path for source files
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the file search pattern for finding valid source files
        /// </summary>
        public string SearchPattern { get; }

        /// <summary>
        /// Gets the output path for processed files
        /// </summary>
        public string OutputPath { get; }

        /// <summary>
        /// Gets the flag whether the X12 document should be converted to XML
        /// </summary>
        public bool MakeXml => !this.options.Contains("NOXML");

        /// <summary>
        /// Gets the flag whether the X12 document should be converted to PDF
        /// </summary>
        public bool MakePdf => !this.options.Contains("NOPDF"); 

        /// <summary>
        /// Gets the name of the log file for additional messages
        /// </summary>
        public string LogFile => ConfigurationManager.AppSettings["LogFile"];

        /// <summary>
        /// Prints a message to the console and logfile
        /// </summary>
        /// <param name="content">Message to be printed</param>
        public void WriteLine(string content)
        {
            string contents = $"{DateTime.Now}: {content}";
            Console.WriteLine(contents);
            File.AppendAllText(this.LogFile, contents);
        }
    }
}
