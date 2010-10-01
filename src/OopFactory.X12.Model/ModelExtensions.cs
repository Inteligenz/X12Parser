using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public static class ModelExtensions
    {
        public static TModel Deserialize<TModel>(string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlTextReader = new XmlTextReader(stringReader);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            if (xmlDoc.GetElementsByTagName(typeof(TModel).Name) == null)
                throw new ArgumentException("xml is not of a " + typeof(TModel).Name, "xml");

            var xmlSerializer = new XmlSerializer(typeof(TModel));

            return ((TModel)(xmlSerializer.Deserialize(xmlTextReader)));
        }


    }
}
