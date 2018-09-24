namespace X12.Shared.Models.TypedElements
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
            string value = 
                $"{this.name}{this.segment.Delimiters.SubElementSeparator}{this.reference}";
            value = value.TrimEnd(this.segment.Delimiters.SubElementSeparator);
            this.segment.SetElement(this.elementNumber, value);
        }

        public string _1_ContextName
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.UpdateElement();
            }
        }

        public string _2_ContextReference
        {
            get
            {
                return this.reference;
            }

            set
            {
                this.reference = value;
                this.UpdateElement();
            }
        }
    }
}
