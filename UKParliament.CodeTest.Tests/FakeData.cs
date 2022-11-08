using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Tests
{
    public static class FakeData
    {
        public static List<Person> People = new List<Person>()
        {
            new Person { Id = 4, Name = "Matt Smith" },
            new Person { Id = 5, Name = "Jane Taylor" },
            new Person { Id = 6, Name = "Liam Mike" }
        };

        public static Person SinglePerson()
        {
            return People[0];
        }
    }
}
