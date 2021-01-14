namespace X12.Tests.Unit
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Common extensions used by the testing library
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets an EDI file given a resource path string
        /// </summary>
        /// <param name="resourcePath">Path to EDI file</param>
        /// <returns>Stream for the EDI file found at the resource path</returns>
        public static Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles." + resourcePath);
        }

        /// <summary>
        /// Writes the string content to a file
        /// </summary>
        /// <param name="fs">FileStream used for writing the content</param>
        /// <param name="content">String data to be written</param>
        public static void PrintToFile(this FileStream fs, string content)
        {
            var writer = new StreamWriter(fs);
            writer.WriteLine(content);
            writer.Close();
            fs.Close();
        }

        /// <summary>
        /// Writes an HTML string using a given filestream, adding structural tags for the document
        /// </summary>
        /// <param name="fs">FileStream to use for writing</param>
        /// <param name="html">HTML string to be written</param>
        public static void PrintHtmlToFile(this FileStream fs, string html)
        {
            var writer = new StreamWriter(fs);
            writer.WriteLine("<html><body>");
            writer.WriteLine(html);
            writer.WriteLine("</body></html>");
            writer.Close();
            fs.Close();
        }
    }
}
