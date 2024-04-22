using System;
using System.Collections.Generic;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.SaveLoad;
using UnityEngine;

namespace Infrastructure.SaveLoad.Player
{
    public interface ICurrencyListener
    {
        void UpdateCurrency(CurrencyType currencyType, int newValue);
    }
    
    public interface IItemListener
    {
        void UpdateItem(ItemType itemType, int newValue);
    }
    public interface IResourceListener
    {
        void UpdateResource(ResourceType resourceType, int newValue);
    }
    
    public interface IPlayerInventory
    {
        int GetResource(ResourceType target);
        int GetCurrency(CurrencyType target);
        int GetItem(ItemType target);
        
        void AddResource(ResourceType resourceType, int value = 1);
        void AddCurrency(CurrencyType currencyType, int value = 1);
        void AddItem(ItemType itemType, int value = 1);

        void SubResource(ResourceType resourceType, int value = 1);
        void AddListener(object obj);
        void SubListener(object obj);
        void SubItem(ItemType itemModelItemType, int value = 1);
        void Clear();
    }

    public class PlayerInventory : IPlayerInventory
    {
        private Dictionary<ResourceType, PrefsProvider> _resources = new();
        private Dictionary<CurrencyType, PrefsProvider> _currencies = new();
        private Dictionary<ItemType, PrefsProvider> _items = new();
        
        private List<ICurrencyListener> _currencyListeners = new List<ICurrencyListener>();
        private List<IResourceListener> _resourceListeners = new List<IResourceListener>();
        private List<IItemListener> _itemListeners = new List<IItemListener>();

        private void ReadPrefs<T>(Dictionary<T, PrefsProvider> data) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));

            for (int i = 0; i < values.Length -1; i++)
            {
                var provider = new PrefsProvider(values[i].ToString());
                data.Add(values[i], provider);
            }
        }
        
        public void Load()
        {
            ReadPrefs<ResourceType>(_resources);
            ReadPrefs<CurrencyType>(_currencies);
            ReadPrefs<ItemType>(_items);
        }

        public int GetResource(ResourceType target)
        {
            return _resources[target].Get();
        }

        public int GetCurrency(CurrencyType target)
        {
            return _currencies[target].Get();
        }

        public int GetItem(ItemType target)
        {
            return _items[target].Get();
        }

        public void SetResource(ResourceType target, int value)
        {
 
            _resources[target].Set(value);
            
            for (int i = 0; i < _resourceListeners.Count; i++)
            {
                if(_resourceListeners[i] != null)
                    _resourceListeners[i].UpdateResource(target, value);
            }

        }

        public void SetCurrency(CurrencyType target, int value)
        {
      
            _currencies[target].Set(value);

            for (int i = 0; i < _currencyListeners.Count; i++)
            {
                if(_currencyListeners[i] != null)
                    _currencyListeners[i].UpdateCurrency(target, value);
            }

        }

        public void SetItem(ItemType target, int value)
        {
          
            _items[target].Set(value);

            for (int i = 0; i < _itemListeners.Count; i++)
            {
                if (_itemListeners[i] != null)
                    _itemListeners[i].UpdateItem(target, value);
            }
        }

        public void AddResource(ResourceType resourceType, int value = 1)
        {
            SetResource(resourceType, GetResource(resourceType) + value);
        }
        
        public void AddCurrency(CurrencyType currencyType, int value = 1)
        {
            SetCurrency(currencyType, GetCurrency(currencyType) + value);
        }
        
        public void AddItem(ItemType itemType, int value = 1)
        {
            SetItem(itemType, GetItem(itemType) + value);
        }

        public void SubResource(ResourceType resourceType, int value = 1)
        {
            SetResource(resourceType, GetResource(resourceType) - value);
        }

        public void SubListener(object obj)
        {
            if (obj is ICurrencyListener cl) _currencyListeners.Remove(cl);
            if (obj is IItemListener il) _itemListeners.Remove(il);
            if (obj is IResourceListener rl) _resourceListeners.Remove(rl);
        }

        public void SubItem(ItemType itemModelItemType, int value = 1)
        {
            SetItem(itemModelItemType, GetItem(itemModelItemType) - value);
        }

        public void Clear()
        {
            foreach (var item in _resources.Values)
                item.Set(0);

            foreach (var item in _currencies.Values)
                item.Set(0);
            
            foreach (var item in _items.Values)
                item.Set(0);
        }
        
        public void AddListener(object obj)
        {
            if (obj is ICurrencyListener cl) _currencyListeners.Add(cl);
            if (obj is IItemListener il) _itemListeners.Add(il);
            if (obj is IResourceListener rl) _resourceListeners.Add(rl);
        }
    }
}