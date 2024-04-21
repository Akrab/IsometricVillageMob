using System;
using System.Collections.Generic;
using IsometricVillageMob.DataModel;

namespace IsometricVillageMob.Infrastructure.Containers
{
    public class ModelContainer
    {
        private readonly Dictionary<Type, BaseModel> _containers = new Dictionary<Type, BaseModel>();

        public void Add(BaseModel container)
        {
            if (_containers.ContainsKey(container.GetType()) == false) _containers[container.GetType()] = container;
        }

        public T Get<T>() where T : BaseModel
        {
            _containers.TryGetValue(typeof(T), out var model);
            return (T)model;
        }

        public void Remove<T>()
        {
            _containers.Remove(typeof(T));
        }
    }
}