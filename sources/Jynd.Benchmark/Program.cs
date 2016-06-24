using Jil;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Diagnostics;

namespace Jynd.Benchmark
{
    public static class Program
    {
        public const string Person = @"{'firstName':'John','lastName':'Smith','isAlive':true,'age':25,'address':{'streetAddress':'21 2nd Street','city':'New York','state':'NY','postalCode':'10021-3100'},'phoneNumbers':[{'type':'home','number':'212 555-1234'},{'type':'office','number':'646 555-4567'},{'type':'mobile','number':'123 456-7890'}],'children':[],'spouse':null}";

        // https://developer.github.com/v3/orgs/teams/
        public const string Team = @"{'id':1,'url':'https://api.github.com/teams/1','name':'Justice League','slug':'justice-league','description':'A great team.','privacy':'closed','permission':'admin','members_url':'https://api.github.com/teams/1/members{/member}','repositories_url':'https://api.github.com/teams/1/repos','members_count':3,'repos_count':10,'organization':{'login':'github','id':1,'url':'https://api.github.com/orgs/github','repos_url':'https://api.github.com/orgs/github/repos','events_url':'https://api.github.com/orgs/github/events','hooks_url':'https://api.github.com/orgs/github/hooks','issues_url':'https://api.github.com/orgs/github/issues','members_url':'https://api.github.com/orgs/github/members{/member}','public_members_url':'https://api.github.com/orgs/github/public_members{/member}','avatar_url':'https://github.com/images/error/octocat_happy.gif','description':'A great organization'}}";

        public static void Main()
        {
            Execute(new BenchmarkCase<Person>
            {
                Name = "wikipedia-person",
                Iterations = 1000000,
                Data = Person.Replace('\'', '\"'),
                OnStatic = CheckPerson,
                OnDynamic = CheckPerson
            });

            Execute(new BenchmarkCase<Team>
            {
                Name = "github-team",
                Iterations = 1000000,
                Data = Team.Replace('\'', '\"'),
                OnDynamic = CheckTeam,
                OnStatic = CheckTeam
            });
        }

        private static void Execute<T>(BenchmarkCase<T> benchmark)
        {
            Console.WriteLine($"Benchmark            : {benchmark.Name} / {benchmark.Iterations}");

            if (benchmark.OnStatic != null)
            {
                Console.WriteLine($"Newtonsoft-static    : {UseNewtonsoftStatic(benchmark)}");
                Console.WriteLine($"Jil-static           : {UseJilStatic(benchmark)}");
                Console.WriteLine($"NetJSON-static       : {UseNetJsonStatic(benchmark)}");
                Console.WriteLine($"ServiceStack-static  : {UseServiceStackStatic(benchmark)}");
            }

            if (benchmark.OnDynamic != null)
            {
                Console.WriteLine($"Newtonsoft-dynamic   : {UseNewtonsoftDynamic(benchmark)}");
                Console.WriteLine($"Jil-dynamic          : {UseJilDynamic(benchmark)}");
                Console.WriteLine($"Jynd-dynamic         : {UseJyndDynamic(benchmark)}");
            }

            Console.WriteLine();
        }

        private static TimeSpan UseNewtonsoftDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JsonConvert.DeserializeObject(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseNewtonsoftStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(JsonConvert.DeserializeObject<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseJilDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JSON.DeserializeDynamic(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseJilStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(JSON.Deserialize<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseNetJsonStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(NetJSON.NetJSON.Deserialize<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseServiceStackStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(benchmark.Data.FromJson<T>());
            }

            return stopwatch.Elapsed;
        }

        private static TimeSpan UseJyndDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JyndConvert.Deserialize(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        private static void CheckPerson(dynamic instance)
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

        private static void CheckPerson(Person instance)
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

        private static void CheckTeam(dynamic instance)
        {
            string url = instance.url;
            int members = instance.members_count;
            int repos = instance.repos_count;
            string avatar = instance.organization.avatar_url;

            if (url != "https://api.github.com/teams/1")
                throw new Exception();

            if (members != 3)
                throw new Exception();

            if (repos != 10)
                throw new Exception();

            if (avatar != "https://github.com/images/error/octocat_happy.gif")
                throw new Exception();
        }

        private static void CheckTeam(Team team)
        {
            string url = team.url;
            int members = team.members_count;
            int repos = team.repos_count;
            string avatar = team.organization.avatar_url;

            if (url != "https://api.github.com/teams/1")
                throw new Exception();

            if (members != 3)
                throw new Exception();

            if (repos != 10)
                throw new Exception();

            if (avatar != "https://github.com/images/error/octocat_happy.gif")
                throw new Exception();
        }
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

    public class Team
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string privacy { get; set; }
        public string permission { get; set; }
        public string members_url { get; set; }
        public string repositories_url { get; set; }
        public int members_count { get; set; }
        public int repos_count { get; set; }
        public Organization organization { get; set; }
    }

    public class Organization
    {
        public string login { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string hooks_url { get; set; }
        public string issues_url { get; set; }
        public string members_url { get; set; }
        public string public_members_url { get; set; }
        public string avatar_url { get; set; }
        public string description { get; set; }
    }
}