using IsometricVillageMob.UI.CustomComponents;
using TMPro;
using UnityEngine;


namespace IsometricVillageMob.UI.Forms
{
    public class ResourceForm : BaseContentForm
    {
        [SerializeField] private ButtonExt _btnStart;
        [SerializeField] private ButtonExt _btnStop;

        [SerializeField] private ResourceSlotView _resourceSlotView;
        [SerializeField] private ResourceSlotClick _resourceSlotClick;
        [SerializeField] private TextMeshProUGUI _durationText;

        private void OnStart()
        {

        }

        private void OnStop()
        {

        }

        private void OnSelectResource(ResourceSlotClick slotClick)
        {

        }

        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            _btnStart.onClick.AddListener(OnStart);
            _btnStop.onClick.AddListener(OnStop);
            _resourceSlotClick.AddListener(OnSelectResource);
            _durationText.enabled = false;

        }

    }
}