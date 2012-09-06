using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSV1 : TypedSegment
    {
        public TypedSegmentSV1() : base("SV1")
        {
        }

        public string SV101_CompositeMedicalProcedure
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string SV102_MonetaryAmount
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
        
        public string SV103_UnitBasisMeasCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string SV104_Quantity
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public string SV105_FacilityCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string SV107_CompDiagCodePoint
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string SV109_YesNoCondRespCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string SV111_YesNoCondRespCode
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string SV112_YesNoCondRespCode
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }        
        
        public string SV115_CopayStatusCode
        {
            get { return _segment.GetElement(15); }
            set { _segment.SetElement(15, value); }
        }
    }
}
