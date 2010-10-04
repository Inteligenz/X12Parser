using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class EntityName
    {
        [XmlAttribute]
        public EntityTypeQualifierEnum Qualifier { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }
        [XmlAttribute]
        public string Suffix { get; set; }

        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }

        public string FullName
        {
            get
            {
                if (Qualifier == EntityTypeQualifierEnum.NonPersonEntity)
                    return Last;
                else
                {
                    var name = String.Format("{0} {1}, {2} {3} {4}", Last, Suffix, Prefix, First, Middle);
                    name = name.Replace("  ", " ");
                    name = name.Replace("  ", " ");
                    name = name.Replace(" ,", ",");

                    return name.Trim();
                }
            }
        }
    }
}
