namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    using OopFactory.X12.Hipaa.Common;

    public class UB04OccurrenceSpan
    {
        public string Code { get; set; }
        public string FromDate { get; set; }
        public string ThroughDate { get; set; }

        public UB04OccurrenceSpan CopyFrom(CodedDate source)
        {
            this.Code = source.Code;
            this.FromDate = string.Format("{0:MMddyy}", source.Date);
            return this;
        }

        public UB04OccurrenceSpan CopyFrom(CodedDateRange source)
        {
            this.Code = source.Code;
            this.FromDate = string.Format("{0:MMddyy}", source.FromDate);
            this.ThroughDate = string.Format("{0:MMddyy}", source.ThroughDate);
            return this;
        }
    }
}
