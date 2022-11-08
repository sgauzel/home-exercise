using UKParliament.CodeTest.Data;
using Xunit;

namespace UKParliament.CodeTest.Tests
{
    public class PersonRepositoryTests
    {
        private PersonRepository _personRepository;
        private PersonSeedDataFixture _personSeedDataFixture;
        public PersonRepositoryTests()
        {
            _personSeedDataFixture = new PersonSeedDataFixture();
            _personRepository = new PersonRepository(_personSeedDataFixture._dbContext);
        }

        [Fact]
        public async void GetAllPerso_FromDatabase_Test()
        {
            var persons = await _personRepository.GetAll();

            Assert.True(persons.Count() > 0);
        }

        [Fact]
        public async void AddPerson_InDatabase_Test()
        {
            var personToUpdate = new Person { Name = "New Person", Email = "New Person Email" };
    
            await _personRepository.Add(personToUpdate);

            var loadUpdatePerson = await _personRepository.GetAll();

            Assert.True(loadUpdatePerson?.Any(x => x.Name =="New Person"));
        }

        [Fact]
        public async void UpdatePerson_InDatabase_Test()
        {
            var personToUpdate = new Person { Id = 4, Name = "New Person", Email = "New Person Email" };

            _personSeedDataFixture._dbContext.ChangeTracker.Clear();
            await _personRepository.Update(personToUpdate);

            var loadUpdatePerson = await _personRepository.GetAll();
            var updatePersonFromDb = loadUpdatePerson.FirstOrDefault(x => x.Id == 4);

            Assert.NotNull(updatePersonFromDb);
            Assert.Equal(updatePersonFromDb.Name, personToUpdate.Name);
        }

        [Fact]
        public async void DeletePerson_InDatabase_Test()
        {
            var personToDelete = FakeData.SinglePerson();

            _personSeedDataFixture._dbContext.ChangeTracker.Clear();
            await _personRepository.Delete(personToDelete);

            var loadUpdatePerson = await _personRepository.GetAll();
            var updatePersonFromDb = loadUpdatePerson.FirstOrDefault(x => x.Id == personToDelete.Id);

            Assert.Null(updatePersonFromDb);
        }
    }
}