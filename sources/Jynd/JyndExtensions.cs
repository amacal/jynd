using System.Runtime.CompilerServices;
using System.Text;

namespace Jynd
{
    public static class JyndExtensions
    {
        public static object GetValue(this JyndData data, JyndItem item)
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
        public static string GetText(this JyndData data, JyndItem item)
        {
            if (item.DataLength > 0)
                return data.Source.Substring(item.Data + 1, item.DataLength - 2);

            StringBuilder builder = new StringBuilder();
            int end = item.Data - item.DataLength - 1;
            int start = item.Data + 1;
            int number;

            for (int i = start; i < end; i++)
            {
                if (data.Source[i] == '\\')
                {
                    builder.Append(data.Source, start, i - start);
                    i++;

                    switch (data.Source[i])
                    {
                        case 'u':

                            number = 0;
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
        public static object GetNull(this JyndData data, JyndItem item)
        {
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetTrue(this JyndData data, JyndItem item)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFalse(this JyndData data, JyndItem item)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JyndObject GetObject(this JyndData data, JyndItem item)
        {
            return new JyndObject(data, item.DataInstance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JyndArray GetArray(this JyndData data, JyndItem item)
        {
            return new JyndArray(data, item.DataInstance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNumber(this JyndData data, JyndItem item)
        {
            bool negate = false;

            int value = 0;
            int zero = '0';

            int offset = item.Data;
            int end = offset + item.DataLength;

            if (data.Source[offset] == '-')
            {
                negate = true;
                offset++;
            }

            for (int i = offset; i < end; i++)
            {
                value = 10 * value + data.Source[i] - zero;
            }

            if (negate)
            {
                value = -value;
            }

            return value;
        }
    }
}