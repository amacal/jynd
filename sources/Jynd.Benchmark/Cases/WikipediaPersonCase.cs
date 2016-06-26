using System;

namespace Jynd.Benchmark.Cases
{
    public static class WikipediaPersonCase
    {
        public const string Data = @"{'firstName':'John','lastName':'Smith','isAlive':true,'age':25,'address':{'streetAddress':'21 2nd Street','city':'New York','state':'NY','postalCode':'10021-3100'},'phoneNumbers':[{'type':'home','number':'212 555-1234'},{'type':'office','number':'646 555-4567'},{'type':'mobile','number':'123 456-7890'}],'children':[],'spouse':null}";

        public static BenchmarkCase<Person> Instance
        {
            get
            {
                return new BenchmarkCase<Person>
                {
                    Name = "wikipedia-person",
                    Iterations = 1000000,
                    Source = "https://en.wikipedia.org/wiki/JSON",
                    Data = Data.Replace('\'', '\"'),
                    OnStatic = CheckStatic,
                    OnDynamic = CheckDynamic
                };
            }
        }

        private static void CheckDynamic(dynamic instance)
        {
            string firstName = instance.firstName;
            string lastName = instance.lastName;
            int? age = instance.age;
            string city = instance.address.city;
            object spouse = instance.spouse;
            string mobile = instance.phoneNumbers[2].number;

            if (firstName == lastName || age == null || city == null || Object.Equals(spouse, 1) || mobile == null)
                throw new Exception();
        }

        private static void CheckStatic(Person instance)
        {
            string firstName = instance.firstName;
            string lastName = instance.lastName;
            int? age = instance.age;
            string city = instance.address.city;
            object spouse = instance.spouse;
            string mobile = instance.phoneNumbers[2].number;

            if (firstName == lastName || age == null || city == null || Object.Equals(spouse, 1) || mobile == null)
                throw new Exception();
        }

        public class Person
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public bool? isAlive { get; set; }
            public int? age { get; set; }

            public Address address { get; set; }
            public PhoneNumber[] phoneNumbers { get; set; }

            public Person[] children { get; set; }
            public Person spouse { get; set; }
        }

        public class Address
        {
            public string streetAddress { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
        }

        public class PhoneNumber
        {
            public string type { get; set; }
            public string number { get; set; }
        }
    }
}