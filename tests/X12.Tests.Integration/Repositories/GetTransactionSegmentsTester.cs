namespace X12.Tests.Integration.Repositories
{
    using System;

    using NUnit.Framework;

    using X12.Sql;

    [TestFixture, Ignore("Database tests have issues with authentication, SetUp, and TearDown")]
    public class GetTransactionSegmentsTester
    {
        [Test]
        public void ReadTransactions()
        {
            // arrange
            var repo = new SqlTransactionRepository("Data Source=(local);Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));

            // act
            var list = repo.GetTransactionSets(new RepoTransactionSetSearchCriteria
            {
                SenderId = "580977458",
                InterchangeMinDate = DateTime.Parse("2011-01-01")
            });

            // assert
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void ReadLoops()
        {
            // arrange
            var repo = new SqlTransactionRepository("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));

            // act
            var list = repo.GetLoops(new RepoLoopSearchCriteria { TransactionSetCode = "837", SpecLoopId = "2300" });

            // assert
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void TestGetTransactionSegments([Values(831, 821)] int loopId)
        {
            // arrange
            var repo = new SqlTransactionRepository("Data Source=localhost;Initial Catalog=X12;Integrated Security=True", "Test", typeof(long));

            // act - assert
            var segments = repo.GetTransactionSegments(loopId, 99, true);
        }
    }
}
