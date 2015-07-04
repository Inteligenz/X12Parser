using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopN1 : TypedLoop
    {
        public TypedLoopN1() : base("N1") { }
        public TypedLoopN1(Loop loop) : base(loop) { }

        public string N101_EntityIdentifierCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public EntityIdentifierCode N101_EntityIdentifierCodeEnum
        {
            get { return Loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { Loop.SetElement(1, value.EDIFieldValue()); }
        }

        public string N102_Name
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string N103_IdentificationCodeQualifier
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public IdentificationCodeQualifier N103_IdentificationCodeQualifierEnum
        {
            get { return Loop.GetElement(3).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { Loop.SetElement(3, value.EDIFieldValue()); }

        }

        public string N104_IdentificationCode
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public string N105_EntityRelationshipCode
        {
            get { return Loop.GetElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public string N106_EntityIdentifierCode
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public EntityIdentifierCode N106_EntityIdentifierCodeEnum
        {
            get { return Loop.GetElement(6).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { Loop.SetElement(6, value.EDIFieldValue()); }
        }
    }
}
