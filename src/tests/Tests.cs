using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fuzzymatchasaservice.core.Match;
using Xunit;

namespace tests
{
    public class Tests
    {
        [Fact]
        public void TestMatchRunner()
        {
            var matcher = new PersonMatcher();

            var firstNameMatchesRule = new MatchRule((o1, o2) => o1 == o2 ? 1 : 0, .5f);
            var lastNameMatchesRule = new MatchRule((o1, o2) => o1 == o2 ? 2 : 0, .5f);

            matcher.Rules.Add("FirstName", firstNameMatchesRule);
            matcher.Rules.Add("LastName", lastNameMatchesRule);
            var runner = new MatchRunner<Person>(new PersonDataSource(), matcher);

            var results = runner.Results(new Person {FirstName = "Bob", LastName = "Jones"}, 2, p => true);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonDataSource : DataSource<Person>
    {
        public override IEnumerable<Person> GetItems(Func<Person, bool> predicate)
        {
            return new Person[]
            {
                new Person
                {
                    FirstName = "John",
                    LastName = "Smith"
                },
                new Person
                {
                    FirstName = "Bob",
                    LastName = "Smith"
                },
                new Person
                {
                    FirstName = "Bob",
                    LastName = "Jones"
                }
            };
        }
    }

    public class PersonMatcher  : SimpleMatcher<Person>
    {
    }


}
