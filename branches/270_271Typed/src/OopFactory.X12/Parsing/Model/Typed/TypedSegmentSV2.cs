using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSV2 : TypedSegment
    {
        public TypedSegmentSV2()
            : base("SV2")
        {
        }

        public TypedSegmentSV2(Segment segment) : base(segment) { }
        public string SV201_ProductOrServiceId
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public TypedElementCompositeMedicalProcedureIdentifier SV202_CompositeMedicalProcedure
        {
            get { return new TypedElementCompositeMedicalProcedureIdentifier(_segment, 2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? SV203_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string SV204_UnitBasisMeasCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public decimal? SV205_Quantity
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? SV206_UnitRate
        {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? SV207_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string SV208_YesNoCondRespCode
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string SV209_NursingHomeResidentialStatusCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string SV210_LevelOfCareCode
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }
    }
}
