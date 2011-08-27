﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public class RelatedEntity : Entity
    {
        public RelatedEntity()
        {
            if (ProviderInfo == null) ProviderInfo = new ProviderInformation();
        }

        public ProviderInformation ProviderInfo { get; set; }
    }
}
