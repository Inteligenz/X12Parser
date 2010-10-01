using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Member
    {
        [XmlIgnore]
        public DateTime? DateOfBirth { get; set; }

        [XmlAttribute(AttributeName = "DateOfBirth")]
        public DateTime XmlSerializableDateOfBirth 
        {
            get { return DateOfBirth ?? DateTime.MinValue; }
            set { DateOfBirth = value; }
        }

        public bool XmlSerializableDateOfBirthSpecified
        {
            get { return DateOfBirth.HasValue; }
            set { }
        }

        [XmlAttribute]
        public GenderEnum Gender { get; set; }

        [XmlAttribute]
        public string MemberId { get; set; }

        public EntityName Name { get; set; }

        public PostalAddress Address { get; set; }
    }
}
