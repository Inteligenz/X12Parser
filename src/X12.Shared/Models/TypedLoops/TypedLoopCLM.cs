﻿namespace X12.Shared.Models.TypedLoops
{
    using System;

    using X12.Shared.Models.TypedElements;
    using X12.Specifications;

    public class TypedLoopCLM : TypedLoop
    {

        public TypedLoopCLM()
            : base("CLM")
        {
        }

        public TypedElementServiceLocationInfo CLM05 { get; private set; }

        public TypedElementRelatedCausesInfo CLM11 { get; private set; }

        internal override void  Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            base.Initialize(parent, delimiters, loopSpecification);
            this.CLM05 = new TypedElementServiceLocationInfo(this.Loop, 5);
            this.CLM11 = new TypedElementRelatedCausesInfo(this.Loop, 11);
        }

        public string CLM01_PatientControlNumber
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public decimal CLM02_TotalClaimChargeAmount
        {
            get 
            {
                decimal amount;
                return decimal.TryParse(this.Loop.GetElement(2), out amount) ? amount : 0;
            }

            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Total Claim Charge Amount must be greater than or equal to zero.");
                }

                this.Loop.SetElement(2, value.ToString().TrimStart('0'));
            }
        }

        public string CLM03_ClaimFilingIndicatorCode
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string CLM04_NonInstitutionalClaimTypeCode
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }
        
        public bool? CLM06_ProviderOrSupplierSignatureIndicator
        {
            get 
            {
                switch (this.Loop.GetElement(6))
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
                    this.Loop.SetElement(6, value.Value ? "Y" : "N");
                }
                else
                {
                    this.Loop.SetElement(6, string.Empty);
                }
            }
        }

        public string CLM07_ProviderAcceptAssignmentCode
        {
            get { return this.Loop.GetElement(7); }
            set { this.Loop.SetElement(7, value); }
        }

        public string CLM08_BenefitsAssignmentCerficationIndicator
        {
            get { return this.Loop.GetElement(8); }
            set { this.Loop.SetElement(8, value); }
        }

        public string CLM09_ReleaseOfInformationCode
        {
            get { return this.Loop.GetElement(9); }
            set { this.Loop.SetElement(9, value); }
        }

        public string CLM10_PatientSignatureSourceCode
        {
            get { return this.Loop.GetElement(10); }
            set { this.Loop.SetElement(10, value); }
        }

        public string CLM12_SpecialProgramCode
        {
            get { return this.Loop.GetElement(12); }
            set { this.Loop.SetElement(12, value); }
        }

        public string CLM20_DelayReasonCode
        {
            get { return this.Loop.GetElement(20); }
            set { this.Loop.SetElement(20, value); }
        }
    }
}
