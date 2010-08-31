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
            var serializer = new XmlSerializer(typeof(TModel));

            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            return (TModel)serializer.Deserialize(ms);
        }


    }
}
