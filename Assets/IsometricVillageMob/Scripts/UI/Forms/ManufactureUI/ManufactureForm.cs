using System;
using DG.Tweening;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.UI.CustomComponents;
using TMPro;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms
{
    public class ManufactureForm : BaseContentForm, IBuildingForm
    {
        [SerializeField] private ResourceSlotView[] _slots;
        [SerializeField] private ResourceSlotClick[] _slotsClicks;
        [SerializeField] private ItemSlotView _resultSlot;
        
        [SerializeField] private ButtonExt _btnStart;
        [SerializeField] private ButtonExt _btnStop;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _durationText;
        
        private IManufactureBuilding _manufactureBuilding;


        private void UpdateBtnsView()
        {
            _btnStart.gameObject.SetActive(!_manufactureBuilding.IsRun);
            _btnStop.gameObject.SetActive(_manufactureBuilding.IsRun);
        }

        private void UpdateView()
        {
            _itemName.enabled = !_manufactureBuilding.ViewData.IsEmpty;
            _itemName.text = _manufactureBuilding.ViewData.ItemModel?.Name;
            _durationText.enabled = !_manufactureBuilding.ViewData.IsEmpty;
            _durationText.text = _manufactureBuilding.ViewData.Duration.ToString();
            
            for(int i = 0; i < _slots.Length; i++)
                _slots[i].SetViewData(_manufactureBuilding.ViewData.ResourceModel(i));

            _resultSlot.SetViewData(_manufactureBuilding.ViewData.ItemModel);
        }

        private void UpdateUI()
        {
            UpdateBtnsView();
            UpdateView();
        }

        private void OnStart()
        {
            _manufactureBuilding.StartBuild();
            UpdateUI();
        }

        private void OnStop()
        {
            _manufactureBuilding.StopBuild();
            UpdateUI();
                
        }

        private void ClickNextSlot(ResourceSlotClick slot)
        {
            var index = Array.IndexOf(_slotsClicks, slot);
            _manufactureBuilding.NextResource(index);
            UpdateUI();
        }

        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            _btnStart.onClick.AddListener(OnStart);
            _btnStop.onClick.AddListener(OnStop);

            foreach (var item in _slotsClicks)
                item.AddListener(ClickNextSlot);

        }

        public void Bind(IBuilding building)
        {
            _manufactureBuilding = building as IManufactureBuilding;
            _manufactureBuilding?.AddUpdateListener(UpdateUI);
        }

        public override Tween Show(bool instance = false)
        {
            UpdateUI();
            return base.Show(instance);
        }

        public override Tween Hide(bool instance = false)
        {
            _manufactureBuilding?.DelUpdateListener();
            return base.Hide(instance);
        }
    }
}