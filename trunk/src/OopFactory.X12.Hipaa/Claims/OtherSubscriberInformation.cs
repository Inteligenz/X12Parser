using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class OtherSubscriberInformation
    {
        public EntityName OtherSubscriber { get; set; }
        
        public EntityName OtherPayer { get; set; }
    }
}
