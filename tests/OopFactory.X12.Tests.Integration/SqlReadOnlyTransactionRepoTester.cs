namespace OopFactory.X12.Tests.Integration
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    
    using OopFactory.X12.Sql;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SqlReadOnlyTransactionRepoTester
    {
        const string dsn = "Data Source=127.0.0.1;Initial Catalog={0};Integrated Security=True";
        const string testDirectory = @"C:\OopFactoryTest";

        [TestInitialize]
        public void TestInitialize()
        {
            if (!System.IO.Directory.Exists(testDirectory))
            {
                System.IO.Directory.CreateDirectory(testDirectory);
            }

            string createDbQuery = string.Format(
                @"CREATE DATABASE Test
                ON
                ( NAME = Test_dat,  
                    FILENAME = '{0}\test_1.mdf',  
                    SIZE = 10,  
                    MAXSIZE = 50,  
                    FILEGROWTH = 5 )
	            LOG ON 
                ( NAME = Test_log,  
                    FILENAME = '{0}\test_log_1.ldf',  
                    SIZE = 10,  
                    MAXSIZE = 50,  
                    FILEGROWTH = 5 )", testDirectory);

            using (var connection = new SqlConnection(string.Format(dsn, "master")))
            {
                connection.Open();
                using (var command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            string deleteDbQuery = "DROP DATABASE Test";

            using (var connection = new SqlConnection(string.Format(dsn, "master")))
            {
                connection.Open();
                using (var command = new SqlCommand(deleteDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (System.IO.Directory.Exists(testDirectory))
            {
                foreach (var file in System.IO.Directory.EnumerateFiles(testDirectory))
                {
                    System.IO.File.Delete(file);
                }

                System.IO.Directory.Delete(testDirectory);
            }
        }

        [TestMethod]
        public void GetEntity()
        {
            var repo = new SqlReadOnlyTransactionRepository(string.Format(dsn, "Test"), typeof(Guid));

            var entities = repo.GetEntities(new RepoEntitySearchCriteria
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
