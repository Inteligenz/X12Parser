namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Extensions;
    
    /// <summary>
    /// Baseline Item Data (Invoice)
    /// </summary>
    public class TypedLoopIT1 : TypedLoop
    {
        public TypedLoopIT1()
            : base("IT1")
        {
        }

        public string IT101_AssignedIdentification
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public decimal? IT102_QuantityInvoiced
        {
            get { return this.Loop.GetDecimalElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public UnitOrBasisOfMeasurementCode IT103_UnitOrBasisForMeasurementCode
        {
            get { return this.Loop.GetElement(3).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Loop.SetElement(3, value.EDIFieldValue()); }
        }

        public decimal? IT104_UnitPrice
        {
            get { return this.Loop.GetDecimalElement(4); }
            set { this.Loop.SetElement(4, value); }
        }

        public string IT106_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(6); }
            set { this.Loop.SetElement(6, value); }
        }

        public string IT107_ProductServiceId
        {
            get { return this.Loop.GetElement(7); }
            set { this.Loop.SetElement(7, value); }
        }
        public string IT108_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(8); }
            set { this.Loop.SetElement(8, value); }
        }

        public string IT109_ProductServiceId
        {
            get { return this.Loop.GetElement(9); }
            set { this.Loop.SetElement(9, value); }
        }
        public string IT110_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(10); }
            set { this.Loop.SetElement(10, value); }
        }

        public string IT111_ProductServiceId
        {
            get { return this.Loop.GetElement(11); }
            set { this.Loop.SetElement(11, value); }
        }
        public string IT112_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(12); }
            set { this.Loop.SetElement(12, value); }
        }

        public string IT113_ProductServiceId
        {
            get { return this.Loop.GetElement(13); }
            set { this.Loop.SetElement(13, value); }
        }
        public string IT114_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(14); }
            set { this.Loop.SetElement(14, value); }
        }

        public string IT115_ProductServiceId
        {
            get { return this.Loop.GetElement(15); }
            set { this.Loop.SetElement(15, value); }
        }
        public string IT116_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(16); }
            set { this.Loop.SetElement(16, value); }
        }

        public string IT117_ProductServiceId
        {
            get { return this.Loop.GetElement(17); }
            set { this.Loop.SetElement(17, value); }
        }
        public string IT118_ProductServiceIdQualifier
        {
            get { return this.Loop.GetElement(18); }
            set { this.Loop.SetElement(18, value); }
        }

        public string IT119_ProductServiceId
        {
            get { return this.Loop.GetElement(19); }
            set { this.Loop.SetElement(19, value); }
        }
    }
}
