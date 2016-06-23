using System.Collections.Generic;

namespace Jynd
{
    public class JyndComparer : IComparer<JyndItem>
    {
        public static readonly JyndComparer Instance = new JyndComparer();

        public int Compare(JyndItem x, JyndItem y)
        {
            if (x.Hash == y.Hash)
                return 0;

            if (x.Hash > y.Hash)
                return 1;

            return -1;
        }
    }
}