using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Jynd
{
    public static class JyndExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static dynamic GetValue(this JyndData data, JyndItem item)
        {
            switch (data.Source[item.Data])
            {
                case '"':
                    return data.GetText(item);

                case '{':
                    return data.GetObject(item);

                case '[':
                    return data.GetArray(item);

                case 'n':
                    return data.GetNull(item);

                case 't':
                    return data.GetTrue(item);

                case 'f':
                    return data.GetFalse(item);

                default:
                    return data.GetNumber(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetText(this JyndData data, JyndItem item)
        {
            if (item.DataLength > 0)
                return data.Source.Substring(item.Data + 1, item.DataLength - 2);

            StringBuilder builder = new StringBuilder();
            int end = item.Data - item.DataLength - 1;
            int start = item.Data + 1;

            for (int i = start; i < end; i++)
            {
                if (data.Source[i] == '\\')
                {
                    builder.Append(data.Source, start, i - start);
                    i++;

                    switch (data.Source[i])
                    {
                        case 'u':

                            int number = 0;
                            i++;

                            for (int k = i; k < i + 4; k++)
                            {
                                if (data.Source[k] <= '9')
                                {
                                    number = number * 16 + data.Source[k] - '0';
                                }
                                else if (data.Source[k] <= 'f')
                                {
                                    number = number * 16 + data.Source[k] - 'a' + 10;
                                }
                                else
                                {
                                    number = number * 16 + data.Source[k] - 'A' + 10;
                                }
                            }

                            builder.Append((char)number);
                            i += 3;
                            break;

                        case '"':
                            builder.Append('"');
                            break;

                        case '\\':
                            builder.Append('\\');
                            break;

                        case '/':
                            builder.Append('/');
                            break;

                        case 'b':
                            builder.Append('\b');
                            break;

                        case 'f':
                            builder.Append('\f');
                            break;

                        case 'n':
                            builder.Append('\n');
                            break;

                        case 'r':
                            builder.Append('\r');
                            break;

                        case 't':
                            builder.Append('\t');
                            break;
                    }

                    start = i + 1;
                }
            }

            if (start != item.Data + 1)
            {
                builder.Append(data.Source, start, end - start);
            }

            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object GetNull(this JyndData data, JyndItem item)
        {
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetTrue(this JyndData data, JyndItem item)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetFalse(this JyndData data, JyndItem item)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static JyndObject GetObject(this JyndData data, JyndItem item)
        {
            return new JyndObject(data, item.DataInstance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static JyndArray GetArray(this JyndData data, JyndItem item)
        {
            return new JyndArray(data, item.DataInstance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe dynamic GetNumber(this JyndData data, JyndItem item)
        {
            fixed (char* ptr = data.Source)
            {
                char* str = ptr + item.Data;
                int length = item.DataLength;
                bool signed = *str == '-';

                if (signed)
                {
                    length--;
                    str++;
                }

                if (length < 10)
                    return GetNumberAsInt32(str, item, signed, length);

                if (length == 10)
                    return GetNumberAsInt32OrInt64(str, item, signed, length);

                if (length > 10 && length < 19)
                    return GetNumberAsInt64(str, item, signed, length);

                if (length == 19)
                    return GetNumberAsInt64OrBigInteger(str, item, signed, length);

                return GetNumberAsBigInteger(str, item, signed, length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe int GetNumberAsInt32(char* str, JyndItem item, bool signed, int length)
        {
            int value = 0;

            while (length-- > 0)
            {
                value = 10 * value + (*str++ - '0');
            }

            if (signed)
            {
                return -value;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe long GetNumberAsInt64(char* str, JyndItem item, bool signed, int length)
        {
            long value = 0;

            while (length-- > 0)
            {
                value = 10 * value + (*str++ - '0');
            }

            if (signed)
            {
                return -value;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe BigInteger GetNumberAsBigInteger(char* str, JyndItem item, bool signed, int length)
        {
            BigInteger value = 0;

            while (length-- > 0)
            {
                value = 10 * value + (*str++ - '0');
            }

            if (signed)
            {
                return -value;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe dynamic GetNumberAsInt32OrInt64(char* str, JyndItem item, bool signed, int length)
        {
            long value = 0;

            while (length-- > 0)
            {
                value = 10 * value + (*str++ - '0');
            }

            if (signed)
            {
                value = -value;
            }

            if (value > Int32.MaxValue || value < Int32.MinValue)
                return value;

            return (int)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe dynamic GetNumberAsInt64OrBigInteger(char* str, JyndItem item, bool signed, int length)
        {
            BigInteger value = 0;

            while (length-- > 0)
            {
                value = 10 * value + (*str++ - '0');
            }

            if (signed)
            {
                value = -value;
            }

            if (value > Int64.MaxValue || value < Int64.MinValue)
                return value;

            return (long)value;
        }
    }
}