namespace X12.Tests.Integration.Repositories
{
    using System;
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using X12.Sql;

    [TestClass, Ignore]
    public class GetTransactionSegmentsTester
    {
        [TestMethod]
        public void ReadTransactions()
        {
            var repo = new SqlTransactionRepository("Data Source=(local);Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));
            var list = repo.GetTransactionSets(new RepoTransactionSetSearchCriteria
            {
                SenderId = "580977458",
                InterchangeMinDate = DateTime.Parse("2011-01-01")
            });

            foreach (var set in list)
            {
                Trace.TraceInformation(
                    "Date: {0}, Transaction Set: {1}, Control Number: {2} ", set.InterchangeDate, set.TransactionSetCode, set.ControlNumber);
            }

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void ReadLoops()
        {
            var repo = new SqlTransactionRepository("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));
            var list = repo.GetLoops(new RepoLoopSearchCriteria { TransactionSetCode = "837", SpecLoopId = "2300" });

            Assert.IsTrue(list.Count > 0);
            foreach (var claim in list)
            {
                Trace.TraceInformation($"{claim.Segment.SegmentString}{claim.Segment.Delimiters.SegmentTerminator}");
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var repo = new SqlTransactionRepository("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));

            var segments = repo.GetTransactionSegments(831, 99, true);

            foreach (var seg in segments)
            {
                Trace.WriteLine(seg.Segment.SegmentString);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var repo = new SqlTransactionRepository("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));

            var segments = repo.GetTransactionSetSegments(821, 99, true);

            foreach (var seg in segments)
            {
                Trace.WriteLine(seg.Segment.SegmentString);
            }
        }
    }
}
