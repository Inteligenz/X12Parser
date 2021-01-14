namespace X12.Shared.Models.TypeSegment
{
    /// <summary>
    /// F.O.B. RElated Instructions
    /// </summary>
    public class TypedSegmentFOB : TypedSegment
    {
        public TypedSegmentFOB()
            : base("FOB")
        {
        }

        /// <summary>
        /// 11 = Rule 11 Shipment
        /// BP = Paid by Buyer
        /// CA = Advance Collect
        /// CC = Collect
        /// CD = Collect on Delivery
        /// CF = Collect, Freight Credited Back to Customer
        /// DE = Per Contract
        /// DF = Defined by Buyer and Seller
        /// FO = FOB Port of Call
        /// HP = Half Prepaid
        /// MX = Mixed
        /// NC = Service Freight, No Charge
        /// NR = Non Revenue
        /// PA = Advance Prepaid
        /// PB = Customer Pickup/Backhaul
        /// PC = Prepaid but charged to Customer
        /// PD = Prepaid by Processor
        /// PE = Prepaid and Summary Bill
        /// PL = Prepaid Local, Collect Outstate
        /// PO = Prepaid Only
        /// PP = Prepaid (by Seller)
        /// PS = Paid by Seller
        /// PU = Pickup
        /// RC = Return Container Freight Paid by Customer
        /// RF = Return Container Freight Free
        /// RS = Return Container Freight Paid by Supplier
        /// TP = Third Party Pay
        /// WC = Weight Condition
        /// ZZ = Mutually Defined
        /// </summary>
        public string FOB01_ShipmentMethodOfPayment
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string FOB02_LocationQualifier
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string FOB03_Description
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string FOB04_TransportationTermsQualifierCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string FOB05_TransportationTermsCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string FOB06_LocationQualifier
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string FOB07_Description
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string FOB08_RiskOfLossCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string FOB09_Description
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
