using System;
using System.Collections.Generic;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.SaveLoad;
using UnityEngine.Events;

namespace Infrastructure.SaveLoad.Player
{
    public interface IPlayerInventory
    {
        int GetResource(ResourceType target);
        int GetCurrency(CurrencyType target);
        int GetItem(ItemType target);

        void SetResource(ResourceType target, int value);
        void SetCurrency(CurrencyType target, int value);
        void SetItem(ItemType target, int value);
        void AddResource(ResourceType resourceType, int value = 1);
        void AddCurrency(CurrencyType currencyType, int value = 1);
        void AddItem(ItemType itemType, int value = 1);

        void AddListener<T>(UnityAction<T, int> callback) where T : Enum;
    }

    public class PlayerInventory : IPlayerInventory
    {
        private Dictionary<ResourceType, PrefsProvider> _resources = new();
        private Dictionary<CurrencyType, PrefsProvider> _currencies = new();
        private Dictionary<ItemType, PrefsProvider> _items = new();

        private UnityAction<ResourceType, int> _updateResourceListeners;
        private UnityAction<CurrencyType, int> _updateCurrencyListeners;
        private UnityAction<ItemType, int> _updateItemListeners;
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
            _updateResourceListeners?.Invoke(target, value);
        }

        public void SetCurrency(CurrencyType target, int value)
        {
            _currencies[target].Set(value);
            _updateCurrencyListeners?.Invoke(target, value);
        }

        public void SetItem(ItemType target, int value)
        {
            _items[target].Set(value);
            _updateItemListeners?.Invoke(target, value);
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
        

        public void AddListener<T>(UnityAction<T, int> callback) where T : Enum
        {
            if (typeof(T) == typeof(ResourceType))
            {
                _updateResourceListeners +=   callback as UnityAction<ResourceType, int>;
            }
            if (typeof(T) == typeof(CurrencyType))
            {
                _updateCurrencyListeners +=   callback as UnityAction<CurrencyType, int>;
            }
            if (typeof(T) == typeof(ItemType))
            {
                _updateItemListeners +=   callback as UnityAction<ItemType, int>;
            }
        }
    }
}