using System;

namespace Jynd.Benchmark
{
    public class BenchmarkCase<T>
    {
        public string Name;

        public string Data;

        public string Source;

        public int Iterations;

        public Action<T> OnStatic;

        public Action<dynamic> OnDynamic;
    }
}