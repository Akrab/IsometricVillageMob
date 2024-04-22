using DG.Tweening;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.UI.CustomComponents;
using TMPro;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms
{
    public class MarketForm : BaseContentForm,IBuildingForm 
    {
        [SerializeField] private ButtonExt _btnSell;
        [SerializeField] private ButtonExt _btnSelectResource;
        
        [SerializeField] private ItemSlotView _itemSlotView;

        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _countText;
        private IMarketBuilding _marketBuilding;
        private void OnSell()
        {
            _marketBuilding?.Sell();
        }
        
        private void OnSelectResource()
        {
            _marketBuilding?.NextItem();
        }

        private void UpdateUI()
        {
            _countText.text = $"x{_marketBuilding.ViewModel.Count}";
            _priceText.text = (_marketBuilding.ViewModel.ItemModel?.Cost ?? 0).ToString();
            _itemSlotView.SetViewData(_marketBuilding.ViewModel.ItemModel);
        }

        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            _btnSell.onClick.AddListener(OnSell);
            _btnSelectResource.onClick.AddListener(OnSelectResource);
            _priceText.text = "0";
        }

        public override Tween Show(bool instance = false)
        {
            UpdateUI();
            return base.Show(instance);
        }

        public override Tween Hide(bool instance = false)
        {
            _marketBuilding?.DelUpdateListener();
            return base.Hide(instance);
        }

        public void Bind(IBuilding building)
        {
            _marketBuilding = building as IMarketBuilding;
            _marketBuilding?.AddUpdateListener(UpdateUI);
        }
    }
}