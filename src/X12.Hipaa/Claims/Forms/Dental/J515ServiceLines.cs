namespace X12.Hipaa.Claims.Forms.Dental
{
    #if DEBUG
    class J515ServiceLines
    {
        public string Field24_ProcedureDate { get; set; }
        public string Field25_AreaOfOralCavity { get; set; }
        public string Field26_ToothSystem { get; set; }
        public string Field27_ToothNumberOrLetter { get; set; }
        public string Field28_ToothSurface { get; set; }
        public string Field29_ProcedureCode { get; set; }
        public string Field30_Description { get; set; }
        public decimal Field31_Fee { get; set; }
    }

    class Field34_MissingTeethInfo_Permanent
    {
        public string MissingTeethInfo_Permanent_hi { get; set; }
    }

    class Field34_MissingTeethInfo_Primary
    {
        public string PrimaryMissing { get; set; }
    }
#endif
}
