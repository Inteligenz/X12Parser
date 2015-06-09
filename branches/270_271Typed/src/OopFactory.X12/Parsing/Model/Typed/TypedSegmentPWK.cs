using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentPWK : TypedSegment
    {
        public TypedSegmentPWK()
            : base("PWK")
        {
        }

        public string PWK01_ReportTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PWK02_ReportTransmissionCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public int? PWK03_ReportCopiesNeeded
        {
            get { return _segment.GetIntElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string PWK04_EntityIdentifierCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public EntityIdentifierCode PWK04_EntityIdentiferCodeEnum
        {
            get { return _segment.GetElement(4).ToEnumFromEDIFieldValue<EntityIdentifierCode>(); }
            set { _segment.SetElement(4, value.EDIFieldValue()); }
        }

        public string PWK05_IdentificationCodeQualifier
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public IdentificationCodeQualifier PWK05_IdentificationCodeQualifierEnum
        {
            get { return _segment.GetElement(5).ToEnumFromEDIFieldValue<IdentificationCodeQualifier>(); }
            set { _segment.SetElement(5, value.EDIFieldValue()); }
        }

        public string PWK06_IdentificationCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string PWK07_Description
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string PWK08_ActionsIndicated
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string PWK09_RequestCategoryCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
