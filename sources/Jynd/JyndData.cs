﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndData
    {
        private readonly string source;
        private readonly JyndItem[] items;
        private readonly short[] hashes;

        private int size;
        private int count;

        public JyndData(string source, JyndItem[] items, short[] hashes)
        {
            this.size = items.Length;
            this.hashes = hashes;
            this.count = 0;

            this.source = source;
            this.items = items;
        }

        public string Source
        {
            get { return source; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddIndexed(ushort index, byte indexLength, short indexInstance, ushort data, short dataLength, short dataInstance)
        {
            ushort hash = ToHash(source, index, indexLength);

            hashes[hash] = (short)count;
            items[count++] = new JyndItem
            {
                Hash = hash,
                Index = index,
                IndexLength = indexLength,
                IndexInstance = indexInstance,
                Data = data,
                DataLength = dataLength,
                DataInstance = dataInstance
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddItem(short indexInstance, ushort data, short dataLength, short dataInstance)
        {
            items[count++] = new JyndItem
            {
                IndexInstance = indexInstance,
                Data = data,
                DataLength = dataLength,
                DataInstance = dataInstance
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JyndItem? Find(string text, int instance)
        {
            ushort hash = ToHash(text);
            short index = hashes[hash];

            if (index < count)
            {
                if (items[index].Hash == hash)
                {
                    if (items[index].IndexInstance == instance)
                    {
                        if (Equals(text, source, items[index].Index, items[index].IndexLength))
                        {
                            return items[index];
                        }
                    }
                }
            }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        public JyndItem? At(int instance, int index)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].IndexInstance == instance)
                {
                    if (index-- == 0)
                    {
                        return items[i];
                    }
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort ToHash(string text)
        {
            return ToHash(text, 0, text.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort ToHash(string text, int offset, int length)
        {
            ushort hash = 5381;

            unchecked
            {
                for (int i = offset; i < offset + length; i++)
                {
                    hash = (ushort)((hash << 1) + hash + text[i]);
                }
            }

            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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