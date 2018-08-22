namespace OopFactory.X12.Validation
{
    using OopFactory.X12.Specifications.Finders;

    public class InstitutionalClaimAcknowledgmentService : X12AcknowledgmentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionalClaimAcknowledgmentService"/> class
        /// </summary>
        public InstitutionalClaimAcknowledgmentService()
            : base(new InstitutionalClaimSpecificationFinder())
        {
        }
    }
}
