namespace OopFactory.X12.Tests.Integration
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using OopFactory.X12.Sql;

    [TestClass]
    public class SqlReadOnlyTransactionRepoTester
    {
        private const string Dsn = "Data Source=localhost;Initial Catalog={0};Integrated Security=True";
        private const string TestDirectory = @"C:\X12Test";

        /// <summary>
        /// Performs test initialization (creates database, test directory, etc)
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            if (!System.IO.Directory.Exists(TestDirectory))
            {
                System.IO.Directory.CreateDirectory(TestDirectory);
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
                    FILEGROWTH = 5 )",
                TestDirectory);

            using (var connection = new SqlConnection(string.Format(Dsn, "master")))
            {
                connection.Open();
                using (var command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Performs test cleanup (deletes database, test directory, etc)
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            string deleteDbQuery = "DROP DATABASE Test";

            using (var connection = new SqlConnection(string.Format(Dsn, "master")))
            {
                connection.Open();
                using (var command = new SqlCommand(deleteDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (System.IO.Directory.Exists(TestDirectory))
            {
                System.IO.Directory.Delete(TestDirectory, true);
            }
        }

        /// <summary>
        /// Tests that entities can be read from the database
        /// </summary>
        /// <remarks>Being ignored due to database population issue</remarks>
        [TestMethod, Ignore]
        public void GetEntity()
        {
            var repo = new SqlReadOnlyTransactionRepository(string.Format(Dsn, "Test"), typeof(Guid));

            var entities = repo.GetEntities(new RepoEntitySearchCriteria
            {
                EntityIdentifierCodes = "IL,QC",
                TransactionSetCode = "837",
                LastNameStartsWith = "Smith",
                DateOfBirthOnOrAfter = DateTime.Parse("1950-01-01")
            });

            Assert.IsTrue(entities.Count > 0);
            Assert.IsTrue(entities.Count(e => e.EntityIdentifierCode == "IL") > 0);
            Assert.IsTrue(entities.Count(e => e.EntityIdentifierCode == "QC") > 0);

            foreach (var entity in entities)
            {
                Trace.TraceInformation(
                    "{0}: {1}, {2} {3}",
                    entity.EntityIdentifierCode,
                    entity.Name,
                    entity.DateOfBirth,
                    entity.City);
            }
        }
    }
}
