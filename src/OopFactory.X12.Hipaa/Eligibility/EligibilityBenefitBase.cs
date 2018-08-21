namespace OopFactory.X12.Hipaa.Eligibility
{
    using OopFactory.X12.Hipaa.Common;

    public abstract class EligibilityBenefitBase
    {

        public Entity Source { get; set; }

        public Provider Receiver { get; set; }
        
        public BenefitMember Subscriber { get; set; }

        public BenefitMember Dependent { get; set; }
    }
}
