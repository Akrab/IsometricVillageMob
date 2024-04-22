using IsometricVillageMob.DataModel.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms
{
    public class ItemSlotView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _iconRt;
       
        [SerializeField] private Image _icon;
        
        public void SetViewData(IItemModel data)
        {
            _iconRt.gameObject.SetActive(data != null);
            _icon.sprite = data?.Icon;
        }


    }
}