using Jil;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Diagnostics;

namespace Jynd.Benchmark
{
    public static class Benchmark
    {
        public static TimeSpan UseNewtonsoftDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JsonConvert.DeserializeObject(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseNewtonsoftStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(JsonConvert.DeserializeObject<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseJilDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JSON.DeserializeDynamic(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseJilStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(JSON.Deserialize<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseNetJsonStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(NetJSON.NetJSON.Deserialize<T>(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseServiceStackStatic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnStatic(benchmark.Data.FromJson<T>());
            }

            return stopwatch.Elapsed;
        }

        public static TimeSpan UseJyndDynamic<T>(BenchmarkCase<T> benchmark)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < benchmark.Iterations; i++)
            {
                benchmark.OnDynamic(JyndConvert.Deserialize(benchmark.Data));
            }

            return stopwatch.Elapsed;
        }
    }
}