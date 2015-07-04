using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositeUnitOfMeasure : BaseElementReference
    {
        private UnitOrBasisOfMeasurementCode _unitOrBasisMeasCode1;
        private string _exponent1;
        private string _multiplier1;

        private UnitOrBasisOfMeasurementCode _unitOrBasisMeasCode2;
        private string _exponent2;
        private string _multiplier2;

        private UnitOrBasisOfMeasurementCode _unitOrBasisMeasCode3;
        private string _exponent3;
        private string _multiplier3;

        private UnitOrBasisOfMeasurementCode _unitOrBasisMeasCode4;
        private string _exponent4;
        private string _multiplier4;

        private UnitOrBasisOfMeasurementCode _unitOrBasisMeasCode5;
        private string _exponent5;
        private string _multiplier5;

        public TypedElementCompositeUnitOfMeasure(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}",
                Segment._delimiters.SubElementSeparator,
                _unitOrBasisMeasCode1.EDIFieldValue(),
                _exponent1,
                _multiplier1,
                _unitOrBasisMeasCode2.EDIFieldValue(),
                _exponent2,
                _multiplier2,
                _unitOrBasisMeasCode3.EDIFieldValue(),
                _exponent3,
                _multiplier3,
                _unitOrBasisMeasCode4.EDIFieldValue(),
                _exponent4,
                _multiplier4,
                _unitOrBasisMeasCode5.EDIFieldValue(),
                _exponent5,
                _multiplier5);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public UnitOrBasisOfMeasurementCode _1_UnitOrBasisMeasCode
        {
            get { return _unitOrBasisMeasCode1; }
            set { _unitOrBasisMeasCode1 = value; }
        }

        public string _2_Exponent1
        {
            get { return _exponent1; }
            set { _exponent1 = value; }
        }

        public string _3_Multiplier
        {
            get { return _multiplier1; }
            set { _multiplier1 = value; }
        }

        public UnitOrBasisOfMeasurementCode _4_UnitOrBasisMeasCode
        {
            get { return _unitOrBasisMeasCode2; }
            set { _unitOrBasisMeasCode2 = value; }
        }

        public string _5_Exponent2
        {
            get { return _exponent2; }
            set { _exponent2 = value; }
        }

        public string _6_Multiplier
        {
            get { return _multiplier2; }
            set { _multiplier2 = value; }
        }

        public UnitOrBasisOfMeasurementCode _7_UnitOrBasisMeasCode
        {
            get { return _unitOrBasisMeasCode3; }
            set { _unitOrBasisMeasCode3 = value; }
        }

        public string _8_Exponent3
        {
            get { return _exponent3; }
            set { _exponent3 = value; }
        }

        public string _9_Multiplier
        {
            get { return _multiplier3; }
            set { _multiplier3 = value; }
        }

        public UnitOrBasisOfMeasurementCode _10_UnitOrBasisMeasCode
        {
            get { return _unitOrBasisMeasCode4; }
            set { _unitOrBasisMeasCode4 = value; }
        }

        public string _11_Exponent4
        {
            get { return _exponent4; }
            set { _exponent4 = value; }
        }

        public string _12_Multiplier
        {
            get { return _multiplier4; }
            set { _multiplier4 = value; }
        }

        public UnitOrBasisOfMeasurementCode _11_UnitOrBasisMeasCode
        {
            get { return _unitOrBasisMeasCode5; }
            set { _unitOrBasisMeasCode5 = value; }
        }

        public string _12_Exponent5
        {
            get { return _exponent5; }
            set { _exponent5 = value; }
        }

        public string _13_Multiplier
        {
            get { return _multiplier5; }
            set { _multiplier5 = value; }
        }
    }
}