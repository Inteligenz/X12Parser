using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopSVD : TypedLoop
    {
        private TypedElementCompositeMedicalProcedureIdentifier _SVD03;

        public TypedLoopSVD()
            : base("SVD")
        {
        }

        internal override void  Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            base.Initialize(parent, delimiters, loopSpecification);
            _SVD03 = new TypedElementCompositeMedicalProcedureIdentifier(_loop, 3);
        }

        public string SVD01_IdentificationCode {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public decimal? SVD02_MonetaryAmount {
            get { return _loop.GetDecimalElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public TypedElementCompositeMedicalProcedureIdentifier SVD03_CompositeMedicalProcedure {
            get { return _SVD03; }
        }

        public string SVD04_ProductOrServiceId {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public decimal? SVD05_Quantiy {
            get { return _loop.GetDecimalElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public decimal? SVD06_AssignedNumber {
            get { return _loop.GetDecimalElement(6); }
            set { _loop.SetElement(6, value); }
        }
    }
}
