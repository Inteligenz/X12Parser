using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Repositories;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Integration
{
    [TestClass]
    public class SqlReadOnlyTransactionRepoTester
    {
        const string dsn = "Data Source=127.0.0.1;Initial Catalog=Test5;Integrated Security=True";
        [TestMethod]
        public void GetEntity()
        {
            var repo = new SqlReadOnlyTransactionRepository<Guid>(dsn, "X12");

            var entities = repo.GetEntities(new RepoEntitySearchCriteria<Guid>
            {
                EntityIdentifierCodes = "IL,QC",
                TransactionSetCode = "837",
                LastNameStartsWith = "Smith",
                DateOfBirthOnOrAfter = DateTime.Parse("1950-01-01")
            });

            Assert.IsTrue(entities.Count > 0);
            Assert.IsTrue(entities.Where(e => e.EntityIdentifierCode == "IL").Count() > 0);
            Assert.IsTrue(entities.Where(e => e.EntityIdentifierCode == "QC").Count() > 0);

            foreach (var entity in entities)
            {
                Trace.TraceInformation("{0}: {1}, {2} {3}",
                    entity.EntityIdentifierCode,
                    entity.Name,
                    entity.DateOfBirth,
                    entity.City);
            }
        }
    }
}
