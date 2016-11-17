using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndParser
    {
        private readonly JyndData data;
        private readonly short[] instances;

        private ushort position;
        private short instance;
        private short depth;

        private bool special;

        public JyndParser(JyndData data, short[] instances)
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

        private short GetValue()
        {
            short assigned = 0;

            switch (data.Source[position])
            {
                case '{':
                    position++;
                    depth++;
                    assigned = ++instance;
                    instances[depth] = instance;
                    ProcessWhiteCharacters();
                    ProcessObject();
                    depth--;
                    break;

                case '[':
                    position++;
                    depth++;
                    assigned = ++instance;
                    instances[depth] = instance;
                    ProcessWhiteCharacters();
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
                ProcessWhiteCharacters();

                if (data.Source[position] == ',')
                {
                    position++;
                    ProcessWhiteCharacters();
                }
            }
        }

        private void ProcessArrayItem()
        {
            ushort start = position;

            GetValue();

            short dataLength = (short)(position - start);

            if (special)
            {
                dataLength = (short)-dataLength;
                special = false;
            }

            data.AddItem(instances[depth], start, dataLength, (short)(instances[depth] + 1));
        }

        private void ProcessProperty()
        {
            byte length = 0;
            ushort start = position;

            while (data.Source[position] != '"')
            {
                length++;
                position++;
            }

            while (data.Source[position] != ':')
            {
                position++;
            }

            position++;
            ProcessWhiteCharacters();

            ushort offset = position;
            short assigned = GetValue();
            short dataLength = (short)(position - offset);

            if (special)
            {
                dataLength = (short)-dataLength;
                special = false;
            }

            data.AddIndexed(start, length, instances[depth], offset, dataLength, assigned);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessText()
        {
            bool escaped = false;
            special = false;

            while (data.Source[position] != '"' || escaped)
            {
                escaped = data.Source[position] == '\\' && escaped == false;
                special |= escaped;
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
            bool completed;

            position--;
            special = false;

            do
            {
                character = data.Source[++position];
                completed = character == ',' || character == '}' || character == ']' || character == ' ';
                special = special || character == '.' || character == 'e' || character == 'E';
            }
            while (completed == false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessWhiteCharacters()
        {
            while (data.Source[position] == ' ')
            {
                position++;
            }
        }
    }
}