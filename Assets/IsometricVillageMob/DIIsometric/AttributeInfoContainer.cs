using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IsometricVillageMob.DIIsometric
{
    public class AttributeInfoContainer
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private readonly Dictionary<Type, AttributeObjInfo> _container = new();

        public AttributeObjInfo Get(Type type)
        {
            if (!_container.TryGetValue(type, out AttributeObjInfo data))
            {
                data = CreateInfo(type);
                _container.Add(type, data);
            }

            return data;
        }

        private AttributeObjInfo CreateInfo(Type target) =>
            new AttributeObjInfo(GetFields(target), GetProperties(target));

        private FieldInfo[] GetFields(Type target) => target
            .GetFields(Flags)
            .Where(f => f.IsDefined(typeof(InjectAttribute)))
            .ToArray();
        
        private PropertyInfo[] GetProperties(Type target) =>
            target
                .GetProperties(Flags)
                .Where(p => p.CanWrite && p.IsDefined(typeof(InjectAttribute)))
                .ToArray();
        

    }
}