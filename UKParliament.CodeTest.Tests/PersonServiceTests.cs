using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests
{
    public class PersonServiceTests
    {
        private IPersonService _personService;
        private Mock<IPersonRepository> _personRepository;

        public PersonServiceTests ()
        {
            _personRepository = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepository.Object);
        }

        [Fact]
        public async void GetAllPerson_Test()
        {
            _personRepository.Setup(x => x.GetAll()).ReturnsAsync(FakeData.People);
            var persons = await _personService.GetAll();

            Assert.True(persons.Count() > 0);
        }

        [Fact]
        public async void AddPerson_Successfull_Test()
        {
            var personToAdd = new Person() { Name = "Add New Person" };
            var saveList = FakeData.People;

            _personRepository.Setup(x => x.Add(personToAdd)).ReturnsAsync(1);
            var response = await _personService.AddPerson(personToAdd);

            Assert.Equal(1, response);
        }

        [Fact]
        public async void AddPerson_Exception_InValidModel_Test()
        {
            var personToAdd = new Person() { Id = 122, Name = "Add New Person" };

            var exception = await Record.ExceptionAsync(() => _personService.AddPerson(personToAdd));

            _personRepository.Verify(x => x.Update(personToAdd), Times.Never);
            Assert.IsType<InvalidOperationException>(exception);

        }

        [Fact]
        public async void UpdatePerson_Exception_InValidModel_Test()
        {
           var personToUpdate = new Person() { Name = "Update Person" };

           var exception = await Record.ExceptionAsync(() => _personService.UpdatePerson(personToUpdate));

            _personRepository.Verify(x => x.Update(personToUpdate), Times.Never);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async void UpdatePerson_Successfull_Test()
        {
            var updatName = "Update Person Name";
            var peopleToUpdate = FakeData.SinglePerson();
            peopleToUpdate.Name = updatName;

            _personRepository.Setup(x => x.GetAll()).ReturnsAsync(FakeData.People);

            var response = await _personService.UpdatePerson(peopleToUpdate);

            var AfterAddList = await _personService.GetAll();

            Assert.True(AfterAddList?.Any(x =>x.Name == updatName));
        }

        [Fact]
        public async void DeletePerson_Successfull_Test()
        {
            var peopleToDelete = FakeData.SinglePerson();

            _personRepository.Setup(x => x.GetAll()).ReturnsAsync(FakeData.People);
            _personRepository.Setup(x => x.Delete(peopleToDelete)).ReturnsAsync(1);

            var response = await _personService.DeletePerson(peopleToDelete);
            _personRepository.Verify(x => x.GetAll(), Times.Once);

            Assert.Equal(1, response);
        }

        [Fact]
        public async void DeletePerson_WhenDataNotavailable_Test()
        {
            var peopleToDelete = FakeData.SinglePerson();

            _personRepository.Setup(x => x.GetAll()).ReturnsAsync(Enumerable.Empty<Person>());
            _personRepository.Setup(x => x.Delete(peopleToDelete)).ReturnsAsync(0);

            var exception = await Record.ExceptionAsync(() => _personService.DeletePerson(peopleToDelete));

            _personRepository.Verify(x => x.GetAll(), Times.Once);
            _personRepository.Verify(x => x.Delete(peopleToDelete), Times.Never);
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
