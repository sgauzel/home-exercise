using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Tests
{
    public class PersonSeedDataFixture : IDisposable
    {
        private readonly DbContextOptions<PersonManagerContext> _options;
        public PersonManagerContext _dbContext { get; set; }

        public List<Person> People = FakeData.People;
        public PersonSeedDataFixture()
        {
            var dbname = "PersonManager_" + Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<PersonManagerContext>()
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseInMemoryDatabase(databaseName: dbname).Options;
            _dbContext = new PersonManagerContext(_options);
            _dbContext.AddRange(People);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
