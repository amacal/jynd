using Jil;
using Newtonsoft.Json;
using System;

namespace Jynd.Benchmark
{
    public static class Program
    {
        public const string Data = @"{'firstName':'John','lastName':'Smith','isAlive':true,'age':25,'address':{'streetAddress':'21 2nd Street','city':'New York','state':'NY','postalCode':'10021-3100'},'phoneNumbers':[{'type':'home','number':'212 555-1234'},{'type':'office','number':'646 555-4567'},{'type':'mobile','number':'123 456-7890'}],'children':[],'spouse':null}";

        public static void Main()
        {
            string data = Data.Replace('\'', '\"');

            Console.WriteLine($"Newtonsoft-static  : {UseNewtonsoftStatic(data)}");
            Console.WriteLine($"Newtonsoft-dynamic : {UseNewtonsoftDynamic(data)}");
            Console.WriteLine($"Jil-static         : {UseJilStatic(data)}");
            Console.WriteLine($"Jil-dynamic        : {UseJilDynamic(data)}");
            Console.WriteLine($"NetJSON-static     : {UseNetJsonStatic(data)}");
            Console.WriteLine($"Jynd-dynamic       : {UseJyndDynamic(data)}");
        }

        private static TimeSpan UseNewtonsoftDynamic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(JsonConvert.DeserializeObject(data));
            }

            return DateTime.Now - start;
        }

        private static TimeSpan UseNewtonsoftStatic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(JsonConvert.DeserializeObject<Person>(data));
            }

            return DateTime.Now - start;
        }

        private static TimeSpan UseJilDynamic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(JSON.DeserializeDynamic(data));
            }

            return DateTime.Now - start;
        }

        private static TimeSpan UseJilStatic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(JSON.Deserialize<Person>(data));
            }

            return DateTime.Now - start;
        }

        private static TimeSpan UseNetJsonStatic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(NetJSON.NetJSON.Deserialize<Person>(data));
            }

            return DateTime.Now - start;
        }

        private static TimeSpan UseJyndDynamic(string data)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Check(JyndConvert.Deserialize(data));
            }

            return DateTime.Now - start;
        }

        private static void Check(dynamic instance)
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

        private static void Check(Person instance)
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