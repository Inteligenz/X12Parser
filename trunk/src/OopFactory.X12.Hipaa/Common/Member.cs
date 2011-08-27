using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public enum GenderEnum
    {
        Unknown,
        Male,
        Female
    }

    public class Member : Entity
    {
        [XmlAttribute]        
        public GenderEnum Gender { get; set; }

        [XmlIgnore]
        public DateTime? DateOfBirth { get; set; }

        #region Serializable DateOfBirth Properties
        [XmlAttribute(AttributeName="DateOfBirth", DataType="date")]
        public DateTime SerializableDateOfBirth 
        {
            get { return DateOfBirth ?? DateTime.MinValue; }
            set { DateOfBirth = value; }
        }

        [XmlIgnore]
        public bool SerializableDateOfBirthSpecified 
        {
            get { return DateOfBirth.HasValue; }
            set {}
        }
        #endregion
    }
}
