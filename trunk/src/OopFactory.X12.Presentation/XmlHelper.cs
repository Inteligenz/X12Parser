using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OopFactory.X12.Presentation
{
    internal static class XmlHelper
    {
        public static string ValueOf(this XmlElement node, string xpath)
        {
            XmlNode result = node.SelectSingleNode(xpath);
            if (result != null)
                return result.Value ?? result.InnerText;
            else
                return null;
        }

    }
}
