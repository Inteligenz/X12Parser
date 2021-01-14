namespace X12.Shared.Models.TypedLoops
{
    using X12.Shared.Enumerations;
    using X12.Shared.Extensions;
    using X12.Specifications;

    public class TypedLoopNM1 : TypedLoop
    {
        private readonly string entityIdentifier;

        public TypedLoopNM1(string entityIdentifier)
            : base("NM1")
        {
            this.entityIdentifier = entityIdentifier;
        }

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return $"{this.SegmentId}{delimiters.ElementSeparator}{this.entityIdentifier}";
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            string segmentString = this.GetSegmentString(delimiters);
            this.Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string NM101_EntityIdCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public EntityIdentifierCode NM101_EntityIdentifierCodeEnum
        {
            get { return this.Loop.GetElement(1).ToEnumFromEdiFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(1, value.EdiFieldValue()); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get  { return this.Loop.GetElement(2).ToEnumFromEdiFieldValue<EntityTypeQualifier>(); }
            set { this.Loop.SetElement(2,value.EdiFieldValue()); }
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
            get { return this.Loop.GetElement(8).ToEnumFromEdiFieldValue<IdentificationCodeQualifier>(); }
            set { this.Loop.SetElement(8, value.EdiFieldValue()); }
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
            get { return this.Loop.GetElement(11).ToEnumFromEdiFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(11, value.EdiFieldValue()); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return this.Loop.GetElement(12); }
            set { this.Loop.SetElement(12, value); }
        }
    }
}
