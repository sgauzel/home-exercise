using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        Task<int> AddPerson(Person person);
        Task<int> UpdatePerson(Person person);
        Task<int> DeletePerson(Person person);
        Task<IEnumerable<Person>> GetAll();
    }
}