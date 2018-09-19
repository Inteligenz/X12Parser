namespace OopFactory.X12.Shared.Models.TypedSegments
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
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string CAD02_EquipmentInitial
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string CAD03_EquipmentNumber
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string CAD04_StandardCarrierAlphaCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string CAD05_Routing
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string CAD06_ShipmentOrderStatusCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string CAD07_ReferenceIdentificationQualifier
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string CAD08_ReferenceIdentification
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string CAD09_ServiceLevelCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
