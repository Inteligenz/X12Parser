using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OopFactory.X12.Presentation.Model
{
    public class FoDocument
    {
        public FoDocument()
        {
            if (PageMasters == null) PageMasters = new List<FoPageMaster>();
            if (PageSequences == null) PageSequences = new List<FoPageSequence>();
        }

        [XmlElement(ElementName="PageMaster")]
        public List<FoPageMaster> PageMasters { get; set; }
        [XmlElement(ElementName="PageSequence")]
        public List<FoPageSequence> PageSequences { get; set; }

        public string Serialize()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, this);

            return writer.ToString();
        }
    }
}
