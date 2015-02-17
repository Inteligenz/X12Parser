using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSV1 : TypedSegment
    {
        private TypedElementCompositeMedicalProcedureIdentifier _sv101;
        private TypedElementCompositDiagnosisCodePointer  _sv107;

        public TypedSegmentSV1() : base("SV1")
        {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters) {
            base.Initialize(parent, delimiters);
            _sv101 = new TypedElementCompositeMedicalProcedureIdentifier(_segment, 1);
            _sv107 = new TypedElementCompositDiagnosisCodePointer(_segment, 7);
        }

        public TypedElementCompositeMedicalProcedureIdentifier SV101_CompositeMedicalProcedure {
            get { return _sv101; }
        }

        public decimal? SV102_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }
        
        public string SV103_UnitBasisMeasCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? SV104_Quantity
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public string SV105_FacilityCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public TypedElementCompositDiagnosisCodePointer SV107_CompDiagCodePoint {
            get { return _sv107; }
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
