using JEdwards.Domain.Entities;
using JEdwards.Infrastructure.Database.Implemenations;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        private readonly TestDbContext _dbContext;

        public DatabaseTests() =>
            _dbContext = new TestDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase").Options);


        [TestMethod]
        public void Save_ThenLoad_CollectionValid()
        {
            // Arange
             var expectedResult = new List<SearchQuery> { new SearchQuery(1.ToString()) , new SearchQuery(2.ToString()) };
            _dbContext.SearchQueries.AddRange(expectedResult);
            _dbContext.SaveChanges();

            // Act
            var testResult = _dbContext.SearchQueries.ToList();

            // Assert
            CollectionAssert.AreEqual(expectedResult, testResult); 
        }

        
    }
}