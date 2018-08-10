namespace OopFactory.X12.Shared.Models.Typed
{
    public class TypedElementHealthCareCodeInfo
    {
        private readonly int elementNumber;
        private readonly Segment segment;

        internal TypedElementHealthCareCodeInfo(Segment segment, int elementNumber)
        {
            this.segment = segment;
            this.elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
        }
    }
}
