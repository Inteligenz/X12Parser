namespace OopFactory.X12.Shared.Models.TypedElements
{
    public class TypedElementContextIdentification
    {
        private readonly int elementNumber;
        private readonly Segment segment;
        private string name;
        private string reference;

        internal TypedElementContextIdentification(Segment segment, int elementNumber)
        {
            this.segment = segment;
            this.elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format("{1}{0}{2}",
                this.segment.Delimiters.SubElementSeparator,
                this.name,
                this.reference);
            value = value.TrimEnd(this.segment.Delimiters.SubElementSeparator);
            this.segment.SetElement(this.elementNumber, value);
        }

        public string _1_ContextName
        {
            get { return this.name; }
            set { this.name = value; UpdateElement(); }
        }

        public string _2_ContextReference
        {
            get { return this.reference; }
            set { this.reference = value; UpdateElement(); }
        }
    }
}
