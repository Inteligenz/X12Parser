namespace X12.Hipaa.Eligibility
{
    using X12.Hipaa.Common;

    /// <summary>
    /// Represents common benefit information, such as subscriber and provider
    /// </summary>
    public abstract class EligibilityBenefitBase
    {
        /// <summary>
        /// Gets or sets the source entity
        /// </summary>
        public Entity Source { get; set; }

        /// <summary>
        /// Gets or sets the benefit provider receiving the subscriber
        /// </summary>
        public Provider Receiver { get; set; }
        
        /// <summary>
        /// Gets or sets the benefit member
        /// </summary>
        public BenefitMember Subscriber { get; set; }

        /// <summary>
        /// Gets or sets the subscriber's dependents
        /// </summary>
        public BenefitMember Dependent { get; set; }
    }
}
