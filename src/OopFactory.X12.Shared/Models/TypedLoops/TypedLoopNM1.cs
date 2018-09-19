namespace OopFactory.X12.Shared.Models.TypedLoops
{
    using System;

    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;
    using OopFactory.X12.Specifications;

    public class TypedLoopNM1 : TypedLoop
    {
        private readonly string entityIdentifer;

        public TypedLoopNM1(string entityIdentifier)
            : base("NM1")
        {
            this.entityIdentifer = entityIdentifier;
        }

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}{2}", this.SegmentId, delimiters.ElementSeparator, this.entityIdentifer);
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);
            this.Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string NM101_EntityIdCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public EntityIdentifierCode NM101_EntityIdentifierCodeEnum
        {
            get { return this.Loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(1, value.EDIFieldValue()); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get  { return this.Loop.GetElement(2).ToEnumFromEDIFieldValue<EntityTypeQualifier>(); }
            set { this.Loop.SetElement(2,value.EDIFieldValue()); }
        }

        public string NM103_NameLastOrOrganizationName
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string NM104_NameFirst
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }

        public string NM105_NameMiddle
        {
            get { return this.Loop.GetElement(5); }
            set { this.Loop.SetElement(5, value); }
        }

        public string NM106_NamePrefix
        {
            get { return this.Loop.GetElement(6); }
            set { this.Loop.SetElement(6, value); }            
        }

        public string NM107_NameSuffix
        {
            get { return this.Loop.GetElement(7); }
            set { this.Loop.SetElement(7, value); }
        }

        public string NM108_IdCodeQualifier
        {
            get { return this.Loop.GetElement(8); }
            set { this.Loop.SetElement(8, value); }
        }

        public IdentificationCodeQualifier NM108_IdCodeQualifierEnum
        {
            get { return this.Loop.GetElement(8).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { this.Loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string NM109_IdCode
        {
            get { return this.Loop.GetElement(9); }
            set { this.Loop.SetElement(9, value); }
        }

        public string NM110_EntityRelationshipCode
        {
            get { return this.Loop.GetElement(10); }
            set { this.Loop.SetElement(10, value); }
        }

        public string NM111_EntityIdentifierCode
        {
            get { return this.Loop.GetElement(11); }
            set { this.Loop.SetElement(11, value); }
        }

        public EntityIdentifierCode NM111_EntityIdentifierCodeEnum
        {
            get { return this.Loop.GetElement(11).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(11, value.EDIFieldValue()); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return this.Loop.GetElement(12); }
            set { this.Loop.SetElement(12, value); }
        }
    }
}
