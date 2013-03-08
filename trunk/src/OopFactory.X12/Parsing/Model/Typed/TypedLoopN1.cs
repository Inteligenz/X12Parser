using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopN1 : TypedLoop
    {
        public TypedLoopN1()
            : base("N1")
        {
        }

        public string N101_EntityIdentifierCode
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public EntityIdentifierCode N101_EntityIdentifierCodeEnum
        {
            get { return _loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _loop.SetElement(1, value.EDIFieldValue()); }
        }

        public string N102_Name
        {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string N103_IdentificationCodeQualifier
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public IdentificationCodeQualifier N103_IdentificationCodeQualifierEnum
        {
            get { return _loop.GetElement(3).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { _loop.SetElement(3, value.EDIFieldValue()); }
            
        }

        public string N104_IdentificationCode
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string N105_EntityRelationshipCode
        {
            get { return _loop.GetElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public string N106_EntityIdentifierCode
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }
        }

        public EntityIdentifierCode N106_EntityIdentifierCodeEnum
        {
            get { return _loop.GetElement(6).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _loop.SetElement(6, value.EDIFieldValue()); }
        }
    }
}
