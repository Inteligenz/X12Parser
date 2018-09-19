namespace OopFactory.X12.Hipaa.Claims.Forms
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Enums;

    public class FormBlock
    {
        public string LetterSpacing { get; set; }

        public TextAlign TextAlign { get; set; }

        public decimal Left { get; set; }

        public decimal Top { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public string Text { get; set; }
    }

    public class FormPage
    {
        public FormPage()
        {
            if (this.Blocks == null)
            {
                this.Blocks = new List<FormBlock>();
            }
        }

        public string MasterReference { get; set; }

        public string ImagePath { get; set; }

        [XmlElement(ElementName = "Block")]
        public List<FormBlock> Blocks { get; set; }
    }

    public class FormDocument
    {
        public FormDocument()
        {
            if (this.Pages == null)
            {
                this.Pages = new List<FormPage>();
            }
        }

        [XmlElement(ElementName = "Page")]
        public List<FormPage> Pages { get; set; }

        #region Serialization Methods
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(FormDocument)).Serialize(writer, this);
            return writer.ToString();
        }

        public static FormDocument Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(FormDocument));
            return (FormDocument)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
