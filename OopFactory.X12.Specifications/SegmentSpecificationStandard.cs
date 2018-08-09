namespace OopFactory.X12.Specifications
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications.Enumerations;

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class SegmentSpecificationStandard
    {
        [XmlAttribute]
        public string Position { get; set; }

        [XmlAttribute]
        public RequirementEnum Requirement { get; set; }

        [XmlIgnore]
        public bool RequirementSpecified { get; set; }

        [XmlAttribute]
        public int MaxUse { get; set; }

        [XmlIgnore]
        public bool MaxUseSpecified { get; set; }
    }
}
