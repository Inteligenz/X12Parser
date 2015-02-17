using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Repositories;
using System.Reflection;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Integration.Repositories
{
    [TestClass]
    public class SaveRevisionTester
    {
        [TestMethod, Ignore]
        public void SaveRevisionTest()
        {
            var repo = new SqlTransactionRepository<long>("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", 
                new SpecificationFinder(),
                "NM1,N1,N3,N4,N9,REF,PER".Split(','), "Test", "dbo");

            var segments = repo.GetTransactionSegments(3293, Int32.MaxValue, false);
            
            var subsriberSeg = segments.First(s => s.SpecLoopId == "2010BA");
            subsriberSeg.Segment.SetElement(5,"MID");

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
            var repo = new SqlTransactionRepository<Guid>("Data Source=127.0.0.1;Initial Catalog=Test5;Integrated Security=True",
                new SpecificationFinder(),
                "NM1,N1,N3,N4,N9,REF,PER".Split(','), "X12", "dbo");

            var segments = repo.GetTransactionSegments(Guid.Parse("DC737E4D-33D3-487D-9C36-00C93759B8C4"), Int32.MaxValue, false);

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
