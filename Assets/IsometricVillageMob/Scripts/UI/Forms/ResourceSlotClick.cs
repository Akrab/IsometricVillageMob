using IsometricVillageMob.UI.CustomComponents;
using UnityEngine;
using UnityEngine.Events;

namespace IsometricVillageMob.UI.Forms
{
    public class ResourceSlotClick : MonoBehaviour
    {
        [SerializeField] private ButtonExt _btnClick;

        private UnityAction<ResourceSlotClick> _onSelectResource;
        public void AddListener(UnityAction<ResourceSlotClick> onSelectResource)
        {
            _onSelectResource = onSelectResource;
        }

        private void Awake()
        {
            _btnClick.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onSelectResource?.Invoke(this);
        }
        
        
        
    }
}