using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IsometricVillageMob.DIIsometric
{
    public class AttributeFieldDuty
    {
        private readonly Type _targetType;
        private readonly FieldInfo _fieldInfo;
        
        public Type TargetType =>_targetType;
        public FieldInfo FieldInfo => _fieldInfo;

        public AttributeFieldDuty(Type target, FieldInfo fieldInfo)
        {
            _targetType = target;
            _fieldInfo = fieldInfo;
        }

    }
    
    public class AttributePropertyDuty
    {
        private readonly Type _targetType;
        private readonly PropertyInfo _propertyInfo;
        
        public Type TargetType =>_targetType;
        public PropertyInfo PropertyInfo => _propertyInfo;

        public AttributePropertyDuty(Type target, PropertyInfo propertyInfo)
        {
            _targetType = target;
            _propertyInfo = propertyInfo;
        }

    }
    
    public class AttributeInfoContainer
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private readonly Dictionary<Type, AttributeObjInfo> _container = new();

        private readonly Dictionary<Type, Queue<AttributeFieldDuty>> _dutyFieldContainer = new();
        private readonly Dictionary<Type, Queue<AttributePropertyDuty>> _dutyPropertyContainer = new();

        private int _totalEmptyFields = 0;
        private int _totalEmptyProperties= 0;
        public bool HaveEmpty => _totalEmptyFields > 0 || _totalEmptyProperties > 0 ;

        public Dictionary<Type, Queue<AttributeFieldDuty>> DutyFieldContainer => _dutyFieldContainer;
        public Dictionary<Type, Queue<AttributePropertyDuty>> DutyPropertyContainer => _dutyPropertyContainer;
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

        public void AddEmptyField(Type instanceType, FieldInfo field)
        {
            _totalEmptyFields++;

            var newDuty = new AttributeFieldDuty(instanceType, field);
            
            if (_dutyFieldContainer.TryGetValue(field.FieldType, out var queue))
            {
                queue.Enqueue(newDuty);
                return;
            }

            queue = new Queue<AttributeFieldDuty>();
            queue.Enqueue(newDuty);
            _dutyFieldContainer.Add(field.FieldType, queue);
        }

        public void AddEmptyProperty(Type type, PropertyInfo propertyInfo)
        {
            _totalEmptyProperties++;
            var newDuty = new AttributePropertyDuty(type, propertyInfo);
            if (_dutyPropertyContainer.TryGetValue(propertyInfo.PropertyType, out var queue))
            {
                queue.Enqueue(newDuty);
                return;
            }

            queue = new Queue<AttributePropertyDuty>();
            queue.Enqueue(newDuty);
            _dutyPropertyContainer.Add(propertyInfo.PropertyType, queue);
        }
    }
}