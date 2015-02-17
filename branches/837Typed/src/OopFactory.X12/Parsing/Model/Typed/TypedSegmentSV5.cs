using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSV5 : TypedSegment
    {
        private TypedElementCompositeMedicalProcedureIdentifier _sv501;

        public TypedSegmentSV5() : base("SV5")
        {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters) {
            base.Initialize(parent, delimiters);
            _sv501 = new TypedElementCompositeMedicalProcedureIdentifier(_segment, 1);
        }

        public TypedElementCompositeMedicalProcedureIdentifier SV501_CompositeMedicalProcedure {
            get { return _sv501; }
        }

        public string SV502_UnitBasisMeasCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
        
        public decimal? SV503_LengthOfMedicalNecessity
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? SV504_DmeRentalPrice
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public decimal? SV505_DmePurchasePrice
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string SV506_RentalUnitPriceIndicator
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string SV507_PrognosisCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
    }
}
