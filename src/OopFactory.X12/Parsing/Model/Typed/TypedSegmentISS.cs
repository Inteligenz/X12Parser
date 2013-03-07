using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Invoice Shipment Summary
    /// </summary>
    public class TypedSegmentISS : TypedSegment
    {
        public TypedSegmentISS()
            : base("ISS")
        {
        }

        public decimal? ISS01_NumberOfUnitsShipped
        {
            get { return _segment.GetDecimalElement(1); }
            set { _segment.SetElement(1, value); }
        }

        /// <summary>
        /// CA = Case
        /// </summary>
        public UnitOrBasisOfMeasurementCode ISS02_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public decimal? ISS03_Weight
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        /// <summary>
        /// LB = Pounds
        /// </summary>
        public UnitOrBasisOfMeasurementCode ISS04_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(4).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(4, value.EDIFieldValue()); }
        }

        public decimal? ISS05_Volume
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public UnitOrBasisOfMeasurementCode ISS06_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(6).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(6, value.EDIFieldValue()); }
        }
    }
}
