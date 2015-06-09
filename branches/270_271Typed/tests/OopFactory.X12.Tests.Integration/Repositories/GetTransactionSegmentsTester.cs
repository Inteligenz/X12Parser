using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;
using System.Reflection;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Integration.Repositories
{
    [TestClass,Ignore]
    public class GetTransactionSegmentsTester
    {
        [TestMethod]
        public void ReadTransactions()
        {
var repo = new SqlTransactionRepository<long>("Data Source=(local);Initial Catalog=X12;Integrated Security=True", "Test");
var list = repo.GetTransactionSets(new RepoTransactionSetSearchCriteria<long>
{
    SenderId = "580977458",
    InterchangeMinDate = DateTime.Parse("2011-01-01")
});

foreach (var set in list)
{
    Trace.TraceInformation("Date: {0}, Transaction Set: {1}, Control Number: {2} ", set.InterchangeDate, set.TransactionSetCode, set.ControlNumber);
}

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void ReadLoops()
        {
            var repo = new SqlTransactionRepository<long>("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test");
            var list = repo.GetLoops(new RepoLoopSearchCriteria<long> { TransactionSetCode = "837", SpecLoopId = "2300" });

            Assert.IsTrue(list.Count > 0);
            foreach (var claim in list)
                Trace.TraceInformation("{0}{1}", claim.Segment.SegmentString, claim.Segment.Delimiters.SegmentTerminator);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var repo = new SqlTransactionRepository<long>("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test");

            var segments = repo.GetTransactionSegments(831, 99, true);

            foreach (var seg in segments)
                Trace.WriteLine(seg.Segment.SegmentString);

        }
        [TestMethod]
        public void TestMethod2()
        {
            var repo = new SqlTransactionRepository<long>("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test");

            var segments = repo.GetTransactionSetSegments(821, 99, true);

            foreach (var seg in segments)
                Trace.WriteLine(seg.Segment.SegmentString);

        }
    }
}
