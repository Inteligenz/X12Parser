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

        public Lookup Relationship { get; set; }
        
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



        [XmlAttribute]
        public string MemberId
        {
            get
            {
                if (Name != null && Name.Identification != null && Name.Identification.Qualifier == "MI")
                    return Name.Identification.Id;
                else
                    return GetReferenceId("1W");
            }
            set { }
        }

        [XmlAttribute]
        public string Ssn
        {
            get { return GetReferenceId("SY"); }
            set { }
        }

        [XmlAttribute]
        public string PlanNumber
        {
            get { return GetReferenceId("18"); }
            set { }
        }

        [XmlAttribute]
        public string GroupNumber
        {
            get { return GetReferenceId("6P"); }
        }
    }
}
