namespace OopFactory.X12.Tests.Integration.Repositories
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Sql;

    [TestClass]
    public class SaveRevisionTester
    {
        private static readonly string Dsn = "Data Source=localhost;Initial Catalog=Test;Integrated Security=True";

        [TestMethod, Ignore]
        public void SaveRevisionTest()
        {
            var repo = new SqlTransactionRepository(
                Dsn, 
                new SpecificationFinder(),
                "NM1,N1,N3,N4,N9,REF,PER".Split(','),
                typeof(long),
                "Test");

            var segments = repo.GetTransactionSegments(3293, int.MaxValue);
            
            var subsriberSeg = segments.First(s => s.SpecLoopId == "2010BA");
            subsriberSeg.Segment.SetElement(5, "MID");

            var claimSeg = segments.First(s => s.SpecLoopId == "2300");
            claimSeg.Segment.SetElement(1, "ABC26403774");

            var tooSeg = segments.First(s => s.PositionInInterchange == 30);
            tooSeg.Deleted = true;
            
            long revId = repo.SaveRevision(segments, "Testing the revision feature", Environment.UserName);

            Trace.WriteLine(revId);

            Assert.IsTrue(revId > 0);
        }

        [TestMethod]
        public void SaveRevisionGuidTest()
        {
            var repo = new SqlTransactionRepository(
                Dsn,
                new SpecificationFinder(),
                "NM1,N1,N3,N4,N9,REF,PER".Split(','),
                typeof(Guid),
                "X12");

            var segments = repo.GetTransactionSegments(Guid.Parse("DC737E4D-33D3-487D-9C36-00C93759B8C4"), int.MaxValue);

            var subsriberSeg = segments.First(s => s.SpecLoopId == "2010BA");
            subsriberSeg.Segment.SetElement(5, "MID");

            var claimSeg = segments.First(s => s.SpecLoopId == "2300");
            claimSeg.Segment.SetElement(1, "ABC26403774");

            var tooSeg = segments.First(s => s.PositionInInterchange == 30);
            tooSeg.Deleted = true;

            long revId = repo.SaveRevision(segments, "Testing the revision feature", Environment.UserName);

            Trace.WriteLine(revId);

            Assert.IsTrue(revId > 0);
        }
    }
}
