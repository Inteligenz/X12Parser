using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    public interface ISpecificationFinder
    {
        TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode);
        SegmentSpecification FindSegmentSpec(string versionCode, string segmentId);
    }
}
