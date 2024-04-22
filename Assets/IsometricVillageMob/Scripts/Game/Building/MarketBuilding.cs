using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.Services;
using UnityEngine.Events;

namespace IsometricVillageMob.Game.Building
{
    public interface IMarketBuilding
    {
        void NextItem();
        void Sell();
        IMarketBuildingModel ViewModel { get; }
 
        void AddUpdateListener(UnityAction action);
        void DelUpdateListener();
    }

    public class MarketBuilding : BaseBuilding, IMarketBuilding, IItemListener
    {

        [Inject] private IPlayerInventory _playerInventory;
        [Inject] private IItemService _itemService;
        private MarketBuildingModel _marketBuildingModel = new();
        private UnityAction _uiListener;
        public IMarketBuildingModel ViewModel => _marketBuildingModel;

        public override BuildingType BuildingType => BuildingType.Market;
        
        private void OnDestroy()
        {
            _playerInventory?.SubListener(this);
        }
        public override void Init()
        {
            _playerInventory.AddListener(this);
        }

        public void NextItem()
        {
            _marketBuildingModel.ItemModel = _itemService.Get(_marketBuildingModel.EnumNextValue.Next());
            
            _marketBuildingModel.Count =
                _playerInventory.GetItem(_marketBuildingModel.ItemModel?.ItemType ?? ItemType.None);
            _uiListener?.Invoke();
        }

        public void Sell()
        {
            if (ViewModel.ItemModel == null) return;

            if (_playerInventory.GetItem(ViewModel.ItemModel.ItemType) <= 0)
                return;

            _playerInventory.SubItem(ViewModel.ItemModel.ItemType);
            _playerInventory.AddCurrency(CurrencyType.Gold, ViewModel.ItemModel.Cost);

        }
        
        public void AddUpdateListener(UnityAction action)
        {
            _uiListener = action;
        }

        public void DelUpdateListener()
        {
            _uiListener = null;
        }
        
        public void UpdateItem(ItemType itemType, int value)
        {
            if (_marketBuildingModel.ItemModel == null)
            {
                _marketBuildingModel.Count = 0;
                _uiListener?.Invoke();
                return;
            }
            
            if (_marketBuildingModel.ItemModel?.ItemType == itemType)
            {
                _marketBuildingModel.Count = value;
                _uiListener?.Invoke();
            }
        }
        
  
    }
}
