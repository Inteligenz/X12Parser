namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    using OopFactory.X12.Hipaa.Common;

    public class UB04Occurrence
    {
        public string Code { get; set; }

        public string Date { get; set; }

        public UB04Occurrence CopyFrom(UB04Occurrence source)
        {
            this.Code = source.Code;
            this.Date = source.Date;
            return this;
        }

        public UB04Occurrence CopyFrom(CodedDate source)
        {
            this.Code = source.Code;
            this.Date = string.Format("{0:MMddyy}", source.Date);
            return this;
        }

        public UB04Occurrence CopyFrom(InstitutionalProcedure source)
        {
            this.Code = source.Code;
            this.Date = string.Format("{0:MMddyy}", source.Date);
            return this;
        }
    }
}
