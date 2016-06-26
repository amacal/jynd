using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Jynd
{
    public class JyndObject : DynamicObject
    {
        private readonly JyndData data;
        private readonly int instance;

        public JyndObject(JyndData data, int instance)
        {
            this.data = data;
            this.instance = instance;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValueOrThrow(binder.Name);
            return true;
        }

        public dynamic this[string name]
        {
            get { return GetValueOrThrow(name); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private dynamic GetValueOrThrow(string name)
        {
            JyndItem? item = data.Find(name, instance);

            if (item.HasValue == false)
                throw new JyndException($"The property '{name}' does not exist.");

            return data.GetValue(item.Value);
        }
    }
}