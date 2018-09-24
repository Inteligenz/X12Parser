namespace X12.Tests.Unit
{
    using System.IO;
    using System.Reflection;

    public static class Extensions
    {
        public static Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("X12.Tests.Unit.Parsing._SampleEdiFiles." + resourcePath);
        }

        public static void PrintToFile(this FileStream fs, string content)
        {
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(content);
            writer.Close();
            fs.Close();
        }
        public static void PrintHtmlToFile(this FileStream fs, string html)
        {
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("<html><body>");
            writer.WriteLine(html);
            writer.WriteLine("</body></html>");
            writer.Close();
            fs.Close();
        }
    }
}
