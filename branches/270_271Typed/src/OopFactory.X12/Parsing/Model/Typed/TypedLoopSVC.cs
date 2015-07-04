using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopSVC : TypedLoop
    {
        public TypedLoopSVC() : base("SVC") { }
        public TypedLoopSVC(Loop loop) : base(loop) { }

        public TypedElementCompositeMedicalProcedureIdentifier CreateTypedElementCompositeMedicalProcedureIdentifier(int elementNumber, ProductOrServiceIdQualifiers qualifier, string procedureCode)
        {
            return new TypedElementCompositeMedicalProcedureIdentifier(Loop, elementNumber)
            {
                _1_ProductOrServiceIdQualifier = qualifier,
                _2_ProcedureCode = procedureCode,
            };
        }

        public TypedElementCompositeMedicalProcedureIdentifier SVC01_CompositeMedicalProcedureIdentifier
        {
            get { return new TypedElementCompositeMedicalProcedureIdentifier(Loop, 1); }
            set { Loop.SetElement(1, value); }
        }

        public decimal? SVC02_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(2); }
            set { Loop.SetElement(2, value); }
        }


        public decimal? SVC03_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string SVC04_ProductOrServiceId
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public decimal? SVD05_Quantiy
        {
            get { return Loop.GetDecimalElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public TypedElementCompositeMedicalProcedureIdentifier SVC06_CompositeMedicalProcedureIdentifier
        {
            get { return new TypedElementCompositeMedicalProcedureIdentifier(Loop, 6); }
            set { Loop.SetElement(6, value); }
        }

        public decimal? SVD07_Quantiy
        {
            get { return Loop.GetDecimalElement(7); }
            set { Loop.SetElement(7, value); }
        }
    }
}
