using System;
using System.Collections.Concurrent;
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
        private static readonly object syncObject = new object();

        public virtual TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            switch (transactionSetCode)
            {
                case "270":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("270-5010");
                    else
                        return GetSpecification("270-4010");
                case "271":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("271-5010");
                    else
                        return GetSpecification("271-4010");
                case "275":
                    return GetSpecification("275-4050");
                case "276":
                case "277":
                    return GetSpecification("276-5010");
                case "278":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("278-5010");
                    else
                        return GetSpecification("278-4010");
                case "834":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("834-5010");
                    else
                        return GetSpecification("834-4010");
                case "835":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("835-5010");
                    else
                        return GetSpecification("835-4010");
                case "837":
                    if (versionCode.Contains("5010"))
                        return GetSpecification("837-5010");
                    else
                        return GetSpecification("837-4010");
                case "875":
                    return GetSpecification("875-5010");
                case "880":
                    if (versionCode.Contains("5050"))
                        return GetSpecification("880-5050");
                    else
                        return GetSpecification("880-4010");
                case "999":
                    return GetSpecification("999-5010");
                default:
                    Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("OopFactory.X12.Specifications.Ansi-{0}-4010Specification.xml", transactionSetCode));
                    if (specStream != null)
                        return GetSpecification(transactionSetCode + "-4010");

                    specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("OopFactory.X12.Specifications.Ansi-{0}-Specification.xml", transactionSetCode));
                    if (specStream != null)
                        return GetSpecification(transactionSetCode + "-");

                    throw new NotSupportedException(String.Format("Transaction Set {0} is not supported.", transactionSetCode));
            }
        }

        public virtual SegmentSpecification FindSegmentSpec(string versionCode, string segmentId)
        {
            if (versionCode.Contains("5010"))
            {
                var idMap5010 = Get5010Spec();
                if (idMap5010.ContainsKey(segmentId))
                    return idMap5010[segmentId];
            }

            var idMap4010 = Get4010Spec();
            if (idMap4010.ContainsKey(segmentId))
                return idMap4010[segmentId];
            else
                return null;
        }

        private static Dictionary<string, SegmentSpecification> _4010Specification;
        private static Dictionary<string, SegmentSpecification> Get4010Spec()
        {
            lock (syncObject)
                if (_4010Specification == null)
                {
                    Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-4010Specification.xml");
                    StreamReader reader = new StreamReader(specStream);
                    SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                    _4010Specification = new Dictionary<string, SegmentSpecification>();
                    foreach (var segment in set.Segments)
                    {
                        foreach (var element in segment.Elements)
                        {
                            if (element.Type == ElementDataTypeEnum.Identifier && !string.IsNullOrEmpty(element.QualifierSetRef))
                            {
                                var qualifierSet = set.QualifierSets.FirstOrDefault(qs => qs.Name == element.QualifierSetRef);
                                if (qualifierSet != null)
                                    element.AllowedIdentifiers.AddRange(qualifierSet.AllowedIdentifiers);
                            }
                        }
                        _4010Specification.Add(segment.SegmentId, segment);
                    }
                }
            return _4010Specification;
        }

        private static Dictionary<string, SegmentSpecification> _5010Specification;
        private static Dictionary<string, SegmentSpecification> Get5010Spec()
        {
            lock (syncObject)
                if (_5010Specification == null)
                {
                    Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-5010Specification.xml");
                    StreamReader reader = new StreamReader(specStream);
                    SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                    _5010Specification = new Dictionary<string, SegmentSpecification>();
                    foreach (var segment in set.Segments)
                    {
                        foreach (var element in segment.Elements)
                        {
                            if (element.Type == ElementDataTypeEnum.Identifier && !string.IsNullOrEmpty(element.QualifierSetRef))
                            {
                                var qualifierSet = set.QualifierSets.FirstOrDefault(qs => qs.Name == element.QualifierSetRef);
                                if (qualifierSet != null)
                                {
                                    element.AllowedIdentifiers.AddRange(qualifierSet.AllowedIdentifiers);
                                    element.QualifierSetId = qualifierSet.Id;
                                }
                            }
                        }

                        _5010Specification.Add(segment.SegmentId, segment);
                    }
                }
            return _5010Specification;
        }

        private static readonly ConcurrentDictionary<string, TransactionSpecification> _specifications;

        static SpecificationFinder()
        {
            _specifications = new ConcurrentDictionary<string, TransactionSpecification>();
        }

        internal static TransactionSpecification GetSpecification(string specKey)
        {
            return _specifications.GetOrAdd(specKey, key =>
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("OopFactory.X12.Specifications.Ansi-{0}Specification.xml", key));
                return TransactionSpecification.Deserialize(new StreamReader(specStream).ReadToEnd());
            });
        }
    }
}