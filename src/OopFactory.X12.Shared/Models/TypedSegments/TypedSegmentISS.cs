namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

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
            get { return this.Segment.GetDecimalElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        /// <summary>
        /// CA = Case
        /// </summary>
        public UnitOrBasisOfMeasurementCode ISS02_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(2).ToEnumFromEdiFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(2, value.EdiFieldValue()); }
        }

        public decimal? ISS03_Weight
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        /// <summary>
        /// LB = Pounds
        /// </summary>
        public UnitOrBasisOfMeasurementCode ISS04_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(4).ToEnumFromEdiFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(4, value.EdiFieldValue()); }
        }

        public decimal? ISS05_Volume
        {
            get { return this.Segment.GetDecimalElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public UnitOrBasisOfMeasurementCode ISS06_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(6).ToEnumFromEdiFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(6, value.EdiFieldValue()); }
        }
    }
}
