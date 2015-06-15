using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
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
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public decimal? IT102_QuantityInvoiced
        {
            get { return Loop.GetDecimalElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public UnitOrBasisOfMeasurementCode IT103_UnitOrBasisForMeasurementCode
        {
            get { return Loop.GetElement(3).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { Loop.SetElement(3, value.EDIFieldValue()); }
        }

        public decimal? IT104_UnitPrice
        {
            get { return Loop.GetDecimalElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public string IT106_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public string IT107_ProductServiceId
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }
        public string IT108_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(8); }
            set { Loop.SetElement(8, value); }
        }

        public string IT109_ProductServiceId
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }
        public string IT110_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(10); }
            set { Loop.SetElement(10, value); }
        }

        public string IT111_ProductServiceId
        {
            get { return Loop.GetElement(11); }
            set { Loop.SetElement(11, value); }
        }
        public string IT112_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(12); }
            set { Loop.SetElement(12, value); }
        }

        public string IT113_ProductServiceId
        {
            get { return Loop.GetElement(13); }
            set { Loop.SetElement(13, value); }
        }
        public string IT114_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(14); }
            set { Loop.SetElement(14, value); }
        }

        public string IT115_ProductServiceId
        {
            get { return Loop.GetElement(15); }
            set { Loop.SetElement(15, value); }
        }
        public string IT116_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(16); }
            set { Loop.SetElement(16, value); }
        }

        public string IT117_ProductServiceId
        {
            get { return Loop.GetElement(17); }
            set { Loop.SetElement(17, value); }
        }
        public string IT118_ProductServiceIdQualifier
        {
            get { return Loop.GetElement(18); }
            set { Loop.SetElement(18, value); }
        }

        public string IT119_ProductServiceId
        {
            get { return Loop.GetElement(19); }
            set { Loop.SetElement(19, value); }
        }
    }
}
