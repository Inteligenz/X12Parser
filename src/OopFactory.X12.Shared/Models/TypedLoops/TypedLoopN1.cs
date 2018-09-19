namespace OopFactory.X12.Shared.Models.TypedLoops
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    public class TypedLoopN1 : TypedLoop
    {
        public TypedLoopN1()
            : base("N1")
        {
        }

        public string N101_EntityIdentifierCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public EntityIdentifierCode N101_EntityIdentifierCodeEnum
        {
            get { return this.Loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(1, value.EDIFieldValue()); }
        }

        public string N102_Name
        {
            get { return this.Loop.GetElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public string N103_IdentificationCodeQualifier
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public IdentificationCodeQualifier N103_IdentificationCodeQualifierEnum
        {
            get { return this.Loop.GetElement(3).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { this.Loop.SetElement(3, value.EDIFieldValue()); }
            
        }

        public string N104_IdentificationCode
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }

        public string N105_EntityRelationshipCode
        {
            get { return this.Loop.GetElement(5); }
            set { this.Loop.SetElement(5, value); }
        }

        public string N106_EntityIdentifierCode
        {
            get { return this.Loop.GetElement(6); }
            set { this.Loop.SetElement(6, value); }
        }

        public EntityIdentifierCode N106_EntityIdentifierCodeEnum
        {
            get { return this.Loop.GetElement(6).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { this.Loop.SetElement(6, value.EDIFieldValue()); }
        }
    }
}
