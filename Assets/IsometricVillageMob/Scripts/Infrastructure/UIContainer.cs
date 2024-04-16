using System;
using System.Collections.Generic;
using IsometricVillageMob.IsometricVillageMob.UI;

namespace IsometricVillageMob.Infrastructure.Containers
{
    public class UIContainer
    {
        private Dictionary<Type, IForm> _forms = new Dictionary<Type, IForm>();

        public void AddForm(IForm form)
        {
            if (_forms.ContainsKey(form.GetType()) == false)
                _forms.Add(form.GetType(), form);
        }
        
        public T GetForm<T>() where T : IForm
        {
            _forms.TryGetValue(typeof(T), out var form);
            return (T)form;
        }

        public void RemoveForm<T>()
        {
            _forms.Remove(typeof(T));
        }
    }
}