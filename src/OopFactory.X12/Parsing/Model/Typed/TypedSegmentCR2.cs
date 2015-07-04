using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCR2 : TypedSegment
    {
        public TypedSegmentCR2()
            : base("CR2")
        {
        }
        public TypedSegmentCR2(Segment seg) : base(seg) { }

        public decimal? CR201_TreatmentSeriesNumber {
            get { return _segment.GetDecimalElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? CR202_TreatmentCount {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string CR203_SubluxationLevelCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CR204_SubluxationLevelCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CR205_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? CR206_TreatmentPeriodCount {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? CR207_MonthlyTreatmentCount {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string CR208_PatientConditionCode {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string CR209_ComplicationIndicator {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string CR210_PatientConditionDescription {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string CR211_PatientConditionDescription {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string CR212_YesNoCondRespCode {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }
    }
}
