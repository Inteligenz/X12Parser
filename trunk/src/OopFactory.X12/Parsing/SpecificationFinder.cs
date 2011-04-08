using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    public class SpecificationFinder : ISpecificationFinder
    {
        public virtual TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            switch (transactionSetCode)
            {
                case "270":
                case "271":
                    return Get270TransactionSpecification();
                case "276":
                case "277":
                    return Get276TransactionSpecification();
                case "834":
                    return Get834TransactionSpecification();
                case "835":
                    return Get835TransactionSpecification();
                case "837":
                    if (versionCode.Contains("5010"))
                        return Get837_5010TransactionSpecification();
                    else
                        return Get837TransactionSpecification();
                case "856":
                    return Get856TransactionSpecification();
                case "997":
                    return Get997TransactionSpecification();
                default:
                    throw new NotSupportedException(String.Format("Transaction Set {0} is not supported.", transactionSetCode));
            }
        }

        public virtual SegmentSpecification FindSegmentSpec(string segmentId)
        {
            var idMap = Get4010Spec();
            if (idMap.ContainsKey(segmentId))
                return idMap[segmentId];
            else
                return null;
        }

        private static Dictionary<string, SegmentSpecification> _4010Specification;
        private static Dictionary<string, SegmentSpecification> Get4010Spec()
        {
            if (_4010Specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                _4010Specification = new Dictionary<string, SegmentSpecification>();
                foreach (var segment in set.Segments)
                    _4010Specification.Add(segment.SegmentId, segment);
            }
            return _4010Specification;
        }


        private static TransactionSpecification _270specification;

        private static TransactionSpecification Get270TransactionSpecification()
        {
            if (_270specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-270-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _270specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _270specification;
        }

        private static TransactionSpecification _276specification;

        private static TransactionSpecification Get276TransactionSpecification()
        {
            if (_276specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-276-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _276specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _276specification;
        }

        private static TransactionSpecification _997specification;

        private static TransactionSpecification Get997TransactionSpecification()
        {
            if (_997specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-997-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _997specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _997specification;
        }

        private static TransactionSpecification _834specification;

        private static TransactionSpecification Get834TransactionSpecification()
        {
            if (_834specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-834-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _834specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _834specification;
        }

        private static TransactionSpecification _837specification;

        private static TransactionSpecification Get837TransactionSpecification()
        {
            if (_837specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-837-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837specification;
        }

        private static TransactionSpecification _837_5010specification;

        private static TransactionSpecification Get837_5010TransactionSpecification()
        {
            if (_837_5010specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-837-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837_5010specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837_5010specification;
        }

        private static TransactionSpecification _835specification;

        private static TransactionSpecification Get835TransactionSpecification()
        {
            if (_835specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-835-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _835specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _835specification;
        }

        private static TransactionSpecification _856specification;

        private static TransactionSpecification Get856TransactionSpecification()
        {
            if (_856specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-856-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _856specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _856specification;
        }
    }
}
