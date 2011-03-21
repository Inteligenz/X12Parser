using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Presentation.Model;
using System.Drawing;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Rendering
{
    [TestClass]
    public class FoDocumentSerializationTester
    {
        [TestMethod]
        public void SimpleSerializationTest()
        {
            FoDocument doc = new FoDocument();
            doc.PageMasters.Add(new FoPageMaster
            {
                Name = "Portrait",
                Height = 11,
                Width = 8.5m,
                MarginLeft = 0.5m,
                MarginTop = 0.5m,
                MarginRight = 0.5m,
                FontFamily = "Courier",
                FontSize = 12
            });

            var xml = doc.Serialize();
            Trace.Write(xml);
        }
    }
}
