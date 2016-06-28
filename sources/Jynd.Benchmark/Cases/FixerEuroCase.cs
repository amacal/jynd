using System;

namespace Jynd.Benchmark.Cases
{
    public static class FixerEuroCase
    {
        public const string Data = @"{'base':'EUR','date':'2016-06-28','rates':{'AUD':1.4984,'BGN':1.9558,'BRL':3.7183,'CAD':1.4438,'CHF':1.0845,'CNY':7.3606,'CZK':27.108,'DKK':7.4386,'GBP':0.8272,'HKD':8.5925,'HRK':7.515,'HUF':317.67,'IDR':14595.53,'ILS':4.2972,'INR':75.207,'JPY':113.4,'KRW':1294.19,'MXN':20.9827,'MYR':4.5077,'NOK':9.3785,'NZD':1.5671,'PHP':52.15,'PLN':4.4413,'RON':4.5247,'RUB':71.6883,'SEK':9.4425,'SGD':1.5006,'THB':39.062,'TRY':3.2201,'USD':1.1073,'ZAR':16.8831}}";

        public static BenchmarkCase<ExchangeReference> Instance
        {
            get
            {
                return new BenchmarkCase<ExchangeReference>
                {
                    Name = "fixer-euro",
                    Iterations = 1000000,
                    Source = "http://fixer.io/",
                    Data = Data.Replace('\'', '\"'),
                    OnDynamic = CheckDynamic,
                    OnStatic = CheckStatic
                };
            }
        }

        private static void CheckDynamic(dynamic instance)
        {
            string date = instance.date;
            string @base = instance.@base;
            double pln = instance.rates.PLN;
            double gbp = instance.rates.GBP;

            if (date != "2016-06-28")
                throw new Exception();

            if (@base != "EUR")
                throw new Exception();

            if (pln <= gbp)
                throw new Exception();
        }

        private static void CheckStatic(ExchangeReference instance)
        {
            string date = instance.date;
            string @base = instance.@base;
            double pln = instance.rates.PLN;
            double gbp = instance.rates.GBP;

            if (date != "2016-06-28")
                throw new Exception();

            if (@base != "EUR")
                throw new Exception();

            if (pln <= gbp)
                throw new Exception();
        }

        public class ExchangeReference
        {
            public string @base { get; set; }
            public string date { get; set; }

            public ExchangeRates rates { get; set; }
        }

        public class ExchangeRates
        {
            public double AUD { get; set; }
            public double BGN { get; set; }
            public double BRL { get; set; }
            public double CAD { get; set; }
            public double CHF { get; set; }
            public double CNY { get; set; }
            public double CZK { get; set; }
            public double DKK { get; set; }
            public double GBP { get; set; }
            public double HKD { get; set; }
            public double HRK { get; set; }
            public double HUF { get; set; }
            public double IDR { get; set; }
            public double ILS { get; set; }
            public double INR { get; set; }
            public double JPY { get; set; }
            public double KRW { get; set; }
            public double MXN { get; set; }
            public double MYR { get; set; }
            public double NOK { get; set; }
            public double NZD { get; set; }
            public double PHP { get; set; }
            public double PLN { get; set; }
            public double RON { get; set; }
            public double RUB { get; set; }
            public double SEK { get; set; }
            public double SGD { get; set; }
            public double THB { get; set; }
            public double TRY { get; set; }
            public double USD { get; set; }
            public double ZAR { get; set; }
        }
    }
}