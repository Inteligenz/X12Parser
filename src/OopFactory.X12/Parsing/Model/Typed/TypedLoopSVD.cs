using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopSVD : TypedLoop
    {

        public TypedLoopSVD() : base("SVD") { }
        public TypedLoopSVD(Loop loop) : base(loop) { }

        public TypedElementCompositeMedicalProcedureIdentifier CreateTypedElementCompositeMedicalProcedureIdentifier(ProductOrServiceIdQualifiers qualifier, string procedureCode)
        {
            return new TypedElementCompositeMedicalProcedureIdentifier(Loop, 3)
            {
                _1_ProductOrServiceIdQualifier = qualifier,
                _2_ProcedureCode = procedureCode,
            };
        }

        public string SVD01_IdentificationCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public decimal? SVD02_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public TypedElementCompositeMedicalProcedureIdentifier SVD03_CompositeMedicalProcedure
        {
            get { return new TypedElementCompositeMedicalProcedureIdentifier(Loop, 3); }
            set { Loop.SetElement(3, value); }
        }

        public string SVD04_ProductOrServiceId
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public decimal? SVD05_Quantiy
        {
            get { return Loop.GetDecimalElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public decimal? SVD06_AssignedNumber
        {
            get { return Loop.GetDecimalElement(6); }
            set { Loop.SetElement(6, value); }
        }
    }
}
