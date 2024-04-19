using DG.Tweening;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.UI.CustomComponents;
using TMPro;
using UnityEngine;


namespace IsometricVillageMob.UI.Forms
{
    public sealed class ResourceForm : BaseContentForm, IBuildingForm
    {
        [SerializeField] private ButtonExt _btnStart;
        [SerializeField] private ButtonExt _btnStop;

        [SerializeField] private ResourceSlotView _resourceSlotView;
        [SerializeField] private ResourceSlotClick _resourceSlotClick;
        [SerializeField] private TextMeshProUGUI _durationText;

        private IResourceBuilding _resourceBuilding;
        
        private void OnStart()
        {
            _resourceBuilding?.StartBuild();
        }

        private void OnStop()
        {
            _resourceBuilding?.StopBuild();
        }

        private void OnSelectResource(ResourceSlotClick slotClick)
        {
            if (_resourceBuilding?.IsRun == false)
            {
                _resourceBuilding?.NextResource();
                UpdateSlotView();
            }
       
        }
        
        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            _btnStart.onClick.AddListener(OnStart);
            _btnStop.onClick.AddListener(OnStop);
            _resourceSlotClick.AddListener(OnSelectResource);
            _durationText.enabled = false;

        }

        public override Tween Show(bool instance = false)
        {

            UpdateBtnsView();
            UpdateSlotView();
            return base.Show(instance);
        }

        private void UpdateBtnsView()
        {
            _btnStart.gameObject.SetActive(!_resourceBuilding.IsRun);
            _btnStop.gameObject.SetActive(_resourceBuilding.IsRun);
        }

        private void UpdateSlotView()
        {
            _resourceSlotView.SetViewData(_resourceBuilding.GetViewData());
        }

        private void TickTimer(float value)
        {
            
        }

        public override Tween Hide(bool instance = false)
        {
            
            _resourceBuilding?.DelTimerListener();
            return base.Hide(instance);
            
        }

        public void Bind(IBuilding building)
        {
            _resourceBuilding = building as IResourceBuilding;
            _resourceBuilding?.AddTimerListener(TickTimer);
        }
    }
}