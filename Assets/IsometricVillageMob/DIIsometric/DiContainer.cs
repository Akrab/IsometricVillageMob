using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IsometricVillageMob.DIIsometric
{
    public interface IContainer
    {
        bool ContainsResolve(Type target);
        object Resolve(Type target);
    }
    public class DiContainer: IContainer
    {
        private IsometricInjector Injector { get;  set; } = new();

        private Dictionary<Type, IResolver> _resolvers = new();
        
        private T CreateNewObj<T>() where T : class
        {
            var constructors = typeof(T).GetConstructors();

            if (constructors.Length > 1) throw new Exception($"More 1 constructors for {typeof(T).Name}");

            var firstConstructor = constructors.FirstOrDefault();
            var constructorParameters = firstConstructor?.GetParameters();

            Debug.Assert(constructorParameters != null, nameof(constructorParameters) + " != null");
            var constrParams = new object[constructorParameters.Length];

            for (int i = 0; i < constructorParameters.Length; i++)
                constrParams[i] = Resolve(constructorParameters[i].ParameterType);
            
            return  (T)firstConstructor?.Invoke(constrParams);
        }

        private void CheckInitializable(object data)
        {
            var have = Array.FindIndex(data.GetType().GetInterfaces(), type => type == typeof(IInitializable)) != -1;
            data.GetType().GetMethod("Initializable")?.Invoke(data, null);
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

        public DiContainer BindInterfaces(object data)
        {
            var interfaces = data.GetType().GetInterfaces();

            foreach (var iFace in interfaces)
            {
                if (iFace == typeof(IInitializable)) continue;

                var resolver = CreateResolver(data);
                if (resolver != null)
                    _resolvers.Add(iFace, resolver);
            }

            return this;
        }

        public DiContainer Inject(object data)
        {
            Injector.Inject(data, this);
            Injector.InjectDuty(this);
            CheckInitializable(data);
            return this;
        }
        
        public DiContainer BindInstance(object data)
        {
            AddToContainer(data);
            Inject(data);
            return this;
        }
        
        public DiContainer BindNew<T>(out T obj) where T : class
        {
            obj = CreateNewObj<T>();
            BindInstance(obj);
            return this;
        }
        
        public DiContainer BindNew<T>() where T : class
        {
            BindNew<T>(out T obj);
            return this;
        }
        
        public object Resolve(Type target)
        {
            return _resolvers.TryGetValue(target, out var resolver) ? resolver.Resolve() : null;
        }

        public bool ContainsResolve(Type target)
        {
            return _resolvers.ContainsKey(target);
        }

        public T Resolve<T>() where T: class 
        {
            return Resolve(typeof(T)) as T;
        }
    }
}