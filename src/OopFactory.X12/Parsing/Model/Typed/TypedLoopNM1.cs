using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopNM1 : TypedLoop
    {
        private string _entityIdentifer;

        public TypedLoopNM1(string entityIdentifier)
            : base("NM1")
        {
            _entityIdentifer = entityIdentifier;
        }

        public TypedLoopNM1(Loop loop)
            : base(loop)
        {
            _entityIdentifer = loop.GetElement(1);
            Loop = loop;
        }

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}{2}", _segmentId, delimiters.ElementSeparator, _entityIdentifer);
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            if (null == Loop)
            {
                string segmentString = GetSegmentString(delimiters);
                Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
            }
        }

        public string NM101_EntityIdCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public EntityIdentifierCode NM101_EntityIdentifierCodeEnum
        {
            get { return Loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { Loop.SetElement(1, value.EDIFieldValue()); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get { return Loop.GetElement(2).ToEnumFromEDIFieldValue<EntityTypeQualifier>(); }
            set { Loop.SetElement(2, value.EDIFieldValue()); }
        }

        public string NM103_NameLastOrOrganizationName
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string NM104_NameFirst
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public string NM105_NameMiddle
        {
            get { return Loop.GetElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public string NM106_NamePrefix
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public string NM107_NameSuffix
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public string NM108_IdCodeQualifier
        {
            get { return Loop.GetElement(8); }
            set { Loop.SetElement(8, value); }
        }

        public IdentificationCodeQualifier NM108_IdCodeQualifierEnum
        {
            get { return Loop.GetElement(8).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { Loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string NM109_IdCode
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }

        public string NM110_EntityRelationshipCode
        {
            get { return Loop.GetElement(10); }
            set { Loop.SetElement(10, value); }
        }

        public string NM111_EntityIdentifierCode
        {
            get { return Loop.GetElement(11); }
            set { Loop.SetElement(11, value); }
        }

        public EntityIdentifierCode NM111_EntityIdentifierCodeEnum
        {
            get { return Loop.GetElement(11).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { Loop.SetElement(11, value.EDIFieldValue()); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return Loop.GetElement(12); }
            set { Loop.SetElement(12, value); }
        }

    }
}
