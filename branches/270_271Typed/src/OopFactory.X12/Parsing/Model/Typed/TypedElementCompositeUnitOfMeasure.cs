using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Linq;
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositeUnitOfMeasure : BaseElementReference
    {
        public TypedElementCompositeUnitOfMeasure(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_UnitOrBasisMeasCode = SubElements.ElementAt(0).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>();
            if (1 < SubElements.Count()) _2_Exponent1 = SubElements.ElementAt(1);
            if (2 < SubElements.Count()) _3_Multiplier = SubElements.ElementAt(2);
            if (3 < SubElements.Count()) _4_UnitOrBasisMeasCode = SubElements.ElementAt(3).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>();
            if (4 < SubElements.Count()) _5_Exponent2 = SubElements.ElementAt(4);
            if (5 < SubElements.Count()) _6_Multiplier = SubElements.ElementAt(5);
            if (6 < SubElements.Count()) _7_UnitOrBasisMeasCode = SubElements.ElementAt(6).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>();
            if (7 < SubElements.Count()) _8_Exponent3 = SubElements.ElementAt(7);
            if (8 < SubElements.Count()) _9_Multiplier = SubElements.ElementAt(8);
            if (9 < SubElements.Count()) _10_UnitOrBasisMeasCode = SubElements.ElementAt(9).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>();
            if (10 < SubElements.Count()) _11_Exponent4 = SubElements.ElementAt(10);
            if (11 < SubElements.Count()) _12_Multiplier = SubElements.ElementAt(11);
            if (12 < SubElements.Count()) _13_UnitOrBasisMeasCode = SubElements.ElementAt(12).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>();
            if (13 < SubElements.Count()) _14_Exponent5 = SubElements.ElementAt(13);
            if (14 < SubElements.Count()) _15_Multiplier = SubElements.ElementAt(14);
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}",
                Segment._delimiters.SubElementSeparator,
                _1_UnitOrBasisMeasCode.EDIFieldValue(),
                _2_Exponent1,
                _3_Multiplier,
                _4_UnitOrBasisMeasCode.EDIFieldValueSafe(),
                _5_Exponent2,
                _6_Multiplier,
                _7_UnitOrBasisMeasCode.EDIFieldValueSafe(),
                _8_Exponent3,
                _9_Multiplier,
                _10_UnitOrBasisMeasCode.EDIFieldValueSafe(),
                _11_Exponent4,
                _12_Multiplier,
                _13_UnitOrBasisMeasCode.EDIFieldValueSafe(),
                _14_Exponent5,
                _15_Multiplier);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public UnitOrBasisOfMeasurementCode _1_UnitOrBasisMeasCode { get; set; }
        public string _2_Exponent1 { get; set; }
        public string _3_Multiplier { get; set; }

        public UnitOrBasisOfMeasurementCode? _4_UnitOrBasisMeasCode { get; set; }
        public string _5_Exponent2 { get; set; }
        public string _6_Multiplier { get; set; }

        public UnitOrBasisOfMeasurementCode? _7_UnitOrBasisMeasCode { get; set; }
        public string _8_Exponent3 { get; set; }
        public string _9_Multiplier { get; set; }

        public UnitOrBasisOfMeasurementCode? _10_UnitOrBasisMeasCode { get; set; }
        public string _11_Exponent4 { get; set; }
        public string _12_Multiplier { get; set; }

        public UnitOrBasisOfMeasurementCode? _13_UnitOrBasisMeasCode { get; set; }
        public string _14_Exponent5 { get; set; }
        public string _15_Multiplier { get; set; }
    }
}