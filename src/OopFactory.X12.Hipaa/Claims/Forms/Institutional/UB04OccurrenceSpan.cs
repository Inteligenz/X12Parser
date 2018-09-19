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
            this.FromDate = $"{source.Date:MMddyy}";
            return this;
        }

        public UB04OccurrenceSpan CopyFrom(CodedDateRange source)
        {
            this.Code = source.Code;
            this.FromDate = $"{source.FromDate:MMddyy}";
            this.ThroughDate = $"{source.ThroughDate:MMddyy}";
            return this;
        }
    }
}
