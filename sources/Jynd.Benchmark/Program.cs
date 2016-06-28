using Jynd.Benchmark.Cases;
using System;

namespace Jynd.Benchmark
{
    public static class Program
    {
        public static void Main()
        {
            Execute(WikipediaPersonCase.Instance);
            Execute(GitHubTeamCase.Instance);
            Execute(GitHubPrimesCase.Instance);
            Execute(FixerEuroCase.Instance);
        }

        private static void Execute<T>(BenchmarkCase<T> benchmark)
        {
            Console.WriteLine($"Benchmark-name         : {benchmark.Name}");
            Console.WriteLine($"Benchmark-iterations   : {benchmark.Iterations}");
            Console.WriteLine($"Benchmark-source       : {benchmark.Source}");

            if (benchmark.OnStatic != null)
            {
                GC.Collect();
                Console.WriteLine($"  Newtonsoft-static    : {Benchmark.UseNewtonsoftStatic(benchmark)}");
                GC.Collect();
                Console.WriteLine($"  Jil-static           : {Benchmark.UseJilStatic(benchmark)}");
                GC.Collect();
                Console.WriteLine($"  NetJSON-static       : {Benchmark.UseNetJsonStatic(benchmark)}");
                GC.Collect();
                Console.WriteLine($"  ServiceStack-static  : {Benchmark.UseServiceStackStatic(benchmark)}");
            }

            if (benchmark.OnDynamic != null)
            {
                GC.Collect();
                Console.WriteLine($"  Newtonsoft-dynamic   : {Benchmark.UseNewtonsoftDynamic(benchmark)}");
                GC.Collect();
                Console.WriteLine($"  Jil-dynamic          : {Benchmark.UseJilDynamic(benchmark)}");
                GC.Collect();
                Console.WriteLine($"  Jynd-dynamic         : {Benchmark.UseJyndDynamic(benchmark)}");
            }

            Console.WriteLine();
        }
    }
}