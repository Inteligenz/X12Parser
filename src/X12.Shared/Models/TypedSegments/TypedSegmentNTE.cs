namespace X12.Shared.Models.TypedSegments
{
    /// <summary>
    /// Note/Secial Instruction
    /// </summary>
    public class TypedSegmentNTE : TypedSegment
    {
        public TypedSegmentNTE()
            : base("NTE")
        {
        }

        /// <summary>
        /// GEN = Entire Transaction Set
        /// </summary>
        public string NTE01_NoteReferenceCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string NTE02_Description
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
    }
}
