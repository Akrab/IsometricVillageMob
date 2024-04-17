using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace IsometricVillageMob.DIIsometric
{
    public class IsometricInjector
    {
        private AttributeInfoContainer _attributeInfoContainer = new();
        
        public void Inject(object obj, IContainer container)
        {
            var info = _attributeInfoContainer.Get(obj.GetType());
            
            foreach (var field in info.Fields)
                FieldInject(field, obj, container);
            
            foreach (var t in info.Properties)
                PropertyInjector(t, obj, container);
            
            // foreach (var t in info.Methods)
            //     MethodInjector(t, obj, container);
        }

        private void FieldInject(FieldInfo field, object instance, IContainer container)
        {
            field.SetValue(instance, container.Resolve(field.FieldType));
        }

        private void PropertyInjector(PropertyInfo property, object instance, IContainer container)
        {
            property.SetValue(instance, container.Resolve(property.PropertyType));
        }

        // private void MethodInjector(ObjMethodInfo method, object instance, IContainer container)
        // {
        //     
        // }
        
    }
}