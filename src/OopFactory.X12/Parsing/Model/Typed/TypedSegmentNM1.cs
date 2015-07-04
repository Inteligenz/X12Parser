using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentNM1 : TypedSegment
    {
        public TypedSegmentNM1()
            : base("NM1")
        {
        }

        public TypedSegmentNM1(Segment seg)
            : base(seg)
        {
        }

        public EntityIdentifierCode NM101_EntityIdentifierCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<EntityTypeQualifier>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public string NM103_NameLastOrOrganizationName
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string NM104_NameFirst
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string NM105_NameMiddle
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string NM106_NamePrefix
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string NM107_NameSuffix
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string NM108_IdCodeQualifier
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public IdentificationCodeQualifier NM108_IdCodeQualifierEnum
        {
            get { return _segment.GetElement(8).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { _segment.SetElement(8, value.EDIFieldValue()); }
        }

        public string NM109_IdCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string NM110_EntityRelationshipCode
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string NM111_EntityIdentifierCode
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public EntityIdentifierCode NM111_EntityIdentifierCodeEnum
        {
            get { return _segment.GetElement(11).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _segment.SetElement(11, value.EDIFieldValue()); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }

    }
}
