﻿namespace X12.Specifications.Finders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    
    using X12.Specifications.Enumerations;
    using X12.Specifications.Interfaces;
    using X12.Specifications.Sets;

    public class SpecificationFinder : ISpecificationFinder
    {
        private static readonly object SyncObject = new object();

        private static readonly ConcurrentDictionary<string, TransactionSpecification> Specifications;

        private static Dictionary<string, SegmentSpecification> _4010Specification;

        private static Dictionary<string, SegmentSpecification> _5010Specification;

        /// <summary>
        /// Initializes static members of the <see cref="SpecificationFinder"/> class
        /// </summary>
        static SpecificationFinder()
        {
            Specifications = new ConcurrentDictionary<string, TransactionSpecification>();
        }

        /// <summary>
        /// Gets the transaction specification for the provided codes
        /// </summary>
        /// <param name="functionalCode">Function code</param>
        /// <param name="versionCode">Specification version code</param>
        /// <param name="transactionSetCode">Transaction set code</param>
        /// <returns>Transaction specification which matches the codes provided</returns>
        /// <exception cref="System.NotSupportedException">Thrown if the codes provided do not map to a known specification</exception>
        public virtual TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            switch (transactionSetCode)
            {
                case "270":
                    return versionCode.Contains("5010")
                               ? GetSpecification("270-5010")
                               : GetSpecification("270-4010");
                case "271":
                    return versionCode.Contains("5010")
                               ? GetSpecification("271-5010")
                               : GetSpecification("271-4010");
                case "275":
                    return GetSpecification("275-4050");
                case "276":
                case "277":
                    return GetSpecification("276-5010");
                case "278":
                    return versionCode.Contains("5010")
                               ? GetSpecification("278-5010")
                               : GetSpecification("278-4010");
                case "834":
                    return versionCode.Contains("5010")
                               ? GetSpecification("834-5010")
                               : GetSpecification("834-4010");
                case "835":
                    return versionCode.Contains("5010")
                               ? GetSpecification("835-5010")
                               : GetSpecification("835-4010");
                case "837":
                    return versionCode.Contains("5010")
                               ? GetSpecification("837-5010")
                               : GetSpecification("837-4010");
                case "875":
                    return GetSpecification("875-5010");
                case "880":
                    return versionCode.Contains("5010")
                               ? GetSpecification("880-5010")
                               : GetSpecification("880-4010");
                case "999":
                    return GetSpecification("999-5010");
                default:
                    Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"X12.Specifications.Resource.Ansi-{transactionSetCode}-4010Specification.xml");
                    if (specStream != null)
                    {
                        return GetSpecification(transactionSetCode + "-4010");
                    }

                    specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"X12.Specifications.Resource.Ansi-{transactionSetCode}-Specification.xml");
                    if (specStream != null)
                    {
                        return GetSpecification(transactionSetCode + "-");
                    }

                    throw new NotSupportedException(string.Format(Properties.Resources.UnsupportedTransactionSet, transactionSetCode));
            }
        }

        /// <summary>
        /// Gets the segment specification for the version code and ID provided
        /// </summary>
        /// <param name="versionCode">Specification version</param>
        /// <param name="segmentId">Segment ID</param>
        /// <returns>Segment specification which matches the parameters provided; otherwise, null</returns>
        public virtual SegmentSpecification FindSegmentSpec(string versionCode, string segmentId)
        {
            if (versionCode.Contains("5010"))
            {
                var idMap5010 = Get5010Spec();
                if (idMap5010.ContainsKey(segmentId))
                {
                    return idMap5010[segmentId];
                }
            }

            var idMap4010 = Get4010Spec();
            return idMap4010.ContainsKey(segmentId) ? idMap4010[segmentId] : null;
        }

        /// <summary>
        /// Gets the transaction specification for the provided key
        /// </summary>
        /// <param name="specKey">Specification key to filter</param>
        /// <returns>Transaction specification found with the key</returns>
        /// <exception cref="ArgumentNullException">Thrown if the key is invalid, or cannot be found in the collection of specifications</exception>
        internal static TransactionSpecification GetSpecification(string specKey)
        {
            return Specifications.GetOrAdd(
                specKey,
                key =>
                    {
                        Stream specStream = Assembly.GetExecutingAssembly()
                            .GetManifestResourceStream($"X12.Specifications.Resource.Ansi-{key}Specification.xml");
                        return TransactionSpecification.Deserialize(new StreamReader(specStream).ReadToEnd());
                    });
        }

        private static Dictionary<string, SegmentSpecification> Get4010Spec()
        {
            lock (SyncObject)
            {
                if (_4010Specification == null)
                {
                    Stream specStream = Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("X12.Specifications.Resource.Ansi-4010Specification.xml");
                    var reader = new StreamReader(specStream);
                    SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                    _4010Specification = new Dictionary<string, SegmentSpecification>();
                    foreach (var segment in set.Segments)
                    {
                        foreach (var element in segment.Elements)
                        {
                            if (element.Type == ElementDataType.Identifier
                                && !string.IsNullOrEmpty(element.QualifierSetRef))
                            {
                                var qualifierSet =
                                    set.QualifierSets.FirstOrDefault(qs => qs.Name == element.QualifierSetRef);
                                if (qualifierSet != null)
                                {
                                    element.AllowedIdentifiers.AddRange(qualifierSet.AllowedIdentifiers);
                                }
                            }
                        }

                        _4010Specification.Add(segment.SegmentId, segment);
                    }
                }
            }

            return _4010Specification;
        }

        private static Dictionary<string, SegmentSpecification> Get5010Spec()
        {
            lock (SyncObject)
            {
                if (_5010Specification == null)
                {
                    Stream specStream = Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("X12.Specifications.Resource.Ansi-5010Specification.xml");
                    var reader = new StreamReader(specStream);
                    SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                    _5010Specification = new Dictionary<string, SegmentSpecification>();
                    foreach (var segment in set.Segments)
                    {
                        foreach (var element in segment.Elements)
                        {
                            if (element.Type == ElementDataType.Identifier
                                && !string.IsNullOrEmpty(element.QualifierSetRef))
                            {
                                var qualifierSet =
                                    set.QualifierSets.FirstOrDefault(qs => qs.Name == element.QualifierSetRef);
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
            }

            return _5010Specification;
        }
    }
}