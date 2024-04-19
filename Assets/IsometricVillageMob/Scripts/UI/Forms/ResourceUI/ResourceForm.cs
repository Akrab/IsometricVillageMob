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

        }

        private void OnStop()
        {

        }

        private void OnSelectResource(ResourceSlotClick slotClick)
        {

        }
        
        //private void 

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
            _btnStart.gameObject.SetActive(!_resourceBuilding.IsRun);
            _btnStop.gameObject.SetActive(_resourceBuilding.IsRun);

            return base.Show(instance);
        }

        public void Bind(IBuilding building)
        {
            _resourceBuilding = building as IResourceBuilding;
        }
    }
}