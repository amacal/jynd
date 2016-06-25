using System;

namespace Jynd
{
    public static class JyndConvert
    {
        [ThreadStatic]
        private static JyndItem[] Items;

        [ThreadStatic]
        private static short[] Instances;

        [ThreadStatic]
        private static short[] Hashes;

        public static dynamic Deserialize(string json)
        {
            Items = Items ?? new JyndItem[UInt16.MaxValue];
            Hashes = Hashes ?? new short[UInt16.MaxValue];
            Instances = Instances ?? new short[UInt16.MaxValue];

            JyndData data = new JyndData(json, Items, Hashes);
            JyndParser parser = new JyndParser(data, Instances);

            parser.Execute();

            if (data.Source[0] == '{')
            {
                return new JyndObject(data, 0);
            }

            return new JyndArray(data, 0);
        }
    }
}