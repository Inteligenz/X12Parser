using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

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

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}{2}", _segmentId, delimiters.ElementSeparator, _entityIdentifer);
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            _loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string NM101_EntityIdCode
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public EntityIdentifierCode NM101_EntityIdentifierCodeEnum
        {
            get { return _loop.GetElement(1).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _loop.SetElement(1, value.EDIFieldValue()); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get  { return _loop.GetElement(2).ToEnumFromEDIFieldValue<EntityTypeQualifier>(); }
            set { _loop.SetElement(2,value.EDIFieldValue()); }
        }

        public string NM103_NameLastOrOrganizationName
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string NM104_NameFirst
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string NM105_NameMiddle
        {
            get { return _loop.GetElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public string NM106_NamePrefix
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }            
        }

        public string NM107_NameSuffix
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }

        public string NM108_IdCodeQualifier
        {
            get { return _loop.GetElement(8); }
            set { _loop.SetElement(8, value); }
        }

        public IdentificationCodeQualifier NM108_IdCodeQualifierEnum
        {
            get { return _loop.GetElement(8).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { _loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string NM109_IdCode
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }

        public string NM110_EntityRelationshipCode
        {
            get { return _loop.GetElement(10); }
            set { _loop.SetElement(10, value); }
        }

        public string NM111_EntityIdentifierCode
        {
            get { return _loop.GetElement(11); }
            set { _loop.SetElement(11, value); }
        }

        public EntityIdentifierCode NM111_EntityIdentifierCodeEnum
        {
            get { return _loop.GetElement(11).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _loop.SetElement(11, value.EDIFieldValue()); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return _loop.GetElement(12); }
            set { _loop.SetElement(12, value); }
        }

    }
}
