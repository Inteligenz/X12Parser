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

namespace OopFactory.X12.Tests.Unit.Repositories
{
    [TestClass]
    public class SaveRevisionTester
    {
        [TestMethod, Ignore]
        public void SaveRevisionTest()
        {
            var repo = new SqlTransactionRepository("Data Source=DSTRU-PC;Initial Catalog=X12;Integrated Security=True", 
                new SpecificationFinder(),
                "NM1,N1,N3,N4,N9,REF,PER".Split(','), "Test", "dbo");

            var segments = repo.GetTransactionSegments(3293, false, 999999);

            var subsriberSeg = segments.First(s => s.SpecLoopId == "2010BA");
            var newSeg = new DetachedSegment(new X12DelimiterSet(subsriberSeg.SegmentTerminator, subsriberSeg.ElementSeparator, subsriberSeg.ComponentSeparator), subsriberSeg.SegmentString);
            newSeg.SetElement(5,"MID");
            subsriberSeg.SegmentString = newSeg.SegmentString;

            var claimSeg = segments.First(s => s.SpecLoopId == "2300");

            var editSeg = new DetachedSegment(new X12DelimiterSet(claimSeg.SegmentTerminator, claimSeg.ElementSeparator, claimSeg.ComponentSeparator), claimSeg.SegmentString);
            editSeg.SetElement(1, "ABC26403774");
            claimSeg.SegmentString = editSeg.SegmentString;

            var tooSeg = segments.First(s => s.PositionInInterchange == 30);
            tooSeg.Deleted = true;
            
            int revId = repo.SaveRevision(3293, segments, "Testing the revision feature", Environment.UserName);

            Trace.WriteLine(revId);

            Assert.IsTrue(revId > 0);
        }
    }
}
