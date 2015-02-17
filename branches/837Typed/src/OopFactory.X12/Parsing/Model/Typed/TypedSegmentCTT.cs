using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Transaction Totals
    /// </summary>
    public class TypedSegmentCTT : TypedSegment
    {
        public TypedSegmentCTT()
            : base("CTT")
        {
        }

        public int? CTT01_NumberOfLineItems
        {
            get { return _segment.GetIntElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? CTT02_HashTotal
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CTT03_Weight
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public UnitOrBasisOfMeasurementCode CTT04_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(4).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(4, value.EDIFieldValue()); }
        }

        public decimal? CTT05_Volume
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public UnitOrBasisOfMeasurementCode CTT06_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(6).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(6, value.EDIFieldValue()); }
        }

        public string CTT07_Description
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
    }
}
