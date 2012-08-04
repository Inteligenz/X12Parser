using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopCLM : TypedLoop
    {
        private TypedElementServiceLocationInfo _clm05;
        private TypedElementRelatedCausesInfo _clm11;

        public TypedLoopCLM() : base("CLM") { }

        internal override void  Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            base.Initialize(parent, delimiters, loopSpecification);
            _clm05 = new TypedElementServiceLocationInfo(_loop, 5);
            _clm11 = new TypedElementRelatedCausesInfo(_loop, 11);
        }

        public string CLM01_PatientControlNumber
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public decimal CLM02_TotalClaimChargeAmount
        {
            get 
            {
                decimal amount;
                if (decimal.TryParse(_loop.GetElement(2), out amount))
                    return amount;
                else
                    return 0; 
            }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Total Claim Charge Amount must be greater than or equal to zero.");
                _loop.SetElement(2, value.ToString().TrimStart('0'));
            }
        }

        public string CLM03_ClaimFilingIndicatorCode
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string CLM04_NonInstitutionalClaimTypeCode
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public TypedElementServiceLocationInfo CLM05
        {
            get { return _clm05; }
        }

        public bool? CLM06_ProviderOrSupplierSignatureIndicator
        {
            get 
            {
                switch (_loop.GetElement(6))
                {
                    case "Y": return true;
                    case "N": return false;
                    default: return null;
                }
            }
            set 
            {
                if (value.HasValue)
                {
                    if (value.Value == true)
                        _loop.SetElement(6, "Y");
                    else
                        _loop.SetElement(6, "N");
                }
                else
                    _loop.SetElement(6, "");
            }
        }

        public string CLM07_ProviderAcceptAssignmentCode
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }

        public string CLM08_BenefitsAssignmentCerficationIndicator
        {
            get { return _loop.GetElement(8); }
            set { _loop.SetElement(8, value); }
        }

        public string CLM09_ReleaseOfInformationCode
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }

        public string CLM10_PatientSignatureSourceCode
        {
            get { return _loop.GetElement(10); }
            set { _loop.SetElement(10, value); }
        }

        public TypedElementRelatedCausesInfo CLM11
        {
            get { return _clm11; }
        }

        public string CLM12_SpecialProgramCode
        {
            get { return _loop.GetElement(12); }
            set { _loop.SetElement(12, value); }
        }

        public string CLM20_DelayReasonCode
        {
            get { return _loop.GetElement(20); }
            set { _loop.SetElement(20, value); }
        }
    }
}
