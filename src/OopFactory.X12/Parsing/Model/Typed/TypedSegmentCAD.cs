using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Carrier Detail
    /// </summary>
    public class TypedSegmentCAD : TypedSegment
    {
        public TypedSegmentCAD()
            : base("CAD")
        {
        }

        /// <summary>
        /// K = Back Haul
        /// M = Motor (Common Carrier)
        /// R = Rail
        /// U = Private Parcel Service
        /// ZZ = Mutually Defined
        /// </summary>
        public string CAD01_TransportationMethodTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CAD02_EquipmentInitial
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string CAD03_EquipmentNumber
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CAD04_StandardCarrierAlphaCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CAD05_Routing
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string CAD06_ShipmentOrderStatusCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string CAD07_ReferenceIdentificationQualifier
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string CAD08_ReferenceIdentification
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string CAD09_ServiceLevelCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
