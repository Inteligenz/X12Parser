using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing;

namespace OopFactory.X12.Validation
{
    public class InstitutionalClaimAcknowledgmentService : X12AcknowledgmentService
    {
        public InstitutionalClaimAcknowledgmentService()
            : base(new InstitutionalClaimSpecificationFinder())
        {
        }

        
    }
}
