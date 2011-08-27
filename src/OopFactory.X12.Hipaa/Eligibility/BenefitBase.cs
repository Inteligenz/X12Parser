using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public abstract class BenefitBase
    {
        internal BenefitBase()
        {
            if (Subscriber == null) Subscriber = new BenefitMember();
            if (Dependent == null) Dependent = new BenefitMember();
        }

        public BenefitMember Subscriber { get; set; }
        public BenefitMember Dependent { get; set; }
    }
}
