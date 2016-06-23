using System;

namespace Jynd
{
    public static class JyndConvert
    {
        [ThreadStatic]
        private static JyndItem[] Items;

        [ThreadStatic]
        private static short[] Instances;

        public static dynamic Deserialize(string json)
        {
            Items = Items ?? new JyndItem[256];
            Instances = Instances ?? new short[32];

            JyndData data = new JyndData(json, Items);
            JyndParser parser = new JyndParser(data, Instances);

            parser.Execute();
            return new JyndObject(data, 0);
        }
    }
}