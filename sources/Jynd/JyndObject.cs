using System.Dynamic;

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
            JyndItem? item = data.Find(binder.Name, instance);

            if (item.HasValue == false)
                throw new JyndException($"The property '{binder.Name}' does not exist.");

            result = data.GetValue(item.Value);
            return true;
        }
    }
}