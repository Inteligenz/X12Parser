namespace OopFactory.X12.Validation
{
    using OopFactory.X12.Specifications.Finders;

    public class InstitutionalClaimAcknowledgmentService : X12AcknowledgmentService
    {
        public InstitutionalClaimAcknowledgmentService()
            : base(new InstitutionalClaimSpecificationFinder())
        {
        }
    }
}
