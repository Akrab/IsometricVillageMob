using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game;
using IsometricVillageMob.Services;
using IsometricVillageMob.UI.Forms.Currency;
using IsometricVillageMob.UI.Forms.CurrencyUI;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms
{
    public class CurrencyForm : BaseForm, ICurrencyListener, IResourceListener
    {
        [SerializeField] private CurrencyView _currencyView;

        [Inject] private IPlayerInventory _playerInventory;

        [Inject] private IResourceService _resourceService;
        [Inject] private ICurrencyService _currencyService;

        private Dictionary<ResourceType, ResourceView> _resourceViews = new(3);

        public override void Init()
        {
            var slots = GetComponentsInChildren<ResourceView>();

            var types = (ResourceType[])Enum.GetValues(typeof(ResourceType));
            for (int i = 0; i < types.Length - 1; i++)
            {
                slots[i].SetIcon(_resourceService.Get(types[i]).Icon);
                _resourceViews.Add(types[i], slots[i]);
            }

            _currencyView.SetIcon(_currencyService.Get(CurrencyType.Gold).Icon);
            
            _playerInventory.AddListener(this);

        }
        
        public override Tween Show(bool instance = false)
        {

            foreach (var key in _resourceViews.Keys)
            {
                _resourceViews[key].SetValue(_playerInventory.GetResource(key));
            }
            _currencyView.SetValue(_playerInventory.GetCurrency(CurrencyType.Gold));
            return base.Show(instance);
        }

        public void UpdateCurrency(CurrencyType currencyType, int newValue)
        {
            _currencyView.SetValue(newValue);
        }

        public void UpdateResource(ResourceType resourceType, int newValue)
        {
            if (_resourceViews.TryGetValue(resourceType, out var view))
            {
                view.SetValue(newValue);
            }
        }
    }
}
