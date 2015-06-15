using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// RELATIONSHIP
    /// </summary>
    public class TypedSegmentINS : TypedSegment
    {
        public TypedSegmentINS()
            : base("INS")
        {
        }

        public TypedSegmentINS(Segment segment)
            : base(segment)
        {
        }

        public YesNoConditionOrResponseCode INS01_InsuredIndicator
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public IndividualRelationshipCode INS02_IndividualRelationshipCodeEnum
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<IndividualRelationshipCode>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }


        public string INS17_BirthSequenceNumber
        {
            get { return _segment.GetElement(17); }
            set { _segment.SetElement(17, value); }
        }

    }
}
