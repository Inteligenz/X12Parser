namespace OopFactory.X12.Hipaa.ClaimStatus
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ClaimStatusResponse : ClaimStatusBase
    {
        public ClaimStatusResponse()
        {
            if (this.ServiceLineResponses == null)
            {
                this.ServiceLineResponses = new List<ClaimStatusServiceLineResponse>();
            }
        }

        [XmlElement(ElementName = "ServiceLineResponse")]
        public List<ClaimStatusServiceLineResponse> ServiceLineResponses { get; set; }
    }
}
