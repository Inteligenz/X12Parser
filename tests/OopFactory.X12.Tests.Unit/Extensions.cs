using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OopFactory.X12.Tests.Unit
{
    public static class Extensions
    {
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
