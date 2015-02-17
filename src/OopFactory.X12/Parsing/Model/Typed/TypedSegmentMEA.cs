using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentMEA : TypedSegment
    {
        private TypedElementCompositeUnitOfMeasure _MEA04;

        public TypedSegmentMEA() : base("MEA")
        {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters) {
            base.Initialize(parent, delimiters);
            _MEA04 = new TypedElementCompositeUnitOfMeasure(_segment, 4);
        }

        public string MEA01_MeasurementReferenceIdCode {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string MEA02_MeasurementQualifier {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? MEA03_MeasurementValue {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(3, value); }
        }

        public TypedElementCompositeUnitOfMeasure MEA04_CompositeUnitOfMeasure {
            get { return _MEA04; }
        }

        public decimal? MEA05_RangeMinimum {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? MEA06_RangeMaximum {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string MEA07_MeasurementSignificanceCode {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string MEA08_MeasurementAttributeCode {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string MEA09_SurfaceLayerPositionCode {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string MEA10_MeasurementMethodOrDevice {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string MEA11_CodeListQualifierCode {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string MEA12_IndustryCode {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }
    }
}
