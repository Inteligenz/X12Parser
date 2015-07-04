using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositeMedicalProcedureIdentifier : BaseElementReference
    {
        private ProductOrServiceIdQualifiers _productOrServiceIdQualifier;
        private string _procedureCode;
        private string _procedureModifier1;
        private string _procedureModifier2;
        private string _procedureModifier3;
        private string _procedureModifier4;
        private string _description;
        private string _productOrServiceId;

        public TypedElementCompositeMedicalProcedureIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            Segment = segment;
            ElementNumber = elementNumber;
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                Segment._delimiters.SubElementSeparator,
                _productOrServiceIdQualifier.EDIFieldValue(),
                _procedureCode,
                _procedureModifier1,
                _procedureModifier2,
                _procedureModifier3,
                _procedureModifier4,
                _description,
                _productOrServiceId);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public ProductOrServiceIdQualifiers _1_ProductOrServiceIdQualifier
        {
            get { return _productOrServiceIdQualifier; }
            set { _productOrServiceIdQualifier = value; }
        }

        public string _2_ProcedureCode
        {
            get { return _procedureCode; }
            set { _procedureCode = value; }
        }

        public string _3_ProcedureModifier
        {
            get { return _procedureModifier1; }
            set
            {
                _procedureModifier1 = value;
            }
        }

        public string _4_ProcedureModifier
        {
            get { return _procedureModifier2; }
            set
            {
                _procedureModifier2 = value;
            }
        }

        public string _5_ProcedureModifier
        {
            get { return _procedureModifier3; }
            set
            {
                _procedureModifier3 = value;
            }
        }

        public string _6_ProcedureModifier
        {
            get { return _procedureModifier4; }
            set
            {
                _procedureModifier4 = value;
            }
        }

        public string _7_Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        public string _8_ProductOrServiceId
        {
            get { return _productOrServiceId; }
            set
            {
                _productOrServiceId = value;
            }
        }
    }
}