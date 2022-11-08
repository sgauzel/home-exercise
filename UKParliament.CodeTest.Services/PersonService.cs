using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _personRepository.GetAll();
        }

        public async Task<int> AddPerson(Person person)
        {
            try
            {
                if (person != null && person.Id == 0)
                {
                   return await _personRepository.Add(person);

                } else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePerson(Person person)
        {
            try
            {
                if (person != null && person.Id > 0)
                {
                   return await _personRepository.Update(person);
                } else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeletePerson(Person person)
        {
            try
            {
                return await _personRepository.GetAll().ContinueWith((allPerson) =>
                {
                    var response = 0;
                    if (allPerson.IsCompleted)
                    {
                        var persons = allPerson.Result ?? Enumerable.Empty<Person>();
                        if (persons?.Any() == true && persons.Any(x => x.Id == person.Id))
                        {
                            response = _personRepository.Delete(person).Result;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    return response;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}