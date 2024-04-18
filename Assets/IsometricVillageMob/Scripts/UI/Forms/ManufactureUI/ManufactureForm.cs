using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;

namespace IsometricVillageMob.UI.Forms
{
    public class ManufactureForm : BaseContentForm
    {
        [SerializeField] private ResourceSlotView[] _slots;
        [SerializeField] private ResourceSlotView _resultSlot;
        
        [SerializeField] private ButtonExt _btnStart;
        [SerializeField] private ButtonExt _btnStop;
        
        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            // _btnSell.onClick.AddListener(OnSell);
            // _btnSelectResource.onClick.AddListener(OnSelectResource);
            //
            // _resourceSelectedIco.enabled = false;
            // _priceText.text = "0";
        }

    }
}