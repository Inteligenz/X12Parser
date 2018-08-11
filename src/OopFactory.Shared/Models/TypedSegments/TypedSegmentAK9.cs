namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentAK9 : TypedSegment
    {
        public TypedSegmentAK9()
            : base("AK9")
        {
        }

        public string AK901_FunctionalGroupAcknowledgeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public int AK902_NumberOfTransactionSetsIncluded
        {
            get
            {
                int count;
                if (int.TryParse(this.Segment.GetElement(2), out count))
                {
                    return count;
                }
                else
                {
                    return 0;
                }

            }

            set
            {
                this.Segment.SetElement(2, value.ToString());
            }
        }

        public int AK903_NumberOfReceivedTransactionSets
        {
            get
            {
                int count;
                if (int.TryParse(this.Segment.GetElement(3), out count))
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                this.Segment.SetElement(3, value.ToString());
            }
        }

        public int AK904_NumberOfAcceptedTransactionSets
        {
            get
            {
                int count;
                if (int.TryParse(this.Segment.GetElement(4), out count))
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                this.Segment.SetElement(4, value.ToString());
            }
        }

        public string AK905_FunctionalGroupSyntaxErrorCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string AK906_FunctionalGroupSyntaxErrorCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string AK907_FunctionalGroupSyntaxErrorCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string AK908_FunctionalGroupSyntaxErrorCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string AK909_FunctionalGroupSyntaxErrorCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
