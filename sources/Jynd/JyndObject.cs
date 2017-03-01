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
            return data.GetValue(FindOrThrow(name));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private JyndItem FindOrThrow(string name)
        {
            JyndItem? item = data.Find(name, instance);

            if (item.HasValue == false)
                throw new JyndException($"The property '{name}' does not exist.");

            return item.Value;
        }

        public bool Has(string name)
        {
            return data.Find(name, instance).HasValue;
        }

        public bool IsObject(string name)
        {
            return data.IsObject(FindOrThrow(name));
        }

        public bool IsArray(string name)
        {
            return data.IsArray(FindOrThrow(name));
        }

        public dynamic GetInt32(string name)
        {
            return data.GetInt32OrNull(FindOrThrow(name));
        }

        public dynamic GetInt64(string name)
        {
            return data.GetInt64OrNull(FindOrThrow(name));
        }

        public dynamic GetBigInteger(string name)
        {
            return data.GetBigIntegerOrNull(FindOrThrow(name));
        }
    }
}