namespace Jynd
{
    public struct JyndItem
    {
        public ushort Hash;

        public ushort Index;
        public byte IndexLength;
        public short IndexInstance;

        public ushort Data;
        public short DataLength;
        public short DataInstance;
    }
}