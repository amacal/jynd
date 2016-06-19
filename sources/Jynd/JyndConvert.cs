using System;

namespace Jynd
{
    public static class JyndConvert
    {
        [ThreadStatic]
        private static JyndItem[] Items;

        [ThreadStatic]
        private static int[] Instances;

        public static dynamic Deserialize(string json)
        {
            Items = Items ?? new JyndItem[256];
            Instances = Instances ?? new int[32];

            JyndData data = new JyndData(json, Items);
            JyndParser parser = new JyndParser(data, Instances);

            parser.Execute();
            return new JyndObject(data, 0);
        }
    }
}