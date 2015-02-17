using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public abstract class EligibilityBenefitBase
    {

        public Entity Source { get; set; }
        public Provider Receiver { get; set; }
        
        public BenefitMember Subscriber { get; set; }
        public BenefitMember Dependent { get; set; }
    }
}
