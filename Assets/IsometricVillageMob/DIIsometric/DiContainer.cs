using System;
using System.Collections.Generic;

namespace IsometricVillageMob.DIIsometric
{
    public interface IContainer
    {
        object Resolve(Type target);
    }
    public class DiContainer: IContainer
    {
        private IsometricInjector Injector { get;  set; } = new();

        private Dictionary<Type, IResolver> _resolvers = new();
        
        public DiContainer()
        {
            var resolver = new ValueResolver(this);
            _resolvers.Add(GetType(), resolver);
        }

        public DiContainer BindInterface<T>(object data)
        {
            if (_resolvers.ContainsKey(typeof(T)))
                return this;

            var resolver = new ValueResolver(data);
            _resolvers.Add(typeof(T), resolver);

            return this;
        }

        public DiContainer BindInstance(object data)
        {
            AddToContainer(data);
            Inject(data);
            return this;
        }
        
        public DiContainer Inject(object data)
        {
            Injector.Inject(data, this);
            return this;
        }

        public DiContainer BindInterfaces(object data)
        {
            var interfaces =  data.GetType().GetInterfaces();

            foreach (var iFace in interfaces)
            {
                var resolver = CreateResolver(iFace);
                if (resolver != null)
                    _resolvers.Add(data.GetType(), resolver);
            }
            return this;
        }

        public DiContainer BindNew<T>(out T obj) where T : new()
        {
            obj = new T();
            BindInstance(obj);
            return this;
        }

        private void AddToContainer(object data)
        {
            var resolver = CreateResolver(data);
            if (resolver != null)
                _resolvers.Add(data.GetType(), resolver);
        }

        private IResolver CreateResolver(object data)
        {
            var type = data.GetType();
            if (_resolvers.ContainsKey(type))
                return null ;
            
            return new ValueResolver(data);
     
        }

        public object Resolve(Type target)
        {
            return _resolvers.TryGetValue(target, out var resolver) ? resolver.Resolve() : null;
        }

        public T Resolve<T>() where T: class 
        {
            return Resolve(typeof(T)) as T;
        }
    }
}