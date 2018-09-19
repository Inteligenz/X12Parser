namespace OopFactory.X12.Hipaa.Common
{
    using System;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Enums;

    /// <summary>
    /// Represents a person (extends the <see cref="Entity"/> class)
    /// </summary>
    public class Member : Entity
    {
        /// <summary>
        /// Gets or sets the gender of the member
        /// </summary>
        [XmlAttribute]        
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the member was born
        /// </summary>
        [XmlIgnore]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Lookup"/> object attached to the member
        /// </summary>
        public Lookup Relationship { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="DateOfBirth"/> of the member that can be serialized to XML
        /// </summary>
        [XmlAttribute(AttributeName = "DateOfBirth", DataType = "date")]
        public DateTime SerializableDateOfBirth 
        {
            get { return this.DateOfBirth ?? DateTime.MinValue; }
            set { this.DateOfBirth = value; }
        }

        /// <summary>
        /// Gets whether the <see cref="DateOfBirth"/> has been specified
        /// </summary>
        [XmlIgnore]
        public bool SerializableDateOfBirthSpecified => this.DateOfBirth.HasValue;

        /// <summary>
        /// Gets the unique identifier of the <see cref="Member"/>
        /// </summary>
        [XmlAttribute]
        public string MemberId
        {
            get
            {
                if (this.Name?.Identification != null && this.Name.Identification.Qualifier == "MI")
                {
                    return this.Name.Identification.Id;
                }
                else
                {
                    return this.GetReferenceId("1W");
                }
            }
        }
        
        /// <summary>
        /// Gets the Social Security Number (SSN) of the <see cref="Member"/>
        /// </summary>
        [XmlAttribute]
        public string Ssn => this.GetReferenceId("SY");

        /// <summary>
        /// Gets the plan number of the <see cref="Member"/>
        /// </summary>
        [XmlAttribute]
        public string PlanNumber => this.GetReferenceId("18");

        /// <summary>
        /// Gets the group number of the <see cref="Member"/>
        /// </summary>
        [XmlAttribute]
        public string GroupNumber => this.GetReferenceId("6P");
    }
}
