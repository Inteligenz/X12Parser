using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCR1 : TypedSegment
    {
        public TypedSegmentCR1()
            : base("CR1")
        {
        }

        public string CR101_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? CR102_PatientWeight {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string CR103_AmbulanceTransportCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CR104_AmbulanceTransportReasonCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CR105_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? CR106_TransportDistance {
            get {  return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string CR107_AddressInformation
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string CR108_AddressInformation {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string CR109_RoundTripPurposeDescription {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string CR110_StretcherPurposeDescription {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }
    }
}
