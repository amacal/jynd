using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndArray : IEnumerable
    {
        private readonly JyndData data;
        private readonly int instance;

        public JyndArray(JyndData data, int instance)
        {
            this.data = data;
            this.instance = instance;
        }

        public int Length
        {
            get { return data.Count(instance); }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (JyndItem i in data.All(instance))
            {
                yield return data.GetValue(i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private JyndItem FindOrThrow(int index)
        {
            JyndItem? item = data.At(instance, index);

            if (item.HasValue == false)
            {
                throw new IndexOutOfRangeException();
            }

            return item.Value;
        }

        public dynamic this[int index]
        {
            get { return data.GetValue(FindOrThrow(index)); }
        }

        public dynamic GetInt32(int index)
        {
            return data.GetInt32OrNull(FindOrThrow(index));
        }

        public dynamic GetInt64(int index)
        {
            return data.GetInt64OrNull(FindOrThrow(index));
        }

        public dynamic GetBigInteger(int index)
        {
            return data.GetBigIntegerOrNull(FindOrThrow(index));
        }
    }
}