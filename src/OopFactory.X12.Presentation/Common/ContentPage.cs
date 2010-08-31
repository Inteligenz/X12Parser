using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Presentation.Common
{
    public class ContentPage
    {
        public ContentPage()
        {
            FontName = "Courier";
            FontSize = 12f;
            XScale = 72 / 10; // 10 characters per inch
            YScale = 72 / 6; // 6 lines per inch
            if (Contents == null) Contents = new Dictionary<string, ContentLocation>();

        }
        public string FontName { get; set; }
        public float FontSize { get; set; }
        public float XScale { get; set; }
        public float YScale { get; set; }

        public Dictionary<string, ContentLocation> Contents { get; set; }

    }
}
