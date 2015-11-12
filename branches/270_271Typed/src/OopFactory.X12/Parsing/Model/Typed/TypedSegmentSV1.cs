using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSV1 : TypedSegment
    {
        public TypedSegmentSV1()
            : base("SV1")
        {
        }
        public TypedSegmentSV1(Segment segment) : base(segment) { }
        public TypedElementCompositeMedicalProcedureIdentifier CreateTypedElementCompositeMedicalProcedureIdentifier(ProductOrServiceIdQualifiers ProductOrServiceIdQualifier
            , string ProcedureCode
            , string Modifier1 = null
            , string Modifier2 = null
            , string Modifier3 = null
            , string Modifier4 = null)
        {
            return new TypedElementCompositeMedicalProcedureIdentifier(_segment, 1)
            {
                _1_ProductOrServiceIdQualifier = ProductOrServiceIdQualifier,
                _2_ProcedureCode = ProcedureCode,
                _3_ProcedureModifier = Modifier1,
                _4_ProcedureModifier = Modifier2,
                _5_ProcedureModifier = Modifier3,
                _6_ProcedureModifier = Modifier4,
            };
        }

        public TypedElementCompositDiagnosisCodePointer CreateTypedElementCompositDiagnosisCodePointer(
            int DiagnosisCodePointer1,
            int? DiagnosisCodePointer2 = null,
            int? DiagnosisCodePointer3 = null,
            int? DiagnosisCodePointer4 = null)
        {
            return new TypedElementCompositDiagnosisCodePointer(_segment, 7)
            {
                _1_DiagnosisCodePointer = DiagnosisCodePointer1,
                _2_DiagnosisCodePointer = DiagnosisCodePointer2,
                _3_DiagnosisCodePointer = DiagnosisCodePointer3,
                _4_DiagnosisCodePointer = DiagnosisCodePointer4,
            };
        }

        public TypedElementCompositeMedicalProcedureIdentifier SV101_CompositeMedicalProcedure
        {
            get { return new TypedElementCompositeMedicalProcedureIdentifier(_segment, 1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? SV102_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public UnitOrBasisOfMeasurementCode SV103_UnitBasisMeasCode
        {
            get { return _segment.GetElement(3).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(3, value.EDIFieldValue()); }
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

        public TypedElementCompositDiagnosisCodePointer SV107_CompDiagCodePoint
        {
            get { return new TypedElementCompositDiagnosisCodePointer(_segment, 7); }
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
