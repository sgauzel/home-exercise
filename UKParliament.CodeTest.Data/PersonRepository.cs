using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PersonManagerContext personManagerContext) : base(personManagerContext)
        {

        }
    }
}
