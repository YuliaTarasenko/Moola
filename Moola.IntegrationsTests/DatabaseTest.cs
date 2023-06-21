namespace Moola.IntegrationsTests
{
    public class DatabaseTest
    {
        private DbContextOptions<MyContext> _options;
        public DatabaseTest()
        {
            _options = new DbContextOptionsBuilder<MyContext>()
                .UseSqlServer("TestDatabase")
                .Options;
        }

        [Fact]
        public void CanAddAndRetrieveFinancialRecordFromDatabase()
        {
            // Arrange
            using (var context = new MyContext(_options))
            {
                var repository = new List<Income>();
                var record = new Income
                {
                    Id = 15,
                    IncomeDate = DateTime.Now,
                    Amount = 100,
                    CategoryId = 1,
                    Note = "Test",
                    FinanceId = 1
                };

                // Act
                repository.Add(record);
                context.SaveChanges();

                // Assert
                var retrievedRecord = repository.Find(i => i.Id == record.Id);
                Assert.NotNull(retrievedRecord);
                Assert.Equal(record.IncomeDate, retrievedRecord.IncomeDate);
                Assert.Equal(record.Amount, retrievedRecord.Amount);
                Assert.Equal(record.CategoryId, retrievedRecord.CategoryId);
                Assert.Equal(record.Note, retrievedRecord.Note);
                Assert.Equal(record.FinanceId, retrievedRecord.FinanceId);
            }
        }         
    }
}
