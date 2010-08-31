using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OopFactory.X12.Presentation.Common
{
    public class PdfRenderingService
    {
        private Stream _outputStream;
        private Document _doc;
        private PdfWriter _writer;
        private string _imagePath;
        private iTextSharp.text.Image _backgroundImage;

        public PdfRenderingService(Stream stream)
        {
            _outputStream = stream;
            _doc = new Document();

            _writer = PdfWriter.GetInstance(_doc, _outputStream);
            _doc.Open();
        }

        public float XOffset { get; set; }
        public float YOffset { get; set; }

        public string BackgroundImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                _backgroundImage = Image.GetInstance(_imagePath);
                _backgroundImage.SetAbsolutePosition(0, 72);
                _backgroundImage.ScaleToFit(72 * 8.22f, 72 * 10.5f);
            }
        }

        public void AddPage(ContentPage content)
        {
            if (_backgroundImage != null)
            {
                _doc.Add(_backgroundImage);
            }

            PdfContentByte cb = _writer.DirectContent;
            cb.SetFontAndSize(BaseFont.CreateFont(content.FontName, "Cp1250", false), content.FontSize);

            cb.BeginText();
            foreach (var block in content.Contents)
            {
                cb.SetTextMatrix(block.Value.XPos * content.XScale + XOffset, block.Value.YPos * content.YScale + YOffset);
                cb.ShowText(block.Value.Content);
            }
            cb.EndText();
        }

        public void Close()
        {
            _doc.Close();
            _writer.Close();
        }
    }
}
