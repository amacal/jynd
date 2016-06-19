using System.Collections;

namespace Jynd
{
    public class JyndArray : IEnumerable
    {
        private readonly JyndData data;
        private readonly int instance;

        public JyndArray(JyndData data, int instance)
        {
            this.data = data;
            this.instance = instance;
        }

        public int Length
        {
            get { return data.Count(instance); }
        }

        public dynamic this[int index]
        {
            get
            {
                int counter = 0;

                foreach (JyndItem i in data.All(instance))
                {
                    if (index == counter++)
                    {
                        return data.GetValue(i);
                    }
                }

                return null;
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (JyndItem i in data.All(instance))
            {
                yield return data.GetValue(i);
            }
        }
    }
}