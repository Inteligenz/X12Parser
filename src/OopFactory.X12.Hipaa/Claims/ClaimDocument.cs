using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace OopFactory.X12.Hipaa.Claims
{
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class ClaimDocument
    {
        public ClaimDocument()
        {
            if (Claims == null) Claims = new List<Claim>();
        }

        [XmlElement(ElementName="Claim")]
        public List<Claim> Claims { get; set; }

        #region Serialization Methods
        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(ClaimDocument)).Serialize(writer, this);
            return writer.ToString();
        }

        public static ClaimDocument Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ClaimDocument));
            return (ClaimDocument)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
