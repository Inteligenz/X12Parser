using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using OopFactory.X12.Extensions;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{

    /// <summary>
    /// Health Care Services Delivery
    /// </summary>
    public class TypedSegmentHSD : TypedSegment
    {
        public TypedSegmentHSD()
            : base("HSD")
        {
        }

        public TypedSegmentHSD(Segment segment)
            : base(segment)
        {
        }

        public QuantityQualifier HSD01_QuantityQualifierEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<QuantityQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string HSD02_BenefitQuantity
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public UnitOrBasisOfMeasurementCode HSD03_UnitOrBasisOfMeasurementCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string HSD04_SampleSelectionModulus
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public TimePeriodQualifier HSD05_TimePeriodQualifierEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<TimePeriodQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string HSD06_PeriodCount
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public ShipDeliveryOrCalendarPatternCode HSD07_DeliveryFrequencyCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<ShipDeliveryOrCalendarPatternCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public ShipDeliveryPatternTimeCode HSD08_DeliveryPatternTimeCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<ShipDeliveryPatternTimeCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }
    }
}
