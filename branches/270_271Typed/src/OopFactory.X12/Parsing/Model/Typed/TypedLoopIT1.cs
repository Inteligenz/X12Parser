using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

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
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public decimal? IT102_QuantityInvoiced
        {
            get { return _loop.GetDecimalElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public UnitOrBasisOfMeasurementCode IT103_UnitOrBasisForMeasurementCode
        {
            get { return _loop.GetElement(3).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _loop.SetElement(3, value.EDIFieldValue()); }
        }

        public decimal? IT104_UnitPrice
        {
            get { return _loop.GetDecimalElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string IT106_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }
        }

        public string IT107_ProductServiceId
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }
        public string IT108_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(8); }
            set { _loop.SetElement(8, value); }
        }

        public string IT109_ProductServiceId
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }
        public string IT110_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(10); }
            set { _loop.SetElement(10, value); }
        }

        public string IT111_ProductServiceId
        {
            get { return _loop.GetElement(11); }
            set { _loop.SetElement(11, value); }
        }
        public string IT112_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(12); }
            set { _loop.SetElement(12, value); }
        }

        public string IT113_ProductServiceId
        {
            get { return _loop.GetElement(13); }
            set { _loop.SetElement(13, value); }
        }
        public string IT114_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(14); }
            set { _loop.SetElement(14, value); }
        }

        public string IT115_ProductServiceId
        {
            get { return _loop.GetElement(15); }
            set { _loop.SetElement(15, value); }
        }
        public string IT116_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(16); }
            set { _loop.SetElement(16, value); }
        }

        public string IT117_ProductServiceId
        {
            get { return _loop.GetElement(17); }
            set { _loop.SetElement(17, value); }
        }
        public string IT118_ProductServiceIdQualifier
        {
            get { return _loop.GetElement(18); }
            set { _loop.SetElement(18, value); }
        }

        public string IT119_ProductServiceId
        {
            get { return _loop.GetElement(19); }
            set { _loop.SetElement(19, value); }
        }
    }
}
