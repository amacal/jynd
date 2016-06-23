using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndData
    {
        private readonly string source;
        private readonly JyndItem[] items;

        private int size;
        private int count;

        public JyndData(string source, JyndItem[] items)
        {
            this.size = items.Length;
            this.count = 0;

            this.source = source;
            this.items = items;
        }

        public string Source
        {
            get { return source; }
        }

        public void Add(short index, short indexLength, short indexInstance, short data, short dataLength, short dataInstance)
        {
            items[count++] = new JyndItem
            {
                Hash = ToHash(source, index, indexLength),
                Index = index,
                IndexLength = indexLength,
                IndexInstance = indexInstance,
                Data = data,
                DataLength = dataLength,
                DataInstance = dataInstance
            };
        }

        public JyndItem? Find(string text, int instance)
        {
            int hash = ToHash(text);

            for (int i = 0; i < count; i++)
            {
                if (items[i].Hash == hash)
                {
                    if (items[i].IndexInstance == instance)
                    {
                        if (Equals(text, source, items[i].Index, items[i].IndexLength))
                        {
                            return items[i];
                        }
                    }
                }
            }

            return null;
        }

        public int Count(int instance)
        {
            int counter = 0;

            for (int i = 0; i < count; i++)
            {
                if (items[i].IndexInstance == instance)
                {
                    counter++;
                }
            }

            return counter;
        }

        public IEnumerable<JyndItem> All(int instance)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].IndexInstance == instance)
                {
                    yield return items[i];
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ToHash(string text)
        {
            return ToHash(text, 0, text.Length);
        }

        private static short ToHash(string text, int offset, int length)
        {
            short hash = 5381;

            unchecked
            {
                for (int i = offset; i < offset + length; i++)
                {
                    hash = (short)((hash << 1) + hash + text[i]);
                }
            }

            return hash;
        }

        private static bool Equals(string left, string right, int offset, int length)
        {
            if (left.Length != length)
                return false;

            for (int i = 0, j = offset; i < length; i++, j++)
                if (left[i] != right[j])
                    return false;

            return true;
        }
    }
}