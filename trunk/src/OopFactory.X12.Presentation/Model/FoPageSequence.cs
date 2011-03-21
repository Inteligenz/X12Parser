using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OopFactory.X12.Presentation.Model
{
    public class FoPageSequence
    {
        public FoPageSequence()
        {
            if (Fields == null) Fields = new List<FoPageField>();
        }
        [XmlAttribute]
        public string MasterReference { get; set; }

        [XmlElement(ElementName="Field")]
        public List<FoPageField> Fields { get; set; }

        public FoPageField AddField(string content, decimal top, decimal left, decimal width)
        {
            var field = new FoPageField
            {
                Content = content,
                Top = top,
                Left = left,
                Width = width,
                Height = 1
            };
            Fields.Add(field);
            return field;
        }
    }
}
