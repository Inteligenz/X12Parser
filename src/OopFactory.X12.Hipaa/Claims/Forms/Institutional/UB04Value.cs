namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    using OopFactory.X12.Hipaa.Common;

    public class UB04Value
    {
        public string Code { get; set; }

        public decimal? Amount { get; set; }

        public UB04Value CopyFrom(CodedAmount source)
        {
            this.Code = source.Code;
            this.Amount = source.Amount;
            return this;
        }
    }
}
