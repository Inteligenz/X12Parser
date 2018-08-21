namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Diagnosis
    {
        public string Code { get; set; }

        public string PresentOnAdmissionIndicator { get; set; }

        public UB04Diagnosis CopyFrom(Diagnosis source)
        {
            this.Code = source.FormattedCode();
            this.PresentOnAdmissionIndicator = source.PoiIndicator;
            return this;
        }
    }
}
