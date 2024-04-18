using IsometricVillageMob.RuntimeData;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms
{
    public class ResourceSlotView : MonoBehaviour, IResourceSlotView
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _iconRt;
       
        [SerializeField] private Image _icon;
        
        public void SetViewData(ResourceViewData data)
        {
            _iconRt.gameObject.SetActive(data.Sprite != null);
            _icon.sprite = data.Sprite;
        }
    }
}