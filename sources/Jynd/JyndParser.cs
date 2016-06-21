using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndParser
    {
        private readonly JyndData data;
        private readonly int[] instances;

        private int position;
        private int instance;
        private int depth;

        private bool escaping;

        public JyndParser(JyndData data, int[] instances)
        {
            this.data = data;
            this.instances = instances;

            this.instance = -1;
            this.depth = -1;
        }

        public void Execute()
        {
            GetValue();
        }

        private int GetValue()
        {
            int assigned = 0;

            switch (data.Source[position])
            {
                case '{':
                    position++;
                    depth++;
                    assigned = ++instance;
                    instances[depth] = instance;
                    ProcessObject();
                    depth--;
                    break;

                case '[':
                    position++;
                    depth++;
                    assigned = ++instance;
                    instances[depth] = instance;
                    ProcessArray();
                    depth--;
                    break;

                case '"':
                    position++;
                    ProcessText();
                    break;

                case 'n':
                    ProcessNull();
                    break;

                case 't':
                    ProcessTrue();
                    break;

                case 'f':
                    ProcessFalse();
                    break;

                default:
                    ProcessNumber();
                    break;
            }

            return assigned;
        }

        private void ProcessObject()
        {
            while (data.Source.Length > position)
            {
                if (data.Source[position] == '"')
                {
                    position++;
                    ProcessProperty();
                }

                if (data.Source[position] == '}')
                {
                    position++;
                    break;
                }

                position++;
            }
        }

        private void ProcessArray()
        {
            while (data.Source.Length > position)
            {
                if (data.Source[position] == ']')
                {
                    position++;
                    break;
                }

                ProcessArrayItem();

                if (data.Source[position] == ',')
                {
                    position++;
                }
            }
        }

        private void ProcessArrayItem()
        {
            int start = position;

            GetValue();
            data.Add(0, 0, instances[depth], start, position - start, instances[depth] + 1);
        }

        private void ProcessProperty()
        {
            int length = 0;
            int start = position;

            while (data.Source[position] != '"')
            {
                length++;
                position++;
            }

            position++;
            position++;

            int offset = position;
            int assigned = GetValue();
            int dataLength = position - offset;

            if (escaping)
            {
                dataLength = -dataLength;
                escaping = false;
            }

            data.Add(start, length, instances[depth], offset, dataLength, assigned);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessText()
        {
            bool escaped = false;
            escaping = false;

            while (data.Source[position] != '"' || escaped)
            {
                escaped = data.Source[position] == '\\' && escaped == false;
                escaping |= escaped;
                position++;
            }

            position++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessNull()
        {
            position += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessTrue()
        {
            position += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessFalse()
        {
            position += 5;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessNumber()
        {
            char character;
            bool completed = false;

            position--;

            do
            {
                character = data.Source[++position];
                completed = character == ',' || character == '}' || character == ']';
            }
            while (completed == false);
        }
    }
}