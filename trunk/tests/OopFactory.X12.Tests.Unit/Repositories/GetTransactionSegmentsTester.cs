using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;
using System.Reflection;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Repositories
{
    [TestClass]
    public class GetTransactionSegmentsTester
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repo = new SqlTransactionRepository("Data Source=DSTRU-PC;Initial Catalog=X12;Integrated Security=True", "Test");

            var segments = repo.GetTransactionSegments(831, true);

            foreach (var seg in segments)
                Trace.WriteLine(seg.Segment);

        }
        [TestMethod]
        public void TestMethod2()
        {
            var repo = new SqlTransactionRepository("Data Source=DSTRU-PC;Initial Catalog=X12;Integrated Security=True", "Test");

            var segments = repo.GetTransactionSetSegments(821, true);

            foreach (var seg in segments)
                Trace.WriteLine(seg.Segment);

        }
    }
}
