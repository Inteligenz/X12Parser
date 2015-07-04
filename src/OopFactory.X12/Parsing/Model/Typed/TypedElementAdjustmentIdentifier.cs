using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementAdjustmentIdentifier : BaseElementReference
    {
        public TypedElementAdjustmentIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }
        private string _adjustmentReasonCode;
        private string _referenceIdentification;

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}",
                 Segment._delimiters.SubElementSeparator,
                 _adjustmentReasonCode,
                 _referenceIdentification);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_AdjustmentReasonCode
        {
            get { return _adjustmentReasonCode; }
            set { _adjustmentReasonCode = value; }
        }

        public string _2_ReferenceIdentification
        {
            get { return _referenceIdentification; }
            set { _referenceIdentification = value; }
        }
    }
}
