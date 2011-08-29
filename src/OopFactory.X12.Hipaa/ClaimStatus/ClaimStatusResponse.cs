using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.ClaimStatus
{
    public class ClaimStatusResponse : ClaimStatusBase
    {
        public ClaimStatusResponse()
        {
            if (ServiceLineResponses == null) ServiceLineResponses = new List<ClaimStatusServiceLineResponse>();
        }

        [XmlElement(ElementName="ServiceLineResponse")]
        public List<ClaimStatusServiceLineResponse> ServiceLineResponses { get; set; }
    }
}
